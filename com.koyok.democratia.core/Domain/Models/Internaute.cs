using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Utils;
using System.Text.Json.Serialization;

namespace com.koyok.democratia.Domain.Models
{

    public class Internaute : IModel
    {

        public Internaute(int? id_internaute, string? nom_internaute, string? prenom_internaute, string? adresse_postale, string? courriel, string? hashageMDP)
        {
            this.idInternaute = id_internaute;
            this.nomInternaute = nom_internaute;
            this.prenomInternaute = prenom_internaute;
            this.adressePostale = adresse_postale;
            this.courriel = courriel;
            this.hashageMDP = hashageMDP;
        }

        

        public int? idInternaute { get;  set; }
        public string? nomInternaute { get;  set; }
        public string? prenomInternaute { get; set; }
        public string? adressePostale { get; set; }
        public string? courriel { get; set {
                if (value is null) { field = value; return; }
                if (!Verification.VerifierFormatage(value!, new(@"^[\w.\+\-]+@[\w\-]+\.[A-Za-z]{2,}$")))
                    throw new MailException();
                else field = value;
        } } 
        public string? hashageMDP { get; set; }
        public string? tempMDP { get; set {
                if (value is null) { field = value; return; }
                if (!Verification.VerifierFormatage(value!, new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$")))
                    throw new PassWordException();
                else field = value;
        } }

        public Internaute() : this(null, null, null, null, null, null) { }

    }
}
