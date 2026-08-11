namespace com.koyok.democratia.Domain.Models
{
    public class Proposition(int? idProposition, string? titre, string? description, string? publication, float? budget, int? nombreSignalement, int? thematique, Guid? idGroupe)
    {
        public int? idProposition = idProposition;
        public string? titre = titre;
        public string? description = description;
        public string? publication = publication;
        public float? budget = budget;
        public int? nombreSignalement = nombreSignalement;
        public int? thematique = thematique;
        public Guid? idGroupe = idGroupe;
        public string formatDateFinDiscussion => DateOnly.FromDateTime(DateTime.Parse(DateOnly.Parse(publication!).ToString())).AddDays(jourDiscussion).ToString("dd MMMM yyy");
        public int jourDiscussion { get; set; } = 1;
        public object Popularite { get; internal set; }
        public object Prix { get; internal set; }
        public object Reactions { get; internal set; }

        public Proposition() : this(null, null, null, null, null, null, null, null) { }


    }
}
