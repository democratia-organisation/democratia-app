using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Windows;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Xunit;

namespace UITests.View
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
        protected AppiumDriver App => AppiumSetup.App;

        public abstract void Dispose();

        // This could also be an extension method to AppiumDriver if you prefer
        protected AppiumElement? FindUIElement(string id)
        {
            try
            {
                if (App is WindowsDriver)
                {
                    return App.FindElement(MobileBy.AccessibilityId(id));
                }

                return App.FindElement(MobileBy.Id(id));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        protected ReadOnlyCollection<AppiumElement>? FindUIElements(string id)
        {
            if (App is WindowsDriver)
            {
                return App.FindElements(MobileBy.AccessibilityId(id)).Count > 0 ? App.FindElements(MobileBy.AccessibilityId(id)) : null;
            }

            return App.FindElements(MobileBy.Id(id)).Count > 0 ? App.FindElements(MobileBy.Id(id)) : null;
        }

        
    }
}