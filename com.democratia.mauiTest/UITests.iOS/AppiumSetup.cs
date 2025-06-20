using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using System.Diagnostics;

namespace UITests
{
    public class AppiumSetup : IDisposable
    {
        private static AppiumDriver? driver;

        public static string device = "ios";

        public static string sshSortie = string.Empty;

        public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver is null");

        public AppiumSetup()
        {
            if (SystemInfo.GetHostOS() != "macOS")
            {
                // TODO : à décommenter quand je serai connecté en ssh à un Mac et que le mac aura
                // installé Appium, dotnet et le projet
                //var sortie = RunAppiumIOSOverSSH("<macIp>", "<macUser>", "<macProjectDir>");
                //if (!sortie.Contains("Test Output")) throw new Exception($"Error starting Appium: {sortie}");
                //else sshSortie = sortie;

                return;
            }

            else
            {
                var iOSOptions = new AppiumOptions
                {
                    // Specify XCUITest as the driver, typically don't need to change this
                    AutomationName = "XCUITest",
                    // Always iOS for iOS
                    PlatformName = "iOS",
                    // iOS Version
                    PlatformVersion = "17.0",
                    // Don't specify if you don't want a specific device
                    DeviceName = "device",
                    // The full path to the .app file to test or the bundle id if the app is already installed on the device
                    App = "/path/to/project/com.democratia.view/bin/Debug/net9.0-ios/iossimulator-x64/com.democratia.view.app"
                };

                // Note there are many more options that you can use to influence the app under test according to your needs

                driver = new IOSDriver(iOSOptions);

            }

        }

        private static string RunAppiumIOSOverSSH(string macIp, string macUser, string macProjectDir)
        {
            var command = $"ssh {macUser}@{macIp} \"cd {macProjectDir} && nohup appium > appium.log 2>&1 & dotnet test\"";
            var process = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using var proc = Process.Start(process);
            proc?.WaitForExit();
            var output = proc?.StandardOutput.ReadToEnd();
            var error = proc?.StandardError.ReadToEnd();

            return !string.IsNullOrEmpty(error) ? $"Error:\n{error}" : $"Test Output:\n{output}";
        }

        public static void RunBeforeAnyTests()
        {

        }


        public void Dispose()
        {
            driver?.Quit();
        }
    }
}
