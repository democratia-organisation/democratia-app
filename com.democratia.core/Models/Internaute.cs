using com.democratia.Utils;
using System.Text.Json.Serialization;

namespace com.democratia.Models
{

    public class Internaute : IModel
    {
        public Internaute() : this(null, null, null, null, null, null) { }
        [JsonConstructor]
        public Internaute(int? id_internaute, string? nom_internaute, string? prenom_internaute, string? adresse_postale, string? courriel, string? hashageMDP)
        {
            this.id_internaute = id_internaute;
            this.nom_internaute = nom_internaute;
            this.prenom_internaute = prenom_internaute;
            this.adresse_postale = adresse_postale;
            this.courriel = courriel;
            this.hashageMDP = hashageMDP;
        }

        public int? id_internaute { get;  set; }
        public string? nom_internaute { get;  set; }
        public string? prenom_internaute { get; set; }
        public string? adresse_postale { get; set; }
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
    }
}
