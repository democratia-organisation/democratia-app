using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Text.Json;
using Crypt = BCrypt.Net.BCrypt;

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

        private readonly INavigationService? navigationService;
        private readonly IEnumerable<IClient?>? clients;

        public MainPageViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients)
            : base(clients?.OfType<InternauteClient>().FirstOrDefault())
        {
            this.navigationService = navigationService;
            this.clients = clients;
            if (client is null)
                this.client = clients?.OfType<FakeClient>().FirstOrDefault();
        }

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
                    ErrorMessage = "";
                    await navigationService!.GoToAsync(commande, parameters);
                }
                else await navigationService!.GoToAsync(commande, null);

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        internal async Task<Internaute?> ConnecterInternaute()
        {

            if (string.IsNullOrEmpty(AdresseMail) || string.IsNullOrEmpty(MotDePasse))
            {
                if (string.IsNullOrEmpty(AdresseMail)) throw new ArgumentException("Veuillez saisir votre adresse mail");
                else throw new ArgumentException("Veuillez saisir votre mot de passe");
            }
            string jsonString;
            try
            { jsonString = await client?.GetModelAsync(AdresseMail)!; }
            catch (Exception)
            { throw new Exception("Erreur de connexion inattendu"); }
            List<Dictionary<string, object>> listeInformation = RecuprerInformationConnexion(jsonString);
            string motDePasseHash = listeInformation?[0]["hashageMDP"]?.ToString()!;
            bool motDePasseValide = await VerifierMotDePasseUtilisateur(motDePasseHash);
            if (!motDePasseValide) throw new Exception("Mot de passe incorrecte");

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


        private static List<Dictionary<string, object>> RecuprerInformationConnexion(string stringJson)
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

        // Tâche rendu asynchrone à cause du temps d'execution de la fonction Verify
        private async Task<bool> VerifierMotDePasseUtilisateur(string hashedMotDePasse) => await Task.Run(() => Crypt.Verify(MotDePasse, hashedMotDePasse));

    }
}

