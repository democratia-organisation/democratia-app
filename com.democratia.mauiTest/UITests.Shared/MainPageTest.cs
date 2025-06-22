using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
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
    
    public class MainPageTest : BaseTest
    {
        public MainPageTest()
        {
            if(AppiumSetup.device == "windows") AppiumSetup.RunBeforeAnyTests();
            
            if (SystemInfo.SSHHost())
            {
                // TODO : à décommenter quand je serai connecté en ssh à un Mac et que le mac aura
                // installé Appium, dotnet et le projet
                // string sortie = AppiumSetup.sshSortie;
                // TODO interpréter la sortie pour l'affichier sur l'explorateur de test
                return;
            }
        }
        
        [Fact]
        public void PresenceDesEntriesTest()
        {
            if(SystemInfo.SSHHost()) return;
            ReadOnlyCollection<AppiumElement> entries = FindUIElements("Entry");
            ReadOnlyCollection<AppiumElement> labels = FindUIElements("Label");
            Assert.Equal(2,entries.Count);
            Assert.Equal(2, labels.Count);
            Assert.Equal("Adresse mail", labels[0].Text);
            Assert.Equal("Mot de passe", labels[1].Text);
        }

        [Fact]
        public void NavigationPage()
        {
            if (SystemInfo.SSHHost()) return;
            

            
            App.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
            AppiumElement seConecterButton = FindUIElement("Se connecter Button");
            ReadOnlyCollection<AppiumElement> entries = FindUIElements("Entry");
            entries[0].Clear();
            entries[1].Clear();

            // TODO : comprendre le problème de sendkeys qui tape les lettres en qwerty afin 
            // de les avoir en azerty quand la fonction est appelé
            entries[0].SendKeys(";odqdqry56@g;qil<co;");
            entries[1].SendKeys("Djonodo20050207/");
            seConecterButton.Click();

            Assert.Equal("HomePage", FindUIElement("HomePage").Text);
        }

        
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
}