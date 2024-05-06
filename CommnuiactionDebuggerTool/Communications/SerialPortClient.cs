using CommnuiactionDebuggerTool.Base;
using CommnuiactionDebuggerTool.Views;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CommnuiactionDebuggerTool.Communications
{
    public class SerialPortItem
    {
        public string SerialPortName { get; set; }

        public int BaudRate { get; set; }

        public int DataBits { get; set; }

        public StopBits StopBits { get; set; }

        public Parity Parity { get; set; }
    }

    public class SerialPortClient : CommunicationBase
    {
        private SerialPort _serialPort;
        public SerialPortClient()
        {
            _serialPort = new SerialPort();
            _serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[_serialPort.BytesToRead];
            _serialPort.Read(buffer, 0, buffer.Length);
            DataReceived(buffer);
        }

        public override void BindOrConnect()
        {
            SerialPortItem item = _view.SerialPortItem;
            _serialPort.PortName = item.SerialPortName;
            _serialPort.BaudRate=item.BaudRate;
            _serialPort.DataBits= item.DataBits; 
            _serialPort.StopBits= item.StopBits;
            _serialPort.Parity= item.Parity;
            _serialPort.Open();
            InitManager.GetInstance().SaveSection(Name, "PortName", item.SerialPortName);
            InitManager.GetInstance().SaveSection(Name, "BaudRate", item.BaudRate.ToString());
            InitManager.GetInstance().SaveSection(Name, "DataBits", item.DataBits.ToString());
            InitManager.GetInstance().SaveSection(Name, "StopBits", item.StopBits.ToString());
            InitManager.GetInstance().SaveSection(Name, "Parity", item.Parity.ToString());
        }

        public override void DisConnect()
        {
            _serialPort.Close();
        }

        SerialPortConfigView _view;

        public override UserControl GetConfigView()
        {
            if (_view == null)
            {
                SerialPortConfigView vi = new SerialPortConfigView();
                SerialPortItem info=new SerialPortItem();
                InitManager mgr=InitManager.GetInstance();
                info.SerialPortName= InitManager.GetInstance().GetSection(Name, "PortName");
                info.BaudRate= int.Parse(InitManager.GetInstance().GetSection(Name, "BaudRate"));
                info.DataBits= int.Parse(InitManager.GetInstance().GetSection(Name, "DataBits"));
                info.StopBits = (StopBits)Enum.Parse(typeof(StopBits), mgr.GetSection(Name, "StopBits"));
                info.Parity = (Parity)Enum.Parse(typeof(Parity), mgr.GetSection(Name, "Parity"));
                //info.StopBits= int.Parse(InitManager.GetInstance().GetSection(Name, "StopBits"));
                vi.SerialPortItem=info;
                _view = vi;
            }
            return _view;
        }
    }
}
