using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MvvmApp
{
    class PhoneViewModel : INotifyPropertyChanged
    {
        // реализации ICommand
        public ICommand SavePhoneCommand { protected set; get; }
        public ICommand DeletePhoneCommand { protected set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public Phone Phone { get; set; }

        public PhoneViewModel()
        {
            Phone = new Phone();
            this.SavePhoneCommand = new Command(SavePhone);
            this.DeletePhoneCommand = new Command(DeletePhone);
        }

        private void SavePhone()
        {
            // код по сохранению объекта Phone в бд, внешнем файле и т.д.
        }

        private void DeletePhone()
        {
            // код по удалению объекта Phone из бд и т.д.
            // в данном примере просто очищаем поля
            this.Title = "";
            this.Company = "";
            this.Price = 0;
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
