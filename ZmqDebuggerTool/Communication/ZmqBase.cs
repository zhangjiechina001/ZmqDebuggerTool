using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmqDebuggerTool.Communication
{
    public class ZmqBase
    {
        public event Action<byte[]>? OnDataReceive;
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
    }
}
