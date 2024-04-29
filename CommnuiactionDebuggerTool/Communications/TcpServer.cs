﻿using CommnuiactionDebuggerTool.Base;
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
            return new SocketConfigView();
        }

        public override void BindOrConnect(JsonObject commParam)
        {
            string[] parts = commParam["Address"].ToString().Split(':');
            string ipAddress = parts[0];
            string port = parts[1];
            IPAddress iPAddress = IPAddress.Parse(ipAddress);
            int _port=int.Parse(port);
            _server.Start(iPAddress, _port);
        }

        public override void SendBytes(byte[] data)
        {
            base.SendBytes(data);
            _server.Broadcast(data);
        }

        public override void SendString(string data)
        {
            base.SendString(data);
            _server.Broadcast(data);
        }

        public override string GetCommunicationType()
        {
            return "TCP服务端";
        }
    }
}
