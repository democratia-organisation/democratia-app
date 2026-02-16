
using System.Text.Json.Serialization;

namespace com.democratia.Models
{
    public class Thematique : IModel
    {
        public int? id_thematique { get; set; }
        public string? nom_thematique { get; set; }


        [JsonConstructor]
        public Thematique(int? id_thematique, string? nom_thematique)
        {
            this.id_thematique = id_thematique;
            this.nom_thematique = nom_thematique;
        }

        public Thematique(string? nom_thematique) : this(null, nom_thematique) { }

        public Thematique() : this(null, null) { }

        public override string ToString() => nom_thematique ?? string.Empty;
        
    }
    public class ThematiqueEqualityComparer : IEqualityComparer<Thematique>
    {
        public bool Equals(Thematique? x, Thematique? y)
        {
            if (x is null || y is null) return false;
            return x.nom_thematique?.ToLower() == y.nom_thematique?.ToLower();
        }
        public int GetHashCode(Thematique obj)
        {
            return obj.nom_thematique?.GetHashCode() ?? 0;
        }
    }
}
