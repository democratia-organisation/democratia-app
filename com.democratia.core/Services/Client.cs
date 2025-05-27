using com.democratia.Models;
using System.Text.Json.Nodes;

namespace com.democratia.Services
{
    /// <summary>
    /// classe qui fournit tous les services de l'api
    /// </summary>
    public class Client
    {
        /// TODO : ne faire qu'une seule classe pour les services de l'api, donc abstraction maximal pour
        /// limiter le nombre de fonction
        /// 
        private readonly string BASE_URL;
        private string statutsMessage;
        private int statuts;

        /// <summary>
        /// Fonction qui permet de récupérer un internaute à partir de son id
        /// </summary>
        /// <param name="mailInternaute">identifiant de l'internaute</param>
        /// <param name="motDePasse">mot de passe de l'internaute</param>
        /// <returns>un json contenant les informations de l'internaute</returns>
        /// <exception cref="NotImplementedException"></exception>
        public JsonArray GetInternaute(string? mailInternaute, string? motDePasse)
        {
            throw new NotImplementedException();
        }

        public Client()
        {
            BASE_URL = "https://api.democratia.com/";
            statutsMessage = string.Empty;
            statuts = 0; 
        }
    }
}
