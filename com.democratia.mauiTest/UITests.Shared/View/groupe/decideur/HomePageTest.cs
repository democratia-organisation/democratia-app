using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xunit;
using UITests.Localization;

namespace UITests.View.groupe.decideur
{
    [Collection("UITests")]
    [DisplayName("Page d'accueil de la partie décideur")]
    public class HomePageTest : BaseTest
    {
        public HomePageTest() : base()
        {
            
            AppiumElement? seConecterButton = FindUIElement("Se connecter Button");
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            foreach (var entry in entries!) entry.Clear();
            var (adresseMailEntry, motDePasseEntry) = (entries?[0], entries?[1]);
            adresseMailEntry?.SendKeys("vincent.leclerc@example.com");
            motDePasseEntry?.SendKeys("root");
            seConecterButton?.Click();
            RentrerDanUnGroupe("AeroParis");
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
            Assert.False(finalPosition != 0);
        }

        [Fact]
        public void ModifierProposition() 
        {
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
        public void GroupeSansThematique() 
        {
            AppiumElement? groupe = RentrerDanUnGroupe("hello");
            Assert.NotNull(groupe);
            AppiumElement messageTexte = FindUIElement("groupeSansThematiqueLabel")!;
            Assert.NotNull(messageTexte);
            //Assert.Equal(AppResources.groupeSansThematique,messageText.Text);
        }
        [Fact]
        public void ThematiqueSansProposition() 
        {
            AppiumElement? groupe = RentrerDanUnGroupe("theme");
            Assert.NotNull(groupe);
            AppiumElement thematique = FindUIElements("thematique")![0];
            Assert.NotNull(thematique);
            thematique.Click();
            AppiumElement messageTexte = FindUIElement("thematiqueSansProposition")!;
            Assert.NotNull(messageTexte);
            //Assert.Equal(AppResources.thematiqueSansProposition,messageText.Text);
        }
        [Fact]
        public void PropositionSansDonneDeCriteres() 
        {
            ReadOnlyCollection<AppiumElement> propositions = FindUIElements("propositions")!;
            AppiumElement bouttonTrie = FindUIElement("trieur")!;
            Assert.NotNull(propositions);
            Assert.NotNull(bouttonTrie);
            var proposition = propositions[0];
            bouttonTrie.Click();
            AppiumElement critere = FindUIElement($"critere-{nomCritere}")!;
            Assert.NotNull(critere);
            Assert.False(critere.Enabled);
        }

        private AppiumElement? RentrerDanUnGroupe(string nomGroupe)
        => FindUIElement($"Groupe{nomGroupe}");
        
    }
}
