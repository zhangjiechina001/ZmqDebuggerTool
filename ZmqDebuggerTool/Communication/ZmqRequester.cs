using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ZmqDebuggerTool.Communication
{
    public class ZmqRequester:ZmqBase
    {
        private RequestSocket? _reqSocket;

        public ZmqRequester()
        {
        }

        public void ReInit(string address)
        {

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

        public override void BindOrConnect(string address)
        {
            _address = address;
            _reqSocket?.Dispose();
            _reqSocket = new RequestSocket();
            _reqSocket.Connect(address);
        }

        public override void SendBytes(byte[] data)
        {
            if (_reqSocket != null)
            {
                _reqSocket.SendFrame(data);
                DataReceived(_reqSocket.ReceiveFrameBytes());
            }
        }

        public override void SendString(string data)
        {
            if (_reqSocket != null)
            {
                _reqSocket.SendFrame(data);
                DataReceived(_reqSocket.ReceiveFrameBytes());
            }
        }

        public string? Address => _address;
    }
}
