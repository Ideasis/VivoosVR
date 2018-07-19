using Castle.Windsor;
using Core.Client;
using Core.Common.Services;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client.Adapters;
using Market.Client.Messages;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vivos.Windows;

namespace Vivos.Controls
{
    public class ProblemListControlViewModel : BaseControlViewModel, IProblemListControlViewModel
    {
        public RxCommand EditCommand { get; set; }
        public Observable<bool> IsOperationsVisible { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public RxCommand NewProblemCommand { get; set; }
        public Observable<PatientEntryModel> PatientEntryModel { get; set; }
        public Observable<ObservableCollection<ProblemEntryModel>> Problems { get; set; }
        public RxCommand SessionListCommand { get; set; }
        public RxCommand PatientListCommand { get; set; }

        public ProblemListControlViewModel(IWindsorContainer container, IProblemListControl view, PatientEntryModel patientEntryModel)
                            : base(container, view, "Şikâyet Listesi")
        {
            IsRunning = new Observable<bool>(false);
            IsOperationsVisible = new Observable<bool>(false);

            PatientEntryModel = new Observable<PatientEntryModel>();
            NewProblemCommand = new RxCommand();
            EditCommand = new RxCommand();
            SessionListCommand = new RxCommand();
            PatientListCommand = new RxCommand();
            Problems = new Observable<ObservableCollection<ProblemEntryModel>>(new ObservableCollection<ProblemEntryModel>());

            PatientEntryModel.Value = patientEntryModel ?? new PatientEntryModel();

            PatientListCommand.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<IPatientListControlViewModel, IPatientListControl>();
                });

            NewProblemCommand.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<IProblemEntryControlViewModel, IProblemEntryControl>(new Arg("patientEntryModel", PatientEntryModel.Value));
                });

            EditCommand.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<IProblemEntryControlViewModel, IProblemEntryControl>(
                        new Arg("patientEntryModel", PatientEntryModel.Value),
                        new Arg("problemEntryModel", x));
                });

            SessionListCommand.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<ISessionListControlViewModel, ISessionListControl>(
                        new Arg("patientEntryModel", PatientEntryModel.Value),
                        new Arg("problemEntryModel", x));
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
            IPublicMarketService marketService = Bootstrapper.ResolveSession().GetProxy<IPublicMarketService>();
            BackgroundThread bt = new BackgroundThread();
            var sync = SynchronizationContext.Current;

            bt.Subscribe(x =>
            {
                IsRunning.Value = true;
                var problems = marketService.GetProblems(PatientEntryModel.Value.Id.Value);

                ProblemEntryAdapter adapter = new ProblemEntryAdapter();
                problems.ForEach(y =>
                {
                    sync.Send(z =>
                    {
                        Problems.Value.Add(adapter.Convert(y));
                    }, null);

                });
                IsRunning.Value = false;
            });

            bt.Run();
        }
    }
}
