using Logger;
using Mapping.TestingWithSelenium;
using OpenQA.Selenium;

namespace Mapping
{
    /// <summary>   
    /// projection of an application under testing   
    /// </summary>     
    public class WebApplication
    {
        internal IWebDriver Driver { get; private set; }
        public WebApplication(IWebDriver driver)
        {
            this.Driver = driver;
            _isNew = true;
        }
        bool _isNew;
        internal static bool IsValid;

        #region Pages
        private HomePage _homePage;
        private GalleryPage _galleryPage;
        private NewTabPage _newTabPage;

        public HomePage HomePage
        {
            get
            {
                if (_isNew)
                    Open<HomePage>();
                if (!IsValid || _homePage == null)
                {
                    _homePage = new HomePage(Driver) { Name = "Home" };
                    //Actually the variable must be set True, but if a page contains frames it may cause problems because some elements of a page may be located in a frame and others may be out. If you refer to an element out of the frame, then refer to an element inside it, you’ll get StaleElementException referring again to outer element. It occurs because the driver is switched inside the frame and everything out of it is not available any more. When IsValid is False it forces recalculating an element every time it is referred to.   
                    IsValid = false;
                }
                return _homePage;
            }
        }

        public GalleryPage GalleryPage
        {
            get
            {
                if (_isNew)
                    Open<GalleryPage>();
                if (!IsValid || _galleryPage == null)
                {
                    _galleryPage = new GalleryPage(Driver)
                    {
                        Name = "Gallery"
                    };
                    IsValid = true;
                }
                return _galleryPage;
            }
        }

        public NewTabPage NewTabPage
        {
            get
            {
                if (_isNew)
                    Open<NewTabPage>(); 
                if (!IsValid || _newTabPage == null)
                {
                    _newTabPage = new NewTabPage(Driver) { Name = "New tab" };
                    IsValid = true;
                }
                return _newTabPage;
            }
        }

        #endregion Pages

        /// <summary>   
        /// Initializes an instance of specified web page   
        /// </summary>   
        /// <typeparam name="T">type of web page for initialization</typeparam>   
        /// <param name="args"></param>        
        public void Open<T>(params object[] args) where T : IWebPage
        {
            _isNew = false;
            if (typeof(T) == typeof(HomePage))
            {
                Report.AddInfo("Navigating to Home page");
                Driver.Navigate().GoToUrl(Globals.applicationURL);
                _homePage = HomePage;
            }
            else if (typeof(T) == typeof(GalleryPage))
            {
                Open<HomePage>();
                Report.AddInfo("Navigating to Gallery page");
                HomePage.MenuItemGallery.Click();
                _galleryPage = GalleryPage;
            }
            else if (typeof(T) == typeof(NewTabPage))
            {
                Open<HomePage>();
                Report.AddInfo("Navigating to New tab page");
                HomePage.MenuItemNewTab.Click();
                _newTabPage = NewTabPage;
            }
        }

        /// <summary>   
        /// Closes current window and switches to previous (if exist). Closes browser if only one tab has been opened   
        /// </summary>      
        public void Close()
        {
            if (Driver.WindowHandles.Count > 1)
            {
                int currentWindowIndex = 0;
                for (int i = 0; i < Driver.WindowHandles.Count; i++)
                {
                    if (Driver.WindowHandles[i] == Driver.CurrentWindowHandle)
                    {
                        currentWindowIndex = i;
                        break;
                    }
                }
                var previousWindow = currentWindowIndex == 0 ? Driver.WindowHandles[Driver.WindowHandles.Count - 1] : Driver.WindowHandles[currentWindowIndex - 1];
                Report.AddInfo("Closing current browser window");
                Driver.Close();
                Report.AddInfo("Switching to previous browser windows");
                Driver.SwitchTo().Window(previousWindow);
            }
            else
            {
                Quit();
            }
        }

        /// <summary>   
        /// Navigates step back in browser history   
        /// </summary>     
        public void Backward()
        {
            Driver.Navigate().Back();
            Report.AddInfo("Navigating backward", string.Empty, Driver.TakeScreenshot("Navigating backward"));
        }

        /// <summary>   
        /// Navigates one step forward in browser history   
        /// </summary>     
        public void Forward()
        {
            Driver.Navigate().Forward();
            Report.AddInfo("Navigating forward", string.Empty, Driver.TakeScreenshot("Navigating forward"));
        }

        /// <summary>   
        /// closes the application (browser)   
        /// </summary>    
        public void Quit()
        {
            Driver.Quit();
            _isNew = true;
        }
    }
}
