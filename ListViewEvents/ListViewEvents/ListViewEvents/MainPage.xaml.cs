using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListViewEvents.Model;
using ListViewEvents.ViewModel;
using Xamarin.Forms;

namespace ListViewEvents
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel vm;
        public MainPage()
        {
            vm = new MainPageViewModel();
            BindingContext = vm;
            InitializeComponent();
        }

        private void EmployeeList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var person = e.Item as Person;
            DisplayAlert("Selection Made", "You tapped" + person.FullName, "Ok");
        }
    }
}
