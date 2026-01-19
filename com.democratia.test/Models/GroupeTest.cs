using com.democratia.Models;

namespace com.democratia.test.Models
{
    public class GroupeTest
    {
        private readonly int? idGroupe = 3, nombreJourDeVote = 4, nombreDeJourDiscussion = 10, nombreSignalement = 0;
        private readonly float? budget = 1000;
        private readonly string? nomGroupe = "les baroudeurs", couleurGroupe = "#F2F3F4", image = "bonjour.png";
        [Fact]
        public void ConstructeurTest()
        {
            Groupe groupe = new(idGroupe, nomGroupe, couleurGroupe, image, budget, nombreJourDeVote, nombreDeJourDiscussion, nombreSignalement);
            Assert.NotNull(groupe);
            VerificationChamp(groupe);
        }

        private void VerificationChamp(Groupe groupe)
        {
            Assert.Equal(idGroupe, groupe.IdGroupe);
            Assert.Equal(nombreJourDeVote, groupe.NombreDeJourVote);
            Assert.Equal(image, groupe.Image);
            Assert.Equal(nombreDeJourDiscussion, groupe.NombreDeJourDiscuss);
            Assert.Equal(budget, groupe.Budget);
            Assert.Equal(couleurGroupe, groupe.CouleurGroupe);
            Assert.Equal(nomGroupe, groupe.NomGroupe);
        }
    }
}
