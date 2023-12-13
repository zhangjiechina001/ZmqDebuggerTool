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

namespace ZmqDebuggerTool.View
{
    /// <summary>
    /// ZmqPublisherView.xaml 的交互逻辑
    /// </summary>
    public partial class ZmqPublisherView : UserControl
    {
        ZmqPublisher _publisher = new ZmqPublisher();
        public ZmqPublisherView()
        {
            InitializeComponent();
            string command = "120 156 99 96 0 3 190 192 99 145 236 185 222 19 207 51 232 129 32 0 38 145 4 40\r\n120 156 99 96 96 96 116 16 97 0 3 0 2 161 0 86\r\n120 156 99 96 96 96 116 48 17 124 221 42 183 35 8 0 12 174 3 31\r\n120 156 99 96 96 96 116 48 81 190 206 101 91 176 4 0 10 233 2 203\r\n120 156 99 96 96 96 116 48 49 61 212 31 163 241 13 0 13 31 3 118\r\n120 156 99 96 0 3 174 82 141 36 243 128 152 236 164 255 140 0 19 136 3 186\r\n120 156 99 96 0 3 174 82 141 36 243 128 152 236 164 255 140 0 19 136 3 186\r\n120 156 99 96 0 3 129 236 228 186 203 233 241 185 222 19 207 51 232 129 32 0 58 221 5 152\r\n120 156 99 96 0 3 190 116 157 108 197 92 239 137 231 235 46 167 199 255 103 4 0 44 22 6 93";
            List<string> cmds = command.Split("\r\n").ToList();
            cmbCmds.ItemsSource= cmds;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            byte[] bytes = txtSend.Text.Split(" ").Select(t => byte.Parse(t)).ToArray();
            _publisher.Publish(bytes);
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            _publisher.ReInit(txtAddress.Text);
        }

        private void cmbCmds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSend.Text=cmbCmds.SelectedItem.ToString();
        }
    }
}
