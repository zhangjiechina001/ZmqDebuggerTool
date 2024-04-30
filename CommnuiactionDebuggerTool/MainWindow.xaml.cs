using CommnuiactionDebuggerTool.Base;
using CommnuiactionDebuggerTool.Communications;
using CommnuiactionDebuggerTool.Views;
using System.Diagnostics;
using System.Reflection;
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
            List<CommunicationView> views = new List<CommunicationView>();
            var sections = InitManager.GetInstance().IniData.Sections.Select(t => t.SectionName).ToList();
            foreach (var section in sections)
            {
                string commType = InitManager.GetInstance().GetSection(section, "CommunicationType");
                CommunicationBase instacne=CreateCommInstane(commType);
                instacne.Name = section;
                views.Add(new CommunicationView(instacne));
            }

            foreach (CommunicationView view in views)
            {
                TabItem newItem = new TabItem();
                newItem.Header = view.TabHeader;
                newItem.Content = view;
                tabItems.Items.Add(newItem);
            }
        }

        private CommunicationBase CreateCommInstane(string commType)
        {
            Assembly assembly= Assembly.GetExecutingAssembly();
            //var names= assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(CommunicationBase))).Select(t=>t.Name).ToList();
            var types=assembly.GetTypes().Where(t=>t.IsSubclassOf(typeof(CommunicationBase))).Where(t=>t.Name==commType).ToList();
            if(types.Any())
            {
                return Activator.CreateInstance(types.First()) as CommunicationBase;
            }
            else
            {
                return null;
            }
        }

        private void btnExtendPingTool_Click(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "./ExtendExe/PingToolV1.7.exe";
            process.StartInfo.WorkingDirectory = "./ExtendExe";
            process.Start();
        }
    }
}