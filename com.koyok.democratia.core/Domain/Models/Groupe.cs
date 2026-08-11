namespace com.koyok.democratia.Domain.Models
{
    public class Groupe(Guid? idGroupe, string? nomGroupe, string? couleurGroupe, string? image, float? budget, int? nombreDeJourVote, int? nombreDeJourDiscuss, int? nombreSignalement, bool? isAdmin, int? imageSize) : IModel
    {

        public Guid? idGroupe = idGroupe;
        public string? nomGroupe { get; set; } = nomGroupe;

        
        public string? couleurGroupe { get; set; } = couleurGroupe;

        public string? image { get; set; } = image;

        public float? budget = budget;

        public int? nombreDeJourVote = nombreDeJourVote;

        public int? nombreDeJourDiscuss = nombreDeJourDiscuss;

        public int? nombreSignalement = nombreSignalement;

        public int? imageSize = imageSize;

        public bool? isAdmin = isAdmin;

        // TODO : attente de plus ample calcul
        public float  ratioUtilise = 0.5f;

        public float ratioAttente = 0.7f;

        public Groupe() : this(null,null,null,null,null,null,null,null, null,null) { }
    }
}
