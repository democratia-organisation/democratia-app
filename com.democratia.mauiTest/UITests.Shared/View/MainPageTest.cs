using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Xunit;

// You will have to make sure that all the namespaces match
// between the different platform specific projects and the shared
// code files. This has to do with how we initialize the AppiumDriver
// through the AppiumSetup.cs files and NUnit SetUpFixture attributes.
// Also see: https://docs.nunit.org/articles/nunit/writing-tests/attributes/setupfixture.html
namespace UITests.View
{
    // This is an example of tests that do not need anything platform specific.
    // Typically you will want all your tests to be in the shared project so they are ran across all platforms.
    [Collection("UITests")]
    [DisplayName("Page d'accueil")]
    public class MainPageTest : BaseTest
    {
        // /!\ écrire les paramètres de SendKeys en qwerti, afin que les caractères utf-8 interprétés
        // par C# puis envoyé dans le driver soit les bons
        // /!\ on ne teste que la présence d'un élément UI car la présence du comtenu du texte est déjà
        // testé dans les test du vue modèle

        [Fact(DisplayName = "Test de la présence des éléments dans la page")]
        public void PresenceDesEntriesTest()
        {

            if (SystemInfo.SSHHost()) return;
            Debug.WriteLine(App.PageSource);
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            ReadOnlyCollection<AppiumElement>? labels = FindUIElements("Label");
            var nombresEntrees = 2;

            var nombresLabels = labels?.Count;
            var nombresEntries = entries?.Count;

            Assert.Equal(nombresEntrees, nombresLabels);
            Assert.Equal(nombresEntrees, nombresEntries);
        }

        [Fact(DisplayName = "Test de la navigation vers la page home")]
        public void NavigationPageTest()
        {


            if (SystemInfo.SSHHost()) return;
            Debug.WriteLine(App.PageSource);
            AppiumElement? seConecterButton = FindUIElement("Se connecter Button");
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            foreach (var entry in entries!) entry.Clear();
            var (adresseMailEntry, motDePasseEntry) = (entries?[0], entries?[1]);
            adresseMailEntry?.Clear();
            motDePasseEntry?.Clear();
            // /!\ important afin de laisser le temps d'arriver la page HomePage
            if (AppiumSetup.device != "android")
                App.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            adresseMailEntry?.SendKeys("example@gmail.com");
            motDePasseEntry?.SendKeys("MotDePasse123/");
            seConecterButton?.Click();

            Assert.NotNull(FindUIElement("ProfileButton"));
        }


        [Theory(DisplayName = "Test de la page en cas d'entrée incorrecte")]
        [InlineData("fezfzfzefz", "Djonodo20050207/")]
        [InlineData("example@gmail.com", "Djonodo20050207/erreur")]
        [InlineData("", "")]
        [InlineData("dadadzadzada", "")]
        public void NavigationPageErrorTest(string adresseMail, string motDePasse)
        {

            if (SystemInfo.SSHHost()) return;
            AppiumElement? seConecterButton = FindUIElement("Se connecter Button");
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            foreach (var entry in entries!) entry.Clear();
            var (adresseMailEntry, motDePasseEntry) = (entries?[0], entries?[1]);
            adresseMailEntry?.Clear();
            motDePasseEntry?.Clear();

            adresseMailEntry?.SendKeys(adresseMail);
            motDePasseEntry?.SendKeys(motDePasse);
            seConecterButton?.Click();

            Assert.NotNull(FindUIElement("Error message"));

        }
    }
}