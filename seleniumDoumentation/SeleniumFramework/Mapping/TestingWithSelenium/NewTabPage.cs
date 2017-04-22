using Mapping.WebElements;
using OpenQA.Selenium;

namespace Mapping.TestingWithSelenium
{
    public class NewTabPage : WebPage //this class is not inherited from MasterPage   
    {
        public NewTabPage(IWebDriver driver) : base(driver) { }

        public override bool IsLoaded
        {
            get
            {
                try
                {
                    return (TextboxSearch.IsDisplayed && ButtonSearch.IsDisplayed);
                }
                catch { return false; }
            }
        }

        #region Elements 
        private WebLabel _description;
        private WebTextbox _searchForm;
        private WebButton _searchButton;
        private WebButton _closeButton;
        private WebCheckbox _searchImagesOnlyCheckbox;

        /// <summary>   
        /// Text in the top of the page which describes page purpose   
        /// </summary>   
        public WebLabel LabelDescription
        {
            get
            {
                if (_description == null || !WebApplication.IsValid)
                {
                    _description = new WebLabel(Driver, "page description text", locators: new ElementLocator(By.TagName("p")));
                }
                return _description;
            }
        }

        /// <summary>   
        /// Search form textbox      
        /// </summary>       
        public WebTextbox TextboxSearch
        {
            get
            {
                if (_searchForm == null || !WebApplication.IsValid)
                {
                    _searchForm = new WebTextbox(Driver, "Search", locators: new ElementLocator(By.Id("searchQuery")));
                }
                return _searchForm;
            }
        }

        /// <summary>   
        /// Search with Google button      
        /// </summary>    
        public WebButton ButtonSearch
        {
            get
            {
                if (_searchButton == null || !WebApplication.IsValid)
                {
                    _searchButton = new WebButton(Driver, "Search", locators: new ElementLocator(By.XPath("//button[contains(.,'Search')]")));
                }
                return _searchButton;
            }
        }

        /// <summary>   
        /// Close button 
        /// </summary> 
        public WebButton ButtonClose
        {
            get
            {
                if (_closeButton == null || !WebApplication.IsValid)
                {
                    _closeButton = new WebButton(Driver, "Close", locators: new ElementLocator(By.XPath("//button[text()='Close']")));
                    _closeButton.ClickValidator = new[] { ClickValidator.NumberOfWindowsChanged };
                }
                return _closeButton;
            }
        }

        /// <summary>
        /// Searct images only checkbox
        /// </summary>
        public WebCheckbox CheckboxSearchImagesOnly
        {
            get
            {
                if (_searchImagesOnlyCheckbox == null || !WebApplication.IsValid)
                    _searchImagesOnlyCheckbox = new WebCheckbox(Driver, "Search images only", locators: new ElementLocator(By.Id("img-only")));
                return _searchImagesOnlyCheckbox;
            }
        }
        #endregion
    }
}
