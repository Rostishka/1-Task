using System;
using Logger;
using OpenQA.Selenium;
using System.Linq;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Reflection;

namespace Mapping.WebElements
{
    public abstract class WebElement
    {
        private WebElement(IWebDriver driver, string name)
        {
            Name = name;
            Driver = driver;
        }

        protected WebElement(IWebDriver driver, string name, IWebElement element)
            : this(driver, name)
        {
            Element = element;
        }

        protected WebElement(IWebDriver driver, string name, int? waitTimeout = null, params ElementLocator[] locators)
            : this(driver, name)
        {
            string elementName = string.IsNullOrEmpty(name) ? string.Empty : name;
            if (locators == null || locators.Length == 0)
                Report.AddWarning("Finding IWebElement inside web element constructor", "At least one locator must be passed to element constructor", "No locator for " + GetType() + " " + elementName + " element specified");
            else
            {
                Element = driver.GetWebElement(GetType(), elementName, waitTimeout, locators);
            }
        }

        protected IWebDriver Driver;
        internal protected IWebElement Element { get; set; }
        public string Name { get; set; }

        public virtual bool IsEnabled
        {
            get
            {
                try
                {
                    if (Element != null)
                        return Element.Enabled;
                }
                catch (Exception ex)
                {
                    Report.AddWarning("Getting IsEnabled property", "IsEnabled property returns true if element is available for interaction or false if it is read-only", ex.Message, Driver.TakeScreenshot("Getting IsEnabled property"));
                }
                return false;
            }
        }
        public bool IsDisplayed
        {
            get
            {
                try
                {
                    if (Element != null)
                        return Element.Displayed;
                }
                catch (Exception ex)
                {
                    Report.AddWarning("Getting IsDisplayed property", "IsDisplayed property returns true if element is displayed on page or false if it is hidden or is out of work area", ex.Message, Driver.TakeScreenshot("Getting IsEnabled property"));
                }
                return false;
            }
        }
        public bool IsFound
        {
            get
            {
                try
                {
                    if (Element != null)
                    {
                        var p = Element.Location;
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Report.AddWarning("Getting IsFound property", "IsFound property returns true if element is found in DOM structure of page or false if it is not found", ex.Message, Driver.TakeScreenshot("Getting IsEnabled property"));
                }
                return false;
            }
        }
    }

    /// <summary>   
    /// Represents web element which may be clicked or dragged on   
    /// </summary>  
    public abstract class ClicableWebElement : WebElement
    {
        protected ClicableWebElement(IWebDriver driver, string name, IWebElement element) : base(driver, name, element) { }
        protected ClicableWebElement(IWebDriver driver, string name, int? waitTimeout = null, params ElementLocator[] locators) : base(driver, name, waitTimeout, locators) { }

        internal bool InvalidatePageOnClick = true;

        internal Func<bool> BeforeClickAction = null;
        internal Func<bool> ClickValidationAction = null;
        internal Action AfterClickAction = null;

        internal Func<bool> BeforeDragNDropAction = null;
        internal Func<bool> DragNDropValidationAction = null;
        internal Action AfterDragNDropAction = null;

        internal ClickValidator[] ClickValidator { get; set; }
        internal ClickAction[] ClickAction { get; set; }

        /// <summary>   
        /// Performs click action over web element   
        /// </summary>   
        public void Click()
        {
            //if the element is not found on the page there is nothing to click and any further action is unsencible 
            if (!IsFound)
            {
                Report.AddError("Clicking " + GetType() + " " + Name, GetType() + " " + Name + " is displayed", GetType() + " " + Name + " is null", Driver.TakeScreenshot(GetType() + " " + Name + " is null"));
                return;
            }

            try
            {
                //if a method is assigned to this delegate the method is invoked 
                if (BeforeClickAction != null)
                    BeforeClickAction.Invoke();

                string pageHashCode = string.Empty;
                int numOfWindows = 0;
                var currentWindowHandles = Driver.WindowHandles;
                string pageTitle = string.Empty;
                string pageUrl = string.Empty;

                //if no criteria is provided for checking that click action has been performed, the following criteria are set by default 
                if (ClickValidator == null)
                    ClickValidator = new[] { Mapping.ClickValidator.AlertDisplayed, Mapping.ClickValidator.NumberOfWindowsChanged, Mapping.ClickValidator.PageHashCodeChanged, Mapping.ClickValidator.PageTitleChanged, Mapping.ClickValidator.PageUrlChanged };

                //when ClickValidator is initialized with default or custom values this block fill previously declared variables with appropriate values (if necessary). The values will be stored remembered for further comparison. Actually they keep state of a page(s) before clicking the element                  
                foreach (var v in ClickValidator)
                {
                    if (v == Mapping.ClickValidator.PageHashCodeChanged)
                        pageHashCode = Driver.GetPageHashCode();
                    else if (v == Mapping.ClickValidator.NumberOfWindowsChanged)
                        numOfWindows = Driver.WindowHandles.Count;
                    else if (v == Mapping.ClickValidator.PageTitleChanged)
                        pageTitle = Driver.Title;
                    else if (v == Mapping.ClickValidator.PageUrlChanged)
                        pageUrl = Driver.Url;
                }

                //you can assign to this delegate any other method but if it hasn’t been done the following anonymous method will be used for verification that some changes occurred on a page after click has been done. The method is based on those ClickValidator enumerators initialized above                  
                if (ClickValidationAction == null)
                    ClickValidationAction = delegate
                    {
                        foreach (var clickValidator in ClickValidator)
                        {
                            switch (clickValidator)
                            {
                                case Mapping.ClickValidator.AlertDisplayed:
                                    if (Driver.IsAlertPresent())
                                    {
                                        Report.AddInfo("Alert message is displayed", Driver.TakeScreenshot("Alert displayed"));
                                        return true;
                                    }
                                    break;
                                case Mapping.ClickValidator.NumberOfWindowsChanged:
                                    if (numOfWindows != Driver.WindowHandles.Count)
                                    {
                                        var windowsHandles = Driver.WindowHandles.Except(currentWindowHandles) as IList<string> ?? Driver.WindowHandles.Except(currentWindowHandles).ToList();
                                        string h = !windowsHandles.Any() ? Driver.WindowHandles.Last() : windowsHandles.Last();
                                        Driver.SwitchTo().Window(h);
                                        Report.AddInfo("Number of opened windows is changed. The driver is switched to the last opened window", string.Empty, Driver.TakeScreenshot("Driver is switched to another tab"));
                                        return true;
                                    }
                                    break;
                                case Mapping.ClickValidator.PageHashCodeChanged:
                                    if (pageHashCode != Driver.GetPageHashCode())
                                    {
                                        Report.AddInfo("Hash code of the page has changed");
                                        return true;
                                    }
                                    break;
                                case Mapping.ClickValidator.PageTitleChanged:
                                    if (pageTitle != Driver.Title)
                                    {
                                        Report.AddInfo("Title of the page has changed");
                                        return true;
                                    }
                                    break;
                                case Mapping.ClickValidator.PageUrlChanged:
                                    if (pageUrl != Driver.Url)
                                    {
                                        Report.AddInfo("Url address of the page has changed");
                                        return true;
                                    }
                                    break;
                            }
                        }
                        return false;
                    };

                //if none of click actions is specified the following click methods will be applied to the element by default                 
                if (ClickAction == null)
                    ClickAction = new[] { Mapping.ClickAction.Click, Mapping.ClickAction.MouseLbClick };

                //Performing click action                 
                foreach (var click in ClickAction)
                {
                    Report.AddInfo(string.Format("Clicking {0} {1}. ClickAction is {2}", GetType().Name, Name, click));
                    IMouse mouse;
                    ILocatable signinloc;
                    var builder = new Actions(Driver);
                    switch (click)
                    {
                        case Mapping.ClickAction.Click:
                            Element.Click();
                            break;
                        case Mapping.ClickAction.DoubleClick:
                            builder.DoubleClick().Build().Perform();
                            break;
                        case Mapping.ClickAction.Hover:
                            mouse = ((IHasInputDevices)Driver).Mouse;
                            signinloc = (ILocatable)Element;
                            mouse.MouseMove(signinloc.Coordinates);
                            break;
                        case Mapping.ClickAction.JsClick:
                            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click()", Element);
                            break;
                        case Mapping.ClickAction.MouseLbClick:
                            mouse = ((IHasInputDevices)Driver).Mouse;
                            signinloc = (ILocatable)Element;
                            mouse.MouseMove(signinloc.Coordinates);
                            mouse.Click(signinloc.Coordinates);
                            break;
                        case Mapping.ClickAction.MouseRbClick:
                            builder.MoveToElement(Element).ContextClick(Element).Build().Perform();
                            break;
                        case Mapping.ClickAction.SendKeyEnter:
                            Element.SendKeys(Keys.Enter);
                            break;
                        case Mapping.ClickAction.SendKeyReturn:
                            Element.SendKeys(Keys.Return);
                            break;
                        case Mapping.ClickAction.SendKeySpacebar:
                            Element.SendKeys(Keys.Space);
                            break;
                    }

                    Thread.Sleep(Globals.timeoutBetweenClicks);

                    if (ClickValidationAction.Invoke())
                    {
                        if (InvalidatePageOnClick)
                            WebApplication.IsValid = false;
                        break;
                    }
                }

                if (AfterClickAction != null)
                    AfterClickAction.Invoke();
                Report.AddInfo("Click action is performed successfully.", string.Empty, Driver.TakeScreenshot("Clicking " + GetType().Name + " " + Name));
            }
            catch (Exception ex)
            {
                Report.AddError("Clicking " + GetType().Name + " " + Name, "Click action is performed", ex.Message, Driver.TakeScreenshot("Clicking " + GetType().Name + " " + Name));
            }
        }

        public void DragNDrop(IWebElement target)
        {
            DragNDrop(target, 0, 0);
        }

        public void DragNDrop(WebElement target)
        {
            DragNDrop(target.Element, 0, 0);
        }

        public void DragNDrop(int x, int y)
        {
            DragNDrop(null, x, y);
        }

        /// <summary>   
        /// moves the element from its current position to new place on a page   
        /// </summary>   
        /// <param name="target">instance of IWebElement over which current element must be placed</param>   
        /// <param name="x">X coordinate of new position</param> 
        /// <param name="y">Y coordinate of new position</param> 
        private void DragNDrop(IWebElement target, int x, int y)
        {
            //if source element is not found there is nothing to drag and drop           
            if (!IsFound)
            {
                Report.AddError(MethodBase.GetCurrentMethod().Name + " error: failed to find web element", Driver.TakeScreenshot(GetType() + " " + Name + " is null"));
                return;
            }
            try
            {
                //if any method is assigned to this delegate the method is invoked      
                if (BeforeDragNDropAction != null)
                    BeforeDragNDropAction.Invoke();

                //these three delegates are assigned with three anonymous method for performing drag’n’drop action on web element   
                //method 1 moves source web element on specified distance by x and y axis. The distance is calculated as target point minus current element’s position by axis.                  
                Action method1 = () =>
                {
                    var builder = new Actions(Driver);
                    builder.ClickAndHold(Element).DragAndDropToOffset(Element, x - Element.Location.X, y - Element.Location.Y).Build().Perform();
                };
                //method 2 moves sources element to specified coordinates using mouse cursor instance   
                Action method2 = () =>
                {
                    var mouse = ((IHasInputDevices)Driver).Mouse;
                    var sourceElement = (ILocatable)Element;
                    var targetElement = (ILocatable)target;
                    mouse.MouseMove(sourceElement.Coordinates);
                    Thread.Sleep(500);
                    mouse.MouseDown(sourceElement.Coordinates);
                    mouse.MouseMove(targetElement.Coordinates);
                    Thread.Sleep(100);
                    mouse.MouseUp(targetElement.Coordinates);
                };
                //method 3 moves sources element over target element and then releases it                 
                Action method3 = () =>
                {
                    var builder = new Actions(Driver);
                    builder.ClickAndHold(Element).MoveToElement(target).Release(target).Build().Perform();
                };

                //one of the methods above is invoked in accordance method input parameters               
                if (target != null)
                {
                    Report.AddInfo("Trying to drag and drop " + GetType() + Name + " to coordinates " + target.Location.X + ", " + target.Location.Y + ". Attempt #1");
                    method3.Invoke();
                    if (!DragNDropValidationAction.Invoke())
                    {
                        Report.AddInfo("Trying to drag and drop " + GetType() + Name + " to coordinates " + target.Location.X + ", " + target.Location.Y + ". Attempt #2");
                        method2.Invoke();
                        if (!DragNDropValidationAction.Invoke())
                        {
                            Report.AddWarning("Failed to drag and drop " + GetType() + Name + " to coordinates " + target.Location.X + ", " + target.Location.Y, GetType().Name + " " + Name + " is moved to target coordinates", "DragNDropValidatinAction method returned false", Driver.TakeScreenshot("DragNDrop " + GetType().Name + "_" + Name));
                            return;
                        }
                    }
                }
                else
                {
                    Report.AddInfo("Trying to drag and drop " + GetType() + Name + " to coordinates " + x + ", " + y + ". Attempt #1");
                    method1.Invoke();
                    if (!DragNDropValidationAction.Invoke())
                    {
                        Report.AddWarning("Failed to drag and drop " + GetType() + Name + " to coordinates " + x + ", " + y, GetType().Name + " " + Name + " is moved to target coordinates", "DragNDropValidatinAction method returned false", Driver.TakeScreenshot("DragNDrop " + GetType().Name + " " + Name));
                        return;
                    }
                }

                if (AfterDragNDropAction != null)
                {
                    Report.AddInfo("AfterDragNDropAction  method is invoked");
                    AfterDragNDropAction.Invoke();
                }
            }
            catch (Exception ex)
            {
                Report.AddWarning("Failed to drag and drop " + GetType() + Name, "Drag and drop action is performed", ex.Message);
            }
        }
    }
}
