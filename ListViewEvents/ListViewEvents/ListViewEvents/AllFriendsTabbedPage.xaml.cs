using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListViewEvents.Model;
using ListViewEvents.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListViewEvents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllFriendsTabbedPage : ContentPage
    {
        private MainPageViewModel vm;
        public AllFriendsTabbedPage()
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
