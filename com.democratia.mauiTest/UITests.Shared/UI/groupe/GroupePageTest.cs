using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UITests.UI.Localization;
using Xunit;

namespace UITests.UI.groupe
{
    [Collection("UITests")]
    [DisplayName("Page d'accueil de la partie décideur")]
    public class GroupePageTest : BaseTest
    {
        public GroupePageTest()
        {
        }

        
        [Theory(DisplayName = "Trie les propostions selon un critère donnée")]
        [InlineData("popularite")]
        [InlineData("prix")]
        [InlineData("like")]
        public void TrierProposition(string nomCritere)
        {
            RentrerDanUnGroupe("Groupe AéroParis");
            ReadOnlyCollection<AppiumElement> propositions = FindUIElements("propositions")!;
            AppiumElement bouttonTrie = FindUIElement("criterePicker")!;
            Assert.NotNull(propositions);
            Assert.NotNull(bouttonTrie);
            var proposition = propositions[0];
            bouttonTrie.Click();
            AppiumElement critere = FindUIElement($"critere-{nomCritere}")!;
            Assert.NotNull(critere);
            critere.Click();
            int finalPosition = propositions.IndexOf(proposition);
            Assert.NotEqual(0, finalPosition);
        }

        [Fact]
        public void NePasTrierPropositionSiNonDecideur()
        {
            RentrerDanUnGroupe("Groupe AéroParis","sophie.lemoine@example.com");
            AppiumElement? bouttonTrie = FindUIElement("criterePicker");
            Assert.Null(bouttonTrie);
        }

        [Fact]
        public void ModifierProposition() 
        {
            RentrerDanUnGroupe("Groupe AéroParis");
            ReadOnlyCollection<AppiumElement>? proposition = FindUIElements("loupePropositionImageButton");
            Assert.NotNull(proposition);
            proposition[0].Click();
            AppiumElement? entrie = FindUIElement("Entry");
            AppiumElement? validation = FindUIElement("validationButton");
            Assert.NotNull(entrie);
            Assert.NotNull(validation);
            entrie.SendKeys("8500");
            validation.Click();
            AppiumElement? validatonTexte = FindUIElement("validationLabel");
            Assert.NotNull(validatonTexte);
        }

        [Fact]
        public void GroupeSansProposition() 
        {
            ChangerLanguage("en-US");
            RentrerDanUnGroupe("theme");
            AppiumElement? messageTexte = FindUIElement("groupeSansPropositionLabel");
            Assert.NotNull(messageTexte);
            Assert.Equal(AppResources.groupeSansProposition,messageTexte.Text);
        }

        [Theory(DisplayName = "Ne pas trier des fonction dont une proposition n'a pas de valeur pour le critère donnée")]
        [InlineData("popularite")]
        [InlineData("like")]
        public void PropositionSansDonneDeCriteres(string nomCritere) 
        {
            RentrerDanUnGroupe("groupeBis");
            ReadOnlyCollection<AppiumElement> propositions = FindUIElements("loupeImageButton")!;
            AppiumElement bouttonTrie = FindUIElement("criterePicker")!;
            Assert.NotNull(propositions);
            Assert.NotNull(bouttonTrie);
            bouttonTrie.Click();
            AppiumElement critere = FindUIElement($"critere-{nomCritere}")!;
            Assert.NotNull(critere);
            Assert.False(critere.Enabled);
        }

        private void RentrerDanUnGroupe(string nomGroupe, string identifiant = "claire.benoit@example.com")
        {
            SeConnecter(identifiant, "root");
            AppiumElement? groupe = FindUIElement($"Groupe {nomGroupe}"); ;
            Assert.NotNull(groupe);
            groupe.Click();
        }

        protected override void PresenceElements()
        {
            RentrerDanUnGroupe("Groupe AéroParis");
            Assert.NotNull(FindUIElement("rouageImageButton"));
            Assert.NotNull(FindUIElement("loupeImageButton"));
            Assert.NotNull(FindUIElement("groupeImage"));
            Assert.NotNull(FindUIElement("groupeImageButton"));
            Assert.NotNull(FindUIElement("criterePicker"));
            Assert.NotNull(FindUIElement("nomGroupeLabel"));
            Assert.NotNull(FindUIElements("titreLabel"));
            Assert.NotNull(FindUIElements("loupePropositionImageButton"));
            Assert.NotNull(FindUIElement("nouvellePropositionButton"));
            Assert.NotNull(FindUIElements("prix"));
        }
    }
}
