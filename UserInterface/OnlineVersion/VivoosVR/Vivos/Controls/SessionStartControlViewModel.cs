using Castle.Windsor;
using Core.Client;
using Core.Common.DataModel;
using Core.Common.Services;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client.Adapters;
using Market.Client.Messages;
using Market.Client.Models;
using Market.Client.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Vivos.Windows;

namespace Vivos.Controls
{
    public class SessionStartControlViewModel : BaseControlViewModel, ISessionStartControlViewModel
    {
        public Observable<ObservableCollection<AssetModel>> Assets { get; set; }
        public RxCommand CancelCommand { get; set; }
        public Observable<bool> IsOperationsVisible { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public IPublicMarketService MarketService { get; set; }
        public Observable<PatientEntryModel> PatientEntryModel { get; set; }
        public Observable<ProblemEntryModel> ProblemEntryModel { get; set; }
        public Observable<SessionEntryModel> SessionEntryModel { get; set; }
        public RxCommand StartCommand { get; set; }

        public SessionStartControlViewModel(
                                    IWindsorContainer container,
                                    ISessionStartControl view,
                                    PatientEntryModel patientEntryModel,
                                    ProblemEntryModel problemEntryModel,
                                    SessionEntryModel sessionEntryModel = null)
                                            : base(container, view, "Oturum Başlatma")
        {
            IsRunning = new Observable<bool>(false);
            IsOperationsVisible = new Observable<bool>(false);
            PatientEntryModel = new Observable<Market.Client.Models.PatientEntryModel>(patientEntryModel);
            ProblemEntryModel = new Observable<Market.Client.Models.ProblemEntryModel>(problemEntryModel);
            SessionEntryModel = new Observable<Market.Client.Models.SessionEntryModel>(sessionEntryModel ?? new SessionEntryModel());
            Assets = new Observable<ObservableCollection<AssetModel>>(new ObservableCollection<AssetModel>());

            CancelCommand = new RxCommand();
            StartCommand = new RxCommand();

            SynchronizationContext sync = SynchronizationContext.Current;

            MarketService = Bootstrapper.ResolveSession().GetProxy<IPublicMarketService>();

            RxMessageBus.Subscribe<OperationsMessage>(x =>
            {
                IsOperationsVisible.Value = !IsOperationsVisible.Value;
            });

            CancelCommand.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<IProblemListControlViewModel, IProblemListControl>(
                        new Arg("patientEntryModel", PatientEntryModel.Value),
                        new Arg("problemEntryModel", ProblemEntryModel.Value));
                });

            StartCommand.Subscribe(x =>
                {
                    sync.Send(z =>
                    {
                        if (SessionEntryModel.Value.AssetModel.Value != null)
                        {
                            SessionEntryModel.Value.AssetModel.Value.AssetRunningCommand.Subscribe(y =>
                            {
                                WindowViewModel.Value.LoadContent<ISessionStartObserverControlViewModel, IObserverControl>(
                                       new Arg("patientEntryModel", PatientEntryModel.Value),
                                       new Arg("problemEntryModel", ProblemEntryModel.Value),
                                       new Arg("sessionEntryModel", SessionEntryModel.Value));
                            });

                            SessionEntryModel.Value.AssetModel.Value.Run();
                        }
                    }, null);
                });

           

            LoadAssetsInSafe();

        }

        private void LoadAssetsInSafe()
        {
            var assetGroups = MarketService.GetAssetGroups();
            AssetAdapter assetAdapter = new AssetAdapter();
            var list = MarketService.GetAssetsInSafe();

            assetGroups
                .SelectMany(x => x.Assets)
                .ToList()
                .ForEach(x =>
                {
                    if (x.Available)
                    {
                        AssetModel tempAssetModel = assetAdapter.Convert(x);
                        tempAssetModel.IsInSafe = list.Contains(x.Id);

                        var sync = SynchronizationContext.Current;

                        if (tempAssetModel.IsInSafe)
                        {
                            Task t = new Task(async y =>
                            {
                                tempAssetModel.ImageSource.Value = await MarketService.GetCachedImage(((AssetModel)y).Id.Value);
                            }, tempAssetModel);
                            t.Start();
                            Assets.Value.Add(tempAssetModel);
                        }
                    }
                });
        }
    }
}
