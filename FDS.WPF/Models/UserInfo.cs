using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FDS.WPF.Models
{
    public class UserInfo : INotifyPropertyChanged
    {
        private string id;
        private string name;
        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;

        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
