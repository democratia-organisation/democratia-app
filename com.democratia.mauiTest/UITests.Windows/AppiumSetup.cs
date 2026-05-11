using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace UITests
{
    public class AppiumSetup : IDisposable
    {
        private static AppiumDriver? driver;

        public readonly static string device = "windows";
        public readonly static string appId = "com.koyok.democratia";

        public readonly static string sshSortie = string.Empty;

        public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver is null");
        public AppiumOptions options;

        public AppiumSetup()
        {
            
            AppiumServerHelper.StartAppiumLocalServer();
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            options = new AppiumOptions
            {
                AutomationName = "windows",
                PlatformName = "Windows",
                App = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\..\..\com.koyok.democratia.view\bin\Debug\net10.0-windows10.0.19041.0\win-x64\com.koyok.democratia.view.exe"))
            };
            options.AddAdditionalAppiumOption("unicodeKeyboard", true);
            options.AddAdditionalAppiumOption("resetKeyboard", true);
            options.AddAdditionalAppiumOption("appium:newCommandTimeout", 300);
        }

        public AppiumDriver CreatePage()
        {
            return new WindowsDriver(new Uri("http://127.0.0.1:4723/"), options);
        }

        public void Dispose()
        {
            driver?.Quit();
            GC.SuppressFinalize(this);
            AppiumServerHelper.DisposeAppiumLocalServer();
        }
    }
}
