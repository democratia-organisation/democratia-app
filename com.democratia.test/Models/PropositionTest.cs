
using com.democratia.Models;

namespace com.democratia.test.Models
{
    public class PropositionTest
    {
        [Fact]
        public void ConstructeurPropositionTest()
        {
            var uid = Guid.CreateVersion7();
            // Arrange
            Proposition proposition = new(1, "Test Proposition",
                "mettre une plante dans le jardin",
                "prendre une plante",
                1000.0f,
                0, 1, uid
            );
            // Act & Assert
            Assert.NotNull(proposition);
            VerificationChamps(proposition,uid);
        }

        private static void VerificationChamps(Proposition proposition,Guid uid )
        {
            Assert.Equal(1, proposition.IdProposition);
            Assert.Equal("Test Proposition", proposition.Titre);
            Assert.Equal("mettre une plante dans le jardin", proposition.Description);
            Assert.Equal("prendre une plante", proposition.Publication);
            Assert.Equal(1000.0f, proposition.Budget);
            Assert.Equal(0, proposition.NombreSignalement);
            Assert.Equal(4, proposition.Thematique);
            Assert.Equal(uid, proposition.IdGroupe);
        }
    }
}
