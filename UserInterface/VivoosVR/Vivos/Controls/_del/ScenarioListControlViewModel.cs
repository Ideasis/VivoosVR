using Castle.Windsor;
using Core.Client;
using Core.Client.Messages;
using Core.Common;
using Core.Common.DataModel;
using Core.Common.Services;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client.Adapters;
using Market.Client.Configuration;
using Market.Client.Messages;
using Market.Client.Models;
using Market.Client.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Vivos.Windows;

namespace Vivos.Controls
{
    public class ScnarioListControlViewModel : BaseControlViewModel, IScenarioListControlViewModel
    {
        public Observable<ObservableCollection<AssetGroupModel>> AssetGroups { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public IPublicMarketService MarketService { get; set; }
        public RxCommand OpenCommand { get; set; }

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
                    //SetupDownloader(x.Asset);
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

        

        
    }
}
