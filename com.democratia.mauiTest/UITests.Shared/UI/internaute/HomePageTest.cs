using OpenQA.Selenium.Appium;
using Xunit;

namespace UITests.UI.internaute
{
    public class HomePageTest : BaseTest
    {
        public HomePageTest() 
        {
            SeConnecter("sophie.lemoine@example.com","root");
        }
    
        protected override void PresenceElements()
        {
            Assert.NotNull(FindUIElement("monGroupeLabel"));
            Assert.NotNull(FindUIElement("profileIconImageButton"));
            Assert.NotNull(FindUIElements("nomGroupeLabel"));
            Assert.NotNull(FindUIElement("creerUnGroupeButton"));
        }

        [Theory]
        [InlineData("creerUnGroupeButton", "creationGroupeLabel")]
        [InlineData("profileIconImageButton", "modifierButton")]
        [InlineData("Groupe Groupe AéroParis", "nomGroupeLabel")]
        public void OuvrirPage(string nomButton, string elementATrouver)
        {
            AppiumElement? button = FindUIElement(nomButton);
            Assert.NotNull(button);
            button.Click();
            Assert.NotNull(FindUIElement(elementATrouver));
        }
    }
}
