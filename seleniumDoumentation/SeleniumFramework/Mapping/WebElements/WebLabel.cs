using Logger;
using OpenQA.Selenium;

namespace Mapping.WebElements
{
    public class WebLabel : WebElement, ILabel
    {
        public WebLabel(IWebDriver driver, string name, IWebElement element) : base(driver, name, element) { }
        public WebLabel(IWebDriver driver, string name, int? waitTimeout = null, params ElementLocator[] locators) : base(driver, name, waitTimeout, locators) { }

        public string Text
        {
            get
            {
                if (!IsFound)
                    Report.AddWarning("Failed to get text value of " + GetType() + Name, GetType() + Name + " is displayed", GetType() + Name + " is not displayed", Driver.TakeScreenshot(GetType() + Name + "is not displayed")); 
                return Element.Text;
            }
        }
    }
}