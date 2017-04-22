using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace GmailUITest
{
    public class BoxMessagePageObject
    {
        public BoxMessagePageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        [FindsBy(How = How.Id, Using = ":do")]
        public IWebElement txtRecipient { get; set; }

        [FindsBy(How = How.Name, Using = "to")]
        public IWebElement txtEmailReciever { get; set; }

        [FindsBy(How = How.Name, Using = "subjectbox")]
        public IWebElement txtMessageSubject { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".Ap div[role='textbox']")]
        public IWebElement txtMessageClick { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//div[text()='Send']")]
        public IWebElement btnSend { get; set; }

        public GmailIMessagesPageObject FillMessageFormAndSend(string emailReciever, string messageSubject, string messageValue)
        {
            Thread.Sleep(2000);

            txtEmailReciever.Click();

            txtEmailReciever.EnterText(emailReciever);

            txtMessageSubject.EnterText(messageSubject);

            txtMessageClick.Click();

            txtMessageClick.EnterText(messageValue);

            btnSend.Click();

            Thread.Sleep(2000);

            try
            {
                PropertiesCollection.txtmessageSended = PropertiesCollection.driver.FindElement(By.ClassName("vh")).Text;
                Assert.AreEqual("Your message has been sent. View message", PropertiesCollection.txtmessageSended);
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Message wasn't sended");

            }

            return new GmailIMessagesPageObject();
        }
    }
}
