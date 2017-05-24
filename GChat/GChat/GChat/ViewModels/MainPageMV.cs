using System;
using Xamarin.Forms;

namespace GChat.ViewModels
{
    public class MainPageMV : ContentPage
    {
        public MainPageMV()
        {
            var toolbarItem = new ToolbarItem
            {
                Text = "Logout",
            };
            toolbarItem.Clicked += OnLogoutButtonClicked;

            ToolbarItems.Add(toolbarItem);

            Title = "Main Page";

            Content = new StackLayout
            {
                Children = {
                    new Label {
                        FontSize = 24,
                        Text = "There will be some other features like: Friends, ListGroup Chats, News etc.",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
        }

        async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            App.IsUserLoggedIn = false;
            Navigation.InsertPageBefore(new LoginPageMV(), this);
            await Navigation.PopAsync();
        }
    }
}
