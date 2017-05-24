using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Navigation
{
    public class Page2 : ContentPage
    {
        Label stackLabel;
        public Page2()
        {
            Title = "Page 2";
            Button forwardBtn = new Button { Text = "Вперед" };
            forwardBtn.Clicked += GoToForward;

            Button backBtn = new Button { Text = "Назад" };
            backBtn.Clicked += GoToBack;

            stackLabel = new Label();
            Content = new StackLayout { Children = { forwardBtn, backBtn, stackLabel } };
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
        // Переход вперед на Page3
        private async void GoToForward(object sender, EventArgs e)
        {
            Page3 page = new Page3();
            await Navigation.PushAsync(page);
            page.DisplayStack();
        }
        // Переход обратно на MainPage
        private async void GoToBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            ((MainPage)navPage.CurrentPage).DisplayStack();
        }
    }
}
