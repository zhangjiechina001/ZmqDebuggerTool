using CommnuiactionDebuggerTool.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TcpUdpTool.ViewModel.Base;
using Utils.Config;
using ZmqDebuggerTool.ViewModel.Base;

namespace ZmqDebuggerTool.ViewModel
{
    public class CommViewModel : ObservableObject
    {
        private readonly CommunicationBase _zmq;

        public CommViewModel(List<OrderItem> orderItems, CommunicationBase comm)
        {
            _zmq= comm;
            OrderItems = new ObservableCollection<OrderItem>(orderItems);
            TextSelected = true;
        }

        public CommunicationBase Comm => _zmq;

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
            try
            {
                //_zmq.BindOrConnect(_address);
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误", ex.Message);
            }
        }

        public void Send()
        {
            try
            {
                if (TextSelected)
                {
                    _zmq.SendString(_sendMsg);
                }
                else if (HexSelected)
                {
                    byte[] data = _sendMsg.Split(" ").Select(t => byte.Parse(t)).ToArray();
                    _zmq.SendBytes(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
