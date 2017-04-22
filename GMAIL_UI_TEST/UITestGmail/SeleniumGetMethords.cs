using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SimpleBrowser;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.PageObjects;

namespace UITestGmail
{
    class SeleniumGetMethords
    {

        public static string GetText(string element, PropertyType elementType)
        {
            if (elementType == PropertyType.Id)
                return PropertyCollection.driver.FindElement(By.Id(element)).GetAttribute("value");//Getting text from html attribute named "value"
            if (elementType == PropertyType.Name)
                return PropertyCollection.driver.FindElement(By.Name(element)).GetAttribute("value");
            else return String.Empty;
        }
        
        public static string GetTextFromDDL(string element, PropertyType elementType)
        {
            if (elementType == PropertyType.Id)
            {
                return new SelectElement(PropertyCollection.driver.FindElement(By.Id(element))).AllSelectedOptions
                    .SingleOrDefault()
                    .Text;
            }
            if (elementType == PropertyType.Name)
                return new SelectElement(PropertyCollection.driver.FindElement(By.Name(element))).AllSelectedOptions.SingleOrDefault().Text;

            else return String.Empty;
        }
    }
}
