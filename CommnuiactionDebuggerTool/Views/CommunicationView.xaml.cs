using CommnuiactionDebuggerTool.Base;
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
        public CommunicationView(CommunicationBase comm)
        {
            InitializeComponent();
            SetConnectView(comm.GetConfigView());
            TabHeader = comm.GetCommunicationType();
        }

        public void SetConnectView(object control)
        {
            if(control is UIElement ui)
            {
                spConfig.Children.Insert(0, ui);
            }
        }

        public string TabHeader { get; set; }
    }
}
