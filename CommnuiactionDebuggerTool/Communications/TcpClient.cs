using CommnuiactionDebuggerTool.Base;
using CommnuiactionDebuggerTool.Views;
using IniFileParser.Model;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Controls;
using Utils.Config;

namespace CommnuiactionDebuggerTool.Communications
{
    class TcpClient : CommunicationBase
    {
        private SimpleTcpClient _client;
        public TcpClient()
        {
            _client= new SimpleTcpClient();
            _client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, Message e)
        {
            base.DataReceived(e.Data);
        }

        public override void BindOrConnect()
        {
            string[] parts = _view.IPPort.Split(':');
            string ipAddress = parts[0];
            string port = parts[1];
            _client.TimeOut = TimeSpan.FromMilliseconds(1000);
            _client.Connect(ipAddress, int.Parse(port));
            InitManager.GetInstance().SaveSection(Name, "Address", _view.IPPort);
        }

        public override void DisConnect()
        {
            _client.Disconnect();
        }

        public override void SendBytes(byte[] data)
        {
            _client.Write(data);
        }

        public override void SendString(string data)
        {
            _client.Write(data);
        }

        SocketConfigView _view;
        public override UserControl GetConfigView()
        {
            if(_view == null )
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
