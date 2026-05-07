using System.Text.Json.Serialization;

namespace com.koyok.democratia.Data.DataSource.Remote
{
    [method: JsonConstructor]
    public class PropositionRemoteSource(int? idProposition, string? titre, string? description, string? publication, float? budget, int? nombreSignalement, int? thematique, Guid? idGroupe) : IRemoteSource
    {
        public int? id_proposition { get; private set; } = idProposition;
        public string? titre_proposition { get; private set; } = titre;
        public string? description_proposition { get; private set; } = description;
        public string? date_publication { get; private set; } = publication;
        public float? budget { get; private set; } = budget;
        public int? nb_signalement { get; private set; } = nombreSignalement; 
        public int? id_thematique { get; private set; } = thematique;
        public Guid? id_groupe { get; private set; } = idGroupe;
        public PropositionRemoteSource() : this(null, null, null, null, null, null, null, null) { }
    }
}
