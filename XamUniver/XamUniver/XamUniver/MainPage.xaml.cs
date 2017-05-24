using System;
using Xamarin.Forms;

namespace XamUniver
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Person me = Init();
            BindingContext = me;
            InitializeComponent();
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new Page1(), true);
        }

        private Person Init()
        {
            return new Person()
            {
                FullName = "SRodstik SDyakiv",
                PhoneNumber = 23654236,
                Sex = "Male"
            };
        }
    }
}
