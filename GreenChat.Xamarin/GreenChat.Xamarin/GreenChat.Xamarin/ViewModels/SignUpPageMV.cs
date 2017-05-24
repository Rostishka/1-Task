using System;
using System.Linq;
using GreenChat.Xamarin;
using GreenChatXamarin.Models;
using GreenChat.Xamarin.Views;
using Xamarin.Forms;

namespace GreenChatXamarin.ViewModels
{
    public class SignUpPageMV : ContentPage
    {
        Entry fullNamEntry, passwordEntry, emailEntry, phoneNumberEntry;

        Label messageLabel;

        public SignUpPageMV() { 

            messageLabel = new Label();
            fullNamEntry = new Entry
            {
                Placeholder = "Input Username"
            };

            passwordEntry = new Entry
            {
                IsPassword = true,
                Placeholder = "Input Password"
            };

            emailEntry = new Entry()
            {
                Placeholder = "Input Email",
                Keyboard = Keyboard.Email
            };

            phoneNumberEntry = new Entry()
            {
                Placeholder = "Input number",
                Keyboard = Keyboard.Telephone
            };

            var signUpButton = new Button
            {
                Text = "Sign Up",
              
            };

            signUpButton.Clicked += OnSignUpButtonClicked;

            Title = "Sign Up";

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label { Text = "Full Name" },
                    fullNamEntry,
                    new Label { Text = "Password" },
                    passwordEntry,
                    new Label { Text = "Email address" },
                    emailEntry,
                    new Label { Text = "Phone number"},
                    signUpButton,
                }
            };
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new User()
            {
                FullName = fullNamEntry.Text,
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
                    Navigation.InsertPageBefore(new MainDetailPageView(), Navigation.NavigationStack.First());
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                await DisplayAlert("Danger", "Invalid Form", "ОK");
                phoneNumberEntry.Text = string.Empty;
                passwordEntry.Text = string.Empty;
                emailEntry.Text = string.Empty;
                //messageLabel.Text = "Sign up failed";
            }
        }
    bool AreDetailsValid(User user)
    {
    return (!string.IsNullOrWhiteSpace(user.FullName) && !string.IsNullOrWhiteSpace(user.Password)
    && !string.IsNullOrWhiteSpace(user.Email) && !string.IsNullOrWhiteSpace(user.PhoneNumber)
    && user.Email.Contains("@") && user.PhoneNumber.StartsWith("+380") && user.PhoneNumber.Length == 13
    && user.Email.EndsWith(".com") || user.Email.EndsWith(".ru") || user.Email.EndsWith(".ua"));
}
    }
}
