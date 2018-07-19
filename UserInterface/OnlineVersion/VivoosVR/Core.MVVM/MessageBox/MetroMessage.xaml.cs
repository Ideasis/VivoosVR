using Core.MVVM.Configuration;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Core.MVVM.MessageBox
{
    /// <summary>
    /// Interaction logic for MetroMessage.xaml
    /// </summary>
    public partial class MetroMessage : MetroWindow
    {
        public MessageBoxButton Button { get; set; }
        public RxCommand CancelCommand { get; set; }
        public Observable<Visibility> CancelVisibility { get; set; }
        public Observable<string> Caption { get; set; }
        public Observable<Visibility> ErrorVisibility { get; set; }
        public Observable<Visibility> InformationVisibility { get; set; }
        public Observable<string> Message { get; set; }
        public RxCommand NoCommand { get; set; }
        public Observable<Visibility> NoVisibility { get; set; }
        public RxCommand OKCommand { get; set; }
        public Observable<Visibility> OKVisibility { get; set; }
        public Observable<Visibility> QuestionVisibility { get; set; }
        public Observable<MessageBoxResult> Result { get; set; }
        public Observable<Visibility> WarningVisibility { get; set; }
        public RxCommand YesCommand { get; set; }
        public Observable<Visibility> YesVisibility { get; set; }

        public MetroMessage(string message, MessageBoxButton button, MessageBoxImage icon = MessageBoxImage.Information)
        {
            InitializeComponent();

            DataContext = this;

            OKVisibility = new Observable<Visibility>(Visibility.Collapsed);
            CancelVisibility = new Observable<Visibility>(Visibility.Collapsed);
            YesVisibility = new Observable<Visibility>(Visibility.Collapsed);
            NoVisibility = new Observable<Visibility>(Visibility.Collapsed);

            QuestionVisibility = new Observable<Visibility>(Visibility.Collapsed);
            InformationVisibility = new Observable<Visibility>(Visibility.Collapsed);
            ErrorVisibility = new Observable<Visibility>(Visibility.Collapsed);
            WarningVisibility = new Observable<Visibility>(Visibility.Collapsed);

            OKCommand = new RxCommand();
            CancelCommand = new RxCommand();
            YesCommand = new RxCommand();
            NoCommand = new RxCommand();

            Result = new Observable<MessageBoxResult>();
            Caption = new Observable<string>(MVVMConfiguration.Configuration.MessageBoxTitle);
            Message = new Observable<string>(message);

            OKCommand.Subscribe(x =>
            {
                Result.Value = MessageBoxResult.OK;
                DialogResult = true;
            });

            CancelCommand.Subscribe(x =>
            {
                Result.Value = MessageBoxResult.Cancel;
                DialogResult = true;
            });

            YesCommand.Subscribe(x =>
            {
                Result.Value = MessageBoxResult.Yes;
                DialogResult = true;
            });

            NoCommand.Subscribe(x =>
            {
                Result.Value = MessageBoxResult.No;
                DialogResult = true;
            });



            switch (button)
            {
                case MessageBoxButton.OK:
                    OKVisibility.Value = Visibility.Visible;
                    CancelVisibility.Value = Visibility.Collapsed;
                    YesVisibility.Value = Visibility.Collapsed;
                    NoVisibility.Value = Visibility.Collapsed;
                    break;
                case MessageBoxButton.OKCancel:
                    OKVisibility.Value = Visibility.Visible;
                    CancelVisibility.Value = Visibility.Visible;
                    YesVisibility.Value = Visibility.Collapsed;
                    NoVisibility.Value = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNoCancel:
                    OKVisibility.Value = Visibility.Collapsed;
                    CancelVisibility.Value = Visibility.Visible;
                    YesVisibility.Value = Visibility.Visible;
                    NoVisibility.Value = Visibility.Visible;
                    break;
                case MessageBoxButton.YesNo:
                    OKVisibility.Value = Visibility.Collapsed;
                    CancelVisibility.Value = Visibility.Collapsed;
                    YesVisibility.Value = Visibility.Visible;
                    NoVisibility.Value = Visibility.Visible;
                    break;
                default:
                    break;
            }

            switch (icon)
            {
                case MessageBoxImage.Error:
                    QuestionVisibility.Value = Visibility.Collapsed;
                    InformationVisibility.Value = Visibility.Collapsed;
                    ErrorVisibility.Value = Visibility.Visible;
                    WarningVisibility.Value = Visibility.Collapsed;
                    break;
                case MessageBoxImage.Question:
                    QuestionVisibility.Value = Visibility.Visible;
                    InformationVisibility.Value = Visibility.Collapsed;
                    ErrorVisibility.Value = Visibility.Collapsed;
                    WarningVisibility.Value = Visibility.Collapsed;
                    break;
                case MessageBoxImage.Warning:
                    QuestionVisibility.Value = Visibility.Collapsed;
                    InformationVisibility.Value = Visibility.Collapsed;
                    ErrorVisibility.Value = Visibility.Collapsed;
                    WarningVisibility.Value = Visibility.Visible;
                    break;
                case MessageBoxImage.Information:
                    QuestionVisibility.Value = Visibility.Collapsed;
                    InformationVisibility.Value = Visibility.Visible;
                    ErrorVisibility.Value = Visibility.Collapsed;
                    WarningVisibility.Value = Visibility.Collapsed;
                    break;
                case MessageBoxImage.None:
                default:
                    QuestionVisibility.Value = Visibility.Collapsed;
                    InformationVisibility.Value = Visibility.Collapsed;
                    ErrorVisibility.Value = Visibility.Collapsed;
                    WarningVisibility.Value = Visibility.Collapsed;
                    break;
            }
        }

        public static MessageBoxResult Show(string message, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Information)
        {
            MetroMessage metroMessage = new MetroMessage(message, button, icon);
            metroMessage.ShowDialog();

            return metroMessage.Result.Value;
        }
    }
}
