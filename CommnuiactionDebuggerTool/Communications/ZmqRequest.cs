using CommnuiactionDebuggerTool;
using CommnuiactionDebuggerTool.Base;
using CommnuiactionDebuggerTool.Views;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZmqDebuggerTool.Communication
{
    public class ZmqRequest : CommunicationBase
    {
        private RequestSocket? _reqSocket;

        public ZmqRequest()
        {
        }

        public override void BindOrConnect()
        {
            _address = _view.IPPort;
            _reqSocket?.Dispose();
            _reqSocket = new RequestSocket();
            _reqSocket.ReceiveReady += _reqSocket_ReceiveReady;
            _reqSocket.Connect(_address);
        }

        public override void SendBytes(byte[] data)
        {
            if (_reqSocket != null)
            {
                _reqSocket.SendFrame(data);
            }
        }

        public override void SendString(string data)
        {
            if (_reqSocket != null)
            {
                _reqSocket.SendFrame(data);
                byte[] rec;
                if(_reqSocket.TryReceiveFrameBytes(TimeSpan.FromMilliseconds(1000),out rec))
                {
                    DataReceived(rec);
                }
                else
                {
                    string msg = "time out";
                    DataReceived(Encoding.UTF8.GetBytes(msg));
                }
            }
        }

        private void _reqSocket_ReceiveReady(object? sender, NetMQSocketEventArgs e)
        {
            DataReceived(_reqSocket.ReceiveFrameBytes());
        }

        public override void DisConnect()
        {
            _reqSocket?.Disconnect(_address);
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
