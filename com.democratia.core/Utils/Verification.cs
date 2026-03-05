using com.democratia.Models;
using System.Text.RegularExpressions;
using Crypt = BCrypt.Net.BCrypt;


namespace com.democratia.Utils
{
    public static class Verification
    {
        public  static bool VerifierFormatage(string valeur, FormatRule champ) => champ.Check(valeur);

        // Tâche rendu asynchrone à cause du temps d'execution de la fonction Verify
        public static async Task HasherMotDePasse(Internaute internaute) => await Task.Run(() => internaute!.hashageMDP = Crypt.HashPassword(internaute!.tempMDP!));
        
        public static async Task<bool> VerifierMotDePasseUtilisateur(string plainMotDePasse, string hashedMotDePasse) => await Task.Run(() => Crypt.Verify(plainMotDePasse, hashedMotDePasse));

        public record FormatRule(string pattern)
        {
            private readonly Regex _regex = new(pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);

            public bool Check(string value) => value is string str && _regex.IsMatch(str);
        }
    }
}
