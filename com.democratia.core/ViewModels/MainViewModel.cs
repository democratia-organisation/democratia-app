using com.democratia.Utils;
using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Text.Json;

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
        private readonly ILocalizationService? localizationService;

        public MainViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients, ILocalizationService localization)
            : base(clients?.OfType<InternauteClient>().FirstOrDefault(), localization)
        {
            this.navigationService = navigationService;
            this.localizationService = localization;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        public MainViewModel() : base(null, null) { }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            try
            {
                if (commande == "/HomePage")
                {
                    modele = await ConnecterInternaute();
                    var parameters = new ShellNavigationQueryParameters { { "modele", modele! } };
                    await navigationService!.GoToAsync(commande, parameters);
                }
                else await navigationService!.GoToAsync(commande);

            }
            catch (Exception ex)
            {
#if DEBUG
                ErrorMessage = MapExceptionMessage.MappingException(ex,localizationService!); 
#elif !DEBUG
                ErrorMessage = localizationService?.GetString("erreurInattendu");    
#endif
            }
        }

        internal async Task<Internaute?> ConnecterInternaute()
        {

            if (string.IsNullOrWhiteSpace(AdresseMail)) throw new EmptyEmailFieldException();
            else if (string.IsNullOrWhiteSpace(MotDePasse)) throw new EmptyPassWordFieldException();
            try
            {
                string jsonString = await client?.GetModelAsync(AdresseMail)!;
                List<object> listeInformation = RecuprerInformationConnexion(jsonString);
                if (listeInformation.Count == 0) throw new NoUserException();
                var internaute = JsonSerializer.Deserialize<Internaute>(listeInformation![0].ToString()!);
                internaute!.tempMDP = MotDePasse;
                string motDePasseHash = internaute?.hashageMDP!;
#if DEBUG
                // utilisation de internaute.tempMDP car son set vérifie le format du mot de passe
                bool estAuthetifie = motDePasseHash != internaute!.tempMDP || !await Verification.VerifierMotDePasseUtilisateur(internaute.tempMDP, motDePasseHash);
#elif !DEBUG
                bool estAuthetifie = await Verification.VerifierMotDePasseUtilisateur(internaute.tempMDP, motDePasseHash);
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