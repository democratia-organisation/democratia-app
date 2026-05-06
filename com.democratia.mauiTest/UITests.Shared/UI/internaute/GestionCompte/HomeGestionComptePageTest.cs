using OpenQA.Selenium.Appium;
using Xunit;

namespace UITests.UI.internaute.GestionCompte
{
    public class HomeGestionComptePageTest : BaseTest
    {
        public HomeGestionComptePageTest() 
        {
            SeConnecter("sophie.lemoine@example.com", "root");
            FindUIElement("profileIconImageButton")?.Click();
        }

        protected override void PresenceElements()
        {
            Assert.NotNull(FindUIElement("supprimerButton"));
            Assert.NotNull(FindUIElement("preferenceButton"));
            Assert.NotNull(FindUIElement("modifierButton"));
        }

        [Theory]
        [InlineData("supprimerButton","supprimerLabel")]
        [InlineData("preferenceButton", "selectionLangueLabel")]
        [InlineData("modifierButton", "Label")]
        public void OuvrirPage(string nomButton, string elementATrouver)
        {
            AppiumElement? button = FindUIElement(nomButton);
            Assert.NotNull(button);
            button.Click();
            Assert.NotNull(FindUIElement(elementATrouver));
        }
    }
}
