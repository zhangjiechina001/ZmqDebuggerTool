using CommnuiactionDebuggerTool;
using CommnuiactionDebuggerTool.Base;
using CommnuiactionDebuggerTool.Views;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ZmqDebuggerTool.Communication
{
    public class ZmqResponse : CommunicationBase
    {
        private ResponseSocket? _server;

        public ZmqResponse()
        {
            _ = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1);
                    if (_server != null && _server.HasIn)
                    {
                        byte[] data;
                        if (_server.TryReceiveFrameBytes(TimeSpan.FromMilliseconds(500), out data))
                        {
                            _server.SendFrame("receive data");
                            DataReceived(data);
                        }
                    }
                }
            });
        }

        public override void BindOrConnect()
        {
            _address = _view.IPPort ;
            _server?.Dispose();
            _server = new ResponseSocket();
            _server.Bind(_address);
        }

        public override void DisConnect()
        {
            _server.Disconnect(_address);
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
