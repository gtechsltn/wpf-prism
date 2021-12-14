using Microsoft.Practices.ServiceLocation;
using System;
using System.ComponentModel.Composition;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace FDS.Prism.Common.Common
{
    [Export(typeof(INavigationManager))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class NavigationManager : INavigationManager
    {
        [ImportingConstructor]
        public NavigationManager()
        {
            this.rootElement = Application.Current.MainWindow as FrameworkElement;
        }

        TView INavigationManager.NavigateTo<TView, TViewModel>(string regionName, TView view)
        {
            var region = this.rootElement.FindName(regionName) as ContentPresenter;

            if (region == null)
            {
                var presenters = this.rootElement.ChildrenOfType<ContentPresenter>();
                foreach (var presenter in presenters)
                {
                    if (string.IsNullOrEmpty(presenter.Name) == false &&
                        presenter.Name == regionName)
                    {
                        region = presenter;
                        break;
                    }
                }
            }
            if (region == null)
            {
                var itemsControls = this.rootElement.ChildrenOfType<ItemsControl>();
                foreach (var ctrl in itemsControls)
                {
                    if (ctrl != null)
                    {
                        region = ctrl.FindName(regionName) as ContentPresenter;
                        if (region != null)
                        {
                            break;
                        }
                    }
                }
            }
            if (region == null)
            {
                var itemsControls = this.rootElement.ChildrenOfType<Panel>();
                foreach (var ctrl in itemsControls)
                {
                    if (ctrl != null)
                    {
                        region = ctrl.FindName(regionName) as ContentPresenter;
                        if (region != null)
                        {
                            break;
                        }
                    }
                }
            }

            if (region == null)
            {
                throw new ArgumentException("regionName");
            }

            var oldView = region.Content;
            if (oldView != null)
            {
                oldView = null;
            }

            if (view == null)
            {
                view = ServiceLocator.Current.GetInstance<TView>();

                var navigationManager = ServiceLocator.Current.GetInstance<INavigationManager>();
                var constructor = typeof(TViewModel).GetConstructor(new Type[] { typeof(INavigationManager) });
                var viewModel = (TViewModel)constructor.Invoke(new object[] { navigationManager });
                view.DataContext = viewModel;
            }
            region.Content = view;

            return view;
        }

        public void RemoveView(string regionName)
        {
            var region = this.rootElement.FindName(regionName) as ContentPresenter;

            if (region == null)
            {
                var presenters = this.rootElement.ChildrenOfType<ContentPresenter>();
                foreach (var presenter in presenters)
                {
                    if (string.IsNullOrEmpty(presenter.Name) == false &&
                        presenter.Name == regionName)
                    {
                        region = presenter;
                        break;
                    }
                }
            }
            if (region == null)
            {
                var itemsControls = this.rootElement.ChildrenOfType<ItemsControl>();
                foreach (var ctrl in itemsControls)
                {
                    if (ctrl != null)
                    {
                        region = ctrl.FindName(regionName) as ContentPresenter;
                        if (region != null)
                        {
                            break;
                        }
                    }
                }
            }

            if (region == null)
            {
                throw new ArgumentException("regionName");
            }

            region.Content = null;
        }

        private FrameworkElement rootElement;
        public FrameworkElement RootElement
        {
            get
            {
                return this.rootElement;
            }
            set
            {
                this.rootElement = value;
            }
        }
    }

    public interface INavigationManager
    {
        FrameworkElement RootElement
        {
            get;
            set;
        }

        TView NavigateTo<TView, TViewModel>(string regionName = "ShellRegion", TView view = null)
            where TView : FrameworkElement
            where TViewModel : ViewModelBase;

        void RemoveView(string regionName);
    }
}
