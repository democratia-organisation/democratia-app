using System.Text.Json.Nodes;

namespace com.democratia.Services
{
    /// <summary>
    /// Interface qui représente un fournisseur des services de l'API.
    /// </summary>
    public interface IClient
    {
        protected static readonly string BASE_URL = "https://projets.iut-orsay.fr/saes3-mmarti32/API/rest.php";
        private static readonly string? statutsMessage;
        private static readonly int statuts;
        private static readonly HttpClient? client;

        /// <summary>
        /// Fonction qui permet une instance d'un modèle
        /// </summary>
        /// <param name="model">le modèle à instancier</param>
        /// <returns>un json contenant du modèle nécessaire</returns>
        /// <exception cref="NotImplementedException"></exception>
        JsonArray GetInstance();
        bool SuprimmerInstance(int id);

    }
}

