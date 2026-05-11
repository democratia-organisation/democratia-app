using SQLite;

namespace com.koyok.democratia.Data.DataSource.Local
{
    public class PropositionLocalSource(int? idProposition, string? titre, string? description, string? publication, float? budget, int? nombreSignalement, int? thematique, Guid? idGroupe) : ILocalSource
    {
        [PrimaryKey, AutoIncrement]
        public int? IdProposition { get; private set; } = idProposition;
        public string? Titre { get; private set; } = titre;
        public string? Description { get; private set; } = description;
        public string? Publication { get; private set; } = publication;
        public float? Budget { get; private set; } = budget;
        public int? NombreSignalement { get; private set; } = nombreSignalement;
        public int? Thematique { get; private set; } = thematique;
        public Guid? IdGroupe { get; private set; } = idGroupe;
        public int JourDiscussion { get; set; } = 1;
        public PropositionLocalSource() : this(null, null, null, null, null, null, null, null) { }


    }
}
