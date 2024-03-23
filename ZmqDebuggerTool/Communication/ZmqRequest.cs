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
    public class ZmqRequest:ZmqBase
    {
        private RequestSocket? _reqSocket;

        public ZmqRequest()
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
            _reqSocket.ReceiveReady += _reqSocket_ReceiveReady;
            _reqSocket.Connect(address);
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
                if(_reqSocket.TryReceiveFrameBytes(TimeSpan.FromMilliseconds(5000),out rec))
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

        public string? Address => _address;
    }
}
