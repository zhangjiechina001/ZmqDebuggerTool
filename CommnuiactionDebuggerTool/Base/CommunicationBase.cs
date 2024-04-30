using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CommnuiactionDebuggerTool.Base
{
    public delegate void CommunicationDataReceivedEventHandler(object sender, byte[] data);
    public abstract class CommunicationBase
    {
        public event CommunicationDataReceivedEventHandler OnDataReceived;
        protected string _address;

        public CommunicationBase()
        {
        }

        public string Name { get; set; }

        public abstract void BindOrConnect(JsonObject commParam);

        public abstract void BindOrConnect();

        public abstract void DisConnect();

        public abstract UserControl GetConfigView();
  
        public virtual void SendBytes(byte[] data)
        { }

        public virtual void SendString(string data)
        { }

        protected void DataReceived(byte[] data)
        {
            OnDataReceived?.Invoke(this, data);
        }
    }
}
