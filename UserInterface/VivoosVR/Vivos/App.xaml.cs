using Vivos.Windows;
using Core.MVVM;
using Core.MVVM.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Core.MVVM.MessageBox;
using Core.Log;
using Vivos;
using Vivos.Controls;
using Core.Log.Faults;
using Core.Log.Exceptions;
using Core.Client;
using MahApps.Metro;

namespace Vivos
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SingleInstanceHelper.Make(SingleInstanceModes.ForEveryUser);

            this.Dispatcher.UnhandledException += new DispatcherUnhandledExceptionEventHandler(Dispatcher_UnhandledException);
            // dogu: MVVM light toolkit için kullanılan, multi threading exception handling yöntemi. Rx için baştan yazılacak.
            //TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(TaskScheduler_UnobservedTaskException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string surum = fvi.FileVersion;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            IMainWindowViewModel mainWindowViewModel = Bootstrapper.Resolve<IMainWindowViewModel>();
            ((IBaseViewModel)mainWindowViewModel).Title.Value = "ANA PENCERE";

            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent("Amber"), ThemeManager.GetAppTheme("Amber")); // or appStyle.Item1

            MainWindow = (MainWindow)mainWindowViewModel.LoadWindow<IMainWindowViewModel, IMainWindow>(mainWindowViewModel);

            if (MainWindow != null)
            {
                //if (System.Windows.Forms.Screen.AllScreens.Count() > 1)
                //{
                //    var left = System.Windows.Forms.Screen.AllScreens[1].Bounds.Left;
                //    MainWindow.Left = left;
                //}

                MainWindow.Show();
                base.OnStartup(e);
            }
        }

        


        void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Exception.LogException();

                var typedException = e.Exception as FaultException<CoreFault>;

                if (typedException != null && !string.IsNullOrWhiteSpace(typedException.Detail.Code))
                {
                    string msg = string.Format("{0}{1}{1}Kod:{2}", typedException.Detail.Message, Environment.NewLine, typedException.Detail.Code);
                    MetroMessage.Show(msg, MessageBoxButton.OK, MessageBoxImage.Error);
                    e.Exception.LogException();
                    e.Handled = true;

                    return;
                }

                var baseException = e.Exception as IBaseException;
                if (baseException != null)
                {
                    string msg = string.Format("{0}{1}{1}Kod:{2}", baseException.Message, Environment.NewLine, baseException.Code);
                    MetroMessage.Show(msg, MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MetroMessage.Show(e.Exception.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }

                e.Handled = true;
            }
            catch (Exception ex)
            {
                ex.LogException();
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //MetroMessage.Show("Bir hata oluştu. Detayları Windows güncelerine eklendi.", MessageBoxButton.OK, MessageBoxImage.Error);

            try
            {
                e.ExceptionObject.ToString().LogError();
            }
            catch (Exception)
            {
            }
        }
    }
}
