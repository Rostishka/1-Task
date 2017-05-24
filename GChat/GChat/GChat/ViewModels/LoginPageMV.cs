using System;
using GChat.Models;
using Xamarin.Forms;

namespace GChat.ViewModels
{
    public class LoginPageMV : ContentPage
    {
        Entry emailEntry, passwordEntry;

        Label messageLabel;

        public LoginPageMV()
        {
            var toolbarItem = new ToolbarItem
            {
                Text = "Sign Up"
            };
            toolbarItem.Clicked += OnGoToSignUpPageButtonClicked;

            ToolbarItems.Add(toolbarItem);

            messageLabel = new Label();

            emailEntry = new Entry
            {
                Placeholder = "email",
                Keyboard = Keyboard.Email
            };

            passwordEntry = new Entry
            {
                IsPassword = true,
                Placeholder = "password"
            };

            var loginButton = new Button
            {
                Text = "Login"
            };

            loginButton.Clicked += OnLoginButtonClicked;

            Title = "Login";

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label { Text = "Email" },
                    emailEntry,
                    new Label { Text = "Password" },
                    passwordEntry,
                    loginButton,
                }
            };
        }

        async void OnGoToSignUpPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPageMV());
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
                Navigation.InsertPageBefore(new MainPageMV(), this);
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
