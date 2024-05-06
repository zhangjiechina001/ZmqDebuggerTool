using CommnuiactionDebuggerTool.Base;
using CommnuiactionDebuggerTool.Communications;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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
using Utils.Extend;

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
            Init();
        }

        private void Init()
        {
            cmbPortName.ItemsSource = SerialPort.GetPortNames().ToList();
            cmbBaudRate.ItemsSource = new List<int> { 4800, 9600, 19200, 38400, 115200 };
            cmbDataBits.ItemsSource = new List<int> { 8,7,6,5};
            var stopBits = EnumExtend.GetEnumValues<StopBits>();
            stopBits.Remove(StopBits.None);
            cmbStopBits.ItemsSource = stopBits;
            cmbParity.ItemsSource = EnumExtend.GetEnumValues<Parity>();
        }

        public SerialPortItem SerialPortItem 
        {
            get { return GetSerialPortItem(); }
            set { SetView(value); }
        }
        public void SetView(SerialPortItem info)
        {
            cmbPortName.SelectedItem = info.SerialPortName;
            cmbBaudRate.SelectedItem= info.BaudRate;
            cmbDataBits.SelectedItem=info.DataBits;
            cmbStopBits.SelectedItem=info.StopBits;
            cmbParity.SelectedItem=info.Parity;
        }

        private SerialPortItem GetSerialPortItem()
        {
            SerialPortItem it=new SerialPortItem();
            it.SerialPortName = cmbPortName.SelectedItem as string;
            it.BaudRate = (int)cmbBaudRate.SelectedItem;
            it.DataBits=(int)cmbDataBits.SelectedItem;
            it.StopBits = (StopBits)cmbStopBits.SelectedItem;
            it.Parity= (Parity)cmbParity.SelectedItem;
            return it;
        }
    }
}
