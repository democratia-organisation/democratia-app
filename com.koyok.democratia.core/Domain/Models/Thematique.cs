
using CommunityToolkit.Mvvm.ComponentModel;

namespace com.koyok.democratia.Domain.Models
{
    public partial class Thematique(int? id_thematique, string? nom_thematique, float? budget, float? budget_groupe) : ObservableObject, IModel
    {
        public int? idThematique { get; set; } = id_thematique;
        public string? nomThematique { get; set; } = nom_thematique;        
        public float? budget { get; set; } = budget;
        public float? budgetGroupe { get; set; } = budget_groupe;
        // TODO: attente de la refonte de la bd pour de plus ample précision
        [ObservableProperty]
        public partial float sommeUtilise { get; set; } = 0;
        [ObservableProperty]
        public partial float sommeAttente { get; set; } = 0;
        [ObservableProperty]
        public partial float ratioUtilise { get; set; }  = 0.5f;
        [ObservableProperty]
        public partial float ratioEnAttente { get; set; } = 0.8f;
        public Thematique(string? nom_thematique) : this(null, nom_thematique, null, null) { }

        public Thematique() : this(null, null, null, null) { }

        public override string ToString() => nomThematique ?? string.Empty;
        
    }
}
