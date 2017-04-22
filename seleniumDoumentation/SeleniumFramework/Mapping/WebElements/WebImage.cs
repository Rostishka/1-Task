using Logger;
using OpenQA.Selenium;
using System;

namespace Mapping.WebElements
{
    public class WebImage : ClicableWebElement, IImage
    {
        public WebImage(IWebDriver driver, string name, IWebElement element) : base(driver, name, element) { }
        public WebImage(IWebDriver driver, string name, int? waitTimeout = null, params ElementLocator[] locators) : base(driver, name, waitTimeout, locators) { }

        public string Src
        {
            get
            {
                if (IsFound)
                {
                    try
                    {
                        return Element.GetAttribute("src");
                    }
                    catch (Exception ex)
                    {
                        Report.AddWarning("Getting image source address", "Value of attribute 'src' is read", "Getting web element attribute 'src' throws the exception: " + ex.Message);
                    }
                } 
                return string.Empty;
            }
        }

        public string Tooltip
        {
            get
            {
                if (IsFound && IsDisplayed)
                {
                    try
                    {
                        string tooltip = Element.GetAttribute("alt"); 
                        if(string.IsNullOrEmpty(tooltip)) 
                            tooltip = Element.GetAttribute("title"); 
                        return tooltip;
                    }
                    catch (Exception ex)
                    {
                        Report.AddWarning("Getting image tooltip", "Value of attribute 'title' is read", "Getting web element attribute 'title' throws the exception: " + ex.Message);
                    }
                } 
                return string.Empty;
            }
        }
    }
}
