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
using ZmqDebuggerTool.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace ZmqDebuggerTool.View
{
    /// <summary>
    /// ZlibTestView.xaml 的交互逻辑
    /// </summary>
    public partial class ZlibTestView : UserControl
    {
        public ZlibTestView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte[] data = txt.Text.Split(" ").Select(t => byte.Parse(t)).ToArray();
                var dest = ZlibUtils.UnCompressWrap(data);
                txt.AppendText(Environment.NewLine);
                txt.AppendText(string.Join(" ", dest));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+ex.StackTrace);
            }

        }
    }
}
