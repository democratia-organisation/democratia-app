using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Mac;
using System.Diagnostics;

namespace UITests
{
    public class AppiumSetup : IDisposable
    {
        private static AppiumDriver? driver;
        public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver est null.");
        public static string device = "macos";

        // Configuration du Mac Scaleway
        private const string MacIp = "51.159.121.26";
        private const string MacUser = "m1";
        // Le chemin vers votre app macOS (.app Catalyst ou native)
        private const string MacAppPath = "/Users/m1/Documents/democratia-mobile/com.democratia.view/bin/Release/net10.0-maccatalyst/maccatalyst-arm64/com.democratia.view.app";
        private const string MacProjectDir = "/Users/m1/Documents/democratia-mobile/com.democratia.mauiTest/UITests.macOS";

        public AppiumSetup()
        {
            if (OperatingSystem.IsWindows())
            {
                // On délègue au Mac
                RunTestsRemotelyOnMac();
            }
            else
            {
                // On exécute localement sur l'interface macOS
                InitLocalMacDriver();
            }
        }
        private void InitLocalMacDriver()
        {
            var options = new AppiumOptions
            {
                AutomationName = "mac2", // Indispensable pour macOS
                PlatformName = "Mac",
                App = MacAppPath
            };

            // Le BundleId est souvent requis pour macOS pour éviter de piloter le Finder par erreur
            options.AddAdditionalAppiumOption("bundleId", "com.democratia.view");
            options.AddAdditionalAppiumOption("appium:waitForAppLaunch", "30");

            // Pour macOS, le driver spécifique est MacDriver
            driver = new MacDriver(new Uri("http://localhost:4723/"), options, TimeSpan.FromSeconds(120));
        }
        private void RunTestsRemotelyOnMac()
        {
            string remoteResultsPath = $"{MacProjectDir}/TestResults/results.trx";
            string localResultsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MacResults.trx");

            // 1. Commande pour lancer les tests et générer le fichier .trx
            var testCommand = $"ssh {MacUser}@{MacIp} \"pgrep appium || nohup appium --address 0.0.0.0 > /tmp/appium.log 2>&1 & sleep 5 && cd {MacProjectDir} && /Users/m1/Library/Caches/maui/PairToMac/SDKs/dotnet/dotnet test --logger 'trx;LogFileName=results.trx'\"";

            // 2. Commande pour rapatrier le fichier de résultat
            var copyCommand = $"scp {MacUser}@{MacIp}:{remoteResultsPath} \"{localResultsPath}\"";

            Console.WriteLine("--- EXÉCUTION DISTANTE ET GÉNÉRATION DE RAPPORT ---");

            // Execution des tests
            ExecuteCommand(testCommand);

            // Récupération du rapport
            Console.WriteLine("--- RÉCUPÉRATION DU RAPPORT DE TEST ---");
            ExecuteCommand(copyCommand);

            Console.WriteLine($"Rapport disponible localement : {localResultsPath}");
            Process.Start(new ProcessStartInfo(localResultsPath) { UseShellExecute = true });
        }

        private void ExecuteCommand(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C {command}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            while (process != null && !process.StandardOutput.EndOfStream)
            {
                Console.WriteLine(process.StandardOutput.ReadLine());
            }
            process?.WaitForExit();
        }

        public void Dispose()
        {
            driver?.Quit();
            driver?.Dispose();
        }
    }
}