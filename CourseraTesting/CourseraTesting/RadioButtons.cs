//using System;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using System.Threading;
//class RadioButtons
//{
//    static IWebDriver driver = new ChromeDriver();
//    static IWebElement radioButton;
//    static void Main()
//    {
//        string url = "http://testing.todvachev.com/special-elements/radio-button-test/";
//        string[] option = { "1", "3", "5" };

//        driver.Navigate().GoToUrl(url);

//        for (int i = 1; i < option.Length+1; i++)
//        {
//            radioButton = driver.FindElement(By.CssSelector("#post-10 > div > form > p:nth-child(6) > input[type=\"radio\"]:nth-child(" + option[i - 1] + ")"));

//            if (radioButton.GetAttribute("checked") == "true")
//            {
//                Console.WriteLine("The " + i + " radio button is checked!");

//                Thread.Sleep(4000);
//            }
//            else
//            {
//                Console.WriteLine("This is " + i + " unchecked radio buttons!");

//                radioButton.Click();

//                Thread.Sleep(4000);
//            }
//        }

//        driver.Quit();
//    }
//}