using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TcpUdpTool.ViewModel.Base;
using ZmqDebuggerTool.Communication;
using ZmqDebuggerTool.Config;
using ZmqDebuggerTool.ViewModel.Base;

namespace ZmqDebuggerTool.ViewModel
{
    public class ZmqViewModel : ObservableObject
    {
        private readonly ZmqBase _zmq;

        public ZmqViewModel(List<OrderItem> orderItems,ZmqBase zmq)
        {
            _zmq= zmq;
            OrderItems = new ObservableCollection<OrderItem>(orderItems);
            HexSelected = true;
        }

        public ZmqBase Zmq => _zmq;

        private string _address= "tcp://*:3000";
        public string Address 
        {
            get { return _address; }
            set 
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _sendMsg = "";
        public string SendMsg
        {
            get { return _sendMsg; }
            set
            {
                _sendMsg = value;
                OnPropertyChanged(nameof(SendMsg));
            }
        }

        string _receive = "";
        public string ReceiveMessages
        {
            get { return _receive; }
        }

        public void Connect()
        {
            _zmq.BindOrConnect(_address);
        }

        public void Send()
        {
            if(TextSelected)
            {
                _zmq.SendString(_sendMsg);
            }
            else if(HexSelected)
            {
                byte[] data=_sendMsg.Split(" ").Select(t=>byte.Parse(t)).ToArray();
                _zmq.SendBytes(data);
            }
        }

        #region public Commands

        public ICommand ConnectCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Connect();
                });
            }
        }

        public ICommand SendCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    Send();
                });
            }
        }

        public bool TextSelected { get; set; }

        public bool HexSelected { get; set; }

        public ObservableCollection<OrderItem> OrderItems { get; set; }
        #endregion
    }
}
