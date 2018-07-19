using Core.Client;
using Core.Client.Messages;
using Core.Log;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client.Configuration;
using Market.Client.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Market.Client.Models
{
    public class AssetModel
    {
        public ProcessHub UnrealProcessHub { get; private set; } = new ProcessHub();
        public ProcessHub NeuLogProcessHub { get; private set; } = new ProcessHub();

        public Observable<string> Description { get; set; }
        public Observable<bool> DownloadIsInProgress { get; set; }
        public Observable<double> DownloadProgress { get; set; }
        public Observable<DateTime> EntryDate { get; set; }
        public Observable<string> Exe { get; set; }
        public string ExtractPath
        {
            get
            {
                return Path.Combine(Market.Client.Properties.Settings.Default.LocalPath, "Extracted", Path.GetFileNameWithoutExtension(Url.Value));
            }
        }
        public WebFileDownloader FileDownloader { get; set; }
        public Observable<Guid> Id { get; set; }
        public Observable<ImageSource> ImageSource { get; set; }
        public bool IsDownloadRequired
        {
            get
            {
                try
                {
                    byte[] locaTimeStamp = File.ReadAllBytes(UpdateStampFilePath);
                    bool ret = !locaTimeStamp.SequenceEqual(UpdateStamp.Value.ToByteArray());

                    return ret;
                }
                catch (FileNotFoundException)
                {
                    return false;
                }
                catch (IOException)
                {
                    return false;
                }
            }
        }
        public Observable<bool> IsEnabled { get; set; }
        public bool IsInSafe { get; set; }
        public string LocalZipPath
        {
            get
            {
                return Path.Combine(Market.Client.Properties.Settings.Default.LocalPath, Path.GetFileName(Url.Value));
            }
        }
        public Observable<DateTime> ModifyDate { get; set; }
        public Observable<string> Name { get; set; }
        public double Opacity
        {
            get
            {
                return IsInSafe ? 1 : .5;
            }
        }
        public Observable<string> Status { get; set; }
        public Observable<Guid> UpdateStamp { get; set; }
        public string UpdateStampFilePath
        {
            get
            {
                return Path.Combine(Market.Client.Properties.Settings.Default.LocalPath, Path.GetFileNameWithoutExtension(Url.Value) + ".stamp");
            }
        }
        public Observable<string> Url { get; set; }
        public RxCommand AssetRunningCommand { get; set; }

        public AssetModel()
        {
            DownloadIsInProgress = new Observable<bool>(false);
            DownloadProgress = new Observable<double>(0);
            Status = new Observable<string>("");
            IsEnabled = new Observable<bool>(true);
            ModifyDate = new Observable<DateTime>();
            Id = new Observable<Guid>();
            Name = new Observable<string>();
            Description = new Observable<string>();
            Url = new Observable<string>();
            Exe = new Observable<string>();
            EntryDate = new Observable<DateTime>();
            UpdateStamp = new Observable<Guid>();
            ImageSource = new Observable<ImageSource>();

            AssetRunningCommand = new RxCommand();

        }

        private void ExtractZipFile()
        {

            string filename = Path.GetFileName(Url.Value);

            using (ZipArchive archive = ZipFile.OpenRead(LocalZipPath))
            {
                var files = archive.Entries.Where(y => !string.IsNullOrWhiteSpace(y.Name));
                var dirs = archive.Entries.Where(y => string.IsNullOrWhiteSpace(y.Name));
                int counter = 0;
                int totalFileCount = files.Count();


                foreach (ZipArchiveEntry entry in dirs)
                {
                    string extractionPath = Path.Combine(ExtractPath, entry.FullName);
                    Directory.CreateDirectory(extractionPath);
                }

                foreach (ZipArchiveEntry entry in files)
                {
                    string extractionPath = Path.Combine(ExtractPath, entry.FullName);

                    entry.ExtractToFile(extractionPath, true);

                    DownloadProgress.Value = ((double)counter / (double)totalFileCount) * 100.0;
                    counter++;
                }
            }
        }

        public void Run()
        {
            if (
                IsDownloadRequired ||
                !File.Exists(LocalZipPath) ||
                !File.Exists(UpdateStampFilePath))
            {
                DownloadIsInProgress.Value = true;

                // Reset for events
                FileDownloader = new WebFileDownloader();

                IsEnabled.Value = false;
                Status.Value = "Dosya indiriliyor...";
                FileDownloader.Download(new Uri(Url.Value), LocalZipPath);

                FileDownloader.SubscribeProgress(x =>
                {
                    DownloadProgress.Value = ((double)x.BytesReceived / (double)x.TotalBytesToReceive) * 100.0;
                });

                FileDownloader.SubscribeCompleted(x =>
                {
                    Directory.CreateDirectory(ExtractPath);

                    File.WriteAllBytes(UpdateStampFilePath, UpdateStamp.Value.ToByteArray());

                    BackgroundThread backgroundOperation = new BackgroundThread();
                    backgroundOperation.SubscribeProgress(y =>
                    {
                        DownloadProgress.Value = y.ProgressPercentage;
                    });

                    backgroundOperation.Subscribe(y =>
                    {
                        Status.Value = "Paket açılıyor...";
                        ExtractZipFile();
                    });


                    backgroundOperation.SubscribeCompleted(y =>
                    {
                        IsEnabled.Value = true;
                        Status.Value = "";
                        DownloadIsInProgress.Value = false;

                        RunAsset();
                        AssetRunningCommand.Execute(this);
                        //RxMessageBus.Send<AssetRunningMessage>(new AssetRunningMessage(this));
                    });
                    backgroundOperation.Run();
                });
            }
            else
            {
                //WindowViewModel.Value.CanClose.Value = false;
                RunAsset();
                AssetRunningCommand.Execute(this);

                //RxMessageBus.Send<AssetRunningMessage>(new AssetRunningMessage(this));
            }
        }

        public void Stop()
        {
            NeuLogProcessHub.Kill();
            UnrealProcessHub.Kill();
        }

        private void RunAsset()
        {
            string exePath = Path.Combine(ExtractPath, Exe.Value);

            RxMessageBus.Subscribe<MainWindowCloseMessage>(x =>
            {
                if (NeuLogProcessHub.IsRunning() || UnrealProcessHub.IsRunning())
                {
                    MetroMessage.Show("Önce oturumu iptal etmeli ya da kaydetmelisiniz.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    x.IsCancel = true;
                }
            });

            try
            {
                UnrealProcessHub.Start(exePath);
                NeuLogProcessHub.Start(Market.Client.Properties.Settings.Default.NeuLogPath);
            }
            catch (Exception exc)
            {
                exc.LogException();
                MetroMessage.Show("Uygulamalardan biri açılırken hata oluştu. Sistemin doğru şekilde yapılandırıldığından emin olun.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
