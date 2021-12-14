using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FDS.Prism
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            InitializeComponent();
            var entryPoint = Application.Current as App;
            entryPoint.ExecuteBootstrapper();
        }

        internal void ExecuteBootstrapper()
        {
            // アプリケーションの動作を開始
            var bootstrapper = new NexBootstrapper();
            bootstrapper.Run();
        }
    }
}
