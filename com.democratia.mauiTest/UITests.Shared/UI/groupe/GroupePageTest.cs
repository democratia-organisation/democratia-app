using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xunit;
using UITests.UI.Localization;
using System.Diagnostics;

namespace UITests.UI.groupe.decideur
{
    [Collection("UITests")]
    [DisplayName("Page d'accueil de la partie décideur")]
    public class GroupePageTest : BaseTest
    {
        public GroupePageTest() : base()
        {
        }

        [Fact]  
        public void VoirListeProposition()
        {
            ReadOnlyCollection<AppiumElement> propositions = FindUIElements("propositions")!;
            Assert.True(propositions.Count > 0);
        }

        [Theory(DisplayName = "Trie les propostions selon un critère donnée")]
        [InlineData("popularite")]
        [InlineData("prix")]
        [InlineData("like")]
        public void TrierProposition(string nomCritere)
        {
            AssertConnexion("sophie.lemoine@example.com", "root");
            AppiumElement? groupe = RentrerDanUnGroupe("Groupe AéroParis");
            Assert.NotNull(groupe);
            groupe.Click();
            ReadOnlyCollection<AppiumElement> propositions = FindUIElements("propositions")!;
            AppiumElement bouttonTrie = FindUIElement("trieur")!;
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
            AssertConnexion("sophie.lemoine@example.com", "root");
            Debug.WriteLine(App.PageSource);
            AppiumElement? groupe = RentrerDanUnGroupe("Groupe AéroParis");
            Assert.NotNull(groupe);
            groupe.Click();
            AppiumElement? bouttonTrie = FindUIElement("trieur");
            Assert.Null(bouttonTrie);

        }

        [Fact]
        public void ModifierProposition() 
        {
            AssertConnexion("sophie.lemoine@example.com", "root");
            AppiumElement? groupe = RentrerDanUnGroupe("Groupe AéroParis");
            Assert.NotNull(groupe);
            groupe.Click();
            AppiumElement proposition = FindUIElements("propositions")![0];
            AppiumElement entrie = FindUIElement("Entry")!;
            AppiumElement validation = FindUIElement("validationButton")!;
            Assert.NotNull(proposition);
            Assert.NotNull(entrie);
            Assert.NotNull(validation);
            proposition.Click();
            entrie.SendKeys("8500");
            validation.Click();
            AppiumElement validatonTexte = FindUIElement("validationLabel")!;
            Assert.NotNull(validatonTexte);
        }

        [Fact]
        public void AfficherPrixProposition() 
        {
            ReadOnlyCollection<AppiumElement> prix = FindUIElements("prix")!;
            Assert.NotNull(prix);
        }

        [Fact]
        public void GroupeSansProposition() 
        {
            AssertConnexion("sophie.lemoine@example.com", "root");
            AppiumElement? groupe = RentrerDanUnGroupe("theme");
            Assert.NotNull(groupe);
            groupe.Click();
            AppiumElement messageTexte = FindUIElement("groupeSansPropositionLabel")!;
            Assert.NotNull(messageTexte);
            Assert.Equal(AppResources.groupeSansProposition,messageTexte.Text);
        }
        [Fact]
        public void ThematiqueSansProposition() 
        {
            AssertConnexion("sophie.lemoine@example.com", "root");
            AppiumElement? groupe = RentrerDanUnGroupe("theme");
            Assert.NotNull(groupe);
            groupe.Click();
            AppiumElement thematique = FindUIElements("thematique")![0];
            Assert.NotNull(thematique);
            thematique.Click();
            AppiumElement messageTexte = FindUIElement("thematiqueSansProposition")!;
            Assert.NotNull(messageTexte);
            Assert.Equal(AppResources.thematiqueSansProposition,messageTexte.Text);
        }


        [Theory(DisplayName = "Ne pas trier des fonction dont une proposition n'a pas de valeur pour le critère donnée")]
        [InlineData("popularite")]
        [InlineData("like")]
        public void PropositionSansDonneDeCriteres(string nomCritere) 
        {
            AssertConnexion("sophie.lemoine@example.com", "root");
            AppiumElement? groupe = RentrerDanUnGroupe("theme");
            Assert.NotNull(groupe);
            groupe.Click();
            ReadOnlyCollection<AppiumElement> propositions = FindUIElements("propositions")!;
            AppiumElement bouttonTrie = FindUIElement("trieur")!;
            Assert.NotNull(propositions);
            Assert.NotNull(bouttonTrie);
            bouttonTrie.Click();
            AppiumElement critere = FindUIElement($"critere-{nomCritere}")!;
            Assert.NotNull(critere);
            Assert.False(critere.Enabled);
        }

        private AppiumElement? RentrerDanUnGroupe(string nomGroupe)
        => FindUIElement($"Groupe {nomGroupe}");

        protected override void PresenceElements()
        {
            throw new NotImplementedException();
        }
    }
}
