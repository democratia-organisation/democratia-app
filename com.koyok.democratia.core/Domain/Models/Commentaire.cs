using CommunityToolkit.Mvvm.ComponentModel;

namespace com.koyok.democratia.Domain.Models
{
    public partial class Commentaire(int id_commentaire, string? contenu_message, DateTime horodatage, int nb_signalement, string nom_auteur, string prenom_auteur, Role role, int id_internaute) : ObservableObject, IModel
    {
        public int idCommentaire = id_commentaire;

        [ObservableProperty]
        public partial string? contenuMessage { get; set; } = contenu_message;
        [ObservableProperty]
        public DateTime horodatage = horodatage;
        public int nbSignalement = nb_signalement;
        [ObservableProperty]
        public partial string nomAuteur { get; set; } = nom_auteur;
        [ObservableProperty]
        public partial string prenomAuteur { get; set; } = prenom_auteur;
        [ObservableProperty]
        public partial Role role { get; set; } = role;
        public int idInternaute { get; set; } = id_internaute;
        public bool? himself { get; set; }

    }
}