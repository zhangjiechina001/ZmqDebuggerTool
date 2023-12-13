using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TcpUdpTool.ViewModel.Base;
using ZmqDebuggerTool.ViewModel.Base;

namespace ZmqDebuggerTool.ViewModel
{
    public class ZmqPublisherViewModel : ObservableObject
    {
        public ZmqPublisherViewModel()
        {

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

        }

        public void Send()
        {

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
        #endregion
    }
}
