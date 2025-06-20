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
        }
        
        [Fact]
        public void AppLaunches()
        {
            
            if((AppiumSetup.device=="ios" || AppiumSetup.device == "macos") && SystemInfo.GetHostOS()=="Windows")
            {
                // TODO : à décommenter quand je serai connecté en ssh à un Mac et que le mac aura
                // installé Appium, dotnet et le projet
                // string sortie = AppiumSetup.sshSortie;
                // TODO interpréter la sortie pour l'affichier sur l'explorateur de test
                return;
                
            }
            App.GetScreenshot().SaveAsFile($"{nameof(AppLaunches)}.png");
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
    }
}