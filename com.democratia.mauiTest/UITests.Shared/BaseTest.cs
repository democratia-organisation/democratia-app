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

    // Add all tests to the same collection as above so that the Appium server is only setup once
    [Collection("UITests")]
    public abstract class BaseTest : IDisposable
    {
        private static readonly int timeout = 15;
        protected AppiumDriver App => AppiumSetup.App;
        protected Func<string,By> funcResearrch => AppiumSetup.device == "android" ? id => MobileBy.XPath($"""//*[@resource-id="com.democratia:id/{id}"]""") : id => MobileBy.AccessibilityId(id);

        protected BaseTest() 
        {
        }

        // This could also be an extension method to AppiumDriver if you prefer
        protected AppiumElement? FindUIElement(string id)
        {
            
            try
            {
                return App.FindElement(funcResearrch(id));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        protected ReadOnlyCollection<AppiumElement>? FindUIElements(string id)
        {
            ReadOnlyCollection<AppiumElement> elements;
            try
            {
                elements = App.FindElements(funcResearrch(id));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            return elements.Count > 0 ? elements : null;
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
            return FindUIElement("monGroupeLabel") != null;
        }

        protected void AssertConnexion(string identifiant, string motDePasse)
        {
            Assert.True(SeConnecter(identifiant, motDePasse));
        }

        public void Dispose()
        {
            App.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}