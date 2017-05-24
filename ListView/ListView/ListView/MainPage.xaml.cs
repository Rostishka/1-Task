using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListView
{
    public partial class MainPage : ContentPage
    {
        //public ObservableCollection<Grouping<string, Phone>> PhoneGroups { get; set; }

        public MainPage()
        {
            var phones = new List<Phone>
            {
                new Phone {Title="Galaxy S8", Company="Samsung", Price=60000 },
                new Phone {Title="Galaxy S7 Edge", Company="Samsung", Price=50000 },
                new Phone {Title="Huawei P10", Company="Huawei", Price=10000 },
                new Phone {Title="Huawe Mate 8", Company="Huawei", Price=29000 },
                new Phone {Title="Mi6", Company="Xiaomi", Price=55000 },
                new Phone {Title="iPhone 7", Company="Apple", Price=38000 },
                new Phone {Title="iPhone 6S", Company="Apple", Price=50000 }
            };
            // получаем группы
            //var groups = phones.GroupBy(p => p.Company).Select(g => new Grouping<string, Phone>(g.Key, g));
            // передаем группы в PhoneGroups
            //PhoneGroups = new ObservableCollection<Grouping<string, Phone>>(groups);
            //this.BindingContext = this;
        }

        // добавление объекта
        //private void AddItem(object sender, EventArgs e)
        //{
        //    Phones.Add(new Phone {Title = "Galaxy S8", Company = "Samsung", Price = 48000});
        //}

        //// удаление выделенного объекта
        //private void RemoveItem(object sender, EventArgs e)
        //{
        //    Phone phone = phonesList.SelectedItem as Phone;
        //    if (phone != null)
        //    {
        //        Phones.Remove(phone);
        //        phonesList.SelectedItem = null;
        //    }
        //}
    }
}
