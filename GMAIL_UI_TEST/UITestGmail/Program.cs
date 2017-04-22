using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UITestGmail
{
    internal class Program
    {
        //public static IWebDriver driver = new ChromeDriver();
        private static void Main(string[] args)
        {
           
        }

        [OneTimeSetUp]
        public void Initialize()
        {
            //Creating the reference to browser
            PropertyCollection.driver.Navigate().GoToUrl("http://executeautomation.com/demosite/Login.html");
            Console.WriteLine("Opened URL!");
        }

        [Test]
        public void Execute()
        {
            //driver.Navigate().GoToUrl("http://executeautomation.com/demosite/Login.html");

            LoginPageObject loginPage = new LoginPageObject();

            EAPageObject pageEA = loginPage.Login("Savo", "Loshka");

            pageEA.FillUserForm("DR", "Rostyslav", "Diakiv");


            ////Title 
            //SeleniumSetMethods.SelectDropDown("TitleId", "Mr.", PropertyType.Id);
            ////Initial
            //SeleniumSetMethods.EnterText("Initial", "ExecuteAutomation", PropertyType.Name);
            ////Click on the button
            //SeleniumSetMethods.Click("Save", PropertyType.button); //Dont kwon why type is "button
            ////Getting Text of title
            //Console.WriteLine(SeleniumGetMethords.GetTextFromDDL("TitleId", PropertyType.Name));
            ////Getting text from the textBox named "Initial"
            //Console.WriteLine(SeleniumGetMethords.GetText("Initial", PropertyType.Id));
            //////Find the element of Search TextBox
            ////IWebElement element = driver.FindElement(By.Name("q"));
            //////Perform Ops - виконати операції
            ////element.SendKeys("executingautomationTEST with SELENIUM");
            Console.WriteLine("Operations executed!");


            PropertyCollection.driver.Navigate().GoToUrl("http://executeautomation.com/demosite/Login.html");

            //CleanUp();
        }

        [Test]
        public void Execute2()
        {
            LoginPageObject loginPage = new LoginPageObject();

            EAPageObject pageEA = loginPage.Login("Rost", "Loggg");

            pageEA.FillUserForm("DRI", "Rostyk", "Dv");

            Console.WriteLine("Operations executed!");
            //driver.Close();
            //CleanUp();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {

            //Closing the Google chrome web window
            PropertyCollection.driver.Close();
            Console.WriteLine("Browser was closed!");
        }
    }
}
