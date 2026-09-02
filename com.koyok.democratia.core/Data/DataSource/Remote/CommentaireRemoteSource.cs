using com.koyok.democratia.Lib;
using System.Text.Json.Serialization;
namespace com.koyok.democratia.Data.DataSource.Remote
{
    [method: JsonConstructor]
    internal class CommentaireRemoteSource(int id_commentaire, string? contenu_message, DateTime horodatage, int nb_signalement, string nom_internaute, string prenom_internaute, int id_role, Guid id_internaute) : IRemoteSource
    {

        public int id_commentaire { get; set; } = id_commentaire;
        public string? contenu_message { get; set; } = contenu_message;
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime horodatage { get; set; } = horodatage;
        public int nb_signalement { get; set; } = nb_signalement;
        public string prenom_internaute { get; set; } = prenom_internaute;
        public string nom_internaute { get; set; } = nom_internaute;
        public int id_role { get; set; } = id_role;
        public Guid id_internaute { get; set; } = id_internaute;
    }
        
}
