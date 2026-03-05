using com.democratia.Utils;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using Crypt = BCrypt.Net.BCrypt;
using com.democratia.Models;
using CommunityToolkit.Mvvm.Messaging;

namespace com.democratia.ViewModels.internaute.gestionCompte
{
    public partial class CreationViewModel : ConnectableViewModel, INavigeablleViewModel
    {

        [ObservableProperty] private Internaute? _internaute = new();
        [ObservableProperty] private string? retourMessage;
        [ObservableProperty] private string? password; // passowrd tempon afin d'éviter le set à chaque écriture dans la varible
        [ObservableProperty] private string? email;
        private readonly INavigationService? navigationService;
        private readonly ILocalizationService? localizationService;
        

        public CreationViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients, ILocalizationService service)
            : base(clients?.OfType<InternauteClient>().FirstOrDefault(), service)
        {
            this.navigationService = navigationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
            this.localizationService = service;
        }

        public CreationViewModel() : base(null, null) { }

        [RelayCommand]
        public async Task NavigateTapped(string commande) => await navigationService?.GoToAsync(commande)!;

        [RelayCommand]
        public async Task CreerInternauteTapped()
        {
            try
            {
                await CreerInternaute();
                WeakReferenceMessenger.Default.Send<EventCreationSucess>();
            }
            catch (Exception ex)
            {
#if DEBUG
                RetourMessage = ex.Message;
#elif !DEBUG
                RetourMessage = localizationService?.GetString("erreurInattendu");    
#endif

            }
        }


        internal async Task CreerInternaute()
        {
            try
            {
                Internaute!.tempMDP = Password;
                Internaute.courriel = Email;
                if (await VerifierToutesLesConditions())
                {
                    Internaute!.hashageMDP = Crypt.HashPassword(Internaute!.tempMDP);
                    string reponse = await client?.CreateModelAsync(Internaute!.nom_internaute, Internaute!.prenom_internaute, Internaute!.courriel, Internaute!.adresse_postale, Internaute.hashageMDP )!;
                    List<object> values = RecuprerInformationConnexion(reponse);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool VerifierChampComplet()
        {
            if (!(!string.IsNullOrWhiteSpace(Internaute!.adresse_postale) &&
              !string.IsNullOrWhiteSpace(Internaute!.nom_internaute) &&
              !string.IsNullOrWhiteSpace(Internaute!.prenom_internaute) &&
              !string.IsNullOrWhiteSpace(Internaute!.hashageMDP) &&
              !string.IsNullOrWhiteSpace(Internaute!.courriel)))
                throw new EmptyRequiredFieldException();
            else return true;
        }
        
        private async Task<bool> VerifierMailDoublon()
        {
            string retourJson = await ((InternauteClient?)client)?.DoublonEmailAsync(Internaute!.courriel!)!;
            List<object>? listeInformation = RecuprerInformationConnexion(retourJson);
            int? nombreMail = JsonSerializer.Deserialize<Dictionary<string, int>>(listeInformation[0].ToString()!)!.TryGetValue("COUNT(courriel)", out var value) ? value : null;
            return nombreMail == 0 ? true : throw new CompteExistantException();
        }
        

        private async Task<bool> VerifierToutesLesConditions() => VerifierChampComplet() && await VerifierMailDoublon();

        public record EventCreationSucess() { }
    }
}
