using System.Text.Json.Serialization;

namespace com.koyok.democratia.Data.DataSource.Remote
{

    [method: JsonConstructor]
    public class InternauteRemoteSource(int? id_internaute, string? nom_internaute, string? prenom_internaute, string? adresse_postale, string? courriel, string? hashageMDP) : IRemoteSource
    {
        public int? id_internaute { get; set; } = id_internaute;
        public string? nom_internaute { get; set; } = nom_internaute;
        public string? prenom_internaute { get; set; } = prenom_internaute;
        public string? adresse_postale { get; set; } = adresse_postale;
        public string? courriel { get; set; } = courriel;
        public string? hashageMDP { get; set; } = hashageMDP;
        public string? tempMDP { get; set; }

        public InternauteRemoteSource() : this(null, null, null, null, null, null) { }

    }
}
