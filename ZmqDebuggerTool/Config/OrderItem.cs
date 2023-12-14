using Newtonsoft.Json.Linq;
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

        public OrderItem(string title, JToken msg)
        {
            Title = title;
            Message = msg;
        }

        public string Title { get; set; }

        public JToken Message { get; set; }

        public static List<OrderItem> Parse(JToken token)
        {
            var arr=token.ToArray();    
            List<OrderItem> result=new List<OrderItem>();
            foreach (var item in arr)
            {
                JObject objItem = item.ToObject<JObject>();
                result.Add(new OrderItem(objItem.Value<string>("Title")!, objItem["Message"]!));
            }
            return result;
        }
    }
}
