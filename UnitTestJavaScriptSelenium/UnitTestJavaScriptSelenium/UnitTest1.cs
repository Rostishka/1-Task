using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace UnitTestJavaScriptSelenium
{
    [TestClass]
    public class UnitTest1
    {
        public IWebDriver _driver = new FirefoxDriver();

        [TestMethod]
        public void TestMethod1()
        {
            _driver.Navigate().GoToUrl("http://amazon.in");

            string loading = "return document.readyState";

            string loadingState = ExecuteJavaScript(loading).ToString();

            while (loadingState != "complete")
            {
                Thread.Sleep(1000);
                Console.WriteLine("Waiting...");
            }
            Console.WriteLine("Page fully loaded!!!");

            string scriptTextBox = "document.getElementById('twotabsearchtextbox').value = 'test'";
            ExecuteJavaScript(scriptTextBox);

            string scriptClickButton = "document.getElementsByClassName('nav-input')[0].click();";
            ExecuteJavaScript(scriptClickButton);

        }

        public object ExecuteJavaScript(string script)
        {
            return ((IJavaScriptExecutor)_driver).ExecuteScript(script);
        }

    }
}
