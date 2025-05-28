using System.Text.Json.Nodes;

namespace com.democratia.Services
{
    /// <summary>
    /// Interface qui représente un fournisseur des services de l'API.
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Fonction qui permet une instance d'un modèle
        /// </summary>
        /// <returns>un json contenant du modèle nécessaire</returns>
        public JsonArray GetInstance();
        public bool SuprimmerInstance(int id);

    }
}

