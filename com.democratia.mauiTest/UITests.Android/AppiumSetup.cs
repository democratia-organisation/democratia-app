using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System.Diagnostics;

namespace UITests
{
    public class AppiumSetup : IDisposable
    {
        private static AppiumDriver? driver;

        public static string device = "android";

        public static string sshSortie = string.Empty;

        public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver is null");

        public AppiumSetup()
        {
            // If you started an Appium server manually, make sure to comment out the next line
            // This line starts a local Appium server for you as part of the test run
            //AppiumServerHelper.StartAppiumLocalServer();
            var androidOptions = new AppiumOptions
            {
                // Specify UIAutomator2 as the driver, typically don't need to change this
                AutomationName = "UIAutomator2",
                // Always Android for Android
                PlatformName = "Android",

                // RELEASE BUILD SETUP
                // The full path to the .apk file
                // This only works with release builds because debug builds have fast deployment enabled
                // and Appium isn't compatible with fast deployment
                // App = Path.Join(TestContext.CurrentContext.TestDirectory, "../../../../MauiApp/bin/Release/net9.0-android/com.companyname.applicationname-Signed.apk"),
                // END RELEASE BUILD SETUP
            };

            // DEBUG BUILD SETUP
            // If you're running your tests against debug builds you'll need to set NoReset to true
            // otherwise appium will delete all the libraries used for Fast Deployment on Android
            // Release builds have Fast Deployment disabled
            // https://learn.microsoft.com/xamarin/android/deploy-test/building-apps/build-process#fast-deployment
            androidOptions.AddAdditionalAppiumOption(MobileCapabilityType.NoReset, "true");
            string activity = ResolveAppActivity("com.democratia");
            androidOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, activity);



            // END DEBUG BUILD SETUP


            // Specifying the avd option will boot the emulator for you
            // make sure there is an emulator with the name below
            // If not specified, make sure you have an emulator booted
            //androidOptions.AddAdditionalAppiumOption("avd", "pixel_5_-_api_33");

            // Note there are many more options that you can use to influence the app under test according to your needs

            try { driver = new AndroidDriver(androidOptions); }
            catch (Exception ex)
            {
                
                if (ex.Message.Contains("Could not find a connected Android device in 20000ms"))
                throw new Exception("J'ai oublié d'allumer de connecter un appareil à l'ordinateur");
                
                else
                    throw new Exception(ex.Message,ex);
                
            }
        }

        public static void RunBeforeAnyTests()
        {

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
        }
    }
}