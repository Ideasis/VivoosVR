using Castle.Windsor;
using Core.Client;
using Core.Client.Configuration;
using Core.Log.Exceptions;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client.Configuration;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Vivos.Controls
{
    public class SettingsControlViewModel : BaseControlViewModel, ISettingsControlViewModel
    {
        public RxCommand CancelCommand { get; set; }
        public RxCommand SaveCommand { get; set; }
        public Observable<SettingsModel> SettingsModel { get; set; }

        public void RedirectToWelcomePage()
        {
            ISession session = Bootstrapper.ResolveSession();

            if (session.DoIHaveRole(ClientConfiguration.Configuration.AdminRoleCode))
            {
                WindowViewModel.Value.LoadContent<IAdminHomeControlViewModel, IAdminHomeControl>();
            }
            else if (session.DoIHaveRole(ClientConfiguration.Configuration.DoctorRole))
            {
                WindowViewModel.Value.LoadContent<IWelcomeControlViewModel, IWelcomeControl>();
            }
            else
            {
                throw new UserException("Giriş yapılan kullanıcının aktif rolü bulunmamaktadır. Başka kullanıcı ile tekrar deneyiniz.");
            }
        }

        public SettingsControlViewModel(IWindsorContainer container, ISettingsControl view)
                                    : base(container, view, "Ayarlar")
        {
            SaveCommand = new RxCommand();
            CancelCommand = new RxCommand();

            var settingsModel = new SettingsModel();
            settingsModel.IpAddress.Value = Market.Client.Properties.Settings.Default.IPAddress;
            settingsModel.Port.Value = Market.Client.Properties.Settings.Default.Port.ToString();
            settingsModel.LocalPath.Value = Market.Client.Properties.Settings.Default.LocalPath;
            settingsModel.NeuLogPath.Value = Market.Client.Properties.Settings.Default.NeuLogPath;
            settingsModel.NeuLogServer.Value = Market.Client.Properties.Settings.Default.NeuLogServer;
            settingsModel.NeuLogPort.Value = Market.Client.Properties.Settings.Default.NeuLogPort.ToString();
            settingsModel.NeuLogVisibleSpan.Value = Market.Client.Properties.Settings.Default.NeuLogVisibleSpan.ToString();
            settingsModel.NeuLogReadMilliseconds.Value = Market.Client.Properties.Settings.Default.NeuLogReadMilliseconds.ToString();
            settingsModel.FlashOperations.Value = Market.Client.Properties.Settings.Default.FlashOperations.ToString();
            settingsModel.FlashOperationsDurationInSeconds.Value = Market.Client.Properties.Settings.Default.FlashOperationsDurationInSeconds.ToString();

            SettingsModel = new Observable<SettingsModel>(settingsModel);


            SaveCommand.Subscribe(x =>
                {
                    if (!settingsModel.IsValid)
                    {
                        MetroMessage.Show("Ekranda belirtilen hataları giderdikten sonra tekrar deneyiniz.", MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                        return;
                    }

                    Market.Client.Properties.Settings.Default.IPAddress = settingsModel.IpAddress.Value;
                    Market.Client.Properties.Settings.Default.Port = Convert.ToInt32(settingsModel.Port.Value);
                    Market.Client.Properties.Settings.Default.LocalPath = settingsModel.LocalPath.Value;
                    Market.Client.Properties.Settings.Default.NeuLogPath = settingsModel.NeuLogPath.Value;
                    Market.Client.Properties.Settings.Default.NeuLogServer = settingsModel.NeuLogServer.Value;
                    Market.Client.Properties.Settings.Default.NeuLogPort = Convert.ToInt32(settingsModel.NeuLogPort.Value);
                    Market.Client.Properties.Settings.Default.NeuLogVisibleSpan = TimeSpan.Parse(settingsModel.NeuLogVisibleSpan.Value);
                    Market.Client.Properties.Settings.Default.NeuLogReadMilliseconds = Convert.ToInt32(settingsModel.NeuLogReadMilliseconds.Value);
                    Market.Client.Properties.Settings.Default.FlashOperations = bool.Parse(settingsModel.FlashOperations.Value);
                    Market.Client.Properties.Settings.Default.FlashOperationsDurationInSeconds = Convert.ToInt32(settingsModel.FlashOperationsDurationInSeconds.Value);

                    Market.Client.Properties.Settings.Default.Save();

                    MetroMessage.Show("Değişiklikleriniz başarı ile kaydedildi.", MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);

                    RedirectToWelcomePage();
                });

            CancelCommand.Subscribe(x =>
                {
                    RedirectToWelcomePage();
                });
        }
    }
}
