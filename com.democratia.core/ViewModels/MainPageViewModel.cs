using BCrypt.Net;
using com.democratia.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace com.democratia.ViewModels
{
    public partial class MainPageViewModel(INavigationService? navigationService) : ConnectableViewModel, INavigeablleViewModel
    {
        [ObservableProperty]
        private string? adresseMail;

        [ObservableProperty]
        private string? motDePasse;

        [ObservableProperty]
        private string? errorMessage;

        public MainPageViewModel() : this(null) {}

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            try
            {
                if (commande == "Home") ConnecterInternaute();
                await navigationService!.GoToAsync(commande);
            }
            catch (Exception)
            {
                ErrorMessage = $"Erreur lors de la navigation vers {commande}";
            }
        }

        public async Task<Internaute?> ConnecterInternaute()
        {
            HttpClient client = new() { BaseAddress = new("https://projets.iut-orsay.fr/saes3-mmarti32/API/rest.php") };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"""?request=SELECT id_internaute, courriel FROM internaute WHERE courriel=?&parameters=+["{AdresseMail}"]""");
            if (!response.IsSuccessStatusCode)
                throw new Exception();
            else
            {
                var jsonArray = await response.Content.ReadAsStringAsync();
                Dictionary<string,object> dictionnary;
                try { dictionnary = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonArray); } 
                catch (Exception e) { throw new Exception($"erreur de réception {e.Message}"); }
                var rawElement = (JsonElement)dictionnary["data"];
                Object message;
                switch (rawElement.ValueKind)
                {
                    case JsonValueKind.String:
                        message = rawElement.GetString();
                        break;

                    case JsonValueKind.Number:
                        message = rawElement.GetInt32(); // ou raw.GetInt32().ToString(), selon besoin
                        break;

                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        message = rawElement.GetBoolean();
                        break;

                    case JsonValueKind.Null:
                        throw new Exception("aucune valeur donnée");
                        break;

                    default:
                        message = rawElement.GetRawText(); // renvoie le JSON brut (utile si objet/tableau)
                        break;
                }
                if (message is int) return null;
                else return new();
                // TODO : transformer le string de message en un dictionnaire utilisable
                // TODO : récupérer le mot de passe afin vérifier le user du bon mot de passe avant de créer l'utilsatuer
                
            }
        }

        private Tuple<string, string> RecuprerInformationConnexion()
        {
            throw new NotImplementedException("Not implemented");
        }

        private string DecrypterMotDePasseUtilisateur()
        {
            throw new NotImplementedException("Not implemented");
        }

        private bool VerifierBonneInformation()
        {
            throw new NotImplementedException("Not implemented");
        }
    }
}

