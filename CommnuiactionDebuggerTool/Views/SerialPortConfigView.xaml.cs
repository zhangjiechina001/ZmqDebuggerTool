using CommnuiactionDebuggerTool.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// SerialPortConfigView.xaml 的交互逻辑
    /// </summary>
    public partial class SerialPortConfigView : UserControl
    {
        public SerialPortConfigView()
        {
            InitializeComponent();
        }

        public JsonObject GetCommunicationParam()
        {
            throw new NotImplementedException();
        }

        public void SetCommunicationParam(JsonObject param)
        {
            throw new NotImplementedException();
        }
    }
}
