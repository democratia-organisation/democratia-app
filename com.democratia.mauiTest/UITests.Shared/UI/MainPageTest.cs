using com.democratia.Test.Localization;
using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xunit;


namespace UITests.UI
{
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
            ReadOnlyCollection<AppiumElement> appiumElements = FindUIElements("Entry")!;
            ReadOnlyCollection<AppiumElement> labelElements = FindUIElements("Label")!;
            
            
            Assert.NotNull(FindUIElement("connexionLabel")!);
            Assert.NotNull(appiumElements);
            Assert.NotNull(labelElements);
            Assert.Equal(2, labelElements.Count);
            Assert.Equal(2, appiumElements.Count);
            Assert.NotNull(FindUIElement("pasDeCompteLabel")!);
            Assert.NotNull(FindUIElement("creerCompteLabel")!);
            Assert.NotNull(FindUIElement("seConnecterButton")!);
            Assert.NotNull(FindUIElement("errorMessageLabel")!);
        }

        [Fact(DisplayName = "Test de la navigation vers la page home")]
        public void NavigationPageTest()
        {
            AssertConnexion("sophie.lemoine@example.com", "root");
        }


        [Theory(DisplayName = "Test de la page en cas d'entrée incorrecte")]
        [InlineData("fezfzfzefz", "Djonodo20050207/", "")]
        [InlineData("example@gmail.com", "Djonodo20050207/erreur", "")]
        [InlineData("", "", "")]
        [InlineData("dadadzadzada", "", "")]
        public void NavigationPageErrorTest(string adresseMail, string motDePasse, string errorMessage)
        {
            Assert.False(SeConnecter(adresseMail, motDePasse));
            Assert.Equal(errorMessage,FindUIElement("errorMessageLabel")!.Text);
        }
    }
}