using Mapping.WebElements;
using OpenQA.Selenium;

namespace Mapping.TestingWithSelenium
{
    public abstract class MasterPage : WebPage
    {
        public MasterPage(IWebDriver driver) : base(driver) { }

        public override bool IsLoaded
        {
            get
            {
                try
                {
                    return (LabelTitle.IsDisplayed && MenuItemHome.IsDisplayed && IframeInnerContent.IsDisplayed);
                }
                catch
                {
                    return false;
                }
            }
        }

        #region Elements
        WebLabel _labelTitle;
        WebContainer _iframeInnerContent;

        public WebLabel LabelTitle
        {
            get
            {
                if (_labelTitle == null || !WebApplication.IsValid)
                    _labelTitle = new WebLabel(Driver, "Title", locators: new ElementLocator(By.XPath("//div[contains(.,'Testing with Selenium') and not(div)]")));
                return _labelTitle;
            }
        }

        public WebContainer IframeInnerContent
        {
            get
            {
                if (_iframeInnerContent == null || !WebApplication.IsValid)
                    _iframeInnerContent = new WebContainer(Driver, "inner content container", locators: new ElementLocator(By.Id("innerContent")));
                return _iframeInnerContent;
            }
        }

        #region Left-side navigation menu
        WebLink _menuItemHome;
        WebLink _menuItemIFrame;
        WebLink _menuItemNewTab;
        WebLink _menuItemSandbox;
        WebLink _menuItemGallery;
        
        public WebLink MenuItemHome
        {
            get
            {
                if (_menuItemHome == null || !WebApplication.IsValid)
                    _menuItemHome = new WebLink(Driver, "menu item Home", locators: new ElementLocator(By.XPath("//a[text()='Home']")));
                return _menuItemHome;
            }
        }  

        public WebLink MenuItemIFrame
        {
            get
            {
                if (_menuItemIFrame == null || !WebApplication.IsValid)
                    _menuItemIFrame = new WebLink(Driver, "menu item iFrame", locators: new ElementLocator(By.XPath("//a[text()='iFrame']")));
                return _menuItemIFrame;
            }
        }

        public WebLink MenuItemNewTab
        {
            get
            {
                if (_menuItemNewTab == null || !WebApplication.IsValid)
                    _menuItemNewTab = new WebLink(Driver, "menu item New tab", locators: new ElementLocator(By.XPath("//a[text()='New tab']")));
                return _menuItemNewTab;
            }
        }

        public WebLink MenuItemSandbox
        {
            get
            {
                if (_menuItemSandbox == null || !WebApplication.IsValid)
                    _menuItemSandbox = new WebLink(Driver, "menu item Sandbox", locators: new ElementLocator(By.XPath("//a[text()='Sandbox']")));
                return _menuItemSandbox;
            }
        }

        public WebLink MenuItemGallery
        {
            get
            {
                if (_menuItemGallery == null || !WebApplication.IsValid)
                    _menuItemGallery = new WebLink(Driver, "menu item Gallery", locators: new ElementLocator(By.XPath("//a[text()='Gallery']")));
                return _menuItemGallery;
            }
        }
        #endregion Left-side navigation menu
        #endregion Elements
    }
}
