using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NetMQ;
using NetMQ.Sockets;


namespace ZmqDebuggerTool.Communication
{
    public class ZmqSubscriber
    {
        public event Action<byte[]>? OnDataReceive;

        private SubscriberSocket? _subSocket;
        private string? _address;

        public ZmqSubscriber() 
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(10);
                    if (_subSocket != null)
                    {
                        byte[] data = _subSocket.ReceiveFrameBytes();
                        OnDataReceive?.Invoke(data);
                    }
                }
            });
        }

        public void ReInit(string address)
        {
            _address= address;
            _subSocket?.Dispose();
            _subSocket = new SubscriberSocket();
            _subSocket.Connect(address);
            _subSocket.SubscribeToAnyTopic();
        }

        public string? Address => _address;    
    }
}
