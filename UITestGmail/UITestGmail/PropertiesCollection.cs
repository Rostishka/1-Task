using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace UITestGmail
{
    enum PropertyType
    {
        Id,
        Name,
        ClassName,
        CssSelector,
        XPath,
        LinkText,
        TagName
    }

    class PropertiesCollection
    {
        public static IWebDriver driver = new FirefoxDriver();
    }
}
