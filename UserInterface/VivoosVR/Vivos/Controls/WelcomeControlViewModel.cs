using Castle.Windsor;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vivos.Windows;

namespace Vivos.Controls
{
    public class WelcomeControlViewModel : BaseControlViewModel, IWelcomeControlViewModel
    {
        public Observable<string> Criteria { get; set; }
        public RxCommand GoNewPatient { get; set; }
        public RxCommand GoScenarioSelector { get; set; }
        public RxCommand GoSearchPatient { get; set; }
        public RxCommand GoSettings { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public RxCommand OpenCommand { get; set; }
        public RxCommand SearchCommand { get; set; }
        public Observable<string> SearchResult { get; set; }

        public WelcomeControlViewModel(IWindsorContainer container, IWelcomeControl view)
                    : base(container, view, "Hoşgeldiniz")
        {
            IsRunning = new Observable<bool>(false);
            FullName = new Observable<string>();

            GoNewPatient = new RxCommand();
            GoSearchPatient = new RxCommand();
            GoSettings = new RxCommand();
            GoScenarioSelector = new RxCommand();


            WindowViewModel.Subscribe(x =>
            {
                FullName.Value = x.FullName.Value;
            });

            WindowViewModel.Subscribe(x =>
            {
                FullName.Value = x.FullName.Value;
            });

            GoNewPatient.Subscribe(x =>
            {
                WindowViewModel.Value.LoadContent<IPatientEntryControlViewModel, IPatientEntryControl>();
            });

            GoSearchPatient.Subscribe(x =>
            {
                WindowViewModel.Value.LoadContent<IPatientListControlViewModel, IPatientListControl>();
            });

            GoSettings.Subscribe(x =>
            {
                WindowViewModel.Value.LoadContent<ISettingsControlViewModel, ISettingsControl>();
            });

            GoScenarioSelector.Subscribe(x =>
            {
                //WindowViewModel.Value.LoadContent<IScenarioListControlViewModel, IScenarioListControl>();
            });



        }
    }
}
