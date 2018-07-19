using Castle.Windsor;
using Core.Client;
using Core.Common.DataModel;
using Core.Common.DataModel.Market;
using Core.Common.Services;
using Core.Log.Exceptions;
using Core.Log.Faults;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client.Adapters;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Vivos.Windows;

namespace Vivos.Controls
{
    public class PatientEntryControlViewModel : BaseControlViewModel, IPatientEntryControlViewModel
    {
        public RxCommand CancelCommand { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public Observable<PatientEntryModel> PatientEntryModel { get; set; }
        public RxCommand SaveCommand { get; set; }
        public Observable<string> TextToConfirm { get; set; }

        public PatientEntryControlViewModel(IWindsorContainer container, IPatientEntryControl view, PatientEntryModel patientEntryModel = null)
                    : base(container, view, "Hasta Girişi")
        {
            IsRunning = new Observable<bool>(false);

            PatientEntryModel = new Observable<PatientEntryModel>();
            PatientEntryModel.Value = patientEntryModel ?? new PatientEntryModel();

            SaveCommand = new RxCommand();
            CancelCommand = new RxCommand();

            TextToConfirm = new Observable<string>();

            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "PUA.txt");

            TextToConfirm.Value = File.ReadAllText(path);

            CancelCommand.Subscribe(x =>
                {
                    WindowViewModel.Value.LoadContent<IPatientListControlViewModel, IPatientListControl>();
                });

            SaveCommand.Subscribe(x =>
                {
                    PatientAdapter adapter = new PatientAdapter();
                    Patient patient = adapter.Convert(PatientEntryModel.Value);

                    Bootstrapper.ResolveSession().GetProxy<IPublicMarketService>().SavePatient(patient);

                    WindowViewModel.Value.LoadContent<IPatientListControlViewModel, IPatientListControl>();
                });


        }
    }
}
