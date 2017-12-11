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
using Core.Common.Model;
using Core.Common.Services;
using Market.Client.Models;
using Market.Client.Adapters;
using Core.Socket.Client;
using System.Threading;
using Vivos.RawControl;

namespace Vivos.Controls
{
    public class ObserverControlViewModel : BaseControlViewModel, IObserverControlViewModel
    {
        public Observable<bool> IsRunning { get; set; }
        public Observable<List<AssetCommandModel>> AssetCommands { get; set; }
        public Observable<List<AssetDefaultModel>> AssetDefaults { get; set; }
        public Observable<AssetModel> Asset { get; set; }
        public SocketClient SocketClient { get; set; }
        public RxCommand OnCommand { get; set; }
        public RxCommand OffCommand { get; set; }
        public RxCommand ActionCommand { get; set; }
        public Observable<IPulseControlViewModel> PulseControlViewModel { get; set; }

        public Observable<bool> IsConnectionOpen { get; set; }

        public ObserverControlViewModel(IWindsorContainer container, IObserverControl view)
            : base(container, view, "Oturum")
        {
            Asset = new Observable<AssetModel>();
            IsRunning = new Observable<bool>(false);
            AssetCommands = new Observable<List<AssetCommandModel>>(new List<AssetCommandModel>());
            AssetDefaults = new Observable<List<AssetDefaultModel>>(new List<AssetDefaultModel>());
            IsConnectionOpen = new Observable<bool>(false);
            OnCommand = new RxCommand();
            OffCommand = new RxCommand();
            ActionCommand = new RxCommand();
            PulseControlViewModel = new Observable<IPulseControlViewModel>();

            ControlLoadedCommand.Subscribe(x =>
            {
                PulseControlViewModel.Value = WindowViewModel.Value.GetContent<IPulseControlViewModel, IPulseControl>(false, new Arg("observerControlViewModel", this));
            });

            IsConnectionOpen.Subscribe(x =>
                {
                    if (x)
                    {
                        OnCommand.Subscribe(y =>
                        {
                            AssetDefaultModel defaultModel = (AssetDefaultModel)y;

                            SocketClient.Send(defaultModel.OnCommandText.Value + "\r");
                            SocketClient.WaitForSendComplete();
                        });

                        OffCommand.Subscribe(y =>
                        {
                            AssetDefaultModel defaultModel = (AssetDefaultModel)y;

                            SocketClient.Send(defaultModel.OffCommandText.Value + "\r");
                            SocketClient.WaitForSendComplete();
                        });

                        ActionCommand.Subscribe(y =>
                        {
                            AssetCommandModel commandModel = (AssetCommandModel)y;

                            SocketClient.Send(commandModel.CommandText.Value + "\r");
                            SocketClient.WaitForSendComplete();
                        });
                    }
                });



            Asset.Subscribe(x =>
                {
                    var resultCommands = Bootstrapper.ResolveSession().GetProxy<IPublicMarketService>().GetAssetCommands(x.Id.Value);
                    var resultDefaults = Bootstrapper.ResolveSession().GetProxy<IPublicMarketService>().GetAssetDefaults(x.Id.Value);

                    AssetCommandAdapter commandAdapter = new AssetCommandAdapter();
                    AssetDefaultAdapter defaultAdapter = new AssetDefaultAdapter();

                    resultCommands.ForEach(y =>
                    {
                        AssetCommands.Value.Add(commandAdapter.Convert(y));
                    });

                    resultDefaults.ForEach(y =>
                    {
                        AssetDefaults.Value.Add(defaultAdapter.Convert(y));
                    });

                    SocketClient = SocketClient.Init(Market.Client.Properties.Settings.Default.IPAddress, Market.Client.Properties.Settings.Default.Port);

                    for (int i = 0; i < 10; i++)
                    {
                        Thread.CurrentThread.Join(1000);
                        try
                        {
                            SocketClient.Connect();
                            SocketClient.WaitForConnectionEstablished();
                            IsConnectionOpen.Value = true;
                            break;
                        }
                        catch (Exception)
                        {
                            IsConnectionOpen.Value = false;
                        }
                    }

                });



        }
    }
}
