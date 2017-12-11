using Core.Client.Messages;
using Core.MVVM;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using frm = System.Windows.Forms;


namespace Vivos.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DoctorMainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindowCloseMessage msg = new MainWindowCloseMessage();
            RxMessageBus.Send<MainWindowCloseMessage>(msg);

            e.Cancel = msg.IsCancel;

        }

        public void MaximizeToSecondaryMonitor()
        {
            var secondaryScreen = frm.Screen.AllScreens.FirstOrDefault(s => !s.Primary);

            if (secondaryScreen != null)
            {
                var workingArea = secondaryScreen.WorkingArea;
                Left = workingArea.Left;
                Top = workingArea.Top;
                Width = workingArea.Width;
                Height = workingArea.Height;

                if (IsLoaded)
                {
                    WindowState = WindowState.Maximized;
                }
            }
        }

        private void DoctorMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MaximizeToSecondaryMonitor();

        }
    }
}
