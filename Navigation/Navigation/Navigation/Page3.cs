using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Navigation
{
    public class Page3 : ContentPage
    {
        Label stackLabel;
        public Page3()
        {
            Title = "Page 3";
            Button backBtn = new Button { Text = "Назад" };
            backBtn.Clicked += GoToBack;

            Button rootBtn = new Button { Text = "To Root" };
            rootBtn.Clicked += GoToRoot;



            stackLabel = new Label();
            Content = new StackLayout { Children = { backBtn, rootBtn, stackLabel } };
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
        // Переход обратно на Page2
        private async void GoToRoot(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            ((MainPage)navPage.CurrentPage).DisplayStack();
        }

        private async void GoToBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            NavigationPage navPage = (NavigationPage)App.Current.MainPage;
            ((Page2)navPage.CurrentPage).DisplayStack();
        }
    }
}
