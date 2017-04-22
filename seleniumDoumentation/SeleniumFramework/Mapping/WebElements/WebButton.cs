using Logger; 
using OpenQA.Selenium; 
using System;

namespace Mapping.WebElements
{
    public class WebButton : ClicableWebElement, IButton
    {
        public WebButton(IWebDriver driver, string name, IWebElement element) : base(driver, name, element) { }
        public WebButton(IWebDriver driver, string name, int? waitTimeout = null, params ElementLocator[] locators) : base(driver, name, waitTimeout, locators) { }
        public string Text
        {
            get
            {
                if (!IsFound)
                    Report.AddWarning("Failed to get text value of " + GetType() + Name, GetType() + Name + " is displayed", GetType() + Name + " is not displayed", Driver.TakeScreenshot(GetType() + Name + "is not found"));
                var text = Element.Text;
                if (!string.IsNullOrEmpty(text))
                    return text;
                try
                {
                    text = Element.GetAttribute("value");
                }
                catch 
                { 
                    return string.Empty; 
                }
                return text;
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
                        var tooltip = Element.GetAttribute("title");
                        return tooltip;
                    }
                    catch (Exception ex)
                    {
                        Report.AddWarning("Getting element tooltip", "Value of attribute 'title' is read", "Getting web element attribute 'title' throws the exception: " + ex.Message);
                    }
                }
                return string.Empty;
            }
        }
    }
}
