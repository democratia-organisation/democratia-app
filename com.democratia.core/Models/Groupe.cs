using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace com.democratia.Models
{
    public class Groupe : IModel
    {
        [JsonPropertyName("id")]
        public Guid? IdGroupe { get; set; }
        [JsonPropertyName("nom_groupe")]
        public string? NomGroupe { get; set; }
        [JsonPropertyName("couleur_groupe")]
        public string? CouleurGroupe { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
        [JsonPropertyName("budget")]
        public float? Budget { get; set; }
        [JsonPropertyName("nbj_dft_vote")]
        public int? NombreDeJourVote { get; set; }
        [JsonPropertyName("nbj_dft_discuss")]
        public int? NombreDeJourDiscuss { get; set; }
        [JsonPropertyName("nb_signalement")]
        public int? NombreSignalement { get; set; }
        [JsonConstructor]
        public Groupe(Guid? idGroupe, string? nomGroupe, string? couleurGroupe, string? image, float? budget, int? nombreDeJourVote, int? nombreDeJourDiscuss, int? nombreSignalement)
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
        public Groupe() : this(null,null,null,null,null,null,null,null) { }
    }
}
