using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Xunit;

namespace UITests.View
{
    [Collection("UITest")]
    [DisplayName("Création de compte")]
    public class CreationPageTest : BaseTest
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
            AppiumElement? mInscrireButton = FindUIElement("M'inscrire");
            var nombresEntrees = 5;

            var nombresLabels = labels?.Count;
            var nombresEntries = entries?.Count;

            Assert.Equal(nombresEntrees, nombresLabels);
            Assert.Equal(nombresEntrees, nombresEntries);
            Assert.NotNull(mInscrireButton);
            Assert.Equal("Nom de Famille", labels?[0].Text);
            Assert.Equal("Prénom", labels?[1].Text);
            Assert.Equal("Adresse Postale", labels?[2].Text);
            Assert.Equal("Adresse mail", labels?[3].Text);
            Assert.Equal("Mot de passe", labels?[4].Text);
            Assert.Equal("M'inscrire", mInscrireButton?.Text);
        }
        public CreationPageTest() : base()
        {
            if (SystemInfo.SSHHost()) return;
            var _ = new AppiumSetup();
            AppiumElement? creationPage = FindUIElement("Créez en un !"); ;
            creationPage?.Click();
        }

        [Fact(DisplayName = "Création de compte")]
        public void CreationCompteTest()
        {
            if (SystemInfo.SSHHost()) return;
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            ReadOnlyCollection<AppiumElement>? labels = FindUIElements("Labels");
            AppiumElement? mInscrireButton = FindUIElement("M'inscrire");
            var nombreElements = 5;
            foreach (var entry in entries !) entry.Clear();
            
            var (nomDeFamille, prenom, adressePostale, adresseMail, motDePass) =
                (entries?[0], entries?[1], entries?[2], entries?[3], entries?[4]);


            nomDeFamille?.SendKeys("Dupont");
            prenom?.SendKeys("Jean");
            adressePostale?.SendKeys("123 Rue de Paris");
            adresseMail?.SendKeys("example@gmail.com");
            motDePass?.SendKeys("MotDePasse123");
            mInscrireButton?.Click();

            Assert.NotNull(entries);
            Assert.NotNull(labels);
            Assert.NotNull(mInscrireButton);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.NotNull(FindUIElement("Bienvenue Texte"));
            Assert.NotNull(FindUIElement("Bienvenue Bouton"));
            Assert.Null(FindUIElements("Entry"));
            Assert.Null(FindUIElements("Labels"));
            Assert.Null(FindUIElement("M'inscrire"));

            // TODO : est-il nécessaire de cliquer sur le bouton "Bienvenue Bouton" pour revenir à la page d'accueil ?

        }

        [Theory(DisplayName = "Différents cas d'erreurs")]
        [InlineData("","","","","")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "dzadazda", "Djonodo8/")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "email@example.com", "Djonodo/")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "modadary56@gmail.com", "Djonodo8/")]
        public void CreationCompteTestError(string nom,string prenom, string adressePostale,string adresseMail,string motDePasse)
        {
            if (SystemInfo.SSHHost()) return;
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            ReadOnlyCollection<AppiumElement>? labels = FindUIElements("Labels");
            AppiumElement? mInscrireButton = FindUIElement("M'inscrire");
            var nombreElements = 5;
            var (nomElement, prenomElement, adressePostaleElement, adresseMailElement, motDePasseElement) =
                (entries?[0], entries?[1], entries?[2], entries?[3], entries?[4]);

            nomElement?.SendKeys(nom);
            prenomElement?.SendKeys(prenom);
            adressePostaleElement?.SendKeys(adressePostale);
            adresseMailElement?.SendKeys(adresseMail);
            motDePasseElement?.SendKeys(motDePasse);
            mInscrireButton?.Click();

            Assert.NotNull(entries);
            Assert.NotNull(labels);
            Assert.NotNull(mInscrireButton);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.Null(FindUIElement("Bienvenue Texte"));
            Assert.NotNull(FindUIElement("Error message"));

        }
    }
}
