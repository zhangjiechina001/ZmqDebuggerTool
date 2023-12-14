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
using ZmqDebuggerTool.Communication;
using ZmqDebuggerTool.Config;
using ZmqDebuggerTool.Utils;

namespace ZmqDebuggerTool.View
{
    /// <summary>
    /// ZmqSubscriberView.xaml 的交互逻辑
    /// </summary>
    public partial class ZmqSubscriberView : UserControl
    {
        ZmqSubscriber _zmqSubscriber=new ZmqSubscriber();

        public ZmqSubscriberView()
        {
            InitializeComponent();
            _zmqSubscriber.OnDataReceive += ZmqSubscriber_OnDataReceive;
        }

        private void ZmqSubscriber_OnDataReceive(byte[] obj)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                if (chbSubscriber.IsChecked == false)
                {
                    return;
                }
                if (chbCompress.IsChecked == true)
                {
                    obj = ZlibUtils.UnCompressWrap(obj);
                }
                string result = string.Join(" ", obj);
                txtRec.AppendText(result);
                txtRec.AppendText(Environment.NewLine);
                if (txtRec.LineCount > 1000)
                {
                    txtRec.Clear();
                }
            }));
        }

        public JsonConfig Configuration { get; set; }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            _zmqSubscriber.ReInit(txtAddress.Text);
            btnConnect.IsEnabled = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtAddress.Text = Configuration.GetSectionString("SubscriberAddress");
        }

        private void cmbCmds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnConnect.IsEnabled = true;
        }
    }
}
