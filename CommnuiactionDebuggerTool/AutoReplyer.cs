using CommnuiactionDebuggerTool.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Utils.Config;
using Utils.Extend;

namespace CommnuiactionDebuggerTool
{
    public class AutoReplyer
    {
        CommunicationBase _comm;
        List<OrderItem> _items;
        public AutoReplyer(CommunicationBase comm)
        {
            _comm= comm;
            _comm.OnDataReceived += Comm_OnDataReceived;
            string fileName = "./Config/"+comm.Name + ".json";
            
            JsonConfig cfg= new JsonConfig(fileName);
            JsonNode nodes=cfg.GetSectionToken("Reply");
            _items=OrderItem.Parse(nodes);
        }

        public bool IsReply { get; set; } = false;

        private void Comm_OnDataReceived(object sender, byte[] data)
        {
            if(IsReply)
            {
                string msg = data.GetFormatString(true);
                var matchItems = _items.Where(t => t.Message["Receive"].ToString() == msg).ToList();
                if (matchItems.Count > 0)
                {
                    string reply = matchItems.First().Message["Reply"].ToString();
                    Thread.Sleep(10);
                    _comm.SendBytes(reply.GetBytesFromLine(true));
                }
            }
        }
    }
}
