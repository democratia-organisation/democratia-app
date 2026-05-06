using System.Text.Json.Serialization;

namespace com.koyok.democratia.Data.DataSource.Remote
{
    [method: JsonConstructor]
    public partial class GroupeRemoteSource(Guid? idGroupe, string? nomGroupe, string? couleurGroupe, string? image, float? budget, int? nombreDeJourVote, int? nombreDeJourDiscuss, int? nombreSignalement) : ILocalSource
    {
        public Guid? id { get; set; } = idGroupe;
        public string? nom_groupe { get; set; } = nomGroupe;
        public string? couleur_groupe { get; set; } = couleurGroupe;
        public string? image { get; set; } = image;
        public float? budget { get; set; } = budget;
        public int? nbj_dft_vote { get; set; } = nombreDeJourVote;
        public int? nbj_dft_discuss { get; set; } = nombreDeJourDiscuss;
        public int? nb_signalement { get; set; } = nombreSignalement;

        public GroupeRemoteSource() : this(null,null,null,null,null,null,null,null) { }
    }
}
