
using com.koyok.democratia.core.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace com.koyok.democratia.Models
{
    [method: JsonConstructor]
    public partial class ThematiqueRemoteSource(int? id_thematique, string? nom_thematique, float? budget, float? budget_groupe) : ObservableObject, IModel
    {
        public int? id_thematique { get; set; } = id_thematique;
        public string? nom_thematique { get; set; } = nom_thematique;        
        [JsonPropertyName("budget_thematique")]
        public float? budget { get; set; } = budget;
        [JsonPropertyName("budget")]
        public float? budget_groupe { get; set; } = budget_groupe;
        // TODO: attente de la refonte de la bd pour de plus ample précision
        [ObservableProperty]
        private float somme_utilise = 0;
        [ObservableProperty]
        private float somme_attente = 0;
        [ObservableProperty]
        private float ratio_utilise = 0.5f;
        [ObservableProperty]
        private float ratio_en_attente = 0.8f;
        public ThematiqueRemoteSource(string? nom_thematique) : this(null, nom_thematique, null, null) { }

        public ThematiqueRemoteSource() : this(null, null, null, null) { }

        public override string ToString() => nom_thematique ?? string.Empty;
        
    }
    public record class ThematiqueEqualityComparer : IEqualityComparer<ThematiqueRemoteSource>
    {
        public bool Equals(ThematiqueRemoteSource? x, ThematiqueRemoteSource? y)
        {
            if (x is null || y is null) return false;
            return x.nom_thematique?.ToLower() == y.nom_thematique?.ToLower();
        }
        public int GetHashCode(ThematiqueRemoteSource obj)
        {
            return obj.nom_thematique?.GetHashCode() ?? 0;
        }
    }
}
