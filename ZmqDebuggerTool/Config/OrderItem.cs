using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmqDebuggerTool.Config
{
    public class OrderItem
    {
        public OrderItem(string title,string msg)
        {
            Title = title;
            Message = msg;
        }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}
