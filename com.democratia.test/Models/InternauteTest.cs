using com.democratia.Models;

namespace com.democratia.test.Models
{

    public class InternauteTest
    {
        private readonly string? exceptPrenom = "Jean", exceptNom = "Louis", exceptAdress = "10 rue de champigny", exceptMail = "mono@g.com", expectedMdp = "MotDePasse127/";
        private readonly int? exceptId = 1;
        [Fact]
        public void ConstruteurTest()
        {

            Internaute internaute = new(exceptId, exceptNom, exceptPrenom, exceptAdress, exceptMail,expectedMdp);
            Assert.NotNull(internaute);
            VerificationChamp(internaute);
        }

        private void VerificationChamp(Internaute internaute)
        {
            Assert.Equal(exceptId, internaute.id_internaute);
            Assert.Equal(exceptMail, internaute.courriel);
            Assert.Equal(exceptAdress, internaute.adresse_postale);
            Assert.Equal(exceptNom, internaute.nom_internaute);
            Assert.Equal(exceptPrenom, internaute.prenom_internaute);
        }
    }
}
