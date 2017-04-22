using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace GmailUITest
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
            try
            {
                btnCompose.Click();
                Thread.Sleep(3000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Can't click the COMPOSE button");
                PropertiesCollection.driver.Quit();
            }
            return new BoxMessagePageObject();
        }
    }
}
