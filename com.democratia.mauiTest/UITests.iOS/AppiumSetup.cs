using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using UITests.View;

namespace UITests
{
    public class AppiumSetup : IDisposable
    {
        private static AppiumDriver? driver;
        public readonly static string device = "ios";

        // URL du serveur Appium sur le Mac Scaleway
        private const string MacIp = "51.159.121.53";
        private const string AppiumServerUrl = $"http://{MacIp}:4723/";

        public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver is null. V�rifiez que le serveur Appium tourne sur le Mac.");

        public AppiumSetup()
        {
            // Si on est sur Windows, on consid�re que le serveur Appium tourne d�j� sur le Mac distant
            if (SystemInfo.GetHostOS() != "macOS")
            {
                InitRemoteDriver();
            }
            else
            {
                // Cas o� le test roulerait directement sur le Mac
                InitLocalDriver();
            }
        }

        private void InitRemoteDriver()
        {
            var iOSOptions = new AppiumOptions
            {
                AutomationName = "XCUITest",
                PlatformName = "iOS",
                PlatformVersion = "26.2",
                DeviceName = "iPhone 16",
            };

            // UDID sp�cifique de votre simulateur identifi� pr�c�demment
            iOSOptions.AddAdditionalAppiumOption("udid", "74DF4917-44E5-4298-9791-7EA5220C48AF");
            iOSOptions.AddAdditionalAppiumOption("appium:derivedDataPath", "/Users/m1/Library/Developer/Xcode/DerivedData");
            iOSOptions.AddAdditionalAppiumOption("appium:noReset", false);


            // Chemin absolu de l'APP sur le disque du MAC (g�n�r� par votre build net10.0-ios)
            iOSOptions.App = "/Users/m1/apps/democratia_test.ipa";

            // �vite de r�installer l'application si elle est d�j� pr�sente
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
            // Version simplifi�e si vous lancez les tests directement depuis le terminal du Mac
            var iOSOptions = new AppiumOptions
            {
                AutomationName = "XCUITest",
                PlatformName = "iOS",
                PlatformVersion = "26.2",
                DeviceName = "iPhone 16",
                App = "/Users/m1/Documents/democratia/democratia-mobile/com.democratia.view/bin/Debug/net10.0-ios/iossimulator-arm64/com.democratia.view.app"
            };

            driver = new IOSDriver(new Uri("http://localhost:4723/"), iOSOptions);
        }

        /// <summary>
        /// M�thode utilitaire pour lancer Appium sur le Mac � distance via SSH depuis Windows
        /// </summary>
        public static string StartAppiumOnMacViaSSH(string macUser, string macPass)
        {
#if WINDOWS
    // Ce code ne sera compil� et ex�cut� QUE sur Windows
    var command = $"ssh {macUser}@51.159.121.26 \"nohup appium ... &\"";
    var process = new ProcessStartInfo
    {
        FileName = "cmd.exe",
        Arguments = $"/C {command}",
        UseShellExecute = false,
        CreateNoWindow = true
    };
    Process.Start(process);
    return "Commande envoy�e depuis Windows.";
#else
            // Sur iOS ou MacCatalyst, on ne fait rien ou on logue un message
            return "Lancement SSH non support� nativement depuis cette plateforme.";
#endif
        }
        public void Dispose()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}