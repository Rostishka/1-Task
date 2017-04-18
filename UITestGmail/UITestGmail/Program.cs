using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

namespace UITestGmail
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        [OneTimeSetUp]
        public void Initialize()
        {
            //Creating the reference to browser
            PropertiesCollection.driver.Navigate().GoToUrl("https://www.google.com");
            Console.WriteLine("Opened URL!");
        }



        [Test]
        public void GoToGmail()
        { 
            GooglePageObject googleObj = new GooglePageObject();

            AccountsPageObject accObjet = googleObj.TextAndPressButton("Gmail");;

            PassPageObject passObject =  accObjet.FillEmail();

            GmailPageObject gmailObject = passObject.FillPassword();

            BoxMessagePageObject boxMessage = gmailObject.PressWriteButton();

            boxMessage.FillMessageFormAndSend();

            Console.WriteLine("Operations executed!");
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            //Closing the Google chrome web window
            PropertiesCollection.driver.Close();
            Console.WriteLine("Browser was closed!");
        }
    }
}
