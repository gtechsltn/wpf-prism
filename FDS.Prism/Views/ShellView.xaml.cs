using FDS.Prism.Common.Common;
using FDS.Prism.Common.ViewModels;
using FDS.Prism.Common.Views;
using System.ComponentModel.Composition;
using System.Windows;

namespace FDS.Prism.Views
{
    [Export]
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
            INavigationManager navigationControler = new NavigationManager();
            navigationControler.NavigateTo<View1, View1ViewModel>();
        }
    }
}
