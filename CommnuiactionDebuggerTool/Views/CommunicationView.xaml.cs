using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// CommunicationView.xaml 的交互逻辑
    /// </summary>
    public partial class CommunicationView : UserControl
    {
        public CommunicationView(string tabHeader, object content)
        {
            InitializeComponent();
            SetConnectView(content);
            TabHeader = tabHeader;
        }

        public void SetConnectView(object control)
        {
            gbConnect.Content = control;
        }

        public string TabHeader { get; set; }
    }
}
