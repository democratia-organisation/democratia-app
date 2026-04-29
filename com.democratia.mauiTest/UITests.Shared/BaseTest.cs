using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Xunit;

namespace UITests.UI
{
    // Add a CollectionDefinition together with a ICollectionFixture
    // to ensure that the setup only runs once
    // xUnit does not have a built-in concept of a fixture that only runs once for the whole test set.
    [CollectionDefinition("UITests")]
    public sealed class UITestsCollectionDefinition : ICollectionFixture<AppiumSetup>
    {

    }

    public class SystemInfo
    {
        public static string GetHostOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return "Windows";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return "macOS";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return "Linux";
            return "Unknown OS";
        }

        public static bool SSHHost() => (AppiumSetup.device == "ios" || AppiumSetup.device == "macos") && SystemInfo.GetHostOS() == "Windows";
    }

    public static class WebDriverExtensions
    {
        private static WebDriverWait? wait;
        extension(IWebDriver driver)
        {
            
            public IWebElement WaitAndFind(By by, int timeoutInSeconds = 15)
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }

            public ReadOnlyCollection<IWebElement> WaitAndFinds(By by, int timeoutInSeconds = 15)
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElements(by));
            }
        }
    }



    // Add all tests to the same collection as above so that the Appium server is only setup once
    [Collection("UITests")]
    public abstract class BaseTest : IDisposable
    {
        private static readonly int timeout = 60;
        protected AppiumDriver App => AppiumSetup.App;
        protected Func<string,By> funcResearrch => AppiumSetup.device == "android" ? id => MobileBy.XPath($"""//*[@resource-id="com.democratia:id/{id}"]""") : id => MobileBy.AccessibilityId(id);

        public virtual void Dispose()
        {
            App?.Dispose();
            GC.SuppressFinalize(this);
        }

        // This could also be an extension method to AppiumDriver if you prefer
        protected AppiumElement? FindUIElement(string id)
        {
            
            try
            {
                return App.WaitAndFind(funcResearrch(id),timeout) as AppiumElement;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        protected ReadOnlyCollection<AppiumElement>? FindUIElements(string id)
        {
            ReadOnlyCollection<IWebElement> elements;
            try
            {
                elements = App.WaitAndFinds(funcResearrch(id), timeout);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            return elements.Cast<AppiumElement>().ToList().AsReadOnly().Count > 0 ? elements.Cast<AppiumElement>().ToList().AsReadOnly() : null;
        }

        protected bool SeConnecter(string identifiant, string motDePasse)
        {
            AppiumElement? seConecterButton = FindUIElement("seConnecterButton");
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            foreach (var entry in entries!) entry.Clear();
            var (adresseMailEntry, motDePasseEntry) = (entries?[0], entries?[1]);
            adresseMailEntry?.SendKeys(identifiant);
            motDePasseEntry?.SendKeys(motDePasse);
            seConecterButton?.Click();
            return FindUIElement("homePage") != null;   
        }

        protected void AssertConnexion(string identifiant, string motDePasse)
        {
            Assert.True(SeConnecter(identifiant, motDePasse));
        }
    }
}