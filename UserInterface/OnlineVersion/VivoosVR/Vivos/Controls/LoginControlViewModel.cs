using Castle.Windsor;
using Core.Client;
using Core.Client.Configuration;
using Core.Log;
using Core.Log.Exceptions;
using Core.Log.Faults;
using Core.MVVM;
using Core.MVVM.MessageBox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Vivos.Windows;
using System.Deployment.Application;

namespace Vivos.Controls
{
    public class LoginControlViewModel : BaseControlViewModel, ILoginControlViewModel
    {
        public RxCommand CancelCommand { get; set; }
        public Observable<string> Error { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public RxCommand LoginCommand { get; set; }
        public Observable<LoginModel> LoginModel { get; set; }
        public Observable<Visibility> WhitePanelVisibility { get; set; }

        public Observable<string> Version { get; set; }

        public LoginControlViewModel(IWindsorContainer container, ILoginControl view)
                            : base(container, view, "Giriş")
        {
            WhitePanelVisibility = new Observable<Visibility>(Visibility.Hidden);
            IsRunning = new Observable<bool>(false);
            LoginCommand = new RxCommand();
            Error = new Observable<string>();
            LoginModel = new Observable<LoginModel>(new LoginModel());
            CancelCommand = new RxCommand();
            
            Version = new Observable<string>("Not Installed");
            try
            {
                Version.Value = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            catch (InvalidDeploymentException)
            {
            }

            if (Core.Client.Properties.Settings.Default.LastUsername != null)
            {
                LoginModel.Value.Username.Value = Core.Client.Properties.Settings.Default.LastUsername;
            }

            RxMessageBus.Subscribe<PasswordChangedMessage>(x =>
            {
                LoginModel.Value.Password.Value = x.Password.Value;
            });

            LoginCommand.Subscribe(async (x) =>
            {
                ISession session = null;

                try
                {
                    IsRunning.Value = true;
                    WhitePanelVisibility.Value = Visibility.Visible;

                    session = await DoLogin();

                    if (session != null)
                    {
                        RxMessageBus.Send<LoginMessage>(new LoginMessage(session.CurrentUser));

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
                }
                catch (SecurityException exc)
                {
                    exc.LogException();

                    session.SignOut();

                    LoginModel.Error = exc.Message;
                }
                catch (FaultException<CoreFault> exc)
                {
                    if (session != null)
                    {
                        session.SignOut();
                    }

                    LoginModel.Error = exc.Message;
                    //throw;
                }
                finally
                {
                    IsRunning.Value = false;
                    WhitePanelVisibility.Value = Visibility.Hidden;
                }
            });

            CancelCommand.Subscribe(x =>
            {
                Close();
            });
        }

        private async Task<ISession> DoLogin()
        {
            ISession ret = null;

            Task<ISession> task = new Task<ISession>(() =>
            {
                ISession session = Bootstrapper.Resolve<ISession>();
                return session.SignIn(LoginModel.Value.Username.Value, LoginModel.Value.Password.Value);
            });

            task.Start();

            ret = await task;

            return ret;
        }
    }
}
