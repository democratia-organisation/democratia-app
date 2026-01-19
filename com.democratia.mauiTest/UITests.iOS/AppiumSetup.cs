using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using System;
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
            iOSOptions.AddAdditionalAppiumOption("appium:derivedDataPath", "/Users/m1/Library/Developer/Xcode/DerivedData");
            iOSOptions.AddAdditionalAppiumOption("appium:noReset", false);


            // Chemin absolu de l'APP sur le disque du MAC (généré par votre build net10.0-ios)
            iOSOptions.App = "/Users/m1/apps/democratia_test.ipa";

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
                App = "/Users/m1/apps/democratia_test.ipa"
            };

            driver = new IOSDriver(new Uri("http://localhost:4723/"), iOSOptions);
        }

        /// <summary>
        /// Méthode utilitaire pour lancer Appium sur le Mac à distance via SSH depuis Windows
        /// </summary>
        public static string StartAppiumOnMacViaSSH(string macUser, string macPass)
        {
#if WINDOWS
    // Ce code ne sera compilé et exécuté QUE sur Windows
    var command = $"ssh {macUser}@51.159.121.26 \"nohup appium ... &\"";
    var process = new ProcessStartInfo
    {
        FileName = "cmd.exe",
        Arguments = $"/C {command}",
        UseShellExecute = false,
        CreateNoWindow = true
    };
    Process.Start(process);
    return "Commande envoyée depuis Windows.";
#else
            // Sur iOS ou MacCatalyst, on ne fait rien ou on logue un message
            return "Lancement SSH non supporté nativement depuis cette plateforme.";
#endif
        }
        public void Dispose()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}