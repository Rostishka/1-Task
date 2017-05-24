using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMPattern.Model;
using MVVMPattern.ViewModel;
using Xamarin.Forms;

namespace MVVMPattern
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel vm;
        public MainPage()
        {
            Person person = MainPageViewModel.GetPerson();
            vm = new MainPageViewModel(person);
            BindingContext = vm;
            InitializeComponent();
        }
    }
}
