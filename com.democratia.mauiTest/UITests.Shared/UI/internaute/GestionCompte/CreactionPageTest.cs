using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UITests.Localization;
using Xunit;

namespace UITests.UI.internaute.GestionCompte
{
    [DisplayName("Création de compte"), Collection("UITest"), CollectionPriority(3)]
    public class CreationPageTest : BaseTest
    {
        
        public CreationPageTest()
        {
            AppiumElement? creationPage = FindUIElement("creationCompteLabelButton");
            creationPage?.Click();
        }

        [Fact(DisplayName = "Création de compte")]
        public void CreationCompteTest()
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


            nomDeFamille?.SendKeys("Dupont");
            prenom?.SendKeys("Jean");
            adressePostale?.SendKeys("123 Rue de Paris");
            adresseMail?.SendKeys("example@gmail.com");
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

        [Theory(DisplayName = "Différents cas d'erreurs")]
        [ClassData(typeof(ParameterDataCreation))]
        public void CreationCompteTestError(string nom, string prenom, string adressePostale, string adresseMail, string motDePasse, string errorMessage)
        {
            
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            ReadOnlyCollection<AppiumElement>? labels = FindUIElements("Label");
            AppiumElement? mInscrireButton = FindUIElement("actionButton");
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
            mInscrireButton?.Click();

            Assert.NotNull(mInscrireButton);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.Equal(nombreElements, entries?.Count);
            Assert.NotNull(errorMessageLabel);
            Assert.Equal(errorMessage, errorMessageLabel?.Text);

        }

        protected override void PresenceElements()
        {
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            ReadOnlyCollection<AppiumElement>? labels = FindUIElements("Label");
            AppiumElement? errorMessageLabel = FindUIElement("errorMessageLabel");
            AppiumElement? mInscrireButton = FindUIElement("actionButton");
            var nombresEntrees = 5;

            var nombresLabels = labels?.Count;
            var nombresEntries = entries?.Count;

            Assert.Equal(nombresEntrees, nombresLabels);
            Assert.Equal(nombresEntrees, nombresEntries);
            Assert.NotNull(mInscrireButton);
            Assert.NotNull(errorMessageLabel);
        }
    }
    public class ParameterCreation<T1, T2, T3, T4, T5, T6> : TheoryData
    {
        public void Add(string prenom, string nom, string adresse, string adresseMail, string motDePasse, string errorMessage)
        {
            AddRow(prenom, nom, adresse, adresseMail, motDePasse, errorMessage);
        }
    }

    public class ParameterDataCreation: ParameterCreation<string, string, string, string,string,string>
    {
        public ParameterDataCreation()
        {
            BaseTest.ChangerLanguage("en-US");
            Add("", "", "", "", "", AppResources.errorUnknowEmptyFieldMessage);
            Add("hello", "bonjour", "132 rue de Lyon", "dzadazda", "Djonodo8/", AppResources.formatEmail);
            Add("hello", "bonjour", "132 rue de Lyon", "email@example.com", "Djonodo/", AppResources.formatMotDePasse);
            Add("hello", "bonjour", "132 rue de Lyon", "modadary56@gmail.com", "Djonodo8/", AppResources.compteExistantErreur);

        }
    }
}
