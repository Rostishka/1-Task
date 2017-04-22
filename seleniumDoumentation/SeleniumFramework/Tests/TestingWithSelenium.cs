using System;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using Logger;
using System.Reflection;
using Mapping;
using System.Linq;
using System.Configuration;
using nUnit = NUnit.Framework;

namespace Tests
{
    [TestFixture]
    [TestClass]
    public class TestingWithSelenium : BaseTest
    {
        [Test]
        [TestMethod]
        public void SwitchingBetweenIframes()
        {
            string expectedText = "I am iframe in iframe";
            var testScope = new Dictionary<string, string> { { "Open the application", "start page is displayed" }, { "Click iFrame in the left-side menu", "iframe page content is displayed" }, { "Verify expected text is displayed on the page", "Text in the bottom part of content area is " + expectedText } };

            Report.StartTestCase(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Verification of iframe text", testScope);

            Report.RunStep("Step 1: Open the application");
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["applicationURL"]);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("innerContent")));
            Report.AddInfo(testScope.ElementAt(0).Key, testScope.ElementAt(0).Value, driver.TakeScreenshot(testScope.ElementAt(0).Value));

            Report.RunStep("Step 2: Click iFrame in the left-side menu");
            driver.FindElement(By.LinkText("iFrame")).Click();
            wait.Until(x => { return driver.FindElements(By.XPath("//iframe[@src='iframe.html']")).Count > 0; });
            Report.AddInfo(testScope.ElementAt(1).Key, testScope.ElementAt(1).Value, driver.TakeScreenshot(testScope.ElementAt(1).Value));

            Report.RunStep("Step 3: Verify expected text is displayed on the page");
            driver.SwitchTo().Frame("innerContent");
            driver.SwitchTo().Frame(0);
            nUnit.Assert.IsTrue(driver.FindElement(By.TagName("p")).Text == expectedText);
            Report.AddInfo(testScope.ElementAt(2).Key, testScope.ElementAt(2).Value, driver.TakeScreenshot(testScope.ElementAt(2).Value));
        }

        [Test]
        [TestMethod]
        public void SwitchingToNewTab()
        {
            string expectedText = "New tab is opened";
            string newTabTitle = "Testing with Selenium - new tab";
            var testScope = new Dictionary<string, string> { { "Open the application", "start page is displayed" }, { "Click New tab item in the left-side menu", "New tab or window is opened" }, { "Verify expected text is displayed in new tab", string.Empty } };
            Report.StartTestCase(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Verification of text in new tab", testScope);

            Report.RunStep("Step 1: Open the application");
            driver.Navigate().GoToUrl(Globals.applicationURL);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("innerContent")));
            Report.AddInfo(testScope.ElementAt(0).Key, testScope.ElementAt(0).Value, driver.TakeScreenshot(testScope.ElementAt(0).Value));

            Report.RunStep("Step 2: Click New tab item in the left-side menu");
            var numberOfTabs = driver.WindowHandles.Count;
            driver.FindElement(By.LinkText("New tab")).Click();
            wait.Until(x => { return driver.WindowHandles.Count == numberOfTabs + 1; });
            Report.AddInfo(testScope.ElementAt(1).Key, testScope.ElementAt(1).Value);

            Report.RunStep("Step 3: Verify expected text is displayed in new tab");
            foreach (var handle in driver.WindowHandles)
            {
                driver.SwitchTo().Window(handle);
                if (driver.Title.Equals(newTabTitle))
                    break;
            }
            nUnit.Assert.IsTrue(driver.FindElement(By.TagName("p")).Text == expectedText);
            Report.AddInfo(testScope.ElementAt(2).Key, testScope.ElementAt(2).Value, driver.TakeScreenshot(testScope.ElementAt(2).Value));
        }

        [Test]
        [TestMethod]
        public void GalleryPicture()
        {
            var pictureAddress = "DSC_0164.jpg";
            var tooltip = "Hungarian Parliament. View from 12th floor of hotel Budapest";

            var testScope = new Dictionary<string, string> { { "Verify Gallery page picture", "Gallery page pricture address is " + pictureAddress + " and picture title is " + tooltip } };
            Report.StartTestCase(GetType().Name, MethodBase.GetCurrentMethod().Name, "Verification of Gallery page picture", testScope);

            Report.RunStep("Step 1: Verify Gallery page");
            NUnit.Framework.Assert.IsTrue(TestedApplication.GalleryPage.Picture.Src.Split('/').Last() == pictureAddress);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(TestedApplication.GalleryPage.Picture.Src.Split('/').Last() == pictureAddress);

            NUnit.Framework.Assert.IsTrue(TestedApplication.GalleryPage.Picture.Tooltip == tooltip);
            Report.AddInfo(testScope.ElementAt(0).Key, testScope.ElementAt(0).Value, driver.TakeScreenshot("Gallery page picture"));
        }

        [Test]
        [TestMethod]
        public void GalleryPictureLabel()
        {
            var testData = new Dictionary<string, string> {   
               {"DSC_0164.jpg","Hungarian Parliament. View from 12th floor of hotel Budapest"},   
  	           {"DSC_0470.jpg","Padua. Central square"},   
  	           {"DSC_0519.jpg","Antient Roma"},   
  	           {"DSC_0832.jpg","Vatican Museum. Sphere Within Sphere"},   
  	           {"DSC_0862.jpg","Vatican. St. Peter's square"},   
  	           {"DSC_0948.jpg","City of Venice. Police"},   
  	           {"DSC_1051.jpg","City of Venice. Fire department"},   
  	           {"DSC_0979.jpg","City of Venice. Ambulance visiting a patient"}};
            int i = 0;

            var testScope = new Dictionary<string, string> { { "Verify Gallery page pictures descriptions", "All Gallery page prictures descriptions are correct" } };

            Report.StartTestCase(GetType().Name, MethodBase.GetCurrentMethod().Name, "Verification of Gallery page pictures descriptions", testScope);

            Report.RunStep("Verifying picture and picture description");
            foreach (var thumbnail in TestedApplication.GalleryPage.PictureThumbnails)
            {
                Report.AddInfo("Verifying picture and picture description related to '" + thumbnail.Tooltip + "' thumbnail");
                thumbnail.Click();
                nUnit.Assert.IsTrue(TestedApplication.GalleryPage.Picture.Src.Split('/').Last() == testData.Keys.ElementAt(i));
                nUnit.Assert.IsTrue(TestedApplication.GalleryPage.PictureTitle.Text == testData.Values.ElementAt(i));
                Report.AddInfo(testScope.ElementAt(0).Key, "Picture description is '" + testData.Values.ElementAt(i) + "'", driver.TakeScreenshot("Gallery page picture description"));
                i++;
            }
        }

        [Test]
        [TestMethod]
        public void LinkLearnSeleniiumTesting()
        {
            string expectedUrl = "learnseleniumtesting.com"; 
            var testScope = new Dictionary<string, string> {   
                 { "Click link Learn Selenium Testing on Home page", "learnseleniumtesting.com resource is navigated" },   
                 { "Close Learn Selenium Testing page", "Home page is still displayed" }};
            Report.StartTestCase(GetType().Name, MethodBase.GetCurrentMethod().Name, "Verification of WebLink, closing of current tab and switching to previous tab", testScope);

            Report.RunStep("Step 1: Click link Learn Selenium Testing on Home page");
            TestedApplication.HomePage.LinkLearnSeleniumTesting.Click(); 
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.UrlMatches(expectedUrl));

            Report.RunStep("Step 2: Close tab");
            TestedApplication.Close();
            nUnit.Assert.IsTrue(TestedApplication.HomePage.IsLoaded);
        }

        [Test]
        [TestMethod]
        public void SearchWithGoogle()
        {
            #region Test data            
            const string searchTerm1 = "jobs";
            const string searchTerm2 = "steve";
            #endregion

            var testScope = new Dictionary<string, string> {
                { "Perform search with " + searchTerm1 + " term on New tab page", "title of the page is " + searchTerm1 + " - Google Search" },
                { "Perform search with " + searchTerm1 + " " + searchTerm2 + " term on New tab page", "Title of the page is " + searchTerm1 + " " + searchTerm2 + " - Google Search" },
                { "Close New tab page", "Home page is displayed" }};

            Report.StartTestCase(GetType().Name, MethodBase.GetCurrentMethod().Name, "Verification of search with Google", testScope);

            Report.RunStep("Step 1: Search with " + searchTerm1 + " term");
            TestedApplication.NewTabPage.TextboxSearch.TypeText(searchTerm1);
            TestedApplication.NewTabPage.ButtonSearch.Click();
            nUnit.Assert.IsTrue(driver.Title == searchTerm1 + " - Google Search");
            Report.AddInfo(testScope.ElementAt(0).Key, testScope.ElementAt(0).Value, driver.TakeScreenshot(testScope.ElementAt(0).Value));
            Assert(driver.Title == searchTerm1 + " - Google Search", "Page title is " + searchTerm1 + " - Google Search");

            Report.RunStep("Step 2: Search with " + searchTerm1 + " " + searchTerm2 + " term");
            TestedApplication.Backward();
            TestedApplication.NewTabPage.TextboxSearch.AppendText(" " + searchTerm2);
            TestedApplication.NewTabPage.CheckboxSearchImagesOnly.Select();
            TestedApplication.NewTabPage.ButtonSearch.Click();
            nUnit.Assert.IsTrue(driver.Title == searchTerm1 + " " + searchTerm2 + " - Google Search");
            Report.AddInfo(testScope.ElementAt(1).Key, testScope.ElementAt(1).Value, driver.TakeScreenshot(testScope.ElementAt(1).Value));
            Assert(driver.Title == searchTerm1 + " " + searchTerm2 + " - Google Search", testScope.ElementAt(1).Value);

            Report.RunStep("Step 3: Close New tab page");
            TestedApplication.Backward();
            TestedApplication.NewTabPage.ButtonClose.Click();
            nUnit.Assert.IsTrue(TestedApplication.HomePage.IsLoaded);
            Report.AddInfo(testScope.ElementAt(2).Key, testScope.ElementAt(2).Value, driver.TakeScreenshot(testScope.ElementAt(2).Value));
            Assert(TestedApplication.HomePage.IsLoaded, "Home page is displayed");
        }
    }
}
