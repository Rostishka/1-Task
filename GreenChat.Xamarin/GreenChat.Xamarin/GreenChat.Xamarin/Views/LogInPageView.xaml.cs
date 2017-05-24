using System;
using GreenChatXamarin.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenChat.Xamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPageView : ContentPage
    {
        public LogInPageView()
        {
            InitializeComponent();
        }

        async void OnGoToSignUpPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPageView());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Email = emailEntry.Text,
                Password = passwordEntry.Text
            };

            var isValid = AreCredentialsCorrect(user);

            if (isValid)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainDetailPageView(), this);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Danger", "Invalid email or password", "ОK");
                passwordEntry.Text = string.Empty;
            }
        }

        bool AreCredentialsCorrect(User user)
        {
            return user.Email == UsersData.Email && user.Password == UsersData.Password;
        }
    }
}