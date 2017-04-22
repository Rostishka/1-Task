using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace GmailUITest
{
    public class PropertiesCollection
    {
        public static IWebDriver driver = new FirefoxDriver();
        public static string myEmail;
        public static string myPassword;
        public static bool messageStatusRecieved;
        public static string txtmessageSended;
    }
}
