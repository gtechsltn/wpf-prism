using FDS.Prism.Common.Common;
using FDS.Prism.Common.Views;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace FDS.Prism.Common.ViewModels
{
    [Export]
    public class View1ViewModel : ViewModelBase
    {
        public View1ViewModel(INavigationManager navigationController) : base("View 1")
        {
            GotoView2Command = new RelayCommand(() => navigationController.NavigateTo<View2, View2ViewModel>());
        }

        public ICommand GotoView2Command { get; }
    }
}
