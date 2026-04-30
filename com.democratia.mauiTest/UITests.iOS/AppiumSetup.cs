using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace UITests
{
    public class AppiumSetup : IDisposable
    {
        private static AppiumDriver? driver;
        public readonly static string device = "ios";
        public readonly static string appId = "com.companyname.com.democratia.view";

        public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver is null. V�rifiez que le serveur Appium tourne sur le Mac.");

        public AppiumSetup()
        {
            InitDriver();
        }

        private void InitDriver()
        {
            AppiumServerHelper.StartAppiumLocalServer();
            var iOSOptions = new AppiumOptions
            {
                AutomationName = "XCUITest",
                PlatformName = "iOS",
                PlatformVersion = "26.2",
                DeviceName = "iPhone 16",
            };

            try
            {
                driver = new IOSDriver(new Uri("http://127.0.0.1:4724/"), iOSOptions, TimeSpan.FromSeconds(120));
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de se connecter au serveur Appium sur {"http://127.0.0.1:4724/"}. Erreur: {ex.Message}");
            }
        }

        public void Dispose()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}