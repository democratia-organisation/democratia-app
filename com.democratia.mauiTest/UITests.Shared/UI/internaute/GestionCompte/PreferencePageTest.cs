using Xunit;

namespace UITests.UI.internaute.GestionCompte
{
    [CollectionPriority(3)]
    public class PreferencePageTest : BaseTest
    {
        public PreferencePageTest()
        {
            SeConnecter("sophie.lemoine@example.com", "root");
            FindUIElement("profileIconImageButton")?.Click();
            FindUIElement("preferencesButton")?.Click();
        }
        protected override void PresenceElements()
        {
            Assert.NotNull(FindUIElement("selectionLanguageLabel"));
            Assert.NotNull(FindUIElement("selectionLanguagePicker"));
            Assert.NotNull(FindUIElement("selectionThemeLabel"));
            Assert.NotNull(FindUIElement("selectionThemePicker"));
            Assert.NotNull(FindUIElement("enregistrerButton"));
        }
    }
}
