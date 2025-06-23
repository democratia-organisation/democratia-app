using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Mac;
using System.Diagnostics;

namespace UITests
{
    public class AppiumSetup : IDisposable
    {
        private static AppiumDriver? driver;

        public static string device = "macos";

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
                var macOptions = new AppiumOptions
                {
                    // Specify mac2 as the driver, typically don't need to change this
                    AutomationName = "mac2",
                    // Always Mac for Mac
                    PlatformName = "Mac",
                    // The full path to the .app file to test
                    App = "/path/to/project/com.democratia.view/bin/Debug/net9.0-maccatalyst/maccatalyst-x64/com.democratia.view.app"
                };

                // Setting the Bundle ID is required, else the automation will run on Finder
                macOptions.AddAdditionalAppiumOption(IOSMobileCapabilityType.BundleId, "com.democratia");

                // Note there are many more options that you can use to influence the app under test according to your needs

                driver = new MacDriver(macOptions);
            }
        }

        public static string RunAppiumIOSOverSSH(string macIp, string macUser, string macProjectDir)
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