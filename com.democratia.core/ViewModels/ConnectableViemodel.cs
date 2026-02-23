using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Storage;
using System.ComponentModel;
using System.Text.Json;

namespace com.democratia.ViewModels
{
    /// <summary>
    /// Classe abstraite qui représente tout viewModel qui peut se connecter à l'API
    /// </summary>
    public abstract class ConnectableViewModel(IClient? client, ILocalizationService? localizationService) : ObservableObject, INotifyPropertyChanged
    {
        protected IClient? client = client;
        public IClient? Client => client;
        
        protected readonly ILocalizationService? LocalizationService = localizationService;


        protected List<object> RecuprerInformationConnexion(string stringJson)
        {
            Dictionary<string, object> dictionnary;
            var finalJson = stringJson.Trim();
            try { dictionnary = JsonSerializer.Deserialize<Dictionary<string, object>>(finalJson)!; }
            catch (Exception) { throw new Exception($"{LocalizationService?.GetString("erreurDonne")}"); }
            var rawElement = dictionnary.TryGetValue("data", out var data) ? data.ToString() : throw new Exception($"{localizationService?.GetString("erreurDonne")}");
            return JsonSerializer.Deserialize<List<object>>(rawElement!)!;

        }
        protected async void EnregistrerModele<T>(T model) where T : class, IModel
        {
            string jsonInternaute = JsonSerializer.Serialize<T>(model);
            string cacheDir = FileSystem.Current.CacheDirectory;
            string filePath = Path.Combine(cacheDir,"cache", $"{model.GetType()}_cache.json");
            if (!File.Exists(filePath)) await File.WriteAllTextAsync(filePath, jsonInternaute);
            else
            {
                File.Delete(filePath);
                await File.WriteAllTextAsync(filePath, jsonInternaute);
            }
        }

        protected async Task<T?> RetrouverModele<T>() where T : class, IModel
        {
            string cacheDir = FileSystem.Current.CacheDirectory;
            string filePath = Path.Combine(cacheDir, "cache", $"{typeof(T)}_cache.json");
            if (File.Exists(filePath))
            {
                string jsonInternaute = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<T>(jsonInternaute)!;
            }
            else return null;
        }
    }

}
