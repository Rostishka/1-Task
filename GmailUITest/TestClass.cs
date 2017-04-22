using NUnit.Framework;
using System;

namespace GmailUITest
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            GooglePageObject googleObj = new GooglePageObject();

            EmailPageObject accObjet = googleObj.TextAndPressButton("Gmail"); ;

            PassPageObject passObject = accObjet.FillEmail(PropertiesCollection.myEmail);

            GmailPageObject gmailObject = passObject.FillPassword(PropertiesCollection.myPassword);

            BoxMessagePageObject boxMessage = gmailObject.PressWriteButton();

            GmailInboxPageObject inboxObject = boxMessage.FillMessageFormAndSend(PropertiesCollection.myEmail, "Some Message Subject", "Hello me, I'm just writing");

            inboxObject.FindUnreadMail("Some Message Subject");

            Console.WriteLine("Operations executed!");
        }
    }
}
