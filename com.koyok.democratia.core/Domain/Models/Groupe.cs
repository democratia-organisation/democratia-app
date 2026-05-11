using CommunityToolkit.Mvvm.ComponentModel;

namespace com.koyok.democratia.Domain.Models
{
    public partial class Groupe(Guid? idGroupe, string? nomGroupe, string? couleurGroupe, string? image, float? budget, int? nombreDeJourVote, int? nombreDeJourDiscuss, int? nombreSignalement) : ObservableObject, IModel
    {

        public Guid? idGroupe { get; set; } = idGroupe;
        [ObservableProperty]
        public partial string? nomGroupe { get; set; } = nomGroupe;

        public string? couleurGroupe { get; set; } = couleurGroupe;

        public string? image { get; set; } = image;

        public float? budget { get; set; } = budget;

        public int? nombreDeJourVote { get; set; } = nombreDeJourVote;

        public int? nombreDeJourDiscuss { get; set; } = nombreDeJourDiscuss;

        public int? nombreSignalement { get; set; } = nombreSignalement;

        // TODO : attente de plus ample calcul
        [ObservableProperty]
        public partial float  ratioUtilise { get; set; } = 0.5f;

        [ObservableProperty]
        public partial float ratioAttente { get; set; } = 0.7f;

        public Groupe() : this(null,null,null,null,null,null,null,null) { }
    }
}
