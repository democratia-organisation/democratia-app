
using System.Text.Json.Serialization;

namespace com.democratia.Models
{
    public class Thematique : IModel
    {
        public int? id_thematique {  get; set; }
        public string? nom_thematique { get; set; }


        [JsonConstructor]
        public Thematique(int? id_thematique, string? nom_thematique)
        {
            this.id_thematique = id_thematique;
            this.nom_thematique = nom_thematique;
        }

        public Thematique() : this(null,null) { }
    }
}
