using FDS.Prism.Common.Common;
using FDS.Prism.Common.Views;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace FDS.Prism.Common.ViewModels
{
    [Export]
    public class View2ViewModel : ViewModelBase
    {
        public View2ViewModel(INavigationManager navigationController) : base("View 2")
        {
            BacktoView1Command = new RelayCommand(() => navigationController.NavigateTo<View1, View1ViewModel>());
        }

        public ICommand BacktoView1Command { get; }
    }
}
