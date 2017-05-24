using System;
using System.Diagnostics;
using GChat.Views;
using Xamarin.Forms;

namespace GChat
{
    public partial class App : Application
    {  
        public static Boolean IsUserLoggedIn { get; set; }

        public App()
        {
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new GChat.Views.Menu());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //There supposed to be connection to Server
            Debug.WriteLine("Application Started");
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Debug.WriteLine("Application on a background/on hold");
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            Debug.WriteLine("Application Resued");
        }
    }
}
