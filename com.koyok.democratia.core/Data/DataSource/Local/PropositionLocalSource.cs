using SQLite;

namespace com.koyok.democratia.Data.DataSource.Local
{
    [Table("Proposition")]
    public class PropositionLocalSource : ILocalSource
    {
        [PrimaryKey, AutoIncrement]
        public int? IdProposition { get; private set; }
        public string? Titre { get; private set; }
        public string? Description { get; private set; }
        public string? Publication { get; private set; }
        public float? Budget { get; private set; }
        public int? NombreSignalement { get; private set; }
        public int? Thematique { get; private set; }
        public Guid? IdGroupe { get; private set; }
        public int JourDiscussion { get; set; }
    }
}
