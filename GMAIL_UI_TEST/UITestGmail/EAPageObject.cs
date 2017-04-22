using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UITestGmail
{
    class EAPageObject
    {
        public EAPageObject()
        {
            PageFactory.InitElements(PropertyCollection.driver, this);//this - to initialise this page
        }

       
        [FindsBy(How = How.Name, Using = "TitleId")]
        public IWebElement ddlTitleID { get; set; }//Give meaningful name for our variables

        [FindsBy(How = How.Name, Using = "Initial")]
        public IWebElement txtInitial { get; set; }

        [FindsBy(How = How.Name, Using = "FirstName")]
        public IWebElement txtFirstName { get; set; }

        [FindsBy(How = How.Name, Using = "MiddleName")]
        public IWebElement txtMiddleName { get; set; }

        [FindsBy(How = How.Name, Using = "Save")]
        public IWebElement btnSave { get; set; }

        public void FillUserForm(string initial, string firstName, string middleName)
        {
            txtInitial.EnterText(initial);
            txtFirstName.EnterText(firstName);
            txtMiddleName.EnterText(middleName);
            btnSave.Clicks();


            //txtInitial.SendKeys(initial);
            //txtFirstName.SendKeys(firstName);
            //txtMiddleName.SendKeys(middleName);
            //btnSave.Click();
        }
    }
}
