using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Core.Socket.Client
{
    public class SocketClient
    {
        public TcpClient Client { get; set; }
        public IAsyncResult ConnectionResult { get; set; }
        public string Host { get; set; }
        public NetworkStream NetworkStream { get; set; }
        public int Port { get; set; }
        public IAsyncResult SendResult { get; set; }

        private SocketClient(string host, int port)
        {
            Client = new TcpClient(AddressFamily.InterNetwork);
            Host = host;
            Port = port;
        }

        public SocketClient Close()
        {
            WaitForConnectionEstablished();
            WaitForSendComplete();

            Client.Close();

            return this;
        }

        public SocketClient Connect(Action<IAsyncResult> connectionCallback = null)
        {
            try
            {
                ConnectionResult = Client.BeginConnect(Host, Port, connectionCallback == null ? null : new AsyncCallback(connectionCallback), null);

            }
            catch (SocketException exc)
            {
                Debug.WriteLine(exc.ToString());
                throw;
            }

            return this;
        }

        public static SocketClient Init(string host, int port)
        {
            SocketClient ret = new SocketClient(host, port);
            return ret;
        }

        public SocketClient Send(string data, Action<IAsyncResult> sendCallback = null)
        {
            WaitForConnectionEstablished();
            NetworkStream = Client.GetStream();
            byte[] buffer = System.Text.Encoding.Default.GetBytes(data);
            SendResult = NetworkStream.BeginWrite(buffer, 0, buffer.Length, sendCallback == null ? null : new AsyncCallback(sendCallback), null);

            return this;
        }

        public SocketClient WaitForConnectionEstablished()
        {
            if (!ConnectionResult.IsCompleted)
                Client.EndConnect(ConnectionResult);

            return this;
        }

        public SocketClient WaitForSendComplete()
        {
            if (!SendResult.IsCompleted)
                NetworkStream.EndWrite(SendResult);

            return this;
        }
    }
}

