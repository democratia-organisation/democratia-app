
using System.Text.Json.Serialization;

namespace com.koyok.democratia.Data.DataSource.Remote
{
    [method: JsonConstructor]
    public partial class ThematiqueRemoteSource(int? id_thematique, string? nom_thematique, float? budget, float? budget_groupe) : ILocalSource
    {
        public int? id_thematique { get; set; } = id_thematique;
        public string? nom_thematique { get; set; } = nom_thematique;        
        public float? budget_thematique { get; set; } = budget;
        public float? budget { get; set; } = budget_groupe;
        public ThematiqueRemoteSource(string? nom_thematique) : this(null, nom_thematique, null, null) { }

        public ThematiqueRemoteSource() : this(null, null, null, null) { }

        public override string ToString() => nom_thematique ?? string.Empty;
        
    }
}
