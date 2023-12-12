using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmqDebuggerTool.Communication
{
    class ZmqRequester
    {
        public event Action<byte[]>? OnDataReceive;

        private RequestSocket? _reqSocket;
        private string? _address;

        public ZmqRequester()
        {
        }

        public void ReInit(string address)
        {
            _address = address;
            _reqSocket?.Dispose();
            _reqSocket = new RequestSocket();
            _reqSocket.Connect(address);
        }

        public string Send(string content)
        {
            if(_reqSocket != null)
            {
                _reqSocket.SendFrame(content);
                return _reqSocket.ReceiveFrameString();
            }
            return "_reqSocket is null";
        }

        public string? Address => _address;
    }
}
