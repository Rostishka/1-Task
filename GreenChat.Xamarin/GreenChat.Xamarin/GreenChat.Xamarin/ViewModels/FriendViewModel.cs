using System.ComponentModel;
using GreenChatXamarin.Models;

namespace GreenChatXamarin.ViewModels
{
    public class FriendViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        FriendsListViewModel _flvm;

        public Friend Friend { get; private set; }

        public FriendViewModel()
        {
            Friend = new Friend();
        }

        public FriendsListViewModel ListViewModel
        {
            get { return _flvm; }
            set
            {
                if (_flvm != value)
                {
                    _flvm = value;
                    OnPropertyChanged("ListViewModel");
                }
            }
        }
        public string FullName
        {
            get { return Friend.FullName; }
            set
            {
                if (Friend.FullName != value)
                {
                    Friend.FullName = value;
                    OnPropertyChanged("FullName");
                }
            }
        }
        public string Email
        {
            get { return Friend.Email; }
            set
            {
                if (Friend.Email != value)
                {
                    Friend.Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        public string PhoneNumber
        {
            get { return Friend.PhoneNumber; }
            set
            {
                if (Friend.PhoneNumber != value)
                {
                    Friend.PhoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return ((!string.IsNullOrEmpty(FullName.Trim())) ||
                        (!string.IsNullOrEmpty(PhoneNumber.Trim())) ||
                        (!string.IsNullOrEmpty(Email.Trim())));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
