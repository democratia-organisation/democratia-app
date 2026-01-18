using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using System.Diagnostics;
using UITests.View;

namespace UITests
{
    public class AppiumSetup : IDisposable
    {
        private static AppiumDriver? driver;
        public readonly static string device = "ios";

        // URL du serveur Appium sur le Mac Scaleway
        private const string MacIp = "51.159.121.26";
        private const string AppiumServerUrl = $"http://{MacIp}:4723/";

        public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver is null. Vérifiez que le serveur Appium tourne sur le Mac.");

        public AppiumSetup()
        {
            // Si on est sur Windows, on considère que le serveur Appium tourne déjà sur le Mac distant
            if (SystemInfo.GetHostOS() != "macOS")
            {
                InitRemoteDriver();
            }
            else
            {
                // Cas où le test roulerait directement sur le Mac
                InitLocalDriver();
            }
        }

        private void InitRemoteDriver()
        {
            var iOSOptions = new AppiumOptions
            {
                AutomationName = "XCUITest",
                PlatformName = "iOS",
                PlatformVersion = "18.4",
                DeviceName = "iPhone 16",
            };

            // UDID spécifique de votre simulateur identifié précédemment
            iOSOptions.AddAdditionalAppiumOption("udid", "74DF4917-44E5-4298-9791-7EA5220C48AF");

            // Chemin absolu de l'APP sur le disque du MAC (généré par votre build net10.0-ios)
            iOSOptions.App = "/Users/m1/Library/Caches/Xamarin/mtbs/builds/com.democratia.view/bin/Debug/net10.0-ios/iossimulator-arm64/com.democratia.view.app";

            // Évite de réinstaller l'application si elle est déjà présente
            iOSOptions.AddAdditionalAppiumOption("noReset", true);

            try
            {
                // On se connecte au serveur Appium distant
                driver = new IOSDriver(new Uri(AppiumServerUrl), iOSOptions, TimeSpan.FromSeconds(120));
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de se connecter au serveur Appium sur {AppiumServerUrl}. Erreur: {ex.Message}");
            }
        }

        private void InitLocalDriver()
        {
            // Version simplifiée si vous lancez les tests directement depuis le terminal du Mac
            var iOSOptions = new AppiumOptions
            {
                AutomationName = "XCUITest",
                PlatformName = "iOS",
                PlatformVersion = "18.4",
                DeviceName = "iPhone 16",
                App = "/Users/m1/Library/Caches/Xamarin/mtbs/builds/com.democratia.view/bin/Debug/net10.0-ios/iossimulator-arm64/com.democratia.view.app"
            };

            driver = new IOSDriver(new Uri("http://localhost:4723/"), iOSOptions);
        }

        /// <summary>
        /// Méthode utilitaire pour lancer Appium sur le Mac à distance via SSH depuis Windows
        /// </summary>
        public static string StartAppiumOnMacViaSSH(string macUser, string macPass)
        {
            // Commande pour lancer appium en tâche de fond sur le Mac
            // Note: Nécessite que 'appium' soit dans le PATH du Mac
            var command = $"ssh {macUser}@{MacIp} \"nohup appium --address 0.0.0.0 --port 4723 --allow-insecure chromedriver_autodownload > appium.log 2>&1 &\"";

            var process = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            using var proc = Process.Start(process);
            return "Commande de lancement Appium envoyée au Mac.";
        }

        public void Dispose()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}