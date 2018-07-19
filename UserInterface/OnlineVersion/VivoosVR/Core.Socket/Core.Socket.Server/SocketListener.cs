using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Socket.Server
{
    public class SocketListener
    {
        private object _CacheLock = new object();

        public StringBuilder Cache { get; set; }
        public string Ip { get; set; }
        public TcpListener Listener { get; set; }
        public NetworkStream NetworkStream { get; set; }
        public int Port { get; set; }

        private SocketListener(string ip, int port)
        {
            Ip = ip;
            Port = port;
            IPAddress address = IPAddress.Parse(ip);
            Listener = new TcpListener(address, port);
            Cache = new StringBuilder();
        }

        public async Task BeginAccept(Action<string> resultCallback, CancellationTokenSource token)
        {
            while (!token.IsCancellationRequested)
            {
                var client = await Listener.AcceptTcpClientAsync();

                await Task.Run(async () =>
                {
                    while (client.Connected && !token.IsCancellationRequested)
                    {
                        await ReadToEnd(client, resultCallback);
                    }
                });
            }
        }

        public static SocketListener Init(string ip, int port)
        {
            SocketListener ret = new SocketListener(ip, port);
            return ret;
        }

        private async Task ReadToEnd(TcpClient client, Action<string> resultDelegate)
        {
            NetworkStream stream = client.GetStream();
            int readByte = 0;

            bool isReturnRead = false;

            while (stream.DataAvailable && !isReturnRead)
            {
                byte[] buffer = new byte[1000];
                readByte = stream.Read(buffer, 0, buffer.Length);
                if (readByte == 0) break;
                string strRead = System.Text.Encoding.Default.GetString(buffer, 0, readByte);
                Cache.Append(strRead);
                isReturnRead = strRead.EndsWith(Environment.NewLine);
            }

            if (isReturnRead)
            {
                await Task.Run(() =>
                {
                    resultDelegate(Cache.ToString());
                    lock (_CacheLock)
                    {
                        Cache = new StringBuilder();
                    }
                });
            }
        }

        public SocketListener Start(int queueLength = 100)
        {
            Listener.Start(queueLength);
            return this;
        }
    }
}
