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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Utils.Config;
using Utils.Extend;

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
            _comm.OnDataReceived += Zmq_OnDataReceived;

            _configView = comm.GetConfigView();
            SetConnectView(_configView);
            TabHeader = comm.Name;
            _cycleTimer = new DispatcherTimer();
            _cycleTimer.Tick += Timer_Tick;
            _autoReply = new AutoReplyer(comm);
            cmbCmds.ItemsSource = _autoReply.SendItems;
            cmbCmds.DisplayMemberPath = nameof(OrderItem.Title);

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
                if(chbLength.IsChecked == true)
                {
                    string len=Encoding.UTF8.GetString(obj).Length.ToString();
                    AddLog("<<", len);
                }
                else
                {
                    AddLog("<<", obj);
                }
            }));

        }

        private void AddLog(string recType, byte[] data)
        {
            string content = chbIsHex.IsChecked == true? data.GetFormatString(true): Encoding.UTF8.GetString(data);
            AddLog(recType, content);
        }

        private void AddLog(string recType, string data)
        {
            string content = data;
            if (IsSubscribe(content, txtTopic.Text))
            {
                string head = string.Format("{0} {1}", recType, DateTime.Now.ToString("HH:mm:ss.fff"));
                AddLine(head,content,Brushes.Green);
            }
            txtRec.ScrollToEnd();
        }

        private void AddLine(string line, string line2,SolidColorBrush color)
        {
            // 创建一个新的段落
            Paragraph paragraph = new Paragraph();
            // 创建一个新的文本块
            Span span = new Span(new Run(line+Environment.NewLine));
            paragraph.Inlines.Add(span);

            Span span2 = new Span(new Run(line2));
            span2.Foreground = color;
            paragraph.Inlines.Add(span2);

            // 将段落添加到 RichTextBox 中
            txtRec.Document.Blocks.Add(paragraph);
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
                _comm.BindOrConnect();
                btnConnect.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string _sendMsg = txtSend.Text;
            try
            {
                if (chbIsHex.IsChecked == true)
                {
                    byte[] data = _sendMsg.GetBytesFromLine(true);
                    _comm.SendBytes(data);
                    AddLog(">>", data);
                }
                else
                {
                    _comm.SendString(_sendMsg);
                    AddLog(">>", _sendMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCrc_Click(object sender, RoutedEventArgs e)
        {
            if(chbIsHex.IsChecked == false)
            {
                MessageBox.Show("请选择Hex发送！");
                return;
            }

            try
            {
                string _sendMsg = txtSend.Text;
                byte[] data = _sendMsg.Split(" ").Select(t => Convert.ToByte(t,16)).ToArray();
                byte[] crc=BitConverter.GetBytes(CRC.CRC16_Check(data));
                List<byte> totalData=new List<byte>();
                totalData.AddRange(data);
                totalData.AddRange(crc);
                txtSend.Text = totalData.ToArray().GetFormatString(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            txtRec.Document.Blocks.Clear();
        }

        private void btnDisConnect_Click(object sender, RoutedEventArgs e)
        {
            _comm.DisConnect();
            btnConnect.IsEnabled = true;
        }

        AutoReplyer _autoReply;
        private void chbIsReply_Checked(object sender, RoutedEventArgs e)
        {
            _autoReply.IsReply = chbIsReply.IsChecked.Value;
        }

        private void cmbCmds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbCmds.SelectedItem is OrderItem orderItem)
            {
                txtSend.Text = orderItem.Message.ToString();
            }
        }
    }
}
