using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace GmailUITest
{
    public class GmailIMessagesPageObject
    {
        public GmailInboxPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        public void FindUnreadMail(string subject)
        {
            //Waiting for just sended email
            Thread.Sleep(20000);

            try
            {
                IWebElement el = PropertiesCollection.driver.FindElement(By.XPath("//span/b[text()='" + subject + "']"));
                PropertiesCollection.messageStatusRecieved = el.Displayed;
                Assert.IsTrue(PropertiesCollection.messageStatusRecieved);
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Web element doesn't excist!");
                PropertiesCollection.driver.Quit();
            }
        }
    }
}