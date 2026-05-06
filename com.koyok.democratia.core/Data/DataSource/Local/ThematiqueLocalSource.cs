
using CommunityToolkit.Mvvm.ComponentModel;
using SQLiteNetExtensions.Attributes;

namespace com.koyok.democratia.Data.DataSource.Local
{
    public partial class ThematiqueLocalSource(int? id_thematique, string? nom_thematique, float? budget, float? budget_groupe) : ObservableObject, ILocalSource
    {
        public int? IdThematique { get; set; } = id_thematique;
        public string? NomThematique { get; set; } = nom_thematique;        
        public float? Budget { get; set; } = budget;
        [ForeignKey(typeof(GroupeLocalSource))]
        public int IdGroupe { get; set; }
        public ThematiqueLocalSource(string? nom_thematique) : this(null, nom_thematique, null, null) { }

        public ThematiqueLocalSource() : this(null, null, null, null) { }

        public override string ToString() => NomThematique ?? string.Empty;
        
    }
}
