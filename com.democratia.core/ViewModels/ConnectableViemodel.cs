using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;

namespace com.democratia.ViewModels
{
    /// <summary>
    /// Classe abstraite qui représente tout viewModel qui peut se connecter à l'API
    /// </summary>
    public abstract class ConnectableViewModel(IClient? client) : ObservableObject
    {
        protected IClient? client = client;
        public IClient? Client => client;

        protected static List<Dictionary<string, object>> RecuprerInformationConnexion(string stringJson)
        {
            Dictionary<string, object> dictionnary;
            try { dictionnary = JsonSerializer.Deserialize<Dictionary<string, object>>(stringJson)!; }
            catch (Exception) { throw new Exception("Erreur lors de la récupération des données"); }
            var rawElement = (JsonElement)dictionnary["data"];
            object message = rawElement.ValueKind switch
            {
                JsonValueKind.Array => rawElement.GetRawText(),
                _ => throw new Exception("Erreur lors de la connexion du compte"),
            };
            return JsonSerializer.Deserialize<List<Dictionary<string, object>>>(message.ToString()!)!;
        }
    }

}
