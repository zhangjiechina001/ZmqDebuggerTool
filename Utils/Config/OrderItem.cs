using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Utils.Config
{
    public class OrderItem
    {
        public OrderItem(string title,string msg)
        {
            Title = title;
            Message = msg;
        }

        public OrderItem(string title, JsonNode msg)
        {
            Title = title;
            Message = msg;
        }

        public string Title { get; set; }

        public JsonNode Message { get; set; }

        public static List<OrderItem> Parse(JsonNode token)
        {
            var arr=token.AsArray();
            List<OrderItem> result=new List<OrderItem>();
            foreach (var item in arr)
            {
                JsonObject objItem = item.AsObject();
                result.Add(new OrderItem(objItem["Title"].ToString(), objItem["Message"]!));
            }
            return result;
        }
    }
}
