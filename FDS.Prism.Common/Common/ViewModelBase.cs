using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace FDS.Prism.Common.Common
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private string _viewName;

        public ViewModelBase(string viewName)
        {
            this._viewName = viewName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            this.RaisePropertyChanged(propertyName);
        }


        public string ViewName
        {
            get => _viewName;
            set
            {
                _viewName = value;
                RaisePropertyChanged("Name");
            }
        }
    }
}
