using Logger;
using Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using MSTest = Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected WebApplication TestedApplication;

        /// <summary>   
        /// Invoked before test class initialization. Used for setting variables related to current test collection   
        /// </summary>        
        [TestFixtureSetUp]
        public void TestFixtureSetUp() { }

        /// <summary>   
        /// The same as method with [TestFixtureSetUp] attribute for NUnit tool.   
        /// </summary>   
        /// <param name="context">static veriable of MSTest tool. Used to store information that is provided to unit tests</param>            
        [ClassInitialize]
        public static void TestCollectionSetUp(MSTest.TestContext context) { }

        /// <summary>   
        /// Invoked after all the tests from current test collection are executed. Used for disposing test collection variables   
        /// </summary>        
        [TestFixtureTearDown]
        public void TestFixtureTearDown() { }

        /// <summary>   
        /// The same as method with [TestFixtureTearDown] attribute for NUnit tool.   
        /// </summary>         	
        [ClassCleanup]
        public static void TestCollectionTearDown() { }

        /// <summary>   
        /// Invoked right before the start of every single test. Used for  initialization of current test variables   
        /// </summary>   
        [SetUp]
        [TestInitialize]
        public void Setup()
        {
            if (ConfigurationManager.AppSettings["browserType"] == "Android")
                StartProcess("cmd.exe", "java -jar selendroid-standalone-0.15.0-with-dependencies.jar");
            driver = SetUpDriverInstance(ConfigurationManager.AppSettings["browserType"]);
            TestedApplication = new WebApplication(driver); 
        }

        /// <summary>   
        /// Invoked right after a test is finished. Variables relative to executed test  are destroyed here   
        /// </summary>   
        [TearDown]
        [TestCleanup]
        public void Teardown()
        {
            Report.FinishTestCase();
            driver.Quit();
        }

        /// <summary>   
        /// Initializes IWebDriver instance as an instance of specified driver   
        /// </summary>   
        /// <param name="browserType">browser type</param>   
        /// <returns>Initialized instance of IWebDriver</returns>       
        IWebDriver SetUpDriverInstance(string browserType)
        {
            switch (browserType)
            {
                case "Firefox":
                    //new instance of browser profile
                    var profile = new FirefoxProfile();
                    //retrieving settings from config file
                    var firefoxSettings = ConfigurationManager.GetSection("FirefoxSettings") as NameValueCollection;
                    //if there are any settings
                    if (firefoxSettings != null)
                        //loop through all of them
                        for (var i = 0; i < firefoxSettings.Count; i++)
                            //and verify all of them
                            switch (firefoxSettings[i])
                            {
                                //if current settings value is "true"
                                case "true":
                                    profile.SetPreference(firefoxSettings.GetKey(i), true);
                                    break;
                                //if "false"
                                case "false":
                                    profile.SetPreference(firefoxSettings.GetKey(i), false);
                                    break;
                                //otherwise
                                default:
                                    int temp;
                                    //an attempt to parse current settings value to an integer. Method TryParse returns True if the attempt is successful (the string is integer) or return False (if the string is just a string and cannot be cast to a number)
                                    if (Int32.TryParse(firefoxSettings.Get(i), out temp))
                                        profile.SetPreference(firefoxSettings.GetKey(i), temp);
                                    else
                                        profile.SetPreference(firefoxSettings.GetKey(i), firefoxSettings[i]);
                                    break;
                            }
                    return new FirefoxDriver(profile);
                case "Internet Explorer":
                    var IEoptions = new InternetExplorerOptions();
                    var ieSettings = ConfigurationManager.GetSection("IESettings") as NameValueCollection;
                    if (ieSettings != null)
                    {
                        IEoptions.IgnoreZoomLevel = ieSettings["IgnoreZoomLevel"].ToString(CultureInfo.InvariantCulture) == "true";
                        IEoptions.IntroduceInstabilityByIgnoringProtectedModeSettings = ieSettings["IntroduceInstabilityByIgnoringProtectedModeSettings"] == "true";
                    }
                    return new InternetExplorerDriver(IEoptions);
                case "Chrome":
                    var options = new ChromeOptions();
                    var chromeSettings = ConfigurationManager.GetSection("ChromeSettings") as NameValueCollection;
                    var optionsList = new List<string>();
                    if (chromeSettings != null)
                        for (var i = 0; i < chromeSettings.Count; i++)
                            if (chromeSettings[i] == "true")
                                optionsList.Add(chromeSettings.GetKey(i));
                    options.AddArguments(optionsList);
                    return new ChromeDriver(options);
                case "Android":
                    var caps = DesiredCapabilities();
                    driver = new TouchCapableRemoteWebDriver(new Uri("http://localhost:4444/wd/hub "), caps);
                    Thread.Sleep(2000);
                    return driver;
                default:
                    Report.AddError(browserType + " web driver instance cannot be initialized. Test will by terminated. Verify configuration parameters.");
                    throw new Exception();
            }
        }

        /// <summary>
        /// Starts new application process
        /// </summary>
        /// <param name="applicationName">path to the application to start</param>
        /// <param name="arguments">application parameters</param>
        private void StartProcess(string applicationName, params string[] arguments)
        {
            var p = new Process();
            var info = new ProcessStartInfo(applicationName) { RedirectStandardInput = true, UseShellExecute = false };
            p.StartInfo = info;
            p.Start();
            using (StreamWriter sw = p.StandardInput)
            {
                foreach (string arg in arguments)
                    if (sw.BaseStream.CanWrite)
                        sw.WriteLine(arg);
            }
        }

        /// <summary>
        /// Initializes Android default desired options
        /// </summary>
        /// <returns>DesiredCapabilities</returns>
        private static DesiredCapabilities DesiredCapabilities()
        {
            DesiredCapabilities caps = OpenQA.Selenium.Remote.DesiredCapabilities.Android();
            return caps;
        }

        protected void Assert(bool condition, string expectedResult)
        {
            if (condition)
                Report.AddInfo("Assertion", expectedResult, driver.TakeScreenshot(expectedResult));
            else
                Report.AddError("Assertion", expectedResult, condition.ToString() + " is " + condition, driver.TakeScreenshot(expectedResult));
        }   
    }

    /// <summary>   
    /// Contains method for setting assembly (entire test session) related data   
    /// </summary>   
    [SetUpFixture]
    [TestClass]
    public class GlobalSetup
    {
        static string testSessionName = ConfigurationManager.AppSettings["testSession"]; 

        /// <summary>   
        /// Sets up assembly related data. Invoked in the beginning of test session   
        /// </summary>       
        [SetUp]
        public static void SetUpAssembly()
        {
            Mapping.Extensions.SetGlobals();
            Report.StartTestSession(testSessionName);
        }

        /// <summary>   
        /// Sets up assembly related data   
        /// </summary>   
        /// <param name="context">static veriable of MSTest tool. Used to store information that is provided to unit tests</param>   
        [AssemblyInitialize()]
        public static void SetUpAssembly(MSTest.TestContext context)
        {
            SetUpAssembly();
        }

        /// <summary>   
        /// Destroys assembly related data. Invoked in the end of entire test session   
        /// </summary>   
        [TearDown]
        [AssemblyCleanup()]
        public static void TearDownAssembly()
        {
            //is invoked once in the very end of a test run     	           	
            Report.SaveReport();
            string subject = "Test report - " + testSessionName;
            string[] addresses = ConfigurationManager.AppSettings["emailTo"].Split(';'); 
            string body = File.ReadAllText(Report.ReportFolderPath + "\\index.html");
            Report.SendReportByEmail(subject, addresses, body); 
        }
    }

    /// <summary>
    /// Represents an instance of remote web driver for interaction wit hweb application on Android OS
    /// </summary>
    public class TouchCapableRemoteWebDriver : RemoteWebDriver, IHasTouchScreen
    {
        public TouchCapableRemoteWebDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : base(remoteAddress, desiredCapabilities)
        {
            TouchScreen = new RemoteTouchScreen(this);
        }

        public ITouchScreen TouchScreen { get; private set; }
    }
}
