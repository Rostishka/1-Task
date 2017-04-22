//using System;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using System.Threading;
//class EntryPoint
//{
//    static IWebDriver driver = new ChromeDriver();
//    static IWebElement textBox;
//    static void Main()
//    {
//        string url = "http://testing.todorvachev.com/special-elements/text-input-field/";

//        driver.Navigate().GoToUrl(url);

//        textBox = driver.FindElement(By.Name("username"));

//        textBox.SendKeys("Test text");

//        Thread.Sleep(3000);

//        //textBox.Clear(); - It clears the Text Box

//        Console.WriteLine(textBox.GetAttribute("maxlength"));//Its checking the maximal possible length of the Text Box

//        Thread.Sleep(3000);

//        driver.Quit();
//    }
//}