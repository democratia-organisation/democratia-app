using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
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
        }
        public CreationPageTest() : base()
        {
            
            var _ = new AppiumSetup();
            AppiumElement? creationPage = FindUIElement("Creer"); ;
            creationPage?.Click();
        }

        [Fact(DisplayName = "Création de compte")]
        public void CreationCompteTest()
        {
            
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            ReadOnlyCollection<AppiumElement>? labels = FindUIElements("Label");
            AppiumElement? mInscrireButton = FindUIElement("M'inscrire");
            var nombreElements = 5;
            foreach (var entry in entries!) entry.Clear();

            var (nomDeFamille, prenom, adressePostale, adresseMail, motDePass) =
                (entries?[0], entries?[1], entries?[2], entries?[3], entries?[4]);


            nomDeFamille?.SendKeys("Dupont");
            prenom?.SendKeys("Jean");
            adressePostale?.SendKeys("123 Rue de Paris");
            adresseMail?.SendKeys("example@gmail.com");
            motDePass?.SendKeys("MotDePasse123/");
            mInscrireButton?.Click();
            AppiumElement? bouttonRetourHome = FindUIElement("BienvenueLabel");
            Assert.NotNull(entries);
            Assert.NotNull(labels);
            Assert.NotNull(mInscrireButton);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.NotNull(bouttonRetourHome);
            Assert.Null(FindUIElements("Entry"));
            Assert.Null(FindUIElements("Labels"));
            Assert.Null(FindUIElement("M'inscrire"));

            bouttonRetourHome?.Click();

        }

        [Theory(DisplayName = "Différents cas d'erreurs")]
        [InlineData("", "", "", "", "")] // champs vide
        [InlineData("hello", "bonjour", "132 rue de Lyon", "dzadazda", "Djonodo8/")] // mauvais format de mail
        [InlineData("hello", "bonjour", "132 rue de Lyon", "email@example.com", "Djonodo/")] // mot de passe pas assez long
        [InlineData("hello", "bonjour", "132 rue de Lyon", "modadary56@gmail.com", "Djonodo8/")] // email déjà utilisé
        public void CreationCompteTestError(string nom, string prenom, string adressePostale, string adresseMail, string motDePasse)
        {
            
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            ReadOnlyCollection<AppiumElement>? labels = FindUIElements("Label");
            AppiumElement? mInscrireButton = FindUIElement("M'inscrire");
            Assert.NotNull(entries);
            Assert.NotNull(labels);
            var nombreElements = 5;
            var (nomElement, prenomElement, adressePostaleElement, adresseMailElement, motDePasseElement) =
                (entries?[0], entries?[1], entries?[2], entries?[3], entries?[4]);

            nomElement?.SendKeys(nom);
            prenomElement?.SendKeys(prenom);
            adressePostaleElement?.SendKeys(adressePostale);
            adresseMailElement?.SendKeys(adresseMail);
            motDePasseElement?.SendKeys(motDePasse);
            mInscrireButton?.Click();

            Assert.NotNull(mInscrireButton);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.NotNull(FindUIElement("RetourMessage"));

        }
    }
}
