using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataTransfer
{
    public partial class MainPage : ContentPage
    {
        protected internal ObservableCollection<Phone> Phones { get; set; }

        public MainPage()
        {
            InitializeComponent();

            Phones = new ObservableCollection<Phone>
            {
                new Phone {Name="iPhone 7", Company="Apple", Price=52000},
                new Phone {Name="Galaxy S8", Company="Samsung", Price=50000},
                new Phone {Name="LG G6", Company="LG", Price=45000},
                new Phone {Name="Huawei P10", Company="Huawei", Price=35000}
            };
            phonesList.BindingContext = Phones;
        }
        // обработчик выбора элемента в списке
        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // Получаем выбранный элемент 
            Phone selectedPhone = args.SelectedItem as Phone;
            if (selectedPhone != null)
            {
                // Снимаем выделение
                phonesList.SelectedItem = null;
                // Переходим на страницу редактирования элемента 
                await Navigation.PushAsync(new PhonePage(selectedPhone));
            }
        }
        // переходим на страницу PhonePage для добавления нового элемента
        private async void AddButton_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PhonePage(null));
        }
        // вспомогательный метод для добавления элемента в список
        protected internal void AddPhone(Phone phone)
        {
            Phones.Add(phone);
        }

        private async void TablePage_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TablePage());
        }
    }
}
