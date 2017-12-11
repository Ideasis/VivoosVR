using Castle.Windsor;
using Core.Client;
using Core.Common.Services;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client.Adapters;
using Market.Client.Messages;
using Market.Client.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using nsMarket = Core.Common.DataModel.Market;

namespace Vivos.Controls
{
    public class SessionListControlViewModel : BaseControlViewModel, ISessionListControlViewModel
    {
        public Observable<bool> IsOperationsVisible { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public RxCommand NewSessionCommand { get; set; }
        public Observable<PatientEntryModel> PatientEntryModel { get; set; }
        public Observable<ProblemEntryModel> ProblemEntryModel { get; set; }
        public RxCommand DeleteSessionCommand { get; set; }
        public IPublicMarketService MarketService { get; set; }
        public RxCommand DownloadDataCommand { get; set; }
        public RxCommand PatientListCommand { get; set; }
        public RxCommand ProblemListCommand { get; set; }



        public Observable<ObservableCollection<SessionEntryModel>> Sessions { get; set; }

        public SessionListControlViewModel(IWindsorContainer container, ISessionListControl view, PatientEntryModel patientEntryModel, ProblemEntryModel problemEntryModel)
                    : base(container, view, "Oturum Listesi")
        {
            IsRunning = new Observable<bool>(false);
            IsOperationsVisible = new Observable<bool>(false);
            NewSessionCommand = new RxCommand();
            DeleteSessionCommand = new RxCommand();
            DownloadDataCommand = new RxCommand();
            PatientEntryModel = new Observable<PatientEntryModel>(patientEntryModel);
            ProblemEntryModel = new Observable<ProblemEntryModel>(problemEntryModel);
            Sessions = new Observable<ObservableCollection<SessionEntryModel>>(new ObservableCollection<SessionEntryModel>());
            MarketService = Bootstrapper.ResolveSession().GetProxy<IPublicMarketService>();
            ProblemListCommand = new RxCommand();
            PatientListCommand = new RxCommand();

            NewSessionCommand.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<ISessionStartControlViewModel, ISessionStartControl>(
                                new Arg("patientEntryModel", PatientEntryModel.Value),
                                new Arg("problemEntryModel", problemEntryModel));
                });

            DeleteSessionCommand.Subscribe(x =>
                {
                    string message = $"Bu oturumu silmek istediğinizden emin misiniz ?{Environment.NewLine}{Environment.NewLine}NOT: Bu işlemin geri dönüşü yoktur.";

                    if (MetroMessage.Show(message, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        SessionEntryModel sessionEntryModel = x as SessionEntryModel;
                        MarketService.DeleteSession(sessionEntryModel.Id.Value);

                        LoadList();
                    }
                });

            DownloadDataCommand.Subscribe(x =>
                {
                    SessionEntryModel sessionEntryModel = x as SessionEntryModel;
                    nsMarket.Session session = MarketService.GetSession(sessionEntryModel.Id.Value);
                    SaveFileDialog saveDialogBox = new SaveFileDialog();

                    saveDialogBox.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saveDialogBox.Filter = "CSV Dosyası (*.csv)|*.csv";
                    saveDialogBox.FilterIndex = 1;
                    saveDialogBox.Title = "Oturum Sensör Kaydı";
                    saveDialogBox.DefaultExt = ".csv";
                    saveDialogBox.AddExtension = true;
                    if (saveDialogBox.ShowDialog() == true)
                    {
                        File.WriteAllText(saveDialogBox.FileName, session.SensorData, Encoding.Default);
                        Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    }
                });

            PatientListCommand.Subscribe(x =>
            {
                WindowViewModel.Value.LoadContent<IPatientListControlViewModel, IPatientListControl>();
            });

            ProblemListCommand.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<IProblemListControlViewModel, IProblemListControl>(new Arg("patientEntryModel", PatientEntryModel.Value));
                });

            RxMessageBus.Subscribe<OperationsMessage>(x =>
            {
                IsOperationsVisible.Value = !IsOperationsVisible.Value;
            });

            ControlLoadedCommand.Subscribe(x =>
            {
                if (Market.Client.Properties.Settings.Default.FlashOperations)
                {
                    IsOperationsVisible.Value = true;
                    if (Market.Client.Properties.Settings.Default.FlashOperationsDurationInSeconds > 0)
                    {
                        Task.Delay(TimeSpan.FromSeconds(Market.Client.Properties.Settings.Default.FlashOperationsDurationInSeconds)).ContinueWith(y => IsOperationsVisible.Value = false);
                    }
                }
            });

            LoadList();
        }

        public void LoadList()
        {

            BackgroundThread bt = new BackgroundThread();
            var sync = SynchronizationContext.Current;
            Sessions.Value = new ObservableCollection<SessionEntryModel>();

            bt.Subscribe(x =>
            {
                IsRunning.Value = true;
                var problems = MarketService.GetSessions(ProblemEntryModel.Value.Id.Value);

                SessionAdapter adapter = new SessionAdapter();
                problems.ForEach(y =>
                {
                    sync.Send(z =>
                    {
                        Sessions.Value.Add(adapter.Convert(y));
                    }, null);

                });
                IsRunning.Value = false;
            });

            bt.Run();
        }
    }
}
