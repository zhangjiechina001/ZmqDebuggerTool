using CommnuiactionDebuggerTool.Base;
using IniFileParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommnuiactionDebuggerTool.Views
{
    /// <summary>
    /// ScoketConfigView.xaml 的交互逻辑
    /// </summary>
    public partial class SocketConfigView : UserControl
    {
        CommunicationBase _comm;
        public SocketConfigView()
        {
            InitializeComponent();
        }

        private string _sectionName;
        public void SetConfig(IniData initData,string sectionName)
        {
            _sectionName = sectionName;
            txtAddress.Text = initData[sectionName]["Address"];
        }

        public JsonObject GetCommunicationParam()
        {
            JsonObject obj = new JsonObject()
            {
                { "Address",txtAddress.Text }
            };

            return obj;
        }

        public string IPPort 
        {
            get { return txtAddress.Text; }
            set { txtAddress.Text = value; }
        }


        public void SetCommunicationParam(JsonObject param)
        {
            txtAddress.Text = param["Address"].ToString();
        }
    }
}
