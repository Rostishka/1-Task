using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UITestGmail
{
    class SeleniumSetMethods
    {
        //Enter Text somewhere
        public static void EnterText(string element, string value_text, PropertyType elementType)
        {//driver - основа, елемент - імя елемента, value_text - our text, elementType - тип імені елемента
            if (elementType == PropertyType.Id)
                PropertyCollection.driver.FindElement(By.Id(element)).SendKeys(value_text);
            if (elementType == PropertyType.Name)
                PropertyCollection.driver.FindElement(By.Id(element)).SendKeys(value_text);      
        }


        //Click on button, option, checkBox etc.
        public static void Click(string element, PropertyType elementType)
        {
            if (elementType == PropertyType.Id)
                PropertyCollection.driver.FindElement(By.Id(element)).Click();
            if (elementType == PropertyType.Name)
                PropertyCollection.driver.FindElement(By.Id(element)).Click();
        }

        //Select drop down control
        public static void SelectDropDown(string element, string value_text, PropertyType elementType)
        {
            if (elementType == PropertyType.Id)
                new SelectElement(PropertyCollection.driver.FindElement(By.Id(element))).SelectByText(value_text);
            if (elementType == PropertyType.Name)
            {
                new SelectElement(PropertyCollection.driver.FindElement(By.Name(element))).SelectByText(value_text);
            }
        }

    }
}
