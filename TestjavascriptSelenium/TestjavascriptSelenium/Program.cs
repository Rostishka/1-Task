using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace UITestGmail
{
    internal class Program
    {
        IWebDriver _driver = new FirefoxDriver();

        private static void Main(string[] args)
        {
            
        }

        [SetUp]
        public void Initialize()
        {
            //Creating the reference to browser
           
        }

        [Test]
        public void Execute()
        {
            _driver.Navigate().GoToUrl("http://amazon.in");

            string loading = "return document.readyState";

            string loadingState = ExecuteJavaScript(loading).ToString();

            while (loadingState != "complete")
            {
                Thread.Sleep(1000);
                Console.WriteLine("Waiting...");
            }
        }

        public object ExecuteJavaScript(string script)
        {
            return ((IJavaScriptExecutor) _driver).ExecuteScript(script);
        }

        [TearDown]
        public void CleanUp()
        {
        }
    }
}

