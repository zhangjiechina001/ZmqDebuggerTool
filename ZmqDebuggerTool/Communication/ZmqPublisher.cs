using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZmqDebuggerTool.Communication
{
    public class ZmqPublisher : ZmqBase
    {
        public event Action<byte[]>? OnDataReceive;

        private PublisherSocket? _subSocket;
        private ManualResetEvent _mutex = new ManualResetEvent(true);
        public ZmqPublisher()
        {
        }

        public override void BindOrConnect(string address)
        {
            _subSocket?.Disconnect(_address);
            _subSocket = new PublisherSocket();
            _subSocket.Bind(address);
            _address = address;
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

        public string? Address => _address;
    }
}
