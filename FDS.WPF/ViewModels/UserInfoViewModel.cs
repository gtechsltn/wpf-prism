using FDS.WPF.Common;
using FDS.WPF.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FDS.WPF.ViewModels
{
    public class UserInfoViewModel : INotifyPropertyChanged
    {
        public UserInfoViewModel()
        {
            UserInfo = new UserInfo() { Id = "1", Name = "Cong 1" };

            //userInfos = new List<UserInfo> 
            userInfos = new ObservableCollection<UserInfo> 
            {
                UserInfo,
                new UserInfo { Id = "2", Name = "Cong 2" },
                new UserInfo { Id = "3", Name = "Cong 3" }
            };

            AddCommand = new RelayCommand(Add);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion INotifyPropertyChanged

        public ICommand AddCommand { get; }
        private void Add()
        {
            var cloneUser = JsonConvert.DeserializeObject<UserInfo>(JsonConvert.SerializeObject(UserInfo));
            userInfos.Add(cloneUser);

            // Clear input value.
            UserInfo.Id = string.Empty;
            UserInfo.Name = string.Empty;
        }

        public UserInfo UserInfo { get; private set; }

        //private List<UserInfo> userInfos;
        //public List<UserInfo> UserInfos
        //{
        //    get => userInfos;
        //    set
        //    {
        //        userInfos = value;
        //        OnPropertyChanged();
        //    }
        //}

        private ObservableCollection<UserInfo> userInfos;
        public ObservableCollection<UserInfo> UserInfos
        {
            get => userInfos;
        }
    }
}
