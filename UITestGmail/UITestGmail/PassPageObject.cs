using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UITestGmail
{
    public class PassPageObject
    {
        public PassPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);//this - to initialise this page
        }

        [FindsBy(How = How.Name, Using = "Passwd")]
        public IWebElement txtPassword { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#signIn")]
        public IWebElement btnSingIn { get; set; }

        public GmailPageObject FillPassword()
        {
            txtPassword.EnterText("SomePassword4321");

            btnSingIn.Clicks();

            Thread.Sleep(10000);

            return  new GmailPageObject();
        }
    }
}
