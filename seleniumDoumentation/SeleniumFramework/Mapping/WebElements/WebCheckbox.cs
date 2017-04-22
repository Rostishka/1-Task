using System;
using Logger;
using OpenQA.Selenium;

namespace Mapping.WebElements
{
    public class WebCheckbox : WebElement, ICheckbox
    {
        public WebCheckbox(IWebDriver driver, string name, IWebElement element) : base(driver, name, element) { }
        public WebCheckbox(IWebDriver driver, string name, int? waitTimeout = null, params ElementLocator[] locators) : base(driver, name, waitTimeout, locators) { }

        public string Tooltip
        {
            get
            {
                if (IsFound && IsDisplayed)
                {
                    try
                    {
                        var tooltip = Element.GetAttribute("title");
                        return tooltip;
                    }
                    catch (Exception ex)
                    {
                        Report.AddWarning("Getting element tooltip", "Value of attribute 'title' is read", "Getting web element attribute 'title' throws the exception: " + ex.Message);
                    }
                }
                return string.Empty;
            }
        }

        public bool IsSelected
        {
            get
            {
                if (!IsDisplayed)
                {
                    Report.AddWarning("Checkbox " + Name + " is not displayed", screenshotPath: Driver.TakeScreenshot("Checkbox " + Name + " is not displayed"));
                    return false;
                }
                try
                {
                    return Element.Selected;
                }
                catch (Exception ex)
                {
                    Report.AddWarning("Getting checbox selected property threw the following exception: " + ex.Message);
                }
                return false;
            }
        }

        public void Select()
        {
            if (!IsSelected)
            {
                //private button is created with the same parameters as this element is. The button has Click() method and the mathod is used for clicking the checkbox.   
                //So, btn as well as this element are referred to the same element on a page. Is like different representations of the same object.   
                WebButton btn = new WebButton(Driver, Name, Element);
                //btn is clicked with only sending Spacebar key code to the element. Similar as you manually put the element in the focus and hit space bar                 
                btn.ClickAction = new[] { Mapping.ClickAction.SendKeySpacebar };
                //Click action is considered done only whet the element is selected. Note that Click() mathos is related to btn element and IsSelected is related to this element, but indeed btn and this are the same element, so the actions are correct                 
                btn.ClickValidationAction = () => IsSelected;
                btn.Click();
            }
        }

        public void Deselect()
        {
            if (IsSelected)
            {
                WebButton btn = new WebButton(Driver, Name, Element);
                btn.ClickAction = new[] { Mapping.ClickAction.SendKeySpacebar };
                btn.ClickValidationAction = () => !IsSelected;
                btn.Click();
            }
        }
    }
}
