using CommnuiactionDebuggerTool.Base;
using CommnuiactionDebuggerTool.Views;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CommnuiactionDebuggerTool.Communications
{
    public class TcpServer : CommunicationBase
    {
        private SimpleTcpServer _server;
        public TcpServer()
        {
            _server = new SimpleTcpServer();
            _server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, Message e)
        {
            DataReceived(e.Data);
        }

        public override UserControl GetConfigView()
        {
            return new ScoketConfigView();
        }

        public override void BindOrConnect(JsonObject commParam)
        {
            IPAddress iPAddress = IPAddress.Parse(commParam["address"].ToString());
            _server.Start(iPAddress, 25);
        }
    }
}
