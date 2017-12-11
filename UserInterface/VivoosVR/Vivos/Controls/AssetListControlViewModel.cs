using Castle.Windsor;
using Core.Client;
using Core.Common.DataModel;
using Core.Common.DataModel.Market;
using Core.Common.Services;
using Core.Log;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client;
using Market.Client.Adapters;
using Market.Client.Models;
using Market.Client.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Vivos.Windows;

namespace Vivos.Controls
{
    public class AssetListControlViewModel : BaseControlViewModel, IAssetListControlViewModel
    {
        private int _ItemCounter;
        private object _ItemCounterLock = new object();

        public RxCommand EditCommand { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public int ItemCounter
        {
            get { return _ItemCounter; }
            set
            {
                lock (_ItemCounterLock)
                {
                    _ItemCounter = value;
                }
            }
        }
        public Observable<ObservableCollection<AssetAdminListModel>> Items { get; set; }
        public IPublicMarketService MarketService { get; set; }
        public Observable<int> Progress { get; set; }
        public RxCommand RefreshCommand { get; set; }

        public AssetListControlViewModel(IWindsorContainer container, IAssetListControl view)
                    : base(container, view, "Senaryo Listesi")
        {
            IsRunning = new Observable<bool>(false);
            Items = new Observable<ObservableCollection<AssetAdminListModel>>();
            RefreshCommand = new RxCommand();
            EditCommand = new RxCommand();
            Progress = new Observable<int>(0);

            MarketService = Bootstrapper.ResolveSession().GetProxy<IPublicMarketService>();

            ControlLoadedCommand.Subscribe(x =>
            {
                PopuplateLists();
            });

            RefreshCommand.Subscribe(x =>
            {
                PopuplateLists();

            });

            EditCommand.Subscribe(x =>
            {
                AssetAdminListModel adminListModel = (AssetAdminListModel)x;

                IAssetEntryControlViewModel entryViewModel = WindowViewModel.Value.LoadContent<IAssetEntryControlViewModel, IAssetEntryControl>();
                Asset asset = MarketService.GetAsset(adminListModel.Id.Value);
                AssetEntryAdapter adapter = new AssetEntryAdapter();
                entryViewModel.AssetEntryModel.Value = adapter.Convert(asset);
            });
        }

        private void PopuplateLists()
        {
            BackgroundThread bt = new BackgroundThread();
            var sync = SynchronizationContext.Current;
            ItemCounter = 0;

            bt.Subscribe(y =>
            {
                Items.Value = new ObservableCollection<AssetAdminListModel>();
                //ObservableCollection<AssetAdminListModel> i = (ObservableCollection<AssetAdminListModel>)y.Argument;

                IsRunning.Value = true;

                var assetList = MarketService.GetAllAssetsForAdmin();

                AssetAdminListAdapter adapter = new AssetAdminListAdapter();

                assetList.ForEach(x =>
                {
                    var model = adapter.Convert(x);

                    sync.Send(z => Items.Value.Add(model), null);
                });

                Items.Value.ToList().ForEach(x =>
                {
                    sync.Send(async z => x.ImageSource.Value = await MarketService.GetCachedImage(x.Id.Value), null);

                    Progress.Value = (int)(((double)ItemCounter / (double)Items.Value.Count) * 100.0);

                    ItemCounter++;
                });

            });

            bt.SubscribeCompleted(x =>
            {
                IsRunning.Value = false;
                Progress.Value = 0;
            });

            bt.Run(Items.Value);
        }
    }
}
