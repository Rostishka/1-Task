using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace GmailUITest
{
    public class GooglePageObject
    {
        public GooglePageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        [FindsBy(How = How.Name, Using = "q")]
        public IWebElement txtSearchField { get; set; }

        [FindsBy(How = How.LinkText, Using = "Gmail - Google")]
        public IWebElement txtGmailSingIn { get; set; }

        [FindsBy(How = How.Id, Using = "_fZl")]
        public IWebElement btnSearch { get; set; }

        public EmailPageObject TextAndPressButton(string searchText)
        {
            txtSearchField.EnterText(searchText);

            btnSearch.Click();

            Thread.Sleep(3000);

            txtGmailSingIn.Click();

            Thread.Sleep(5000);

            try
            {
                Assert.AreEqual("Gmail", PropertiesCollection.driver.Title);
            }
            catch (NoSuchWindowException ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Unfamiliar window");
                PropertiesCollection.driver.Quit();
            }

            return new EmailPageObject();
        }
    }
}
