using OpenQA.Selenium.Appium;
using System.Collections.ObjectModel;
using UITests.Localization;
using Xunit;

namespace UITests.UI.internaute.CreerGroupe
{
    public class PremierePageTest : BaseTest
    {
        public PremierePageTest() 
        {
            SeConnecter("sophie.lemoine@example.com", "root");
            FindUIElement("creerGroupeButton")?.Click();
        }
        protected override void PresenceElements()
        {
            Assert.NotNull(FindUIElements("Entry"));
            Assert.NotNull(FindUIElements("Label"));
            Assert.NotNull(FindUIElement("themeSearchBar"));
            Assert.NotNull(FindUIElement("suivantButton"));
        }

        [Theory]
        [ClassData(typeof(ParametreCreationGroupeData))]
        public void InvalideGroupeCreation(string nomGroupe, string nbjDiscuss, string nbjVote, string budget, string errorMessage)
        {
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            AppiumElement? button = FindUIElement("suivantButton");
            AppiumElement? errorMessageLabel = FindUIElement("errorMessageLabel");
            AppiumElement? searchBar = FindUIElement("themeSearchBar");

            Assert.NotNull(entries);
            Assert.NotNull(button);
            Assert.NotNull(searchBar);

            string[] textes = [nomGroupe, nbjDiscuss, nbjVote, budget];
            for (int i = 0; i < entries.Count; i++)
                entries[i].SendKeys(textes[i]);
            
            searchBar.SendKeys("environnement");

            AppiumElement? themeEntry = FindUIElement("themeEntry");
            Assert.NotNull(themeEntry);
            themeEntry.SendKeys("200");
            button.Click();
            Assert.NotNull(errorMessageLabel);
            Assert.Equal(errorMessage, errorMessageLabel.Text);
        }



        [Fact]
        public void ValideGroupeCreation()
        {
            ReadOnlyCollection<AppiumElement>? entries = FindUIElements("Entry");
            AppiumElement? button = FindUIElement("suivantButton");
            AppiumElement? searchBar = FindUIElement("themeSearchBar");

            Assert.NotNull(entries);
            Assert.NotNull(button);
            Assert.NotNull(searchBar);

            string[] textes = ["clairvoyance", "10", "5", "300"];
            for (int i = 0; i < entries.Count; i++)
                entries[i].SendKeys(textes[i]);
            searchBar.SendKeys("environnement");
            button.Click();
            AppiumElement? themeEntry = FindUIElement("themeEntry");
            Assert.NotNull(themeEntry);
            themeEntry.SendKeys("200");
            button.Click();
            Assert.NotNull(FindUIElement("colorGrid"));
        }
    }

    public class  ParemetreCreationGroupe<t1, t2, t3, t4,t5> : TheoryData
    {
        public void Add(string nomGroupe, string nbjDiscuss, string nbjVote, string budget,string errorMessage)
        {
            AddRow(nomGroupe, nbjDiscuss, nbjVote, budget, errorMessage);
        }
    }

    public class ParametreCreationGroupeData : ParemetreCreationGroupe<string, string, string, string, string>
    {
        public ParametreCreationGroupeData()
        {
            BaseTest.ChangerLanguage("en-US");
            Add("", "10", "5", "300", AppResources.errorUnknowEmptyFieldMessage);
            Add("clairvoyance", "", "5", "300", AppResources.errorUnknowEmptyFieldMessage);
            Add("clairvoyance", "10", "", "300", AppResources.errorUnknowEmptyFieldMessage);
            Add("clairvoyance", "10", "5", "", AppResources.errorUnknowEmptyFieldMessage);
            Add("clairvoyance", "10", "5", "50" , AppResources.erreurThematiqueBudget);
        }
        
    }
}
