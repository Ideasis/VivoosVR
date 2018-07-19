using Castle.Windsor;
using Core.Client;
using Core.Common.Services;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Core.Socket.Client;
using Market.Client.Adapters;
using Market.Client.Models;
using nsMarket = Core.Common.DataModel.Market;
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
using Vivos.Windows;

namespace Vivos.Controls
{
    public class SessionStartObserverControlViewModel : BaseObserverControlViewModel, ISessionStartObserverControlViewModel
    {
        public RxCommand CancelCommand { get; set; }
        public RxCommand CommandSendSud { get; set; }
        public RxCommand KeyCommand { get; set; }
        public Observable<PatientEntryModel> PatientEntryModel { get; set; }
        public Observable<ProblemEntryModel> ProblemEntryModel { get; set; }
        public Observable<bool> Recording { get; set; }
        public RxCommand SaveCommand { get; set; }
        public Observable<SessionEntryModel> SessionEntryModel { get; set; }
        public Observable<byte> Sud { get; set; }
        public Observable<string> SudText { get; set; }

        public SessionStartObserverControlViewModel(
                                                    IWindsorContainer container,
                                                    IObserverControl view,
                                                    PatientEntryModel patientEntryModel,
                                                    ProblemEntryModel problemEntryModel,
                                                    SessionEntryModel sessionEntryModel) : base(container, view)
        {
            PatientEntryModel = new Observable<PatientEntryModel>(patientEntryModel);
            ProblemEntryModel = new Observable<ProblemEntryModel>(problemEntryModel);
            SessionEntryModel = new Observable<SessionEntryModel>(sessionEntryModel);

            AssetModel.Value = SessionEntryModel.Value.AssetModel.Value;

            SaveCommand = new RxCommand();
            CancelCommand = new RxCommand();
            KeyCommand = new RxCommand();
            CommandSendSud = new RxCommand();

            Recording = new Observable<bool>(false);
            SudText = new Observable<string>("0");
            Sud = new Observable<byte>(0);

            Recording.Subscribe(x =>
                {
                    PulseControlViewModel.Value.SetCommand(x ? "Çalışıyor" : "Beklemede", x ? 101 : 100);
                });

            SudText.SubscribeValidate(x =>
            {
                byte value = 0;

                if (!byte.TryParse(x, out value)) return "SUD değeri 0 ile 100 arasında olmalıdır.";
                if (value < 0 || value > 100) return "SUD değeri 0 ile 100 arasında olmalıdır.";

                return null;
            });

            SudText.Subscribe(x =>
            {
                if (SudText.Validate() == null) Sud.Value = Convert.ToByte(SudText.Value);
            });

            CommandSendSud.Subscribe(x =>
                {
                    if (SudText.Validate() == null)
                    {
                        PulseControlViewModel.Value.SetSud(Sud.Value);
                        SudText.Value = "0";
                    }
                });

            SaveCommand.Subscribe(x =>
                {
                    if (MetroMessage.Show("Oturum kaydını tamamlamak istiyor musunuz ?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        SessionEntryModel.Value.AssetModel.Value.Stop();
                        PulseControlViewModel.Value.Stop();

                        // Read Sensor Data from temp
                        var fileStream = new FileStream(PulseControlViewModel.Value.FilePath.Value, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        StreamReader streamReader = new StreamReader(fileStream, Encoding.Default);
                        SessionEntryModel.Value.SensorData.Value = streamReader.ReadToEnd();
                        streamReader.Close();
                        fileStream.Close();

                        SessionEntryModel.Value.ProblemId.Value = ProblemEntryModel.Value.Id.Value;
                        SessionEntryModel.Value.Id.Value = Guid.NewGuid();
                        SessionEntryModel.Value.SessionDateTime.Value = DateTime.Now;

                        IPublicMarketService marketService = Bootstrapper.ResolveSession().GetProxy<IPublicMarketService>();
                        SessionAdapter adapter = new SessionAdapter();
                        nsMarket.Session session = adapter.Convert(SessionEntryModel.Value);
                        session.Asset = null;
                        marketService.SaveSession(session);

                        WindowViewModel.Value.LoadContent<ISessionListControlViewModel, ISessionListControl>(
                            new Arg("patientEntryModel", PatientEntryModel.Value),
                            new Arg("problemEntryModel", ProblemEntryModel.Value));
                    }
                });

            CancelCommand.Subscribe(x =>
                {
                    if (MetroMessage.Show("Oturum kaydı yapılmadan çıkmak istiyor musunuz ?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        SessionEntryModel.Value.AssetModel.Value.Stop();
                        PulseControlViewModel.Value.Stop();

                        WindowViewModel.Value.LoadContent<ISessionListControlViewModel, ISessionListControl>(
                            new Arg("patientEntryModel", PatientEntryModel.Value),
                            new Arg("problemEntryModel", ProblemEntryModel.Value));
                    }
                });

            KeyCommand.Subscribe(x =>
                {
                    PulseControlViewModel.Value.SetMarker(x.ToString());

                    Debug.WriteLine("Key " + x.ToString());
                });
        }
    }
}
