using Castle.Windsor;
using Core.MVVM;
using Core.MVVM.MessageBox;
using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vivos.Controls;
using Vivos.Windows;

namespace Vivos.Controls
{
    public class PulseControlViewModel : BaseControlViewModel, IPulseControlViewModel
    {
        public ObservableCollection<PulseNode> Series { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public IPulseControl PulseControl
        {
            get
            {
                return (IPulseControl)View.Value;
            }
        }
        //public ObservableCollection<PulseNode> PulseSeries { get; set; }
        public IDisposable TimerDisposable { get; set; }
        public Observable<string> FilePath { get; set; }
        public FileStream FileStream { get; set; }
        public StreamWriter StreamWriter { get; set; }

        public Observable<double> GSRAverage { get; set; }
        public Observable<double> PulseAverage { get; set; }

        string _Marker = "0";
        string _CommandText = "-";
        int _CommandStep = 0;
        string _SwitchText = "-";
        int _SwitchStep = 0;
        byte _SUD = 0;

        public void SetMarker(string marker)
        {
            _Marker = marker;
        }

        public void SetCommand(string command, int step)
        {
            _CommandText = command;
            _CommandStep = step;
        }

        public void SetSwitch(string @switch, int step)
        {
            _SwitchText = @switch;
            _SwitchStep = step;
        }

        public void SetSud(byte sud)
        {
            _SUD = sud;
        }

        public PulseControlViewModel(IWindsorContainer container, IPulseControl view)
                                    : base(container, view, "Pals Monitörü")
        {
            FilePath = new Observable<string>();
            Series = new ObservableCollection<PulseNode>();
            PulseAverage = new Observable<double>(0);
            GSRAverage = new Observable<double>(0);


            IsRunning = new Observable<bool>(false);

            Start();


            FilePath.Value = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".csv");
            FileStream = new FileStream(FilePath.Value, FileMode.CreateNew, FileAccess.Write, FileShare.Read, 10000, true);

            StreamWriter = new StreamWriter(FileStream, Encoding.Default);

            StreamWriter.WriteLine($"Zaman,Pulse,GSR,Marker,Komut,Komut Adımı,Durum,DurumAdımı,SUD");
            StreamWriter.FlushAsync();
        }

        public void Start()
        {
            string neulogUrl = string.Format("http://{0}:{1}/NeuLogAPI?GetSensorValue:[GSR],[1],[Pulse],[1]",
                Market.Client.Properties.Settings.Default.NeuLogServer,
                Market.Client.Properties.Settings.Default.NeuLogPort);


            TimeSpan readSpan = TimeSpan.FromMilliseconds(Market.Client.Properties.Settings.Default.NeuLogReadMilliseconds);

            var sync = SynchronizationContext.Current;

            var timer = Observable.Timer(readSpan, readSpan);
            TimerDisposable = timer.Subscribe(x =>
            {
                DateTime argument = DateTime.Now;
                TimeSpan visibleSpan = Market.Client.Properties.Settings.Default.NeuLogVisibleSpan;

                try
                {
                    HttpWebRequest request = WebRequest.Create(neulogUrl) as HttpWebRequest;
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            StreamReader sr = new StreamReader(response.GetResponseStream());
                            string rawData = sr.ReadToEnd();

                            var splitted = rawData.Split(new char[] { '[', ',', ']' });

                            string gsrString = splitted[1];
                            string pulseString = splitted[2];
                            double gsr = double.Parse(gsrString.Replace(".", ","));
                            double pulse = double.Parse(pulseString.Replace(".", ","));

                            sync.Send(z =>
                            {
                                PulseNode node = new PulseNode(argument, gsr, pulse, _Marker, _CommandText, _CommandStep, _SwitchText, _SwitchStep, _SUD);

                                Series.Add(node);
                                var GSRRange = Series.Where(y => y.Time < DateTime.Now - visibleSpan).ToList();
                                GSRRange.ForEach(y => Series.Remove(y));

                                _Marker = "0";
                                _CommandText = "-";
                                _CommandStep = 0;
                                _SwitchText = "-";
                                _SwitchStep = 0;
                                _SUD = 0;

                                Task.Run(() =>
                                {
                                    GSRAverage.Value = Series.Where(y => y.Time > DateTime.Now - visibleSpan).Select(y => y.GSR).Average();
                                    PulseAverage.Value = Series.Where(y => y.Time > DateTime.Now - visibleSpan).Select(y => y.Pulse).Average();
                                });

                                Task.Run(() =>
                                {
                                    if (StreamWriter != null)
                                    {
                                        StreamWriter.WriteLineAsync(node.ToString());
                                        StreamWriter.FlushAsync();
                                    }
                                });
                            }
                            , null);
                        }
                    }
                }
                catch
                {
                    //MessageHelper.Show("Sensör bilgilerinin okunması sırasında hata oluştu.")
                }
            });
        }

        public void Stop()
        {
            TimerDisposable.Dispose();

            StreamWriter.Flush();
            FileStream.Flush();

            StreamWriter.Close();
            FileStream.Close();

            StreamWriter = null;
            FileStream = null;

        }
    }
}
