
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace com.koyok.democratia.Data.DataSource.Local
{
    [Table("Thematique")]
    public partial class  ThematiqueLocalSource : ILocalSource
    {
        public int? IdThematique { get; set; }
        public string? NomThematique { get; set; }
        public float? Budget { get; set; }
        [ForeignKey(typeof(GroupeLocalSource))]
        public Guid IdGroupe { get; set; }
        [OneToOne]
        public GroupeLocalSource? Groupe { get; set; }


    }
}
