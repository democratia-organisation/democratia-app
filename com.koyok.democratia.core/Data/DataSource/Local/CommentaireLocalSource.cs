using SQLite;
using SQLiteNetExtensions.Attributes;

namespace com.koyok.democratia.Data.DataSource.Local
{
    [Table("Commentaire")]
    internal class CommentaireLocalSource : ILocalSource
    {
        [PrimaryKey]
        public int IdCommentaire { get; set; }
        public string? ContenuCommentaire { get; set; }
        public DateTime Horodatage { get; set; }
        public int NbSignalement { get; set;}
        [ForeignKey(typeof(GroupeLocalSource))]
        public Guid IdGroupe { get; set; }
        [OneToOne]
        public GroupeLocalSource? Groupe { get; set; }
        [ForeignKey(typeof(PropositionLocalSource))]
        public int IdProposition{ get; set; }
        [OneToOne]
        public PropositionLocalSource? Proposition { get; set; }
        [ForeignKey(typeof(InternauteLocalSource))]
        public int IdInternaute { get; set; }
        [OneToOne]
        public InternauteLocalSource? Internaute { get; set; }


    }
}
