using Castle.Windsor;
using Core.Client;
using Core.Client.Configuration;
using Core.Client.Messages;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Market.Client.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Vivos.Controls;

namespace Vivos.Windows
{
    public class MainWindowViewModel : BaseWindowViewModel, IMainWindowViewModel
    {
        public RxAsyncCommand GoHomeCommand { get; set; }
        public RxAsyncCommand GoSettingsCommand { get; set; }
        public Observable<bool> IsMenuOpen { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public RxCommand OperationsCommand { get; set; }
        public Observable<Visibility> UserCommandsVisibility { get; set; }

        public MainWindowViewModel(IWindsorContainer container, IMainWindow window, ISecurityChecker securityChecker, ISecurityFailedControlViewModel securityFailedControlViewModel)
                            : base(container, window, securityChecker, securityFailedControlViewModel)
        {
            GoSettingsCommand = new RxAsyncCommand();
            GoHomeCommand = new RxAsyncCommand();
            OperationsCommand = new RxCommand();

            IsMenuOpen = new Observable<bool>(true);
            IsRunning = new Observable<bool>(false);
            ClosingCommand = new RxCommand();
            FullName = new Observable<string>();
            UserCommandsVisibility = new Observable<Visibility>(Visibility.Hidden);

            RxMessageBus.Subscribe<MainWindowCloseMessage>(x =>
            {
                if (MetroMessage.Show("Uygulamadan çıkmak istiyor musunuz ?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    x.IsCancel = false;
                }
                else
                {
                    x.IsCancel = true;
                }
            });

            RxMessageBus.Subscribe<LoginMessage>(x =>
            {
                UserCommandsVisibility.Value = Visibility.Visible;
                FullName.Value = SecurityChecker.GetCurrentFullNameOfUser();
            });

            LoadedCommand.Subscribe(x =>
            {
                RxMessageBus.Send<MainWindowLoadedMessage>(new MainWindowLoadedMessage());

                LoadContent<ILoginControlViewModel, ILoginControl>();
            });


            GoSettingsCommand.Subscribe(x =>
            {
                CurrentControlViewModel.Value = GetContent<ISettingsControlViewModel, ISettingsControl>(true);
            });

            GoHomeCommand.Subscribe(x =>
            {
                if (SecurityChecker.DoIHaveRole(ClientConfiguration.Configuration.AdminRoleCode))
                {
                    LoadContent<IAdminHomeControlViewModel, IAdminHomeControl>();
                }
                else
                {
                    LoadContent<IWelcomeControlViewModel, IWelcomeControl>();
                }
            });

            OperationsCommand.Subscribe(x =>
                {
                    RxMessageBus.Send<OperationsMessage>(new OperationsMessage());
                });


        }
    }
}
