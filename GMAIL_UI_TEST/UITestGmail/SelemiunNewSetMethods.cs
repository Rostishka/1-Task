using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UITestGmail
{
    public static class SelemiunNewSetMethods
    {   /// <summary>
    /// Extended method for entering text in the control 
    /// </summary>
    /// <param name="element"></param>
    /// <param name="value_text"></param>
        public static void EnterText(this IWebElement element, string value_text)//if I write this before IWebElement I will create a new Extention method for this type 
        {//якшо я пишу this то воно вказує на цей обєкт і тоді не потрібно буде його прописувати при виклиці цієї функції
            element.SendKeys(value_text);
        }

        /// <summary>
        ///Click on button, option, checkBox etc.
        /// </summary>
        /// <param name="element"></param>
        public static void Clicks(this IWebElement element)
        {
            element.Click();
        }

        /// <summary>
        /// Select drop down control
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value_text"></param>
        public static void SelectDropDown(this IWebElement element, string value_text)
        {
            new SelectElement(element).SelectByText(value_text);
        }
    }
}
