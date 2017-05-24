using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MvvmApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new PhoneViewModel
            {
                Title = "iPhone 7",
                Company = "Apple",
                Price = 52000
            };
        }
    }
}
