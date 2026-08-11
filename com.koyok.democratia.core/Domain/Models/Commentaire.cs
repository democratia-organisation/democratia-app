namespace com.koyok.democratia.Domain.Models
{
    public class Commentaire(int id_commentaire, string? contenu_message, DateTime horodatage, int nb_signalement, string nom_auteur, string prenom_auteur, Role role, int id_internaute) : IModel
    {
        public int idCommentaire = id_commentaire;

        public string? contenuMessage { get; set; } = contenu_message;
        public DateTime horodatage = horodatage;
        public int nbSignalement = nb_signalement;
        public string nomAuteur = nom_auteur;
        public string prenomAuteur = prenom_auteur;
        public Role role = role;
        public int idInternaute = id_internaute;
        public bool? himself;

    }
}