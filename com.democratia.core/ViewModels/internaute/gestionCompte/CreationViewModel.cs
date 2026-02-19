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

        [ObservableProperty] private readonly Internaute? _internaute;
        [ObservableProperty] private readonly string? retourMessage;
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
                if (await VerifierToutesLesConditions())
                {

                    string reponse = await client?.CreateModelAsync(Internaute!.nom_internaute, Internaute!.prenom_internaute, Internaute!.courriel, Internaute!.adresse_postale, Crypt.HashPassword(Internaute!.hashageMDP))!;
                    List<object> values = RecuprerInformationConnexion(reponse);
                    if (values.Count != 0) throw new Exception($"{localizationService?.GetString("erreurCreation")}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool VerifierChampComplet()
        {
            if (!(!string.IsNullOrEmpty(Internaute!.adresse_postale) &&
              !string.IsNullOrEmpty(Internaute!.nom_internaute) &&
              !string.IsNullOrEmpty(Internaute!.prenom_internaute) &&
              !string.IsNullOrEmpty(Internaute!.hashageMDP) &&
              !string.IsNullOrEmpty(Internaute!.courriel)))
                throw new Exception($"{localizationService?.GetString("champComplet")}");
            else return true;
        }
        private bool VerifierFormatageMail() => 
            Verification.VerifierFormatage(Internaute!.courriel!, new(@"^[\w.\+\-]+@[\w\-]+\.[A-Za-z]{2,}$")) ? true : throw new Exception($"{localizationService?.GetString("formatEmail")}");
        
        private async Task<bool> VerifierMailDoublon()
        {
            string retourJson = await ((InternauteClient?)client)?.DoublonEmailAsync(Internaute!.courriel!)!;
            List<object>? listeInformation = RecuprerInformationConnexion(retourJson);
            int? nombreMail = JsonSerializer.Deserialize<Dictionary<string, int>>(listeInformation[0].ToString()!)!.TryGetValue("COUNT(courriel)", out var value) ? value : null;
            return nombreMail == 0 ? true : throw new Exception($"{localizationService?.GetString("compteExistantErreur")}");
        }
        private bool VerifierFormatageMotDePasse() => 
            Verification.VerifierFormatage(Internaute!.hashageMDP!, new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$")) ? true : throw new Exception($"{localizationService?.GetString("formatMotDePasse")}");
        

        private async Task<bool> VerifierToutesLesConditions() => VerifierChampComplet() && VerifierFormatageMail() && await VerifierMailDoublon() && VerifierFormatageMotDePasse();

        public record EventCreationSucess() { }
    }
}
