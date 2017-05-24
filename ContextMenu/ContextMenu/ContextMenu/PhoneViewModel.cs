using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextMenu
{
    class PhoneViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Phone Phone { get; set; }
        public PhonesListViewModel ListViewModel { get; set; }
        public PhoneViewModel()
        {
            Phone = new Phone();
        }
        public string Title
        {
            get { return Phone.Title; }
            set
            {
                if (Phone.Title != value)
                {
                    Phone.Title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        public string Company
        {
            get { return Phone.Company; }
            set
            {
                if (Phone.Company != value)
                {
                    Phone.Company = value;
                    OnPropertyChanged("Company");
                }
            }
        }
        public int Price
        {
            get { return Phone.Price; }
            set
            {
                if (Phone.Price != value)
                {
                    Phone.Price = value;
                    OnPropertyChanged("Price");
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
