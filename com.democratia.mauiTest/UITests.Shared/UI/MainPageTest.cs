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
namespace UITests.UI
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
            Assert.NotNull(FindUIElement("connexionLabel")!);
            Assert.NotNull(FindUIElement("mailEntryComponent")!);
            Assert.NotNull(FindUIElement("passwordEntryComponent")!);
            Assert.NotNull(FindUIElement("pasDeCompteLabel")!);
            Assert.NotNull(FindUIElement("creerCompteLabel")!);
            Assert.NotNull(FindUIElement("seConnecterButton")!);
        }

        [Fact(DisplayName = "Test de la navigation vers la page home")]
        public void NavigationPageTest()
        {
            AssertConnexion("sophie.lemoine@example.com", "root");
        }


        [Theory(DisplayName = "Test de la page en cas d'entrée incorrecte")]
        [InlineData("fezfzfzefz", "Djonodo20050207/")]
        [InlineData("example@gmail.com", "Djonodo20050207/erreur")]
        [InlineData("", "")]
        [InlineData("dadadzadzada", "")]
        public void NavigationPageErrorTest(string adresseMail, string motDePasse)
        {
            Assert.False(SeConnecter(adresseMail, motDePasse));
            Assert.NotNull(FindUIElement("Error message"));
        }
    }
}