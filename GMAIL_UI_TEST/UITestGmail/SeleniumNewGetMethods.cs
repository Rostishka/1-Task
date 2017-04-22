using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UITestGmail
{
    public static class SeleniumNewGetMethods
    {
        public static string GetText(this IWebElement element)
        {
           return element.GetAttribute("value");
        }

        public static string GetTextFromDDL(this IWebElement element)
        {
            return new SelectElement(element).AllSelectedOptions.SingleOrDefault().Text;
        }
    }
}
