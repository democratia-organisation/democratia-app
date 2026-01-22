using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace com.democratia.Models
{
    public class Groupe : IModel
    {
        [JsonPropertyName("id_groupe")]
        public int? IdGroupe { get; private set; }
        [JsonPropertyName("nom_groupe")]
        public string? NomGroupe { get; private set; }
        [JsonPropertyName("couleur_groupe")]
        public string? CouleurGroupe { get; private set; }
        [JsonPropertyName("image")]
        public string? Image { get; private set; }
        [JsonPropertyName("budget")]
        public float? Budget { get; private set; }
        [JsonPropertyName("nbj_dft_vote")]
        public int? NombreDeJourVote { get; private set; }
        [JsonPropertyName("nbj_dft_discuss")]
        public int? NombreDeJourDiscuss { get; private set; }
        [JsonPropertyName("nb_signalement")]
        public int? NombreSignalement { get; private set; }
        [JsonConstructor]
        public Groupe(int? idGroupe, string? nomGroupe, string? couleurGroupe, string? image, float? budget, int? nombreDeJourVote, int? nombreDeJourDiscuss, int? nombreSignalement)
        {
            this.IdGroupe = idGroupe;
            this.NomGroupe = nomGroupe;
            this.CouleurGroupe = couleurGroupe;
            this.Image = image;
            this.Budget = budget;
            this.NombreDeJourVote = nombreDeJourVote;
            this.NombreDeJourDiscuss = nombreDeJourDiscuss;
            this.NombreSignalement = nombreSignalement;
        }
    }
}
