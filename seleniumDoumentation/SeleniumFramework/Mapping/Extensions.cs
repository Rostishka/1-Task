using Logger;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Mapping
{
    /// <summary>   
    /// stores static extension methods for IWebDriver and other entities   
    /// </summary>   
    public static class Extensions
    {
        /// <summary>   
        /// creates screenshot of opened browser window and saves it as a file   
        /// </summary>   
        /// <param name="driver">Current IWebDriver or IWebElement</param>   
        /// <param name="screenshotDescription">Description of the screenshot</param>   
        /// <returns>Path to saved picture file or empty string if an exception has occurred</returns>     
        public static string TakeScreenshot(this IWebDriver driver, string screenshotDescription)
        {
            string screenshotFolder = Report.ReportFolderPath + "\\ScreenShots";
            string screenshotFileName = string.Empty;
            try
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                Directory.CreateDirectory(screenshotFolder);
                screenshotFileName = screenshotFolder + "\\" + Regex.Replace(screenshotDescription, @"\s+", " ").Replace(" ", "-") + "_" + DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(":", "-").Replace("/", "-").Replace(" ", "_") + ".jpeg";
                screenshot.SaveAsFile(screenshotFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Report.AddWarning("Getting screenshot", "Screenshot picture is not saved with path '" + screenshotFileName + "'", e.Message);
            }
            return screenshotFileName;
        }

        /// <summary>   
        /// Sets up global variables values   
        /// </summary>       
        public static void SetGlobals()
        {
            int temp;
            Globals.timeoutForPageLoading = Int32.TryParse(ConfigurationManager.AppSettings["timeoutPageLoading"], out temp) ? temp : 60;
            Globals.timeoutForWaitingElement = Int32.TryParse(ConfigurationManager.AppSettings["timeoutElementLoading"], out temp) ? temp : 30;
            Globals.timeoutBetweenClicks = Int32.TryParse(ConfigurationManager.AppSettings["timeoutBetweenClicks"], out temp) ? temp : 1;
            Report.doEventLogging = ConfigurationManager.AppSettings["doEventLogging"] == "true";
            Globals.applicationURL = ConfigurationManager.AppSettings["applicationURL"];
        }

        /// <summary>   
        /// Finds web element on a page   
        /// </summary>   
        /// <param name="driver">instance of IWebDriver or IWebElement</param>   
        /// <param name="elementType">type of class the targeted element is wrapped in. E.g.:  Button, Link, Textbox, etc.</param>   
        /// <param name="elementName">name of targeted web element</param>   
        /// <param name="waitTimeout">time in seconds for waiting targeted element is loaded and is present on a page</param>   
        /// <param name="locators">targeted element locators</param>   
        /// <returns>an instance of IWebElement or null if the element is not found</returns> 
        public static IWebElement GetWebElement(this IWebDriver driver, Type elementType = null, string elementName = null, int? waitTimeout = null, params ElementLocator[] locators)
        {
            var elements = GetWebElements(driver, elementType, elementName, waitTimeout, locators);
            if (elements == null || elements.Count == 0)
                return null;
            return elements[0];
        }

        /// <summary>   
        /// Gets a web element collection located on a page by specified parameter. If no element is found the method makes second attempt   
        /// </summary>   
        /// <param name="driver">Instance of ISearchContext. Actually it may be either IWebDriver or IWebElement</param>   
        /// <param name="elementType">Type of targeted element(s). This parameter is used only for reproting</param>   
        /// <param name="elementName">Name of sought-for element (is for reporting only as well)</param>   
        /// <param name="waitTimeout">Timeout between first and second attempt to find the element</param>   
        /// <param name="locators">List of locators to be used for finding the element</param>   
        /// <returns>Returns a collection of IWebElements</returns>   
        public static ReadOnlyCollection<IWebElement> GetWebElements(this IWebDriver driver, Type elementType = null, string elementName = null, int? waitTimeout = null, params ElementLocator[] locators)
        {
            bool continueSearch = false;
            string type = elementType == null ? "IWebElement(s)" : elementType.Name;
            string name = elementName ?? "Undefined";
            int timeout = (waitTimeout == null || waitTimeout < 0) ? Globals.timeoutForWaitingElement : (int)waitTimeout;
            ReadOnlyCollection<IWebElement> elements = null;
            SwitchToFrame(driver);
            do
            {
                foreach (var locator in locators)
                {
                    try
                    {
                        //if driver parameter is IWebDriver it may be switched between frames and if sought-for element(s) is/are inside iframe the driver will be switched into it. Otherwise, if ISearchContext is IWebElement, no switching into frames is possible 
                        if (locator.Frames != null)
                        {
                            SwitchToFrame(driver, locator.Frames);
                        }
                        elements = driver.FindElements(locator.Locator);
                        //if no element is found, another locator will be taken for searching until all locator will be verified or element(s) will be found – continue statement                     
                        if (elements.Count == 0)
                        {
                            Report.AddInfo("No element is found using locator " + locator.Locator);
                            continue;
                        }
                        //otherwise – when element(s) is/are found the method finishes its work and the collection is returned   
                        Report.AddInfo(elements.Count + " " + type + " " + elementName + " element(s) found using locator " + locator.Locator);
                        return elements;
                    }
                    catch (Exception ex)
                    {
                        Report.AddError("Unexpected exception has uccurred while finding web elements. Exception message: " + ex.Message);
                        return null;
                    }
                }
                //if no element is found with all passed locators or an exception has been thrown, second attempt is performed after the pause specified in timeout parameter 
                continueSearch = !continueSearch;
                if (continueSearch)
                {
                    Report.AddInfo("Waiting for " + timeout + " seconds");
                    Thread.Sleep(timeout);
                }
            } while (continueSearch);
            //if no element is found even after second attempt empty collection is returned  
            Report.AddInfo("No" + type + " " + elementName + " is found", driver.TakeScreenshot("Looking for " + type + " " + name));
            return elements;
        }

        /// <summary>   
        /// Switches driver sequently to specified frames   
        /// </summary>   
        /// <param name="driver">instance of IWebDriver</param>   
        /// <param name="frames">sequence of locators of iframes which the driver must be switched into</param> 
        public static void SwitchToFrame(this IWebDriver driver, params object[] frames)
        {
            //initially the driver is switched to the top level of a page  
            driver.SwitchTo().DefaultContent();

            //if frames array is empty or it is null there is nothing to switch into. So, return   
            //By the way, you can guess that invoke of the method without parameter just switches the driver to the top of page DOM structure 
            if (frames == null || frames.Length == 0)
                return;

            //in the loop the method consequentially finds every frame from the array and switches the driver into it 
            foreach (var frame in frames)
            {
                //first the method tries to find the frame by its id or name 
                if (frame is string)
                {
                    string s = frame.ToString();
                    try
                    {
                        //if a frame with specified id of name is found, the driver will be switched into it, then the next frame from the array will be taken   
                        //otherwise an exception will be thrown  
                        driver.SwitchTo().Frame(s);
                        Report.AddInfo("Switching into iframe " + s);
                        continue;
                    }
                    catch (Exception ex)
                    {
                        Report.AddWarning("Failed to switch into iframe " + s, "Web driver is switched into iframe " + s, ex.Message);
                        break;
                    }
                }
                //the method tries to switch the driver into the frame by its index   
                else if (frame is int)
                {
                    int i = (int)frame;
                    string s = i.ToString();
                    try
                    {
                        if (i < 0)
                            throw new ArgumentOutOfRangeException();
                        driver.SwitchTo().Frame(i);
                        Report.AddInfo("Switching into iframe " + s);
                        continue;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Report.AddWarning("Failed to switch into iframe " + s, "Web driver is switched into iframe " + s, "Index of frame cannot be negative. Verify input parameters");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Report.AddWarning("Failed to switch into iframe " + s, "Web driver is switched into iframe " + s, ex.Message);
                        break;
                    }
                }
                //with frame locator. As an iframe is common web element it may be represented as IWebElement interface, hence it may be found on a page like any other IWebElement instance – with OpenQA.Selenium.By locator.
                else if (frame is By)
                {
                    By locator = (By)frame;
                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Globals.timeoutForWaitingElement));
                    try
                    {
                        wait.Until(ExpectedConditions.ElementIsVisible(locator));
                        IWebElement e = driver.FindElement(locator);
                        driver.SwitchTo().Frame(e);
                        Report.AddInfo("Switching into iframe by locator '" + locator.ToString());
                        continue;
                    }
                    catch (Exception ex)
                    {
                        Report.AddWarning("Cannot switch to iframe by locator " + locator, "Web driver is switched to iframe by locator" + locator, ex.Message);
                    }
                }
                else
                    Report.AddError("Input parameter " + frame.ToString() + " is incorrect", "Input parameter type must be String either Integer or OpenQA.Selenium.By", "Type of input parameter " + frame.ToString() + " is " + frame.GetType());
            }
        }

        /// <summary>   
        /// calculates hash code of a web page   
        /// </summary>   
        /// <param name="driver">instance of IWebDrive</param> 
        /// <returns>web page hash code formatted as a string</returns> 
        public static string GetPageHashCode(this IWebDriver driver)
        {
            MD5 md5 = MD5.Create();
            byte[] bytesArray = Encoding.ASCII.GetBytes(driver.PageSource);
            byte[] hashCode = md5.ComputeHash(bytesArray);
            var sb = new StringBuilder();
            foreach (byte t in hashCode)
                sb.Append(t.ToString("X2"));
            return sb.ToString();
        }

        /// <summary>   
        /// Indicates if an alert message is displayed on a page   
        /// </summary>   
        /// <param name="driver">current instance of IWebDriver</param>   
        /// <returns></returns>   
        public static bool IsAlertPresent(this IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    /// <summary>   
    /// stores global static variables used all over the solution   
    /// </summary>   
    public static class Globals
    {
        public static int timeoutForPageLoading;
        public static int timeoutForWaitingElement;
        public static int timeoutBetweenClicks;
        public static string applicationURL;
    }   
}
