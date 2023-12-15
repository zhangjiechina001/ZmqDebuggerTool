using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Text;
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
using ZmqDebuggerTool.Config;
using ZmqDebuggerTool.ViewModel;

namespace ZmqDebuggerTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        JsonConfig _configuration;
        public MainWindow(JsonConfig configuration)
        {
            InitializeComponent();

            _configuration = configuration;

            viewReq.SetDataContext(CreateModel<ZmqRequester>("RequestOrders", "RequestAddress"));
            
            viewSub.SetDataContext(CreateModel<ZmqSubscriber>("PublishOrders", "SubscriberAddress"));

            viewPub.SetDataContext(CreateModel<ZmqPublisher>("PublishOrders", "PublisherAddress"));

            DataContext = this;
        }

        private ZmqViewModel CreateModel<T>(string orderKey,string addressKey) where T : new()
        {
            var orders = OrderItem.Parse(_configuration.GetSectionToken(orderKey));
            var model = new ZmqViewModel(orders, ((new T()) as ZmqBase)!);
            model.Address = _configuration.GetSectionString(addressKey);
            return model;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //zmqSubView.Configuration= _configuration;
        }
    }
}