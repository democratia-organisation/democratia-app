using SQLite;

namespace com.koyok.democratia.Data.DataSource.Local
{
    [Table("Internaute")]
    public class InternauteLocalSource : ILocalSource
    {
        [PrimaryKey]
        public int? IdInternaute { get; set; }
        public string? NomInternaute { get; set; }
        public string? PrenomInternaute { get; set; }
        public string? AdressePostale { get; set; }
        public string? Courriel { get; set; }
        public string? HashageMDP { get; set; }

    }
}
