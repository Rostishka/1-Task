using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenChat.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageView : ContentPage
    {
        public MainPageView()
        {
            InitializeComponent();
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LogInPageView(), this);
            await Navigation.PopAsync();
        }
    }
}