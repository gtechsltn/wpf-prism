using System.ComponentModel.Composition.Hosting;
using System.Windows;
using FDS.Prism;
using FDS.Prism.Common;
using FDS.Prism.ViewModels;
using FDS.Prism.Views;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef;
using Prism.Modularity;

namespace FDS.Prism
{
	public class NexBootstrapper : MefBootstrapper
	{
        protected override DependencyObject CreateShell()
        {
            var view = ServiceLocator.Current.GetInstance<ShellView>();
            var viewModel = ServiceLocator.Current.GetInstance<ShellViewModel>();
            view.DataContext = viewModel;
            return view;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            var shellView = App.Current.MainWindow as ShellView;
            shellView.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            this.ModuleCatalog.AddModule(new ModuleInfo("CommonModule", typeof(CommonModule).AssemblyQualifiedName));
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(NexBootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CommonModule).Assembly));
        }
    }
}
