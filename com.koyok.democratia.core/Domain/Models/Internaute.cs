using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Utils;

namespace com.koyok.democratia.Domain.Models
{

    public class Internaute(int? id_internaute, string? nom_internaute, string? prenom_internaute, string? adresse_postale, string? courriel, string? hashageMDP) : IModel
    {
        public int? idInternaute { get; set; } = id_internaute;
        public string? nomInternaute { get; set; } = nom_internaute;
        public string? prenomInternaute { get; set; } = prenom_internaute;
        public string? adressePostale { get; set; } = adresse_postale;
        public string? courriel
        {
            get; set
            {
                if (value is null) { field = value; return; }
                if (!Verification.VerifierFormatage(value!, new(@"^[\w.\+\-]+@[\w\-]+\.[A-Za-z]{2,}$")))
                    throw new MailException();
                else field = value;
            }
        } = courriel;
        public string? hashageMDP { get; set; } = hashageMDP;
        public string? tempMDP { 
            get; set 
            {
                if (value is null) { field = value; return; }
                if (!Verification.VerifierFormatage(value!, new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$")))
                    throw new PassWordException();
                else field = value;
            }
        }

        public Internaute() : this(null, null, null, null, null, null) { }

    }
}
