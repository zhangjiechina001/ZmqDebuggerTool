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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ZmqDebuggerTool.Communication
{
    public class ZmqPublisher : CommunicationBase
    {
        public event Action<byte[]>? OnDataReceive;

        private PublisherSocket? _subSocket;
        public ZmqPublisher()
        {
        }

        public override void BindOrConnect()
        {
            _address = _view.IPPort;
            _subSocket?.Disconnect(_address); 
            _subSocket = new PublisherSocket();
            _subSocket.Bind(_address);
        }

        public override void SendBytes(byte[] data)
        {
            if (_subSocket != null)
            {
                _subSocket.SendFrame(data);
            }
        }

        public override void SendString(string data)
        {         
            if (_subSocket != null)
            {
                _subSocket.SendFrame(data);
            }
        }

        public override void DisConnect()
        {
            _subSocket?.Disconnect(_address);
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
