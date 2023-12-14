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
    public class ZmqPublisherViewModel : ObservableObject
    {
        ZmqBase _zmq=new ZmqBase();

        public ZmqPublisherViewModel()
        {
            OrderItems = new ObservableCollection<OrderItem>() 
            {
                new OrderItem("cmd1","12345"),
                new OrderItem("cmd2","23456")
            };
        }

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

        public void Connect()
        {
            _zmq.BindOrConnect(_address);
        }

        public void Send()
        {
            if(TextSlected)
            {
                _zmq.SendString(_sendMsg);
            }
            else if(HexSlected)
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
                    Connect();
                });
            }
        }

        public bool TextSlected { get; set; }

        public bool HexSlected { get; set; }

        public ObservableCollection<OrderItem> OrderItems { get; set; }
        #endregion
    }
}
