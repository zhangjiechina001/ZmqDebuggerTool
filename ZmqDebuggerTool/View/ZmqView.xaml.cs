using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Unicode;
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
using ZmqDebuggerTool.ViewModel;

namespace ZmqDebuggerTool.View
{
    /// <summary>
    /// ZmqPublisherView.xaml 的交互逻辑
    /// </summary>
    public partial class ZmqView : UserControl
    {
        //ZmqPublisher _publisher = new ZmqPublisher();
        public ZmqView()
        {
            InitializeComponent();
        }

        public void SetDataContext(ZmqViewModel vm)
        {
            this.DataContext = vm;
            vm.Zmq.OnDataReceived += Zmq_OnDataReceived;
        }

        private void Zmq_OnDataReceived(byte[] obj)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                string content= GetFormatInfomation(obj);
                if(IsSubscribe(content,txtTopic.Text))
                {
                    txtRec.AppendText(content);
                    txtRec.AppendText(Environment.NewLine);
                    if (txtRec.LineCount > 10000)
                    {
                        txtRec.Clear();
                    }
                    txtRec.ScrollToEnd();
                }
            }));
        }

        private string GetFormatInfomation(byte[] obj)
        {
            if(radRecText.IsChecked==true)
            {
                return Encoding.UTF8.GetString(obj);
            }
            else if(radRecHex.IsChecked==true)
            {
                return string.Join(" ", obj);
            }
            return obj.Length.ToString();
        }

        private bool IsSubscribe(string content,string topic)
        {
            if(topic==string.Empty)
            {
                return true;
            }

            string[] topicItems=topic.Split(" ");
            return topicItems.Any(t=>content.Contains(t));
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            btnConnect.IsEnabled = false;
        }

        private void cmbCmds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbCmds.SelectedItem is OrderItem orderItem)
            {
                txtSend.Text = orderItem.Message.ToString();
            }
        }

        private void txtAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnConnect.IsEnabled = true;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtRec.Clear();
        }
    }
}
