using Logger;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Mapping.WebElements
{
    public class WebTextbox : WebElement, ITextbox
    {
        public WebTextbox(IWebDriver driver, string name, IWebElement element) : base(driver, name, element) { }
        public WebTextbox(IWebDriver driver, string name, int? waitTimeout = null, params ElementLocator[] locators) : base(driver, name, waitTimeout, locators) { }

        internal Action BeforeTypingAction = null;
        public string Text
        {
            get
            {
                if (!IsFound)
                    Report.AddWarning("Failed to get text value of " + GetType() + Name, GetType() + Name + " is displayed", GetType() + Name + " is not displayed", Driver.TakeScreenshot(GetType() + Name + "is null"));
                string text = Element.Text;
                if (!string.IsNullOrEmpty(text))
                    return text;
                try
                {
                    text = Element.GetAttribute("value");
                }
                catch
                {
                    return string.Empty;
                }
                return text;
            }
        }

        public void Clear()
        {
            Element.Clear();
        }

        public void AppendText(string text)
        {
            string initialText = Text;
            string newText = initialText + text;
            if (BeforeTypingAction != null)
            {
                Report.AddInfo("BeforeTypingAction method is invoked", string.Empty, string.Empty);
                BeforeTypingAction.Invoke();
            }
            Element.Clear();
            Element.SendKeys(newText);
            if (Text.Equals(text))
            {
                Report.AddInfo("Type text into " + GetType().Name + " " + Name, "Inner element's text value is equal to expected, Text value: " + Text + ", expected value: " + newText, Driver.TakeScreenshot("Type text into " + GetType().Name + " " + Name));
                return;
            }
            Report.AddInfo("Inserting text into " + GetType().Name + " " + Name + " using JavaScript", string.Empty, string.Empty);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].value = arguments[1]", Element, newText);
            Thread.Sleep(Globals.timeoutBetweenClicks);
            if (Text.Equals(newText))
            {
                Report.AddInfo("Type text into " + GetType().Name + " " + Name, "Inner element's text value is equal to expected, Text value: " + Text + ", expected value: " + text, Driver.TakeScreenshot("Type text into " + GetType().Name + " " + Name));
                return;
            }
            Report.AddWarning("Type text into " + GetType().Name + " " + Name, "Inner element's text value is not equal to expected", "Text value: " + Text + ", expected value: " + text, Driver.TakeScreenshot("Type text into " + GetType().Name + " " + Name));
        }

        public void TypeText(string text)
        {
            Clear();
            AppendText(text);
        }
    }
}
