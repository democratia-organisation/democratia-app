
using Xunit;

namespace UITests.UI.internaute.CreerGroupe
{
    public class TroisiemePageTest : BaseTest
    {
        protected override void PresenceElements()
        {
            Assert.NotNull(FindUIElement("ajouterButton"));
        }
    }
}
