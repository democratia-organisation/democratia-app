using com.democratia.Utils;
using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Text.Json;
using Microsoft.Maui.Storage;

namespace com.democratia.ViewModels.internaute
{
    public partial class MainViewModel : ConnectableViewModel, INavigeablleViewModel
    {
        [ObservableProperty]
        private string? adresseMail;

        private Internaute? modele;

        [ObservableProperty]
        private string? motDePasse;

        [ObservableProperty]
        private string? errorMessage;

        private readonly INavigationService? navigationService;

        public MainViewModel(INavigationService navigationService, IEnumerable<IClient?>? clients, ILocalizationService localization)
            : base(clients!.OfType<InternauteClient>().FirstOrDefault(), localization)
        {
            this.navigationService = navigationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        public MainViewModel() : base(null, null) { }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            if (commande == "/HomePage")
            {
                try {
                    modele = await ConnecterInternaute(false);
                }
                catch (Exception ex) {
#if DEBUG
                    ErrorMessage = MapExceptionMessage.MappingException(ex, LocalizationService!);
#elif !DEBUG
                    ErrorMessage = LocalizationService?.GetString("erreurInattendu");    
#endif
                    return;
                }
                var parameters = new ShellNavigationQueryParameters { { "modele", modele! } };
                await navigationService!.GoToAsync(commande, parameters);
            }
            else await navigationService!.GoToAsync(commande);
            
        }

        internal async Task<Internaute?> ConnecterInternaute(bool autoConnect)
        {

            if (string.IsNullOrWhiteSpace(AdresseMail)) throw new EmptyEmailFieldException();
            else if (string.IsNullOrWhiteSpace(MotDePasse) && !autoConnect) throw new EmptyPassWordFieldException();
            try
            {
                await SecureStorage.Default.SetAsync("id_internaute", AdresseMail);
                string jsonString = await client?.GetModelAsync(AdresseMail)!;
                List<Internaute> listeInformation = RecuprerInformationConnexion<Internaute>(jsonString);
                if (listeInformation.Count == 0) throw new NoUserException();
                var internaute = listeInformation![0];
                string motDePasseHash = internaute?.hashageMDP!;
                bool estAuthetifie;
#if DEBUG
                if (MotDePasse != "root")
                // les mots de passe avec le mot root ne vont pas dans tempMDP pour éviter une erreur
                {
                    internaute!.tempMDP = MotDePasse; // utilisation de internaute.tempMDP car son set vérifie le format du mot de passe
                    bool hashedPasswordIsNotEqual = !await Verification.VerifierMotDePasseUtilisateur(internaute!.tempMDP!, motDePasseHash);
                    estAuthetifie = motDePasseHash != internaute!.tempMDP || hashedPasswordIsNotEqual;
                }
                else
                    estAuthetifie = true;
#elif !DEBUG
                internaute!.tempMDP = MotDePasse;
                estAuthetifie = !await Verification.VerifierMotDePasseUtilisateur(internaute!.tempMDP!, motDePasseHash);;
#endif
                if (!estAuthetifie) throw new BadPasswordException();
                else
                {
                    EnregistrerModele(internaute!);
                    return internaute;
                }
            }
            catch (Exception)
            { throw; }
        }

        public async Task AutoConnect()
        {
            string? mail = await SecureStorage.Default.GetAsync("id_internaute") ;
            if (mail == null) return;
            string reponse = await client!.GetModelAsync(mail, "relogin");
            var success = bool.Parse(JsonSerializer.Deserialize<Dictionary<string, object>>(reponse)!["success"].ToString()!); ;
            if (success)
            {
                AdresseMail = mail;
                modele = await ConnecterInternaute(true);
                var parameters = new ShellNavigationQueryParameters { { "modele", modele! } };
                await navigationService!.GoToAsync("/HomePage", parameters)!;
            }

        }
    }
}