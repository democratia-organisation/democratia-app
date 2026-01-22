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

        public MainPageViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients)
            : base(clients?.OfType<InternauteClient>().FirstOrDefault())
        {
            this.navigationService = navigationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        public MainPageViewModel() : base(null) { }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            try
            {
                if (commande == "/Home")
                {
                    modele = await ConnecterInternaute();
                    var parameters = new ShellNavigationQueryParameters { { "modele", modele! } };
                    ErrorMessage = "";
                    await navigationService!.GoToAsync(commande, parameters);
                }
                else await navigationService!.GoToAsync(commande);

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
            List<object> listeInformation = RecuprerInformationConnexion(jsonString);
            if(listeInformation.Count == 0) throw new Exception("Pas d'utilisateur trouvé");
            var internaute = JsonSerializer.Deserialize<Internaute>(listeInformation![0].ToString()!);
            string motDePasseHash = internaute?.hashageMDP!;
            EnregistrerInternaute(internaute!);
            if (motDePasseHash != MotDePasse && ! await VerifierMotDePasseUtilisateur(motDePasseHash)) 
                throw new Exception("Mot de passe incorrecte");// verification brut car user ont des mot de passe non hashé dans la BDD 
            return internaute;
        }
        // Tâche rendu asynchrone à cause du temps d'execution de la fonction Verify
        private async Task<bool> VerifierMotDePasseUtilisateur(string hashedMotDePasse) => await Task.Run(() => Crypt.Verify(MotDePasse, hashedMotDePasse));

    }
}

