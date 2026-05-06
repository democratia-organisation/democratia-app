namespace com.koyok.democratia.Domain.Models
{
    public class Proposition(int? idProposition, string? titre, string? description, string? publication, float? budget, int? nombreSignalement, int? thematique, Guid? idGroupe) : IModel
    {
        public int? idProposition { get; private set; } = idProposition;
        public string? titre { get; private set; } = titre;
        public string? description { get; private set; } = description;
        public string? publication { get; private set; } = publication;
        public float? budget { get; private set; } = budget;
        public int? nombreSignalement { get; private set; } = nombreSignalement;
        public int? thematique { get; private set; } = thematique;
        public Guid? idGroupe { get; private set; } = idGroupe;
        public string formatDateFinDiscussion => DateOnly.FromDateTime(DateTime.Parse(DateOnly.Parse(publication!).ToString())).AddDays(jourDiscussion).ToString("dd MMMM yyy");
        public int jourDiscussion { get; set; } = 1;
        public Proposition() : this(null, null, null, null, null, null, null, null) { }


    }
}
