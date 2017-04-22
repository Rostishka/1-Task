using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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

    class PropertyCollection
    {
        public static IWebDriver driver = new ChromeDriver();//Maybe it's singleTon
    }
}
