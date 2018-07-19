using Castle.Windsor;
using Core.Client;
using Core.Common.DataModel;
using Core.Common.DataModel.Market;
using Core.Common.Services;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client.Adapters;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vivos.Windows;

namespace Vivos.Controls
{
    public class ProblemEntryControlViewModel : BaseControlViewModel, IProblemEntryControlViewModel
    {
        public RxCommand CancelCommand { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public Observable<PatientEntryModel> PatientEntryModel { get; set; }
        public Observable<ProblemEntryModel> ProblemEntryModel { get; set; }
        public RxCommand SaveCommand { get; set; }

        public ProblemEntryControlViewModel(IWindsorContainer container, IProblemEntryControl view, PatientEntryModel patientEntryModel, ProblemEntryModel problemEntryModel = null)
                    : base(container, view, "Şikâyet Kayıt")
        {
            IsRunning = new Observable<bool>(false);
            ProblemEntryModel = new Observable<ProblemEntryModel>(problemEntryModel ?? new ProblemEntryModel());
            PatientEntryModel = new Observable<Market.Client.Models.PatientEntryModel>(patientEntryModel);

            ProblemEntryModel.Value.PatientId.Value = patientEntryModel.Id.Value;

            SaveCommand = new RxCommand();
            SaveCommand.Subscribe(x =>
                {
                    ProblemEntryAdapter adapter = new ProblemEntryAdapter();
                    Problem problem = adapter.Convert(ProblemEntryModel.Value);
                    Bootstrapper.ResolveSession().GetProxy<IPublicMarketService>().SaveProblem(problem);

                    MetroMessage.Show("Şikâyet başarı ile kaydedildi.", MessageBoxButton.OK, MessageBoxImage.Information);

                    WindowViewModel.Value.LoadContent<IProblemListControlViewModel, IProblemListControl>(new Arg("patientEntryModel", PatientEntryModel.Value));
                });

            CancelCommand = new RxCommand();
            CancelCommand.Subscribe(x =>
                {
                    if (MetroMessage.Show("Yapılan değişiklikler kaydedilmeden Şikâyet liste ekranına dönülecek. Emin misiniz ?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        WindowViewModel.Value.LoadContent<IProblemListControlViewModel, IProblemListControl>(new Arg("patientEntryModel", PatientEntryModel.Value));
                    }
                });

        }
    }
}
