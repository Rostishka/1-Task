using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace GmailUITest
{
    public class EmailPageObject
    {
        public EmailPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        [FindsBy(How = How.Name, Using = "Email")]
        public IWebElement txtEmail { get; set; }

        [FindsBy(How = How.Name, Using = "signIn")]
        public IWebElement btnNext { get; set; }


        public PassPageObject FillEmail(string userEmail)
        {

            txtEmail.EnterText(userEmail);

            btnNext.ClickOn();

            Thread.Sleep(3000);

            return new PassPageObject();
        }
    }
}
