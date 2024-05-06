using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Markup;
using CommnuiactionDebuggerTool;
using CommnuiactionDebuggerTool.Base;
using CommnuiactionDebuggerTool.Views;
using NetMQ;
using NetMQ.Sockets;


namespace ZmqDebuggerTool.Communication
{
    public class ZmqSubscriber:CommunicationBase
    {
        private SubscriberSocket? _subSocket;
        private string? _address;
        private ManualResetEvent _mutex =new ManualResetEvent(true);
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

        public override void BindOrConnect()
        {
            _address = _view.IPPort;
            _subSocket?.Disconnect(_address);
            _subSocket = new SubscriberSocket();
            _subSocket.Connect(_address);
            _subSocket.SubscribeToAnyTopic();
        }

        public override void SendBytes(byte[] data)
        {
            base.SendBytes(data);
        }

        public override void SendString(string data)
        {
            base.SendString(data);
        }

        public override void DisConnect()
        {
            _subSocket.Disconnect(_address);
        }

        SocketConfigView _view;
        public override UserControl GetConfigView()
        {
            if (_view == null)
            {
                SocketConfigView vi = new SocketConfigView();
                string address = InitManager.GetInstance().GetSection(Name, "Address");
                vi.IPPort = address;
                _view = vi;
            }
            return _view;
        }
    }
}
