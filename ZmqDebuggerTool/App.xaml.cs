using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Windows;
using System.Windows.Navigation;
using ZmqDebuggerTool.Config;

namespace ZmqDebuggerTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ServiceProvider = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mw = ServiceProvider.GetRequiredService<MainWindow>();
            mw.Show();
        }

        /// <summary>
        /// 获取当前 App 实例
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// 获取存放应用服务的容器
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 配置应用的服务
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton<NavigationService>()
                .AddSingleton<MainWindow>()
                .AddSingleton<JsonConfig>();
            return serviceCollection.BuildServiceProvider();
        }
    }

}
