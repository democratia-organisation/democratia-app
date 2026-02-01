
using System.Text.Json.Serialization;

namespace com.democratia.Models
{

    public class Internaute : IModel
    {
        public Internaute() : this(null, null, null, null, null, null)
        {
        }
        [JsonConstructor]
        public Internaute(int? id_internaute, string? nom_internaute, string? prenom_internaute, string? adresse_postal, string? courriel, string? hashageMDP)
        {
            this.id_internaute = id_internaute;
            this.nom_internaute = nom_internaute;
            this.prenom_internaute = prenom_internaute;
            this.adresse_postal = adresse_postal;
            this.courriel = courriel;
            this.hashageMDP = hashageMDP;
        }

        public int? id_internaute { get;  set; }
        public string? nom_internaute { get;  set; }
        public string? prenom_internaute { get; set; }
        public string? adresse_postal { get; set; }
        public string? courriel { get; set; }
        public string? hashageMDP { get; set; }
    }
}
