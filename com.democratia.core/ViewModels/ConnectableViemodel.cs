using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Storage;
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

        protected static List<object> RecuprerInformationConnexion(string stringJson)
        {
            Dictionary<string, object> dictionnary;
            var finalJson = stringJson.Trim();
            try { dictionnary = JsonSerializer.Deserialize<Dictionary<string, object>>(finalJson)!; }
            catch (Exception) { throw new Exception("Erreur lors de la récupération des données"); }
            var rawElement = dictionnary.TryGetValue("data", out var data) ? data.ToString() : throw new Exception("Erreur lors de la récupération des données");
            return JsonSerializer.Deserialize<List<object>>(rawElement!)!;

        }
        protected async void EnregistrerInternaute(Internaute internaute)
        {
            string jsonInternaute = JsonSerializer.Serialize(internaute);
            string cacheDir = FileSystem.Current.CacheDirectory;
            string filePath = Path.Combine(cacheDir, "internaute_cache.json");
            if (!File.Exists(filePath)) await File.WriteAllTextAsync(filePath, jsonInternaute);
        }
        protected async Task<Internaute?> RetrouverInternaute()
        {
            string cacheDir = FileSystem.Current.CacheDirectory;
            string filePath = Path.Combine(cacheDir, "internaute_cache.json");
            if (File.Exists(filePath))
            {
                string jsonInternaute = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<Internaute>(jsonInternaute)!;
            }
            else return null;
        }
    }

}
