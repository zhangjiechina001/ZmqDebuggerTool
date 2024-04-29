using CommnuiactionDebuggerTool.Communications;
using CommnuiactionDebuggerTool.Views;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommnuiactionDebuggerTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void zmqDebug_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tabItems.Items.Clear();
            List<CommunicationView> views = new List<CommunicationView>
            {
                new CommunicationView(new TcpServer()),
                new CommunicationView(new TcpServer())
            };

            foreach (CommunicationView view in views)
            {
                TabItem newItem = new TabItem();
                newItem.Header = view.TabHeader;
                newItem.Content = view;
                tabItems.Items.Add(newItem);
            }
        }
    }
}