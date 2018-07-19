using Core.MVVM.MessageBox;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Core.MVVM.Tools
{
    public enum SingleInstanceModes
    {
        /// <summary>
        /// Do nothing.
        /// </summary>
        NotInited = 0,

        /// <summary>
        /// Every user can have own single instance.
        /// </summary>
        ForEveryUser,
    }

    public static class SingleInstanceHelper
    {
        [SuppressMessage("Microsoft.Performance", "CA1823")]
        internal static DispatcherTimer AutoExitAplicationIfStartupDeadlock;

        public static void Make()
        {
            Make(SingleInstanceModes.ForEveryUser);
        }

        public static void Make(SingleInstanceModes singleInstanceModes)
        {
            var appName = Application.Current.GetType().Assembly.ManifestModule.ScopeName;

            var windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var keyUserName = windowsIdentity != null ? windowsIdentity.User.ToString() : String.Empty;

            // Be careful! Max 260 chars!
            var eventWaitHandleName = string.Format(
                "{0}{1}",
                appName,
                singleInstanceModes == SingleInstanceModes.ForEveryUser ? keyUserName : String.Empty
                );

            try
            {
                using (var eventWaitHandle = EventWaitHandle.OpenExisting(eventWaitHandleName))
                {
                    // It informs first instance about other startup attempting.
                    eventWaitHandle.Set();
                }

                // Let's terminate this posterior startup.
                // For that exit no interceptions.
                Environment.Exit(0);
            }
            catch
            {
                // It's first instance.

                // Register EventWaitHandle.
                using (var eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset, eventWaitHandleName))
                {
                    ThreadPool.RegisterWaitForSingleObject(eventWaitHandle, OtherInstanceAttemptedToStart, null, Timeout.Infinite, false);
                }

                RemoveApplicationsStartupDeadlockForStartupCrushedWindows();
            }
        }

        private static void OtherInstanceAttemptedToStart(Object state, Boolean timedOut)
        {
            RemoveApplicationsStartupDeadlockForStartupCrushedWindows();
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    MetroMessage.Show("Sisteminizde tek bir Vivos uygulaması açabilirsiniz, Vivos programını kapatıp tekrar deneyebilirsiniz.", MessageBoxButton.OK, MessageBoxImage.Warning);

                    Application.Current.MainWindow.Activate();
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                }
                catch { }
            }));
        }

        public static void RemoveApplicationsStartupDeadlockForStartupCrushedWindows()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                AutoExitAplicationIfStartupDeadlock =
                    new DispatcherTimer(
                        TimeSpan.FromSeconds(6),
                        DispatcherPriority.ApplicationIdle,
                        (o, args) =>
                        {
                            if (Application.Current.Windows.Cast<Window>().Count(window => !Double.IsNaN(window.Left)) == 0)
                            {
                                // For that exit no interceptions.
                                Environment.Exit(0);
                            }
                        },
                        Application.Current.Dispatcher
                    );
            }),
                DispatcherPriority.ApplicationIdle
                );
        }
    }
}
