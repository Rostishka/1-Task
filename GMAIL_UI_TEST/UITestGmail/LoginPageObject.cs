using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UITestGmail
{
    class LoginPageObject
    {
        public LoginPageObject()
        {
            PageFactory.InitElements(PropertyCollection.driver, this);
        }

        [FindsBy(How = How.Name, Using = "UserName")]
        public IWebElement txtUserName { get; set; }//Give meaningful name for our variables

        [FindsBy(How = How.Name, Using = "Password")]
        public IWebElement txtPassword { get; set; }

        [FindsBy(How = How.Name, Using = "Login")]
        public IWebElement btnLogin { get; set; }

        public EAPageObject Login(string userName, string password)
        {
            //Username
            txtUserName.EnterText(userName);
            //Password
            txtPassword.EnterText(password);
            //Click Login button
            btnLogin.Submit();//To go to the ohter page use SUBMIT!!!!

            //Returning OA page
            return new EAPageObject();
        }
    }
}
