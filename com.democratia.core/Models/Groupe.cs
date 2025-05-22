namespace com.democratia.Models
{
    public class Groupe(int? idGroupe, string? nomGroupe, string? couleurGroupe, string? image, float? budget, int? nombreDeJourVote, int? nombreDeJourDiscuss, int? nombreSignalement)
    {
        public int? IdGroupe { get; private set; } = idGroupe;
        public string? NomGroupe { get; private set; } = nomGroupe;
        public string? CouleurGroupe { get; private set; } = couleurGroupe;
        public string? Image { get; private set; } = image;
        public float? Budget { get; private set; } = budget;
        public int? NombreDeJourVote { get; private set; } = nombreDeJourVote;
        public int? NombreDeJourDiscuss { get; private set; } = nombreDeJourDiscuss;
        public int? NombreSignalement { get; private set; } = nombreSignalement;
    }
}
