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
    public class GmailPageObject
    {
        public GmailPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);//this - to initialise this page
        }

        [FindsBy(How = How.CssSelector, Using = ".aic div[role='button']")]
        public IWebElement btnCompose { get; set; }

        public BoxMessagePageObject PressWriteButton()
        {
            btnCompose.Click();

            Thread.Sleep(3000);

            return new BoxMessagePageObject();
        }
    }
}
