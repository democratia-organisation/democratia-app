using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using UITests.Localization;
using Xunit;

namespace UITests.UI.internaute.GestionCompte
{
    [CollectionPriority(3)]
    public class ModifierCompteTest : BaseTest
    {
        public ModifierCompteTest() 
        {
            SeConnecter("sophie.lemoine@example.com", "root");
            FindUIElement("profileIconImageButton")?.Click();
            FindUIElement("modifierButton")?.Click();
        }
        protected override void PresenceElements()
        {
            Assert.NotNull(FindUIElements("Label"));
            Assert.NotNull(FindUIElements("Entry"));
            Assert.NotNull(FindUIElement("actionButton"));
            Assert.NotNull(FindUIElement("errorMessageLabel"));
        }

        [Fact]
        public void ModifierCompte()
        {
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            ReadOnlyCollection<AppiumElement>? labels = FindUIElements("Label");
            Assert.NotNull(entries);
            Assert.NotNull(labels);
            AppiumElement? mInscrireButton = FindUIElement("actionButton");
            var nombreElements = 5;
            foreach (var entry in entries) entry.Clear();

            var (nomDeFamille, prenom, adressePostale, adresseMail, motDePass) =
                (entries[0], entries[1], entries[2], entries[3], entries[4]);


            nomDeFamille?.SendKeys("Lemoine");
            prenom?.SendKeys("Sophiana");
            adressePostale?.SendKeys("123 Rue de Paris");
            adresseMail?.SendKeys("sophiana.lemoine@gmail.com");
            motDePass?.SendKeys("MotDePasse123/");
            mInscrireButton?.Click();
            AppiumElement? bouttonRetourHome = FindUIElement("suiteActionButton");

            Assert.NotNull(mInscrireButton);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.NotNull(bouttonRetourHome);
            Assert.Null(FindUIElements("Entry"));
            Assert.Null(FindUIElements("Labels"));
            Assert.Null(FindUIElement("actionButton"));

            bouttonRetourHome?.Click();
        }

        [Theory]
        [ClassData(typeof(ParameterDataModification))]
        public void ModifierCompteTestError(string nom, string prenom, string adressePostale, string adresseMail, string motDePasse, string errorMessage)
        {
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            ReadOnlyCollection<AppiumElement>? labels = FindUIElements("Label");
            AppiumElement? mModifierButton = FindUIElement("actionButton");
            AppiumElement? errorMessageLabel = FindUIElement("errorMessageLabel");
            Assert.NotNull(entries);
            Assert.NotNull(labels);
            var nombreElements = 5;
            var (nomElement, prenomElement, adressePostaleElement, adresseMailElement, motDePasseElement) =
                (entries[0], entries[1], entries[2], entries[3], entries[4]);
            nomElement?.SendKeys(nom);
            prenomElement?.SendKeys(prenom);
            adressePostaleElement?.SendKeys(adressePostale);
            adresseMailElement?.SendKeys(adresseMail);
            motDePasseElement?.SendKeys(motDePasse);
            mModifierButton?.Click();
            Assert.NotNull(mModifierButton);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.NotNull(errorMessageLabel);
            Assert.Equal(errorMessage, errorMessageLabel?.Text);
        }
    }
    public class ParameterModification<T1, T2, T3, T4, T5, T6> : TheoryData
    {
        public void Add(string prenom, string nom, string adresse, string adresseMail, string motDePasse, string errorMessage)
        {
            AddRow(prenom, nom, adresse, adresseMail, motDePasse, errorMessage);
        }
    }

    public class ParameterDataModification : ParameterModification<string, string, string, string, string, string>
    {
        public ParameterDataModification()
        {
            BaseTest.ChangerLanguage("en-US");
            Add("hello", "bonjour", "132 rue de Lyon", "dzadazda", "Djonodo8/", AppResources.formatEmail);
            Add("hello", "bonjour", "132 rue de Lyon", "email@example.com", "Djonodo/", AppResources.formatMotDePasse);
            Add("hello", "bonjour", "132 rue de Lyon", "modadary56@gmail.com", "Djonodo8/", AppResources.compteExistantErreur);
        }
    }
}
