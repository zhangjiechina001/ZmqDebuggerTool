using Microsoft.Extensions.Configuration;
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
            var items = OrderItem.Parse(configuration.GetSectionToken("PublishOrders"));
            PublishModel = new ZmqViewModel(items,new ZmqPublisher());
            PublishModel.Address = configuration.GetSectionString("PublisherAddress");

            var items1 = OrderItem.Parse(configuration.GetSectionToken("RequestOrders"));
            SubscriberModel=new ZmqViewModel(items1,new ZmqSubscriber());
            SubscriberModel.Address = configuration.GetSectionString("SubscriberAddress");

            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            zmqView.Configuration= _configuration;
            //zmqSubView.Configuration= _configuration;
        }

        public ZmqViewModel PublishModel { get; set; }

        public ZmqViewModel SubscriberModel { get; set; }
    }
}