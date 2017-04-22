//using System;
//using System.Drawing;
//using System.Threading;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;

//class XPath
//{

//    static void Main()
//    {
//        IWebDriver driver = new ChromeDriver();

//        driver.Navigate().GoToUrl("http://testing.todvachev.com/selectors/class-name/");
//        string _cssPath = "#post-107 > div > fig mg";//Better to use css selectors
//        string _XPath = "//*[@id=\"post-107\"]/div/figure/img";

//        Thread.Sleep(4000);

//        IWebElement Css_element;

//        try
//        {
//            Css_element = driver.FindElement(By.CssSelector(_cssPath));
//            if (Css_element.Displayed) Console.WriteLine("We can see the image FROM CSS SELECTOR");
//        }
//        catch (NoSuchElementException ex)
//        {
//            Console.WriteLine(ex);
//            Console.WriteLine("NO Image FROM CSS SELECTOR!!!!");
//        }


//        IWebElement XPaht_element = driver.FindElement(By.XPath(_XPath));

//        if (XPaht_element.Displayed) Console.WriteLine("We can see the image FROM XPATH");
//        else Console.WriteLine("NO Image FROM XPATH!!!!");

//        Console.ReadKey();

//        driver.Quit();
//    }
//}