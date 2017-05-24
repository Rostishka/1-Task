using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Navigation
{
    public partial class MainPage : ContentPage
    {
        Label stackLabel;
        bool loaded = false;
        public MainPage()
        {
            Title = "Main Page";
            Button forwardButton = new Button { Text = "Вперед" };
            forwardButton.Clicked += GoToForward;

            stackLabel = new Label();
            Content = new StackLayout { Children = { forwardButton, stackLabel } };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (loaded == false)
            {
                DisplayStack();
                loaded = true;
            }
        }

        protected internal void DisplayStack()
        {
            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            stackLabel.Text = "";
            foreach (Page p in navPage.Navigation.NavigationStack)
            {
                stackLabel.Text += p.Title + "\n";
            }
        }
        // Переход вперед на Page2
        private async void GoToForward(object sender, EventArgs e)
        {
            Page2 page = new Page2();
            await Navigation.PushAsync(page);
            page.DisplayStack();
        }
    }
}
