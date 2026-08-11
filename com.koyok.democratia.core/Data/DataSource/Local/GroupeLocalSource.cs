using SQLite;

namespace com.koyok.democratia.Data.DataSource.Local
{
    [Table("Groupe")]
    public partial class GroupeLocalSource : ILocalSource
    {
        [PrimaryKey]
        public Guid? IdGroupe { get; set; }
        public string? NomGroupe { get; set; }
        public string? CouleurGroupe { get; set; }
        public string? Image { get; set; }
        public float? Budget { get; set; }
        public int? NombreDeJourVote { get; set; }
        public int? NombreDeJourDiscuss { get; set; }
        public int? NombreSignalement { get; set; }
        public int? ImageSize { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
