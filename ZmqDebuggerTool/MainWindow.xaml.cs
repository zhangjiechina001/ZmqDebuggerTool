using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Text;
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

namespace ZmqDebuggerTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ZmqSubscriber _sub = new ZmqSubscriber();
        ZmqRequester _req = new ZmqRequester();
        JsonConfig _configuration;
        public MainWindow(JsonConfig configuration)
        {
            InitializeComponent();
            _configuration = configuration;
        }

        private void SubOnDataReceive(byte[] obj)
        {
            string result=Encoding.UTF8.GetString(obj);
            Dispatcher.Invoke(new Action(() =>
            {
                txtLog.AppendText(result);
                txtLog.AppendText(Environment.NewLine);
            }));
        }

        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
            _sub.ReInit(cmbAddress.Text);
        }

        private void btnSetSend_Click(object sender, RoutedEventArgs e)
        {
            _req.ReInit(cmbAddress.Text);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            _req.Send(txtSend.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sub.OnDataReceive += SubOnDataReceive;
            JObject obj = new JObject()
            {
                { "theme","measureControl"},
                { "param","startMeasure"}
            };
            txtSend.Text = obj.ToString();
            zmqView.Configuration= _configuration;
        }
    }
}