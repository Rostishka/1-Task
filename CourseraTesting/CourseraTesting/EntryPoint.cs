//using System;
//using System.Threading;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;

//class EntryPoint
//{
//    static void Main(string[] args)
//    {
//        IWebDriver driver = new ChromeDriver();

//        driver.Navigate().GoToUrl("http://testing.todvachev.com/selectors/class-name/");
//        string _class = "testClass";

//        Thread.Sleep(4000);

//        IWebElement element = driver.FindElement(By.ClassName(_class));

//        Console.WriteLine(element.Text);

//        driver.Quit();
//    }
//}

