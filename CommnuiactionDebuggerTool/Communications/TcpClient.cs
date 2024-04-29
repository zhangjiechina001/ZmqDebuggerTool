using CommnuiactionDebuggerTool.Base;
using CommnuiactionDebuggerTool.Views;
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

        public override void BindOrConnect(JsonObject commParam)
        {
            string[] parts = commParam["Address"].ToString().Split(':');
            string ipAddress = parts[0];
            string port = parts[1];
            _client.Connect(ipAddress, int.Parse(port));
        }

        public override void SendBytes(byte[] data)
        {
            _client.Write(data);
        }

        public override void SendString(string data)
        {
            _client.Write(data);
        }

        public override string GetCommunicationType()
        {
            return "TCP客户端";
        }

        UserControl _view;
        public override UserControl GetConfigView()
        {
            if(_view == null )
            {
                _view = new SocketConfigView();
            }
            return _view;
        }
    }
}
