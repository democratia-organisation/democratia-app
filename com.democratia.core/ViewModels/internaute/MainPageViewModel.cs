using com.democratia.Utils;
using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Text.Json;
using Crypt = BCrypt.Net.BCrypt;

namespace com.democratia.ViewModels.internaute
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
        private readonly ILocalizationService? localizationService;

        public MainPageViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients, ILocalizationService localization)
            : base(clients?.OfType<InternauteClient>().FirstOrDefault(), localization)
        {
            this.navigationService = navigationService;
            this.localizationService = localization;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        public MainPageViewModel() : base(null, null) { }

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
#if DEBUG
                ErrorMessage = ex.Message;
#elif !DEBUG
                ErrorMessage = localizationService?.GetString("erreurInattendu");    
#endif

            }
        }

        internal async Task<Internaute?> ConnecterInternaute()
        {

            if (string.IsNullOrEmpty(AdresseMail) || string.IsNullOrEmpty(MotDePasse))
            {
                if (string.IsNullOrEmpty(AdresseMail)) throw new ArgumentException($"{localizationService?.GetString("errorMailMessage")}");
                else throw new ArgumentException($"{localizationService?.GetString("errorMailMessage")}");
            }
            string jsonString;
            try
            { jsonString = await client?.GetModelAsync(AdresseMail)!; }
            catch (Exception)
            { throw new Exception($"{localizationService?.GetString("connexionErreur")}"); }
            List<object> listeInformation = RecuprerInformationConnexion(jsonString);
            if(listeInformation.Count == 0) throw new Exception($"{localizationService?.GetString("noUser")}");
            var internaute = JsonSerializer.Deserialize<Internaute>(listeInformation![0].ToString()!);
            string motDePasseHash = internaute?.hashageMDP!;
            EnregistrerModele(internaute!);
#if DEBUG  
            if (motDePasseHash != MotDePasse && ! await VerifierMotDePasseUtilisateur(motDePasseHash)) 
                throw new Exception($"{localizationService?.GetString("mauvaisMdp")}");
#elif !DEBUG
            if(! await VerifierMotDePasseUtilisateur(motDePasseHash)) 
                throw new Exception($"{localizationService?.GetString("mauvaisMdp")}");
# endif
            return internaute;
        }
        // Tâche rendu asynchrone à cause du temps d'execution de la fonction Verify
        private async Task<bool> VerifierMotDePasseUtilisateur(string hashedMotDePasse) => await Task.Run(() => Crypt.Verify(MotDePasse, hashedMotDePasse));

    }
}