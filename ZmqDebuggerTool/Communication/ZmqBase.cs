using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmqDebuggerTool.Communication
{
    public abstract class ZmqBase
    {
        public event Action<byte[]>? OnDataReceived;
        protected string? _address;

        public ZmqBase() { }

        public virtual void BindOrConnect(string address)
        {
            _address= address; 
        }

        public virtual void SendBytes(byte[] data) 
        { }
        
        public virtual void SendString(string data) 
        { }

        protected void DataReceived(byte[] data)
        {
            OnDataReceived?.Invoke(data);
        }
    }
}
