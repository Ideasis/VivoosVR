using Castle.Windsor;
using Core.MVVM;
using Core.MVVM.MessageBox;
using Vivos.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using DevExpress.Xpf.Charts;
using Vivos.Controls;
using System.Threading;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace Vivos.RawControl
{
    public class PulseControlViewModel : BaseControlViewModel, IPulseControlViewModel
    {
        public Observable<bool> IsRunning { get; set; }

        public IPulseControl PulseControl
        {
            get
            {
                return (IPulseControl)View.Value;
            }
        }
        public IObserverControlViewModel ObserverControlViewModel { get; set; }

        public ObservableCollection<PulseNode> PulseSeries { get; set; }
        public ObservableCollection<PulseNode> GSRSeries { get; set; }
        public Observable<string> GSRLegend { get; set; }
        public Observable<string> PulseLegend { get; set; }
        public Observable<int> MinValue { get; set; }
        public Observable<int> MaxValue { get; set; }



        public PulseControlViewModel(IWindsorContainer container, IPulseControl view, IObserverControlViewModel observerControlViewModel)
            : base(container, view, "Pals Monitörü")
        {
            PulseSeries = new ObservableCollection<PulseNode>();
            GSRSeries = new ObservableCollection<PulseNode>();
            PulseLegend = new Observable<string>(string.Format("Pulse (x{0})", Market.Client.Properties.Settings.Default.NeuLogPulseMultiplier));
            GSRLegend = new Observable<string>(string.Format("GSR (x{0})", Market.Client.Properties.Settings.Default.NeuLogGSRMultiplier));
            MinValue = new Observable<int>(Market.Client.Properties.Settings.Default.NeuLogMinValue);
            MaxValue = new Observable<int>(Market.Client.Properties.Settings.Default.NeuLogMaxValue);

            string neulogUrl = string.Format("http://{0}:{1}/NeuLogAPI?GetSensorValue:[GSR],[1],[Pulse],[1]",
                Market.Client.Properties.Settings.Default.NeuLogServer,
                Market.Client.Properties.Settings.Default.NeuLogPort);

            ObserverControlViewModel = observerControlViewModel;

            IsRunning = new Observable<bool>(false);
            TimeSpan readSpan = TimeSpan.FromMilliseconds(Market.Client.Properties.Settings.Default.NeuLogReadMilliseconds);

            var sync = SynchronizationContext.Current;

            var timer = Observable.Timer(readSpan, readSpan);
            IDisposable timerDisposable = timer.Subscribe(x =>
            {
                DateTime argument = DateTime.Now;
                TimeSpan visibleSpan = Market.Client.Properties.Settings.Default.NeuLogVisibleSpan;


                Task.Run(async () =>
                {
                    try
                    {
                        HttpWebRequest request = WebRequest.Create(neulogUrl) as HttpWebRequest;
                        using (HttpWebResponse response = (await request.GetResponseAsync()) as HttpWebResponse)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                StreamReader sr = new StreamReader(response.GetResponseStream());
                                string rawData = sr.ReadToEnd();

                                var splitted = rawData.Split(new char[] { '[', ',', ']' });

                                double gsr = double.Parse(splitted[1].Replace(".", ",")) * Market.Client.Properties.Settings.Default.NeuLogGSRMultiplier;
                                double pulse = double.Parse(splitted[2]) * Market.Client.Properties.Settings.Default.NeuLogPulseMultiplier;

                                sync.Send(z =>
                                {
                                    GSRSeries.Add(new PulseNode(argument, gsr));
                                    var GSRRange = GSRSeries.Where(y => y.Time < DateTime.Now - visibleSpan).ToList();
                                    GSRRange.ForEach(y => GSRSeries.Remove(y));

                                    PulseSeries.Add(new PulseNode(argument, pulse));
                                    var pulseRange = PulseSeries.Where(y => y.Time < DateTime.Now - visibleSpan).ToList();
                                    pulseRange.ForEach(y => PulseSeries.Remove(y));
                                }, null);
                            }
                        }
                    }
                    catch
                    {
                        //MessageHelper.Show("Sensör bilgilerinin okunması sırasında hata oluştu.")
                    }
                });
            });
        }
    }
}
