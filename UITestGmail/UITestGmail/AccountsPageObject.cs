using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UITestGmail
{
    public class AccountsPageObject
    {
        public AccountsPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);//this - to initialise this page
        }


        [FindsBy(How = How.Name, Using = "Email")]
        public IWebElement txtEmail { get; set; }

        [FindsBy(How = How.Name, Using = "signIn")]
        public IWebElement btnNext { get; set; }


        public PassPageObject FillEmail()
        {
            txtEmail.EnterText("dyakivrostik@gmail.com");

            btnNext.Clicks();

            Thread.Sleep(3000);

            return new PassPageObject();
        }
    }
}
