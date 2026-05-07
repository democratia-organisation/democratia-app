
using CommunityToolkit.Mvvm.ComponentModel;
using SQLiteNetExtensions.Attributes;

namespace com.koyok.democratia.Data.DataSource.Local
{
    public partial class ThematiqueLocalSource(int? id_thematique, string? nom_thematique, float? budget) : ObservableObject, ILocalSource
    {
        public int? IdThematique { get; set; } = id_thematique;
        public string? NomThematique { get; set; } = nom_thematique;        
        public float? Budget { get; set; } = budget;
        [ForeignKey(typeof(GroupeLocalSource))]
        public Guid IdGroupe { get; set; }
        public ThematiqueLocalSource(string? nom_thematique) : this(null, nom_thematique, null) { }

        public ThematiqueLocalSource() : this(null, null, null) { }
        
    }
}
