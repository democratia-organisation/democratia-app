using CommunityToolkit.Mvvm.ComponentModel;

namespace com.koyok.democratia.Domain.Models
{
    public partial class Proposition(int? idProposition, string? titre, string? description, string? publication, float? budget, int? nombreSignalement, int? thematique, Guid? idGroupe) : ObservableObject, IModel
    {
        public int? idProposition { get; private set; } = idProposition;
        [ObservableProperty] public partial string? titre { get; private set; } = titre;
        public string? description { get; private set; } = description;
        public string? publication { get; private set; } = publication;
        public float? budget { get; private set; } = budget;
        public int? nombreSignalement { get; private set; } = nombreSignalement;
        public int? thematique { get; private set; } = thematique;
        public Guid? idGroupe { get; private set; } = idGroupe;
        public string formatDateFinDiscussion => DateOnly.FromDateTime(DateTime.Parse(DateOnly.Parse(publication!).ToString())).AddDays(jourDiscussion).ToString("dd MMMM yyy");
        public int jourDiscussion { get; set; } = 1;
        public object Popularite { get; internal set; }
        public object Prix { get; internal set; }
        public object Reactions { get; internal set; }

        public Proposition() : this(null, null, null, null, null, null, null, null) { }


    }
}
