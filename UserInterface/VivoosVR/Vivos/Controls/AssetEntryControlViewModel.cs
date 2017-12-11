using Castle.Windsor;
using Core.Client;
using Core.Common.DataModel;
using Core.Common.DataModel.Market;
using Core.Common.Services;
using Core.Log;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client.Adapters;
using Market.Client.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Vivos.Windows;

namespace Vivos.Controls
{
    public class AssetEntryControlViewModel : BaseControlViewModel, IAssetEntryControlViewModel
    {
        public RxCommand AddNewCommandCommand { get; set; }
        public RxCommand AddNewDefaultCommand { get; set; }
        public Observable<AssetEntryModel> AssetEntryModel { get; set; }
        public Observable<ObservableCollection<AssetGroupModel>> AssetGroups { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public IPublicMarketService MarketService { get; set; }
        public RxCommand RemoveCommandCommand { get; set; }
        public RxCommand RemoveDefaultCommand { get; set; }
        public RxCommand Save { get; set; }
        public RxCommand SelectImageCommand { get; set; }
        public RxCommand SelectZipCommand { get; set; }

        public AssetEntryControlViewModel(IWindsorContainer container, IAssetEntryControl view)
                                                            : base(container, view, "Senaryo Girişi")
        {
            IsRunning = new Observable<bool>(false);
            AssetGroups = new Observable<ObservableCollection<AssetGroupModel>>();
            SelectImageCommand = new RxCommand();
            AssetEntryModel = new Observable<AssetEntryModel>(new AssetEntryModel());
            Save = new RxCommand();
            SelectZipCommand = new RxCommand();
            AddNewCommandCommand = new RxCommand();
            AddNewDefaultCommand = new RxCommand();
            RemoveDefaultCommand = new RxCommand();
            RemoveCommandCommand = new RxCommand();

            RemoveDefaultCommand.Subscribe(x =>
                {
                    var item = (AssetDefaultModel)x;
                    AssetEntryModel.Value.Defaults.Value.Remove(item);
                });

            RemoveCommandCommand.Subscribe(x =>
                {
                    var item = (AssetCommandModel)x;
                    AssetEntryModel.Value.Commands.Value.Remove(item);
                });

            AddNewCommandCommand.Subscribe(x =>
                {
                    AssetCommandModel newCommand = new AssetCommandModel();
                    AssetEntryModel.Value.Commands.Value.Add(newCommand);
                });

            AddNewDefaultCommand.Subscribe(x =>
                {
                    AssetDefaultModel newDefault = new AssetDefaultModel();
                    AssetEntryModel.Value.Defaults.Value.Add(newDefault);
                });

            SelectZipCommand.Subscribe(x =>
                {
                    OpenFileDialog openDialog = new OpenFileDialog();
                    openDialog.CheckFileExists = true;
                    openDialog.Filter = "Arşiv Dosyaları (*.zip)|*.zip";
                    if (openDialog.ShowDialog() == true)
                    {
                        AssetEntryModel.Value.ZipPath.Value = openDialog.FileName;
                        if (AssetEntryModel.Value.ImagePath.IsValid)
                        {
                            AssetEntryModel.Value.Image.Value = File.ReadAllBytes(openDialog.FileName);
                        }
                    }
                });

            SelectImageCommand.Subscribe(x =>
                {
                    OpenFileDialog openDialog = new OpenFileDialog();
                    openDialog.CheckFileExists = true;
                    openDialog.Filter = "Resim Dosyaları (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                    if (openDialog.ShowDialog() == true)
                    {
                        AssetEntryModel.Value.ImagePath.Value = openDialog.FileName;
                        AssetEntryModel.Value.Image.Value = File.ReadAllBytes(openDialog.FileName);
                    }
                });

            Save.Subscribe(x =>
                {
                    if (!AssetEntryModel.Value.GetIsValid())
                    {
                        MetroMessage.Show("Lütfen ekrandaki hataları giderip tekrar deneyin.", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Stop);
                        return;
                    }
                    else
                    {
                        AssetEntryAdapter adapter = new AssetEntryAdapter();
                        Asset asset = adapter.Convert(AssetEntryModel.Value);

                        MarketService.SaveAsset(asset);

                        MetroMessage.Show("Kayıt başarı ile tamamlandı");
                        WindowViewModel.Value.LoadContent<IAdminHomeControlViewModel, IAdminHomeControl>();
                    }
                });


            MarketService = Bootstrapper.ResolveSession().GetProxy<IPublicMarketService>();

            PopuplateLists();

        }

        private void PopuplateLists()
        {
            var assetGroups = MarketService.GetAssetGroups();
            AssetGroups.Value = new ObservableCollection<AssetGroupModel>();

            AssetGroupAdapter adapter = new AssetGroupAdapter();

            assetGroups.ForEach(x =>
            {
                AssetGroups.Value.Add(adapter.Convert(x));
            });
        }
    }
}
