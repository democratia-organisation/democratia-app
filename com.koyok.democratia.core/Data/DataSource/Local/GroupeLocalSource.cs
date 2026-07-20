using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace com.koyok.democratia.Data.DataSource.Local
{
    public partial class GroupeLocalSource(Guid? idGroupe, string? nomGroupe, string? couleurGroupe, string? image, float? budget, int? nombreDeJourVote, int? nombreDeJourDiscuss, int? nombreSignalement, int? imageSize, bool? isAdmin) : ObservableObject, ILocalSource
    {
        [PrimaryKey]
        public Guid? IdGroupe { get; set; } = idGroupe;
        public string? NomGroupe { get; set; } = nomGroupe;
        public string? CouleurGroupe { get; set; } = couleurGroupe;
        public string? Image { get; set; } = image;
        public float? Budget { get; set; } = budget;
        public int? NombreDeJourVote { get; set; } = nombreDeJourVote;
        public int? NombreDeJourDiscuss { get; set; } = nombreDeJourDiscuss;
        public int? NombreSignalement { get; set; } = nombreSignalement;
        public int? ImageSize { get; set; } = imageSize;
        public bool? IsAdmin { get; set; } = isAdmin;

        public GroupeLocalSource() : this(null,null,null,null,null,null,null,null,null,null) { }
    }
}
