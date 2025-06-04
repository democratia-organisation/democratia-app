
namespace com.democratia.Models
{
    
    public class Internaute(int? id_internaute, string? nom_internaute, string? prenom_internaute, string? adresse_postal, string? courriel) : IModel
    {
        public Internaute() : this(null,null,null,null,null)
        {
        }

        public int? id_internaute { get; private set; } = id_internaute;
        public string? nom_internaute { get; private set; } = nom_internaute;
        public string? prenom_internaute { get; private set; } = prenom_internaute;
        public string? adresse_postal { get; private set; } = adresse_postal;
        public string? courriel { get; private set; } = courriel;
    }
}
