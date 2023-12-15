using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ZmqDebuggerTool.Communication
{
    public class ZmqResponse : ZmqBase
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

        public override void BindOrConnect(string address)
        {
            _address = address;
            _server?.Dispose();
            _server = new ResponseSocket();
            _server.Bind(address);
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
