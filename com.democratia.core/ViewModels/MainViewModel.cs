using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace com.democratia.ViewModels.internaute
{
    public partial class MainViewModel : ConnectableViewModel, INavigeablleViewModel
    {
        [ObservableProperty]
        public partial string? adresseMail { get; set; }

        public Internaute? modele { get; private set; }
        private Services.AppContext? contexte;

        [ObservableProperty]
        public partial string? motDePasse { get; set; }

        [ObservableProperty]
        public partial string? errorMessage { get; set; }
        private readonly INavigationService? navigationService;

        public MainViewModel(INavigationService navigationService, IEnumerable<IClient?>? clients, ILocalizationService localization, Services.AppContext context)
            : base(clients!.OfType<InternauteClient>().FirstOrDefault(), localization)
        {
            this.navigationService = navigationService;
            contexte = context;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        public MainViewModel() : base(null, null) { }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            try
            {
                modele = await ConnecterInternaute();
            }
            catch (Exception ex)
            {
#if DEBUG
                errorMessage = MapExceptionMessage.MappingException(ex, LocalizationService!);
#elif !DEBUG
                errorMessage = LocalizationService?.GetString("erreurInattendu");    
#endif
                return;
            }
            contexte!.Internaute = modele;
            var parameters = new ShellNavigationQueryParameters { { "modele", modele! } };
            await navigationService!.GoToAsync(commande, parameters);

        }

        internal async Task<Internaute?> ConnecterInternaute()
        {

            if (string.IsNullOrWhiteSpace(adresseMail)) throw new EmptyEmailFieldException();
            else if (string.IsNullOrWhiteSpace(motDePasse)) throw new EmptyPassWordFieldException();
            try
            {
                await SecureStorage.Default.SetAsync("id_internaute", adresseMail);
                string jsonString = await client?.GetModelAsync(adresseMail)!;
                List<Internaute> listeInformation = RecuprerInformationConnexion<Internaute>(jsonString);
                if (listeInformation.Count == 0) throw new NoUserException();
                var internaute = listeInformation![0];
                string motDePasseHash = internaute?.hashageMDP!;
                bool estAuthetifie;
#if DEBUG
                if (motDePasse != "root")
                // les mots de passe avec le mot root ne vont pas dans tempMDP pour éviter une erreur
                {
                    internaute!.tempMDP = motDePasse; // utilisation de internaute.tempMDP car son set vérifie le format du mot de passe
                    bool hashedPasswordIsNotEqual = !await Verification.VerifierMotDePasseUtilisateur(internaute!.tempMDP!, motDePasseHash);
                    estAuthetifie = motDePasseHash != internaute!.tempMDP || hashedPasswordIsNotEqual;
                }
                else
                    estAuthetifie = true;
#elif !DEBUG
                internaute!.tempMDP = motDePasse;
                estAuthetifie = !await Verification.VerifiermotDePasseUtilisateur(internaute!.tempMDP!, motDePasseHash);;
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
    }
}