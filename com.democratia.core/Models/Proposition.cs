using System.Text.Json.Serialization;

namespace com.democratia.Models
{
    public class Proposition : IModel
    {
        [JsonConstructor]
        public Proposition(int? idProposition, string? titre, string? description, string? publication, float? budget, int? nombreSignalement, string? thematique, int? idGroupe)
        {
            this.IdProposition  = idProposition;
            this.Titre  = titre;
            this.Description  = description;
            this.Publication  = publication;
            this.Budget  = budget;
            this.nombreSignalement  = nombreSignalement;
            this.Thematique  = thematique;
            this.IdGroupe  = idGroupe;
        }
        public int? IdProposition { get; private set; } 
        public string? Titre { get; private set; } 
        public string? Description { get; private set; } 
        public string? Publication { get; private set; } 
        public float? Budget { get; private set; } 
        public int? nombreSignalement { get; private set; } 
        public string? Thematique { get; private set; } 
        public int? IdGroupe { get; private set; } 
        public Proposition() : this(null, null, null, null, null, null, null, null) { }
        

    }
}
