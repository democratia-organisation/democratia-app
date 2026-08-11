namespace com.koyok.democratia.Domain.Models
{
    public class Thematique(int? id_thematique, string? nom_thematique, float? budget, float? budget_groupe)
    {
        public int? idThematique = id_thematique;
        public string? nomThematique = nom_thematique;        
        public float? budget = budget;
        public float? budgetGroupe = budget_groupe;
        // TODO: attente de la refonte de la bd pour de plus ample précision
        
        public float sommeUtilise = 0;
        
        public float sommeAttente = 0;
        
        public float ratioUtilise  = 0.5f;
        
        public float ratioEnAttente = 0.8f;
        public Thematique(string? nom_thematique) : this(null, nom_thematique, null, null) { }

        public Thematique() : this(null, null, null, null) { }

        public override string ToString() => nomThematique ?? string.Empty;
        
    }
}
