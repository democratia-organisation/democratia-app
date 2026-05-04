using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace UITests
{
    public class AppiumSetup : IDisposable
    {
        private static AppiumDriver? driver;

        public readonly static string device = "windows";
        public readonly static string appId = "com.democratia";

        public readonly static string sshSortie = string.Empty;

        public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver is null");

        public AppiumSetup()
        {
            
            AppiumServerHelper.StartAppiumLocalServer();
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var windowsOptions = new AppiumOptions
            {
                AutomationName = "windows",
                PlatformName = "Windows",
                App = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\..\..\com.democratia.view\bin\Debug\net10.0-windows10.0.19041.0\win-x64\com.democratia.view.exe"))
            }; 
            windowsOptions.AddAdditionalAppiumOption("unicodeKeyboard", true);
            windowsOptions.AddAdditionalAppiumOption("resetKeyboard", true);
            windowsOptions.AddAdditionalAppiumOption("appium:newCommandTimeout", 300);
            driver = new WindowsDriver(new Uri("http://127.0.0.1:4723/"), windowsOptions);
        }

        public void Dispose()
        {
            driver?.Quit();
            GC.SuppressFinalize(this);
            AppiumServerHelper.DisposeAppiumLocalServer();
        }
    }
}
