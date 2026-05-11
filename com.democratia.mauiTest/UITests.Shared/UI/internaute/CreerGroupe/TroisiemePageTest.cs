
using Xunit;

namespace UITests.UI.internaute.CreerGroupe
{
    [CollectionPriority(3)]
    public class TroisiemePageTest : BaseTest
    {
        protected override void PresenceElements()
        {
            Assert.NotNull(FindUIElement("ajouterButton"));
        }
    }
}
