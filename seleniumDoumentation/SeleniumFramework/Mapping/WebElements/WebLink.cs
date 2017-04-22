using Logger;
using OpenQA.Selenium;
using System;

namespace Mapping.WebElements
{
    public class WebLink : WebButton, ILink
    {
        public WebLink(IWebDriver driver, string name, IWebElement element) : base(driver, name, element) { }
        public WebLink(IWebDriver driver, string name, int? waitTimeout = null, params ElementLocator[] locators) : base(driver, name, waitTimeout, locators) { }

        public string Href
        {
            get
            {
                if (IsFound && IsDisplayed)
                {
                    try
                    {
                        var href = Element.GetAttribute("href");
                        return href;
                    }
                    catch (Exception ex)
                    {
                        Report.AddWarning("Getting link tooltip", "Value of attribute 'href' is read", "Getting web element attribute 'href' throws the exception: " + ex.Message);
                    }
                }
                return string.Empty;
            }
        }
    }
}   
