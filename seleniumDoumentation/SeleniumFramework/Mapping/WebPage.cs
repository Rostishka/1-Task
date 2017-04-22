using System.Diagnostics;
using Logger;
using OpenQA.Selenium;
using System.Threading;
namespace Mapping
{
    /// <summary>   
    /// Abstract web page. Contains general for all web pages methods and properties   
    /// </summary>   
    public abstract class WebPage : IWebPage
    {
        protected WebPage(IWebDriver driver) : this(driver, Globals.timeoutForPageLoading) { }
        protected WebPage(IWebDriver driver, int timeoutForPageLoading)
        {
            Driver = driver;
            WaitUntilIsLoaded(timeoutForPageLoading);
        }
        internal IWebDriver Driver { get; private set; }
        public string Name { get; set; }
        public string Title { get { return Driver.Title; } }
        public string Url { get { return Driver.Url; } }
        public abstract bool IsLoaded { get; }

        /// <summary>   
        /// Waits until IsLoaded property of current page is True within specified period of time   
        /// </summary>   
        /// <param name="timeout">Time in seconds for waiting until a page is loaded</param>       
        protected void WaitUntilIsLoaded(int timeout)
        {
            var watch = new Stopwatch();
            while (!IsLoaded)
            {
                watch.Start();
                Thread.Sleep(Globals.timeoutForWaitingElement * 1000);
                watch.Stop();
                var secondsPassed = (int)watch.ElapsedMilliseconds / 1000;
                Report.AddInfo(string.Format("Waiting for {0} page is loaded. {1} second{2} passed", Name ?? GetType().Name, secondsPassed, secondsPassed > 1 ? "s" : string.Empty));
                if (secondsPassed < timeout)
                    continue;
                Report.AddError(description: Name + " page is not loaded", expectedResult: Name + " page is loaded withing " + timeout + " seconds", actualResult: "Page " + Name + " is not loaded after " + secondsPassed + " seconds waiting", screenshotPath: Driver.TakeScreenshot("Page " + Name + " is not loaded")); break;
            }
        }
    }
}
