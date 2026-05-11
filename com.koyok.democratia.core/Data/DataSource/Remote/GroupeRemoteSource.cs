using System.Text.Json.Serialization;

namespace com.koyok.democratia.Data.DataSource.Remote
{
    [method: JsonConstructor]
    public partial class GroupeRemoteSource(Guid? id_groupe, 
        string? nom_groupe, 
        string? couleur_groupe, 
        string? image, float? budget,
        int? nbj_dft_vote,
        int? nbj_dft_discuss,
        int? nb_signalement) : IRemoteSource
    {
        public Guid? id_groupe { get; set; } = id_groupe;
        public string? nom_groupe { get; set; } = nom_groupe;
        public string? couleur_groupe { get; set; } = couleur_groupe;
        public string? image { get; set; } = image;
        public float? budget { get; set; } = budget;
        public int? nbj_dft_vote { get; set; } = nbj_dft_vote;
        public int? nbj_dft_discuss { get; set; } = nbj_dft_discuss;
        public int? nb_signalement { get; set; } = nb_signalement;

        public GroupeRemoteSource() : this(null,null,null,null,null,null,null,null) { }
    }
}
