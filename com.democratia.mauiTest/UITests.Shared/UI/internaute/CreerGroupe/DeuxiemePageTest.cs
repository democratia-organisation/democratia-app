
using OpenQA.Selenium.Appium;
using Xunit;

namespace UITests.UI.internaute.CreerGroupe
{
    [CollectionPriority(3)]
    public class DeuxiemePageTest : BaseTest
    {
        protected override void PresenceElements()
        {
            AppiumElement? button = FindUIElement("suivantButton");
            Assert.NotNull(FindUIElement("colorGrid"));
            Assert.NotNull(button);
            button.Click();
            Assert.NotNull(FindUIElement("ajouterButton"));
        }
    }
}
