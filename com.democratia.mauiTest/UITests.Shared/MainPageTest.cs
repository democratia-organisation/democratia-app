using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Xunit;

// You will have to make sure that all the namespaces match
// between the different platform specific projects and the shared
// code files. This has to do with how we initialize the AppiumDriver
// through the AppiumSetup.cs files and NUnit SetUpFixture attributes.
// Also see: https://docs.nunit.org/articles/nunit/writing-tests/attributes/setupfixture.html
namespace UITests
{
    // This is an example of tests that do not need anything platform specific.
    // Typically you will want all your tests to be in the shared project so they are ran across all platforms.
    
    public class MainPageTest : BaseTest, IClassFixture<MainPageTestFixture>
    {
        public MainPageTest()
        {
            if(AppiumSetup.device == "windows") AppiumSetup.RunBeforeAnyTests();
            
            if (SystemInfo.SSHHost()) return;
            
        }
        
        [Fact]
        public void PresenceDesEntriesTest()
        {
            


            ReadOnlyCollection<AppiumElement> entries = FindUIElements("Entry");
            ReadOnlyCollection<AppiumElement> labels = FindUIElements("Label");
            
            var nombresEntrees = 2;

            var nombresLabels = labels.Count;
            var nombresEntries = entries.Count;

            Assert.Equal(nombresEntrees, nombresLabels);
            Assert.Equal(nombresEntrees, nombresEntries);
            Assert.Equal("Adresse mail", labels[0].Text);
            Assert.Equal("Mot de passe", labels[1].Text);
        }

        [Fact]
        public void NavigationPageTest()
        {
            

            Debug.WriteLine(App.PageSource);
            AppiumElement seConecterButton = FindUIElement("Se connecter Button");
            ReadOnlyCollection<AppiumElement> entries = FindUIElements("Entry");
            AppiumElement adresseMailEntry = entries[0];
            AppiumElement motDePasseEntry = entries[1];
            adresseMailEntry.Clear();
            motDePasseEntry.Clear();
            // /!\ important afin de laisser le temps d'arriver la page HomePage
            if(AppiumSetup.device!="android")
                App.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // TODO : écrire les paramètres de SendKeys en qwerti, afin que les caractères utf-8 interprétés
            // par C# puis envoyé dans le driver soit les bons 
            adresseMailEntry.SendKeys("modadary56@gmail.com");
            motDePasseEntry.SendKeys("Djonodo20050207/");
            seConecterButton.Click();

            Assert.NotNull(FindUIElement("HomePage"));
        }

        [Theory]
        [InlineData("fezfzfzefz", "Djonodo20050207/")]
        [InlineData("modadary56@gmail.com", "Djonodo20050207/erreur")]
        [InlineData("", "")]
        [InlineData("dadadzadzada", "")]
        public void NavigationPageErrorTest(string adresseMail,string motDePasse)
        {
            


            Debug.WriteLine(App.PageSource);
            AppiumElement seConecterButton = FindUIElement("Se connecter Button");
            ReadOnlyCollection<AppiumElement> entries = FindUIElements("Entry");
            AppiumElement adresseMailEntry = entries[0];
            AppiumElement motDePasseEntry = entries[1];
            adresseMailEntry.Clear();
            motDePasseEntry.Clear();
            var wait = new WebDriverWait(AppiumSetup.App, TimeSpan.FromSeconds(3));

            // /!\ écrire les paramètres de SendKeys en qwerti, afin que les caractères utf-8 interprétés
            // par C# puis envoyé dans le driver soit les bons 
            adresseMailEntry.SendKeys(adresseMail);
            motDePasseEntry.SendKeys(motDePasse);
            seConecterButton.Click();
            if(AppiumSetup.device!="windows")
            {
                AppiumElement? button = wait.Until(d => FindUIElement("OK"));
                button.Click();
                Assert.NotNull(FindUIElement("ConnexionPage"));
            }

            
        }

        public override void Dispose() => App.Dispose();
        
    }

    public class SystemInfo
    {
        public static string GetHostOS()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return "Windows";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return "macOS";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return "Linux";
            return "Unknown OS";
        }

        public static bool SSHHost() => (AppiumSetup.device == "ios" || AppiumSetup.device == "macos") && SystemInfo.GetHostOS() == "Windows";
    }

    public class MainPageTestFixture : IDisposable
    {
        public MainPageTestFixture()
        {
            // TODO : à décommenter quand je serai connecté en ssh à un Mac et que le mac aura
            // installé Appium, dotnet et le projet
            //if (SystemInfo.SSHHost())
            //{
            //   string output = AppiumSetup.RunAppiumIOSOverSSH("macIP","macUser","acProjectDir");
            //    if (!output.Contains("Error"))
            //    {
            //        // TODO : 
            //        // - créer un fichier tsx à partir de la sortie de la commande
            //    }
            //}
               
        }

        public void Dispose()
        {
            // AfterAllTestsInClass
        }
    }
}