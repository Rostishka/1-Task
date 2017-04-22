using Mapping.WebElements;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Mapping.TestingWithSelenium
{
    public class GalleryPage : MasterPage
    {
        public GalleryPage(IWebDriver driver) : base(driver) { }

        public override bool IsLoaded
        {
            get
            {
                try
                {
                    return (base.IsLoaded && Picture.IsDisplayed);
                }
                catch { return false; }
            }
        }

        #region Elements
        private WebImage picture;
        private WebLabel pictureTitle;
        private List<WebImage> pictureThumbnails = new List<WebImage>();

        public WebImage Picture
        {
            get
            {
                if (picture == null || !WebApplication.IsValid)
                    picture = new WebImage(Driver, "Gallery picture", locators: new ElementLocator(new[] { "innerContent" }, By.Id("pic")));
                return picture;
            }
        }

        public WebLabel PictureTitle
        {
            get
            {
                if (pictureTitle == null || !WebApplication.IsValid)
                    pictureTitle = new WebLabel(Driver, "Gallery picture title", locators: new ElementLocator(new[] { "innerContent" }, By.Id("title")));
                return pictureTitle;
            }
        }

        public List<WebImage> PictureThumbnails
        {
            get
            {
                if (pictureThumbnails.Count == 0 || !WebApplication.IsValid)
                {
                    var collection = Driver.GetWebElements(typeof(WebImage), "picture thumbnail", locators: new ElementLocator(new[] { "innerContent" }, By.XPath(".//tr/td[1]/img")));
                    for (int i = 0; i < collection.Count; i++)
                        pictureThumbnails.Add(new WebImage(Driver, "picture thumbnail " + (i + 1), collection.ElementAt(i)));
                }
                return pictureThumbnails;
            }
        }

        #endregion Elements
    }
}
