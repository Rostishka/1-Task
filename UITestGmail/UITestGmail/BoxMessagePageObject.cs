using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UITestGmail
{
    public class BoxMessagePageObject
    {
        public BoxMessagePageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);//this - to initialise this page
        }

        [FindsBy(How = How.Id, Using = ":do")]
        public IWebElement txtRecipient { get; set; }

        [FindsBy(How = How.Name, Using = "to")]
        public IWebElement txtEmailReciever { get; set; }

        [FindsBy(How = How.Name, Using = "subjectbox")]
        public IWebElement txtMessageSubject { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".Ap div[role='textbox']")]
        public IWebElement txtMessageClick { get; set; }

        //[FindsBy(How = How.CssSelector, Using = "")]
        //public IWebElement txtMessageValue { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[text()='Send']")]
        public IWebElement btnSend { get; set; }

        public void FillMessageFormAndSend()
        {
            //txtRecipient.Clicks();
           Thread.Sleep(2000);

            txtEmailReciever.Click();

            txtEmailReciever.EnterText("dyakivrostik@gmail.com");

            txtMessageSubject.EnterText("Some Message Subject");

            txtMessageClick.Click();

            txtMessageClick.EnterText("asdfl;gldk;fnb;lnfsdl;kbl;arfhpjwrtjpogfbpoijAFD;OSGIFJBL;ASKDFBM'STJH[PSTKGHPAEB;KNQASDFO;GINJORJGHL;KAFSDM;LBNT;SOJH;W");

            btnSend.Click();
        }
    }
}
