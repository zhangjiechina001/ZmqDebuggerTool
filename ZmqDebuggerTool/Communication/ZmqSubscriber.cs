using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Markup;
using NetMQ;
using NetMQ.Sockets;
using ZmqDebuggerTool.Utils;


namespace ZmqDebuggerTool.Communication
{
    public class ZmqSubscriber:ZmqBase
    {
        private SubscriberSocket? _subSocket;
        private string? _address;
        private ManualResetEvent _mutex =new ManualResetEvent(true);
        bool _isIniting = false;
        public ZmqSubscriber() 
        {
            _ = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1);
                    _mutex.WaitOne();
                    if (_subSocket != null && _subSocket.HasIn)
                    {
                        byte[] data;
                        if (_subSocket.TryReceiveFrameBytes(TimeSpan.FromMilliseconds(500),out data))
                        {
                            DataReceived(data);
                        }
                    }
                }
            });
        }

        public void ReInit(string address)
        {
            _isIniting = true;
            _subSocket?.Disconnect(_address);
            _subSocket = new SubscriberSocket();
            _subSocket.Connect(address);
            _subSocket.SubscribeToAnyTopic();
            _address = address;
            _isIniting = false;
        }

        public override void BindOrConnect(string address)
        {
            _isIniting = true;
            _subSocket?.Disconnect(_address);
            _subSocket = new SubscriberSocket();
            _subSocket.Connect(address);
            _subSocket.SubscribeToAnyTopic();
            _address = address;
            _isIniting = false;
        }

        public override void SendBytes(byte[] data)
        {
            base.SendBytes(data);
        }

        public override void SendString(string data)
        {
            base.SendString(data);
        }  
    }
}
