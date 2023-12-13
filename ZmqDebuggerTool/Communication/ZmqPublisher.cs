using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmqDebuggerTool.Communication
{
    public class ZmqPublisher
    {
        public event Action<byte[]>? OnDataReceive;

        private PublisherSocket? _subSocket;
        private string? _address;
        private ManualResetEvent _mutex = new ManualResetEvent(true);
        public ZmqPublisher()
        {
        }

        public void Publish(string content)
        {
            if (_subSocket != null)
            {
                _subSocket.SendFrame(content);
            }
        }

        public void Publish(byte[] content)
        {
            if (_subSocket != null)
            {
                _subSocket.SendFrame(content);
            }
        }

        public void ReInit(string address)
        {
            _subSocket?.Disconnect(_address);
            _subSocket = new PublisherSocket();
            _subSocket.Bind(address);
            _address = address;
        }

        public string? Address => _address;
    }
}
