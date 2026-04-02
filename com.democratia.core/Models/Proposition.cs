using System.Text.Json.Serialization;

namespace com.democratia.Models
{
    [method: JsonConstructor]
    public class Proposition(int? idProposition, string? titre, string? description, string? publication, float? budget, int? nombreSignalement, int? thematique, Guid? idGroupe) : IModel
    {
        [JsonPropertyName("id_proposition")]
        public int? IdProposition { get; private set; } = idProposition;
        [JsonPropertyName("titre_proposition")]
        public string? Titre { get; private set; } = titre;
        [JsonPropertyName("description_proposition")] 
        public string? Description { get; private set; } = description;
        [JsonPropertyName("date_publication")] 
        public string? Publication { get; private set; } = publication;
        [JsonPropertyName("budget")] 
        public float? Budget { get; private set; } = budget;
        [JsonPropertyName("nb_signalement")] 
        public int? NombreSignalement { get; private set; } = nombreSignalement;
        [JsonPropertyName("id_thematique")] 
        public int? Thematique { get; private set; } = thematique;
        [JsonPropertyName("id_groupe")]
        public Guid? IdGroupe { get; private set; } = idGroupe;
        public DateOnly FormatDateFinDiscussion => DateOnly.FromDateTime(DateTime.Parse(DateOnly.Parse(Publication!).ToString("dd MMMM yyy"))).AddDays(JourDiscussion);
        public int JourDiscussion { get; set; } = 1;
        public Proposition() : this(null, null, null, null, null, null, null, null) { }


    }
}
