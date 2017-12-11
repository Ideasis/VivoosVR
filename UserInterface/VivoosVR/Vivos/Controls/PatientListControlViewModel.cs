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
    public class PatientListControlViewModel : BaseControlViewModel, IPatientListControlViewModel
    {
        public RxCommand EditCommand { get; set; }
        public Observable<bool> IsOperationsVisible { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public RxCommand NewPatientCommand { get; set; }
        public Observable<ObservableCollection<PatientEntryModel>> Patients { get; set; }
        public RxCommand ProblemsCommand { get; set; }

        public PatientListControlViewModel(IWindsorContainer container, IPatientListControl view)
                    : base(container, view, "Hasta Listesi")
        {
            IsRunning = new Observable<bool>(false);
            IsOperationsVisible = new Observable<bool>(false);

            Patients = new Observable<ObservableCollection<PatientEntryModel>>(new ObservableCollection<PatientEntryModel>());

            EditCommand = new RxCommand();
            ProblemsCommand = new RxCommand();
            NewPatientCommand = new RxCommand();

            EditCommand.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<IPatientEntryControlViewModel, IPatientEntryControl>(new Arg("patientEntryModel", x));
                });

            NewPatientCommand.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<IPatientEntryControlViewModel, IPatientEntryControl>();
                });

            ProblemsCommand.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<IProblemListControlViewModel, IProblemListControl>(new Arg("patientEntryModel", x));
                });

            RxMessageBus.Subscribe<OperationsMessage>(x =>
            {
                IsOperationsVisible.Value = !IsOperationsVisible.Value;
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
                var patients = marketService.GetPatients().OrderBy(y=>y.Code).ToList();
                PatientAdapter adapter = new PatientAdapter();
                patients.ForEach(y =>
                {
                    sync.Send(z =>
                    {
                        Patients.Value.Add(adapter.Convert(y));
                    }, null);

                });
                IsRunning.Value = false;
            });

            bt.Run();
        }
    }
}
