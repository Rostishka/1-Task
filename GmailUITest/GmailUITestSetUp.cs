using NUnit.Framework;
using System;

namespace GmailUITest
{
    [SetUpFixture]
    public class GmailUITestSetUp
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            PropertiesCollection.driver.Navigate().GoToUrl("https://www.google.com");
            PropertiesCollection.myEmail = "dyakivrostik@gmail.com";
            PropertiesCollection.myPassword = "SomePassword4321";
            PropertiesCollection.messageStatusRecieved = false;
            PropertiesCollection.txtmessageSended = null;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            PropertiesCollection.driver.Quit();
        }
    }
}