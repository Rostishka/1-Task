using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GmailUITest
{
    public static class SelemiumMethods
    { 
        public static void EnterText(this IWebElement element, string value_text)
        {
            element.SendKeys(value_text);
        }

        public static void ClickOn(this IWebElement element)
        {
            element.Click();
        }
    }
}
