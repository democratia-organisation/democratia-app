using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System.Diagnostics;
using UITests.View;

namespace UITests
{
    public class AppiumSetup : IDisposable
    {
        private static AppiumDriver? driver;

        public readonly static string device = "android";

        public readonly static string sshSortie = string.Empty;

        public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver is null");

        public AppiumSetup()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (SystemInfo.SSHHost()) Environment.Exit(0);
            AppiumServerHelper.StartAppiumLocalServer();
            var androidOptions = new AppiumOptions
            {
                AutomationName = "UIAutomator2",
                PlatformName = "Android",
#if !DEBUG
                // App = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\..\com.democratia.view\bin\Debug\net10.0-android/com.democratia-Signed.apk")),
# endif
            };
#if DEBUG
            androidOptions.AddAdditionalAppiumOption(MobileCapabilityType.NoReset, "true");
            string activity = ResolveAppActivity("com.democratia");
            androidOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, activity);
            androidOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, "com.democratia");
            androidOptions.AddAdditionalAppiumOption(MobileCapabilityType.App, Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\..\com.democratia.view\bin\Debug\net10.0-android/com.democratia-Signed.apk")));
# endif
            try
            {
                driver = new AndroidDriver(androidOptions);
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("Could not find a connected Android device in 20000ms"))
                    throw new Exception("J'ai oublié d'allumer de connecter un appareil à l'ordinateur");

                else
                    throw new Exception(ex.Message, ex);

            }
        }

        private static string ResolveAppActivity(string packageName)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = $"shell cmd package resolve-activity --brief {packageName}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            string output = process?.StandardOutput.ReadToEnd().Trim() ?? string.Empty;

            // Expected format: com.democratia/crc64...MainActivity or com.democratia/.MainActivity
            var parts = output.Split('/');
            return parts.Length == 2 ? parts[1] : ".MainActivity"; // Fallback
        }


        public void Dispose()
        {
            driver?.Quit();
            // If an Appium server was started locally above, make sure we clean it up here
            AppiumServerHelper.DisposeAppiumLocalServer();
            GC.SuppressFinalize(this);
        }
    }
}