using System;
using System.Linq;
using GChat.Models;
using GChat.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GChat.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new User()
            {
                FullName = fullNameEntry.Text,
                Password = passwordEntry.Text,
                Email = emailEntry.Text,
                PhoneNumber = phoneNumberEntry.Text
            };

            // Validation entered User's data

            var signUpSucceeded = AreDetailsValid(user);

            if (signUpSucceeded)
            {
                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                if (rootPage != null)
                {
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore(new Menu(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                await DisplayAlert("Danger", "Invalid Form", "ОK");
            }
        }

        bool AreDetailsValid(User user)
        {
            return (!string.IsNullOrWhiteSpace(user.FullName) && !string.IsNullOrWhiteSpace(user.Password) 
                && !string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.PhoneNumber) 
                && user.Email.Contains("@")&& user.PhoneNumber.StartsWith("0") && user.PhoneNumber.Length == 10 
                && user.Email.EndsWith(".com") || user.Email.EndsWith(".ru") || user.Email.EndsWith(".ua"));
        }

        void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            header.Text = "You have choosen: " + picker.Items[picker.SelectedIndex];
        }
    }
}
