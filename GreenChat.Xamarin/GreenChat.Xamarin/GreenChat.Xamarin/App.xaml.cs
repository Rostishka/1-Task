using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GreenChat.Xamarin.Views;
using Xamarin.Forms;

namespace GreenChat.Xamarin
{
    public partial class App : Application
    {
        public static Boolean IsUserLoggedIn { get; set; }

        public App()
        {
            MainPage = new MainPage();
            //if (!IsUserLoggedIn)
            //{
            //    MainPage = new NavigationPage(new LogInPageView());
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new SignUpPageView());
            //}
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
