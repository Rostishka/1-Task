using OpenQA.Selenium;

namespace Mapping.WebElements
{
    public class WebContainer : WebElement
    {
        public WebContainer(IWebDriver driver, string name, IWebElement element) : base(driver, name, element) { }
        public WebContainer(IWebDriver driver, string name, int? waitTimeout = null, params ElementLocator[] locators) : base(driver, name, waitTimeout, locators) { }
    }
}
