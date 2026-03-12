using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using Xunit;

namespace UITests.View.groupe.decideur
{
    public class HomePageTest : BaseTest
    {
        public HomePageTest() : base()
        {
            
            AppiumElement? seConecterButton = FindUIElement("Se connecter Button");
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            foreach (var entry in entries!) entry.Clear();
            var (adresseMailEntry, motDePasseEntry) = (entries?[0], entries?[1]);
            adresseMailEntry?.Clear();
            motDePasseEntry?.Clear();
            adresseMailEntry?.SendKeys("vincent.leclerc@example.com");
            motDePasseEntry?.SendKeys("root");
            seConecterButton?.Click();
            AppiumElement? groupeAeroportButton = FindUIElement("Groupetheme");
            groupeAeroportButton?.Click();
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
            bouttonTrie.Click();
            AppiumElement critere = FindUIElement($"critere-{nomCritere}")!;
            var proposition = propositions[0];
            critere.Click();
            int finalPosition = propositions.IndexOf(proposition);
            Assert.False(finalPosition != 0);
        }

        [Fact]
        public void ModifierProposition() { }

        [Fact]
        public void AfficherPrixProposition() { }

        [Fact]
        public void GroupeSansThematique() { }
        [Fact]
        public void ThematiqueSansProposition() { }
        [Fact]
        public void PropositionSansDonneDeCriteres() { }
    }
}
