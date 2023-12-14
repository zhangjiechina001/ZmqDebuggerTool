using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Windows;
using System.Windows.Controls;
using ZmqDebuggerTool.Communication;
using ZmqDebuggerTool.Config;

namespace ZmqDebuggerTool.View
{
    /// <summary>
    /// ZmqRequestView.xaml 的交互逻辑
    /// </summary>
    public partial class ZmqRequestView : UserControl
    {
        ZmqRequester _req = new ZmqRequester();
        JObject _obj;
        public ZmqRequestView()
        {
            InitializeComponent();
        }

        public JsonConfig Configuration {get;set;}

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            _req.ReInit(txtAddress.Text);     
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //txtAddress.Text = Configuration.GetSectionString("RequestAddress");

            //var obj = Configuration.GetSectionObj("Commands");
            //_obj = obj;
            //cmbCmds.Items.Clear();
            //foreach (var item in obj)
            //{
            //    cmbCmds.Items.Add(item.Key);
            //}
        }

        private void cmbCmds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count==0)
            {
                return;
            }

            string key = e.AddedItems[0].ToString();
            if (key!=string.Empty)
            {
                txtSend.Text = _obj[key].ToString();
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            txtRec.AppendText(_req.Send(txtSend.Text));
            txtRec.AppendText(Environment.NewLine);
        }
    }
}
