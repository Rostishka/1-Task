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
    public class GooglePageObject
    {
        public GooglePageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        [FindsBy(How = How.Name, Using = "q")]
        public IWebElement txtSearchField { get; set; }//Give meaningful name for our variables

        [FindsBy(How = How.LinkText, Using = "Gmail - Google")]
        public IWebElement txtGmailSingIn { get; set; }

        [FindsBy(How = How.Id, Using = "_fZl")]
        public IWebElement btnSearch { get; set; }

        public AccountsPageObject TextAndPressButton(string searchText)
        {
            //Search
            txtSearchField.EnterText(searchText);

            //Click Login button
            btnSearch.Click();//To go to the ohter page use SUBMIT!!!!

            Thread.Sleep(3000);

            txtGmailSingIn.Click();

            Thread.Sleep(3000);

            return new AccountsPageObject();
        }
    }
}
