using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
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
using System.Xml.Linq;
using ZmqDebuggerTool.Communication;
using ZmqDebuggerTool.Config;
using ZmqDebuggerTool.View;
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

            JArray arr = _configuration.GetSectionArray("Items");

            foreach (var item in arr)
            {
                JObject iterObj = item.ToObject<JObject>();
                string title = iterObj["Title"].ToString();
                ZmqView view=new ZmqView();
                ZmqViewModel model = CreateModelEntity(iterObj);

                if(GlobalVar.GetInstance().GetValue(title,"Address")==string.Empty)
                {
                    model.Address = iterObj["Address"].ToString();
                }
                else
                {
                    model.Address = GlobalVar.GetInstance().GetValue(title, "Address");
                }

                model.Title = title;
                view.SetDataContext(model);

                TabItem ti = new TabItem();
                ti.Header = model.Title;
                ti.Content = view;
                tab.Items.Add(ti);
                
            }
            DataContext = this;
        }

        private ZmqViewModel CreateModelEntity(JObject obj)
        {
            string zmqType = obj["Type"].ToString();
            ZmqBase zmq = CreateZmqEntity(zmqType);
            var orders= OrderItem.Parse(obj["Orders"]);
            var model= new ZmqViewModel(orders, zmq);
            return model;
        }

        private ZmqBase CreateZmqEntity(string name)
        {
            var baseType = typeof(ZmqBase);
            var subTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(baseType) && !t.IsAbstract).ToList();

            foreach (var subType in subTypes)
            {
                if (subType.Name == name)
                {
                    var instance = (ZmqBase)Activator.CreateInstance(subType);
                    return instance;
                }
            }
            throw new Exception($"找不到{name}类型");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //zmqSubView.Configuration= _configuration;
        }

        private void btnUpdateLog_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @".\ConfigFile\release.log";
            // 创建一个新的ProcessStartInfo对象
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "notepad.exe", // 要启动的程序
                Arguments = filePath, // 启动程序时的参数（这里是文件路径）
            };
            // 启动进程
            Process process = Process.Start(startInfo);
        }
    }
}