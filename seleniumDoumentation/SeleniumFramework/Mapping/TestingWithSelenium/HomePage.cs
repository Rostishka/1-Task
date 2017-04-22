using Mapping.WebElements;
using OpenQA.Selenium;

namespace Mapping.TestingWithSelenium
{
    public class HomePage : MasterPage
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        public override bool IsLoaded
        {
            get
            {
                try
                {
                    return (base.IsLoaded && Paragraph_1.IsDisplayed);
                }
                catch { return false; }
            }
        }

        #region Elements
        WebLabel _p1;
        WebLink _linkLearnSeleniumTesting;

        /// <summary>   
        /// First paragraph of page text   
        /// </summary>     
        public WebLabel Paragraph_1
        {
            get
            {
                if (_p1 == null || !WebApplication.IsValid)
                {
                    _p1 = new WebLabel(Driver, "paragraph 1", locators: new ElementLocator(new[] { "innerContent" }, By.XPath("//p[1]")));
                } return _p1;
            }
        }

        public WebLink LinkLearnSeleniumTesting
        {
            get
            {
                if (_linkLearnSeleniumTesting == null || !WebApplication.IsValid)
                {
                    _linkLearnSeleniumTesting = new WebLink(Driver, "Learn Selenium Testing", locators: new ElementLocator(new[] { "innerContent" }, By.CssSelector("#container a")));
                }
                return _linkLearnSeleniumTesting;
            }
        }
        #endregion Elements
    }
}
