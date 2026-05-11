using SQLite;

namespace com.koyok.democratia.Data.DataSource.Local
{
    public class InternauteLocalSource(int? id_internaute, string? nom_internaute, string? prenom_internaute, string? adresse_postale, string? courriel, string? hashageMDP) : ILocalSource
    {
        [PrimaryKey, AutoIncrement]
        public int? IdInternaute { get; set; } = id_internaute;
        public string? NomInternaute { get; set; } = nom_internaute;
        public string? PrenomInternaute { get; set; } = prenom_internaute;
        public string? AdressePostale { get; set; } = adresse_postale;
        public string? Courriel { get; set; } = courriel;
        public string? HashageMDP { get; set; } = hashageMDP;
        public string? TempMDP { get; set; }

        public InternauteLocalSource() : this(null, null, null, null, null, null) { }

    }
}
