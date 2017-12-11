using Core.MVVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Models
{
    public class SettingsModel
    {
        public Observable<string> FlashOperations { get; set; }
        public Observable<string> FlashOperationsDurationInSeconds { get; set; }
        public Observable<string> IpAddress { get; set; }
        public bool IsValid
        {
            get
            {
                return IpAddress.IsValid &&
                    LocalPath.IsValid &&
                    NeuLogPath.IsValid &&
                    NeuLogPort.IsValid &&
                    NeuLogReadMilliseconds.IsValid &&
                    NeuLogServer.IsValid &&
                    NeuLogVisibleSpan.IsValid &&
                    Port.IsValid &&
                    FlashOperations.IsValid &&
                    FlashOperationsDurationInSeconds.IsValid;
            }
        }
        public Observable<string> LocalPath { get; set; }
        public Observable<string> NeuLogPath { get; set; }
        public Observable<string> NeuLogPort { get; set; }
        public Observable<string> NeuLogReadMilliseconds { get; set; }
        public Observable<string> NeuLogServer { get; set; }
        public Observable<string> NeuLogVisibleSpan { get; set; }
        public Observable<string> Port { get; set; }

        public SettingsModel()
        {
            IpAddress = new Observable<string>();
            Port = new Observable<string>();
            LocalPath = new Observable<string>();
            NeuLogPath = new Observable<string>();
            NeuLogServer = new Observable<string>();
            NeuLogPort = new Observable<string>("22002");
            NeuLogVisibleSpan = new Observable<string>("00:00:30");
            NeuLogReadMilliseconds = new Observable<string>("4");
            FlashOperations = new Observable<string>("false");
            FlashOperationsDurationInSeconds = new Observable<string>("0");

            FlashOperations.SubscribeValidate(x =>
                {
                    return null;
                });

            IpAddress.SubscribeValidate(x =>
            {
                IPAddress result;
                if (!IPAddress.TryParse(x, out result))
                {
                    return "Geçersiz Ip Adresi.";
                }
                return null;
            });

            FlashOperationsDurationInSeconds.SubscribeValidate(x =>
            {
                int result;
                if (!int.TryParse(x, out result))
                {
                    return "Geçersiz süre";
                }
                else
                {
                    if (result < 0) return "Geçersiz süre";
                }

                return null;
            });

            Port.SubscribeValidate(x =>
            {
                int result;
                if (!int.TryParse(x, out result))
                {
                    return "Geçersiz port numarası";
                }
                else
                {
                    if (result <= 0) return "Geçersiz port numarası";
                }

                return null;
            });

            NeuLogPort.SubscribeValidate(x =>
            {
                int result;
                if (!int.TryParse(x, out result))
                {
                    return "Geçersiz port numarası";
                }
                else
                {
                    if (result <= 0) return "Geçersiz port numarası";
                }

                return null;
            });

            NeuLogReadMilliseconds.SubscribeValidate(x =>
            {
                int result;
                if (!int.TryParse(x, out result))
                {
                    return "Geçersiz değer";
                }
                else
                {
                    if (result <= 0) return "Geçersiz değer";
                }

                return null;
            });

            LocalPath.SubscribeValidate(x =>
            {
                if (!Directory.Exists(x))
                {
                    Directory.CreateDirectory(x);
                    return null;
                }

                return null;
            });

            NeuLogPath.SubscribeValidate(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return "Bu alan mutlaka dolu olmalıdır";
                    }

                    if (!File.Exists(x))
                    {
                        return "Belirtilen dizinde dosya bulunamadı";
                    }

                    return null;
                });

            NeuLogServer.SubscribeValidate(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return "Geçersiz sunucu adı";
                    }

                    return null;
                });

            NeuLogVisibleSpan.SubscribeValidate(x =>
                {
                    TimeSpan result;
                    if (!TimeSpan.TryParse(x, out result))
                    {
                        return "Geçersiz zaman aralığı, SS:dd:ss şeklinde verilmelidir";
                    }
                    return null;
                });
        }
    }
}
