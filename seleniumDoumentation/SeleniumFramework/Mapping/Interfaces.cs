using Mapping.WebElements; 
using OpenQA.Selenium; 
using OpenQA.Selenium.Support.UI;

namespace Mapping
{
    /// <summary>   
    /// Describes a web page   
    /// </summary>   
    public interface IWebPage
    {
        string Name { get; set; } //name of a page as you call it      
        string Title { get; } //page title      
        string Url { get; } //page address      
        bool IsLoaded { get; } //indicates if a page is completely loaded   
    }

    /// <summary>   
    /// defines names of conditions which are used for click action result verification   
    /// </summary>     
    public enum ClickValidator
    {
        AlertDisplayed,
        NumberOfWindowsChanged,
        PageHashCodeChanged,
        PageUrlChanged,
        PageTitleChanged
    }

    /// <summary>   
    /// names of click actions used for interaction with web elements   
    /// </summary>   
    public enum ClickAction
    {
        Click, DoubleClick,
        SendKeyEnter, SendKeyReturn,
        SendKeySpacebar,
        MouseLbClick,
        MouseRbClick,
        JsClick,
        Hover
    }

    /// <summary>   
    /// web element which may be clicked   
    /// </summary>        
    interface IClickableElement
    {
        void Click();
        void DragNDrop(IWebElement target);
        void DragNDrop(WebElement target);
        void DragNDrop(int x, int y);
    }

    /// <summary>   
    /// web button   
    /// </summary>   
    interface IButton : IClickableElement
    {
        string Text { get; }
        string Tooltip { get; }
    }

    /// <summary>   
    /// Link   
    /// </summary>   
    interface ILink : IButton
    {
        string Href { get; }
    }

    /// <summary>   
    /// checkbox   
    /// </summary>   
    interface ICheckbox
    {
        string Tooltip { get; }
        bool IsSelected { get; }
        void Select();
        void Deselect();
    }

    /// <summary>   
    /// dropdown list   
    /// </summary>    	   	
    interface IDropdownList : IClickableElement
    {
        SelectElement SelectElement { get; set; }
        void SelectByText(string text);
        void SelectByIndex(int index);
        void SelectByValue(string value);
        void SelectByTextPart(string text);
        void SelectMultipleOptions(params string[] texts);
        void SelectMultipleOptions(params int[] indexes);
    }

    /// <summary>   
    /// image (picture), imagebutton, imagelink   
    /// </summary>   
    interface IImage : IClickableElement
    {
        string Tooltip { get; }
        string Src { get; }
    }

    /// <summary>   
    /// label, plain text   
    /// </summary>   
    interface ILabel
    {
        string Text { get; }
    }

    /// <summary>   
    /// textbox     
    /// </summary>   
    interface ITextbox
    {
        string Text { get; }
        void Clear();
        void AppendText(string text);
        void TypeText(string text);
    }

    public class ElementLocator
    {
        internal object[] Frames;
        internal By Locator;

        internal ElementLocator(By locator)
        {
            Locator = locator;
        }

        internal ElementLocator(object[] frames, By locator) : this(locator)
        {
            Frames = frames;
        }
    }
}
