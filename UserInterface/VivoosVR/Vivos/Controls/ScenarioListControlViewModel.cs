using Castle.Windsor;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Vivos.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Services;
using Core.Common;
using Core.Common.Model;
using Market.Client.Models;
using Market.Client.Adapters;
using Market.Client.Messages;
using System.Net;
using System.ComponentModel;
using Market.Client.Configuration;
using System.IO;
using System.IO.Compression;
using Core.Client;
using System.Windows;
using Core.Client.Messages;
using Core.Common.DataModel;
using System.Threading;
using Market.Client.Tools;

namespace Vivos.Controls
{
    public class ScnarioListControlViewModel : BaseControlViewModel, IScenarioListControlViewModel
    {
        public Observable<bool> IsRunning { get; set; }
        public RxCommand OpenCommand { get; set; }
        public Observable<ObservableCollection<AssetGroupModel>> AssetGroups { get; set; }

        public IPublicMarketService MarketService { get; set; }
        public WebFileDownloader FileDownloader { get; set; }




        public ScnarioListControlViewModel(IWindsorContainer container, IScenarioListControl view)
            : base(container, view, "Senaryo Seçimi")
        {
            IsRunning = new Observable<bool>(false);
            FullName = new Observable<string>();
            AssetGroups = new Observable<ObservableCollection<AssetGroupModel>>();

            MarketService = Bootstrapper.ResolveSession().GetProxy<IPublicMarketService>();

            PopuplateLists();

            RxMessageBus.Subscribe<AssetSelectedMessage>(x =>
                {
                    SetupDownloader(x.Asset);
                });
        }

        private void PopuplateLists()
        {
            BackgroundThread bt = new BackgroundThread();
            var sync = SynchronizationContext.Current;

            bt.Subscribe(o =>
            {
                var assetGroups = MarketService.GetAssetGroups();
                AssetGroups.Value = new ObservableCollection<AssetGroupModel>();

                IsRunning.Value = true;

                AssetGroupAdapter adapter = new AssetGroupAdapter();

                assetGroups.ForEach(x =>
                {
                    var syncg = sync.CreateCopy();
                    var synci = sync.CreateCopy();
                    BackgroundThread btg = new BackgroundThread();
                    BackgroundThread bti = new BackgroundThread();
                    AssetGroupModel assetGroupModel = adapter.Convert(x);

                    btg.Subscribe(g =>
                    {
                        syncg.Send(z => AssetGroups.Value.Add(assetGroupModel), null);
                    });

                    bti.Subscribe(g =>
                    { 
                        foreach (var item in assetGroupModel.Assets.Value)
                        {
                            synci.Send(async z => item.ImageSource.Value = await MarketService.GetCachedImage(item.Id.Value), null);
                            synci.Send(z => assetGroupModel.Progress.Value = (int)(((double)assetGroupModel.ItemCounter / (double)x.Assets.Count) * 100.0), null);
                            bt.ReportProgress(assetGroupModel.Progress.Value);

                            assetGroupModel.ItemCounter++;
                        }
                    });

                    bti.SubscribeCompleted(y =>
                    {
                        assetGroupModel.Progress.Value = 0;
                    });

                    btg.Run();
                    bti.Run();

                });
            });

            bt.SubscribeCompleted(x =>
            {
                IsRunning.Value = false;
            });

            bt.Run();
        }

        private void SetupDownloader(AssetModel assetModel)
        {
            if (
                assetModel.IsDownloadRequired ||
                !File.Exists(assetModel.LocalZipPath) ||
                !File.Exists(assetModel.TimeStampFilePath))
            {
                assetModel.DownloadIsInProgress.Value = true;

                // Reset for events
                FileDownloader = new WebFileDownloader();

                assetModel.IsEnabled.Value = false;
                assetModel.Status.Value = "Dosya indiriliyor...";
                FileDownloader.Download(new Uri(assetModel.Url.Value), assetModel.LocalZipPath);

                FileDownloader.SubscribeProgress(x =>
                {
                    assetModel.DownloadProgress.Value = ((double)x.BytesReceived / (double)x.TotalBytesToReceive) * 100.0;
                });

                FileDownloader.SubscribeCompleted(x =>
                {
                    File.WriteAllBytes(assetModel.TimeStampFilePath, assetModel.TimeStamp.Value);

                    Directory.CreateDirectory(assetModel.ExtractPath);

                    BackgroundThread backgroundOperation = new BackgroundThread();
                    backgroundOperation.SubscribeProgress(y =>
                    {
                        assetModel.DownloadProgress.Value = y.ProgressPercentage;
                    });

                    backgroundOperation.Subscribe(y =>
                    {
                        assetModel.Status.Value = "Paket açılıyor...";
                        ExtractZipFile(assetModel);
                    });


                    backgroundOperation.SubscribeCompleted(y =>
                    {
                        assetModel.IsEnabled.Value = true;
                        assetModel.Status.Value = "";
                        assetModel.DownloadIsInProgress.Value = false;

                        RunAsset(assetModel);
                    });
                    backgroundOperation.Run();
                });
            }
            else
            {
                WindowViewModel.Value.CanClose.Value = false;
                RunAsset(assetModel);
            }
        }

        private void RunAsset(AssetModel assetModel)
        {
            string exePath = Path.Combine(assetModel.ExtractPath, assetModel.Exe.Value);

            ProcessHub unrealProcessHub = new ProcessHub();
            ProcessHub neuLogProcessHub = new ProcessHub();
            bool isUnrealOpen = false, isNeuLogOpen = false;

            unrealProcessHub.SubscribeApplicationExit((x, y) =>
            {
                if (!y.HasExited)
                {
                    MetroMessage.Show("Lütfen önce hasta ekranını kapatınız.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    x.IsCancel = true;
                }
            });

            unrealProcessHub.SubscribeExit(x =>
            {
                isUnrealOpen = false;
                WindowViewModel.Value.CanClose.Value = !isUnrealOpen && !isNeuLogOpen;
            });

            unrealProcessHub.Start(exePath);
            isUnrealOpen = true;


            neuLogProcessHub.SubscribeApplicationExit((x, y) =>
            {
                if (!y.HasExited)
                {
                    MetroMessage.Show("Lütfen önce NeuLog sensör ekranını kapatınız.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    x.IsCancel = true;
                }
            });

            neuLogProcessHub.SubscribeExit(x =>
            {
                isNeuLogOpen = false;
                WindowViewModel.Value.CanClose.Value = !isUnrealOpen && !isNeuLogOpen;
            });

            var anyActiveProcess = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Market.Client.Properties.Settings.Default.NeuLogPath)).FirstOrDefault();

            if (anyActiveProcess != null)
            {
                anyActiveProcess.Kill();
                anyActiveProcess.WaitForExit();
            }

            neuLogProcessHub.Start(Market.Client.Properties.Settings.Default.NeuLogPath);
            isNeuLogOpen = true;

            IObserverControlViewModel viewModel = WindowViewModel.Value.LoadContent<IObserverControlViewModel, IObserverControl>();

            viewModel.Asset.Value = assetModel;
        }

        private void ExtractZipFile(AssetModel assetModel)
        {
            string filename = Path.GetFileName(assetModel.Url.Value);

            using (ZipArchive archive = ZipFile.OpenRead(assetModel.LocalZipPath))
            {
                var files = archive.Entries.Where(y => !string.IsNullOrWhiteSpace(y.Name));
                var dirs = archive.Entries.Where(y => string.IsNullOrWhiteSpace(y.Name));
                int counter = 0;
                int totalFileCount = files.Count();


                foreach (ZipArchiveEntry entry in dirs)
                {
                    string extractionPath = Path.Combine(assetModel.ExtractPath, entry.FullName);
                    Directory.CreateDirectory(extractionPath);
                }

                foreach (ZipArchiveEntry entry in files)
                {
                    string extractionPath = Path.Combine(assetModel.ExtractPath, entry.FullName);

                    entry.ExtractToFile(extractionPath, true);

                    assetModel.DownloadProgress.Value = ((double)counter / (double)totalFileCount) * 100.0;
                    counter++;
                }
            }
        }
    }
}
