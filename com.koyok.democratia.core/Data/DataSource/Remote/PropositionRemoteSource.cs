using System.Text.Json.Serialization;

namespace com.koyok.democratia.Data.DataSource.Remote
{
    [method: JsonConstructor]
    public class PropositionRemoteSource(int? id_proposition, string? titre_proposition, string? description_proposition, string? date_publication, float? budget, int? nb_signalement, int? id_thematique, Guid? id_groupe) : IRemoteSource
    {
        public int? id_proposition { get; private set; } = id_proposition;
        public string? titre_proposition { get; private set; } = titre_proposition;
        public string? description_proposition { get; private set; } = description_proposition;
        public string? date_publication { get; private set; } = date_publication;
        public float? budget { get; private set; } = budget;
        public int? nb_signalement { get; private set; } = nb_signalement; 
        public int? id_thematique { get; private set; } = id_thematique;
        public Guid? id_groupe { get; private set; } = id_groupe;

        public PropositionRemoteSource() : this(null, null, null, null, null, null, null, null) { }
    }
}
