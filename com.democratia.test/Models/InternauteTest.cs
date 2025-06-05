using com.democratia.Models;
using System;
using System.Security;

namespace com.democratia.test.Models
{
    
    public class InternauteTest
    {
        private string? exceptPrenom = "Jean", exceptNom = "Louis", exceptAdress = "10 rue de champigny", exceptMail = "mono@g.com";
        private int? exceptId = 1;
        [Fact]
        public void ConstruteurTest()
        {

            Internaute internaute = new(exceptId, exceptNom, exceptPrenom, exceptAdress, exceptMail);
            Assert.NotNull(internaute);
            VerificationChamp(internaute);
        }

        private void VerificationChamp(Internaute internaute)
        {
            Assert.Equal(exceptId, internaute.id_internaute);
            Assert.Equal(exceptMail, internaute.courriel);
            Assert.Equal(exceptAdress, internaute.adresse_postal);
            Assert.Equal(exceptNom, internaute.nom_internaute);
            Assert.Equal(exceptPrenom, internaute.prenom_internaute);
        }
    }
}
