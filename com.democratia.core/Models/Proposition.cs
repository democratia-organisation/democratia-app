using System.Text.Json.Serialization;

namespace com.democratia.Models
{
    public class Proposition
    {
        [JsonPropertyName("id_proposition")]
        public int? IdProposition { get; init; }

        [JsonPropertyName("titre_proposition")]
        public string? Titre { get; init; }

        [JsonPropertyName("description_proposition")]
        public string? Description { get; init; }

        [JsonPropertyName("date_publication")]
        public string? Publication { get; init; }

        [JsonPropertyName("budget")]
        public float? Budget { get; init; }

        [JsonPropertyName("nb_signalement")]
        public int? NombreSignalement { get; init; }

        [JsonPropertyName("id_thematique")]
        public int? Thematique { get; init; }

        [JsonPropertyName("id_groupe")]
        public string? IdGroupe { get; init; } 
        public Proposition() { }
    }
}
