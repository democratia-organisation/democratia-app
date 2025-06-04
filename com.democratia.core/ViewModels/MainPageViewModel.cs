using BCrypt.Net;
using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Text.Json;

namespace com.democratia.ViewModels
{
    public partial class MainPageViewModel : ConnectableViewModel, INavigeablleViewModel
    {
        [ObservableProperty]
        private string? adresseMail;

        private Internaute? modele;

        [ObservableProperty]
        private string? motDePasse;

        [ObservableProperty]
        private string? errorMessage;

        private INavigationService? navigationService;

        public MainPageViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients) 
            : this(clients?.OfType<InternauteClient>().FirstOrDefault())
        {
            this.navigationService = navigationService;
        }

        public MainPageViewModel(Client? client) : base(client) { }


        public MainPageViewModel() : base(null) { }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            try
            {
                if (commande == "Home")
                {
                    modele = await ConnecterInternaute();
                    var parameters = new ShellNavigationQueryParameters { { "modele", modele! } };
                    await navigationService!.GoToAsync(commande, parameters);
                }
                else await navigationService!.GoToAsync(commande,null);
                
            }
            catch (Exception ex)
            {
                // TODO : dans la branche main, garder comme message d'erreur : $"Erreur lors de la navigation vers {commande}"
                ErrorMessage = ex.Message;
            }
        }

        internal async Task<Internaute?> ConnecterInternaute()
        {
                
            var dictionnary = await client?.GetModelAsync(AdresseMail) !;
            var listeInformation = RecuprerInformationConnexion(dictionnary);
            // /!\ le casting est important car les valeurs ne
            // sont pas dans le type voulu mais dans le type JsonElement
            return new(
                    ((JsonElement?)listeInformation?[0]["id_internaute"])?.GetInt32() ?? 0,
                    listeInformation[0]["nom_internaute"]?.ToString() ?? string.Empty,
                    listeInformation[0]["prenom_internaute"]?.ToString() ?? string.Empty,
                    listeInformation[0]["adresse_postale"]?.ToString() ?? string.Empty,
                    listeInformation[0]["courriel"]?.ToString() ?? string.Empty
            );
        }


        private List<Dictionary<string, object>>? RecuprerInformationConnexion(string jsonArray)
        {
            Dictionary<string, object> dictionnary;
            try { dictionnary = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonArray)!; }
            catch (Exception) { throw new Exception($"erreur de réception des données"); }
            var rawElement = (JsonElement)dictionnary["data"];
            Object message;
            switch (rawElement.ValueKind)
            {

                case JsonValueKind.Number:
                    message = rawElement.GetInt32(); // ou raw.GetInt32().ToString(), selon besoin
                    break;

                case JsonValueKind.True:
                case JsonValueKind.False:
                    message = rawElement.GetBoolean();
                    break;

                default:
                    message = rawElement.GetRawText(); // renvoie le JSON brut (utile si objet/tableau)
                    break;
            }
            if (message is int) return null;
            else  return JsonSerializer.Deserialize<List<Dictionary<string, object>>>(message.ToString()!)!;

        }

        private bool VerifierMotDePasseUtilisateur(string hashedMotDePasse) => BCrypt.Net.BCrypt.Verify(MotDePasse,hashedMotDePasse);
        
    }
}

