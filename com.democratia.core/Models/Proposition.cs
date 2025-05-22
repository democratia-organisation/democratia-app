
namespace com.democratia.Models
{
    public class Proposition(int? idProposition,string? titre, string? description, string? publication, float? budget, int? nombreSignalement, string? thematique, int? idGroupe)
    {
        public int? IdProposition { get; private set; } = idProposition;
        public string? Titre { get; private set; } = titre;
        public string? Description { get; private set; } = description;
        public string? Publication { get; private set; } = publication;
        public float? Budget { get; private set; } = budget;
        public int? nombreSignalement { get; private set; } = nombreSignalement;
        public string? Thematique { get; private set; } = thematique;
        public int? IdGroupe { get; private set; } = idGroupe;
        public Proposition() : this(null, null, null, null, null, null, null, null) {}

    }
}
