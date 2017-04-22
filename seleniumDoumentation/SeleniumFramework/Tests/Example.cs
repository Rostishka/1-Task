using System;
using NUnit.Framework; 
using Microsoft.VisualStudio.TestTools.UnitTesting; 
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox; 
using OpenQA.Selenium.IE; 
using OpenQA.Selenium.Chrome; 
using OpenQA.Selenium.Support.UI; 
using nUnit = NUnit.Framework;
using System.Collections.Generic;
using Logger;
using System.Reflection;
using Mapping;

namespace Tests
{
    /// <summary>
    /// Represents collection of tests (test methods). Name of the class is the name of test collection
    /// Attributes [TestFixture] and [TestClass] indicates that the class is a test suite (collection of tests)
    /// </summary>
    [TestFixture][TestClass]
    public class Example : BaseTest
    {
        /// <summary>
        /// Test method that performs searching with Google and verifies page title after the search
        /// </summary>
        [Test][TestMethod]
        public void GoogleSearch()//name of the test
        {
            string searchTerm = "selenium";
            Dictionary<string, string> scope = new Dictionary<string, string>();
            scope.Add("Navigate to Google search page", "Google search page is loaded");
            scope.Add("Do nothing", "");
            scope.Add("Perform search on the term " + searchTerm, "Title of search result page starts with " + searchTerm);

            Report.StartTestCase(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Google Search", scope);

            Report.RunStep();

            driver.Navigate().GoToUrl("http://www.google.com");
            Report.AddInfo("Navigation to Google Search page", string.Empty, driver.TakeScreenshot("Google Search page"));

            Report.RunStep();
            Report.AddInfo("No action");

            Report.RunStep();
            IWebElement query, btnSearch;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            query = driver.FindElement(By.Name("q"));
            query.Clear();
            query.SendKeys("selenium");
            btnSearch = driver.FindElement(By.Name("btnG"));
            btnSearch.Click();
            Report.AddInfo("Search is performed", "Search result page is opened", driver.TakeScreenshot("Search button is clicked result"));
            wait.Until(d => { return d.Title.StartsWith("selenium"); });
            nUnit.Assert.IsTrue(driver.Title.StartsWith("selenium"));
            Report.AddInfo("Verification", screenshotPath: driver.TakeScreenshot("Search result"));
        }

        /// <summary>   
        /// Test method that verifies Google Accounts page title   
        /// </summary>   
        [Test]
        [TestMethod]
        public void GoogleAccountTitleVerification()
        {
            // Run StartTestCase with the test scope declared anonymously           
            Report.StartTestCase(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Google Account Verification", new Dictionary<string, string> {{ "Open Google Accounts page", "Title of Google Accounts page is 'Sign in - Google Accounts'" }});
            Report.RunStep();

            driver.Navigate().GoToUrl("https://accounts.google.com/");
            //verification that the title of the page is equal to expected             
            nUnit.Assert.AreEqual("Sign in - Google Accounts", driver.Title);
            Report.AddInfo("nUnit verification");
            Report.AddInfo("MSTest verification");
        }
    }
}
