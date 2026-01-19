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
        private const string MacProjectDir = "/Users/m1/Documents/democratia-mobile/com.democratia.mauiTest/UITests.macOS/";

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
                AutomationName = "mac2",
                PlatformName = "macOS", // "macOS" est la valeur standard attendue par Mac2Driver
                App = MacAppPath
            };

            // Ajoutez ceci pour éviter les erreurs de "Path" sur le Mac
            Environment.SetEnvironmentVariable("NODE_BINARY_PATH", "/opt/homebrew/bin/node");

            driver = new MacDriver(new Uri("http://127.0.0.1:4723/"), options, TimeSpan.FromSeconds(120));
        }
        private void RunTestsRemotelyOnMac()
        {
            string remoteResultsPath = $"/Users/m1/Documents/democratia-mobile/com.democratia.mauiTest/UITests.macOS/TestResults/results.trx";
            string localResultsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MacResults.trx");

            // 1. Commande pour lancer les tests et générer le fichier .trx
            var command = $"ssh {MacUser}@{MacIp} \"" +
                            "killall -9 node dotnet 2>/dev/null; sleep 2; " +
                            "/usr/bin/nohup /opt/homebrew/bin/appium --address 0.0.0.0 --use-drivers mac2 > /tmp/appium.log 2>&1 & " +
                            "echo 'Attente du serveur Appium...'; " +
                            "/bin/sleep 5 && /bin/bash -c 'until printf \"\" 2>>/dev/null >/dev/tcp/127.0.0.1/4723; do sleep 1; done'; " +
                            $"cd {MacProjectDir} && /Users/m1/Library/Caches/maui/PairToMac/SDKs/dotnet/dotnet test UITests.macos.csproj --logger 'trx;LogFileName=results.trx'\"";
            // 2. Commande pour rapatrier le fichier de résultat
            var copyCommand = $"scp {MacUser}@{MacIp}:{remoteResultsPath} \"{localResultsPath}\"";

            Console.WriteLine("--- EXÉCUTION DISTANTE ET GÉNÉRATION DE RAPPORT ---");

            // Execution des tests
            ExecuteCommand(command);

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