using CommnuiactionDebuggerTool.Base;
using IniFileParser.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
using System.Windows.Threading;
using Utils.Config;

namespace CommnuiactionDebuggerTool.Views
{
    /// <summary>
    /// CommunicationView.xaml 的交互逻辑
    /// </summary>
    public partial class CommunicationView : UserControl
    {
        private readonly CommunicationBase _comm;
        private readonly UserControl _configView;
        private readonly DispatcherTimer _cycleTimer;
        public CommunicationView(CommunicationBase comm)
        {
            InitializeComponent();
            _comm = comm;
            _configView = comm.GetConfigView();
            SetConnectView(_configView);
            TabHeader = comm.GetCommunicationType();
            comm.OnDataReceived += Zmq_OnDataReceived;
            chbSendHex.IsChecked = true;
            _cycleTimer = new DispatcherTimer();
            _cycleTimer.Tick += Timer_Tick;
            IniData data=IniConfig.ReadInit("Configuration.ini");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(chbTimer.IsChecked == true)
            {
                btnSend_Click(null, null);
            }
        }

        public string TabHeader { get; set; }

        private void Zmq_OnDataReceived(object sender,byte[] obj)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                AddLine("接收", obj);
            }));
        }

        private void AddLine(string recType, byte[] data)
        {
            string content = GetFormatInfomation(data);
            if(IsSubscribe(content, txtTopic.Text))
            {
                string line = string.Format("{0}:{1}\r\n{2}", recType, DateTime.Now, content);
                txtRec.AppendText(line);
                txtRec.AppendText(Environment.NewLine);
            }
            txtRec.ScrollToEnd();
        }

        private string GetFormatInfomation(byte[] obj)
        {
            if (radRecText.IsChecked == true)
            {
                return Encoding.UTF8.GetString(obj);
            }
            else if (radRecHex.IsChecked == true)
            {
                return string.Join(" ", obj);
            }
            return obj.Length.ToString();
        }

        private bool IsSubscribe(string content, string topic)
        {
            if (topic == string.Empty)
            {
                return true;
            }

            string[] topicItems = topic.Split(" ");
            return topicItems.Any(t => content.Contains(t));
        }

        public void SetConnectView(object control)
        {
            if(control is UIElement ui)
            {
                spConfig.Children.Insert(0, ui);
            }
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JsonObject obj = (_configView as IConfigView).GetCommunicationParam();
                _comm.BindOrConnect(obj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string _sendMsg = txtSend.Text;
            if (chbSendHex.IsChecked == true)
            {
                try
                {
                    byte[] data = _sendMsg.Split(" ").Select(t => byte.Parse(t)).ToArray();
                    _comm.SendBytes(data);
                    AddLine("发送", data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if(chbSendText.IsChecked == true)
            {
                _comm.SendString(_sendMsg);
            }
        }

        private void btnCrc_Click(object sender, RoutedEventArgs e)
        {
            string _sendMsg = txtSend.Text;
            if (chbSendHex.IsChecked == true)
            {
                try
                {
                    byte[] data = _sendMsg.Split(" ").Select(t => Convert.ToByte(t,16)).ToArray();
                    byte[] crc=BitConverter.GetBytes(CRC.CRC16_Check(data));
                    List<byte> totalData=new List<byte>();
                    totalData.AddRange(data);
                    totalData.AddRange(crc);
                    txtSend.Text = BitConverter.ToString(totalData.ToArray()).Replace("-"," ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void chbTimer_Checked(object sender, RoutedEventArgs e)
        {
            if(chbTimer.IsChecked == true)
            {
                int ts = int.Parse(txtTimsSpan.Text);
                _cycleTimer.Interval = TimeSpan.FromMilliseconds(ts);
                _cycleTimer.Start();
            }
            else
            {
                _cycleTimer.Stop();
            }
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtRec.Clear();
        }
    }
}
