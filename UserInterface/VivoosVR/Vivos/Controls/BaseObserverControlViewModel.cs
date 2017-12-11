using Castle.Windsor;
using Core.Client;
using Core.Common.Services;
using Core.MVVM;
using Core.Socket.Client;
using Market.Client.Adapters;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public abstract class BaseObserverControlViewModel : BaseControlViewModel
    {
        public RxCommand ActionCommand { get; set; }
        public Observable<List<AssetCommandModel>> AssetCommands { get; set; }
        public Observable<List<AssetDefaultModel>> AssetDefaults { get; set; }
        public Observable<AssetModel> AssetModel { get; set; }
        public Observable<bool> IsConnectionOpen { get; set; }
        public Observable<bool> IsRunning { get; set; }
        public RxCommand OffCommand { get; set; }
        public RxCommand OnCommand { get; set; }
        public Observable<IPulseControlViewModel> PulseControlViewModel { get; set; }
        public SocketClient SocketClient { get; set; }

        public BaseObserverControlViewModel(IWindsorContainer container, IObserverControl view)
                            : base(container, view, "Oturum")
        {
            IsRunning = new Observable<bool>(false);
            AssetCommands = new Observable<List<AssetCommandModel>>(new List<AssetCommandModel>());
            AssetDefaults = new Observable<List<AssetDefaultModel>>(new List<AssetDefaultModel>());

            AssetModel = new Observable<AssetModel>();
            IsConnectionOpen = new Observable<bool>(false);
            OnCommand = new RxCommand();
            OffCommand = new RxCommand();
            ActionCommand = new RxCommand();
            PulseControlViewModel = new Observable<IPulseControlViewModel>();

            ControlLoadedCommand.Subscribe(x =>
            {
                PulseControlViewModel.Value = WindowViewModel.Value.GetContent<IPulseControlViewModel, IPulseControl>(false);
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

                        PulseControlViewModel.Value.SetCommand($"[{defaultModel.OnCommandName}] - {defaultModel.Description.Value}", defaultModel.Step.Value);
                    });

                    OffCommand.Subscribe(y =>
                    {
                        AssetDefaultModel defaultModel = (AssetDefaultModel)y;

                        SocketClient.Send(defaultModel.OffCommandText.Value + "\r");
                        SocketClient.WaitForSendComplete();

                        PulseControlViewModel.Value.SetCommand($"[{defaultModel.OffCommandName}] - {defaultModel.Description.Value}", defaultModel.Step.Value);

                    });

                    ActionCommand.Subscribe(y =>
                    {
                        AssetCommandModel commandModel = (AssetCommandModel)y;

                        SocketClient.Send(commandModel.CommandText.Value + "\r");
                        SocketClient.WaitForSendComplete();

                        PulseControlViewModel.Value.SetCommand(commandModel.Description.Value, commandModel.Step.Value);
                    });
                }
            });

            AssetModel.Subscribe(x =>
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
