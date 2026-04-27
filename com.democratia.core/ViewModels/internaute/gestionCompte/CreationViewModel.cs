using com.democratia.Utils;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using com.democratia.Models;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;

namespace com.democratia.ViewModels.internaute.gestionCompte
{
    public partial class CreationViewModel : ConnectableViewModel, INavigeablleViewModel
    {

        [ObservableProperty] public partial Internaute? internaute { get; set; } = new();
        [ObservableProperty] public partial string? retourMessage { get; set; }
        [ObservableProperty] public partial string? password { get; set; } // passowrd tempon afin d'éviter le set à chaque écriture dans la varible
        [ObservableProperty] public partial string? email { get; set; }
        private readonly INavigationService? navigationService;
        

        public CreationViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients, ILocalizationService service)
            : base(clients?.OfType<InternauteClient>().FirstOrDefault(), service)
        {
            this.navigationService = navigationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        public CreationViewModel() : base(null, null) { }

        [RelayCommand]
        public async Task NavigateTapped(string commande) 
            => await navigationService?.GoToAsync(commande, new ShellNavigationQueryParameters { { "modele", internaute! } })!;

        [RelayCommand]
        private async Task CreerInternauteTapped()
        {
            try
            {
                await CreerInternaute();
                WeakReferenceMessenger.Default.Send<EventCreationSucess>();
            }
            catch (Exception ex)
            {
#if DEBUG
                retourMessage = ex.Message;
#elif !DEBUG
                RetourMessage = localizationService?.GetString("erreurInattendu");    
#endif

            }
        }


        internal async Task CreerInternaute()
        {
            try
            {
                internaute!.tempMDP = password;
                internaute!.courriel = email;
                if (await VerifierToutesLesConditions())
                {
                    await Verification.HasherMotDePasse(internaute!);
                    string reponse = await client?.CreateModelAsync(internaute!.nom_internaute, internaute!.prenom_internaute, internaute!.courriel, internaute!.adresse_postale, internaute!.hashageMDP )!;
                    List<object> values = RecuprerInformationConnexion<object>(reponse);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool VerifierChampComplet()
        {
            if (!(!string.IsNullOrWhiteSpace(internaute!.adresse_postale) &&
              !string.IsNullOrWhiteSpace(internaute!.nom_internaute) &&
              !string.IsNullOrWhiteSpace(internaute!.prenom_internaute) &&
              !string.IsNullOrWhiteSpace(internaute!.tempMDP) &&
              !string.IsNullOrWhiteSpace(internaute!.courriel)))
                throw new EmptyRequiredFieldException();
            else return true;
        }
        
        private async Task<bool> VerifierMailDoublon()
        {
            string retourJson = await ((InternauteClient?)client)?.DoublonEmailAsync(internaute!.courriel!)!;
            List<Dictionary<string, int>>? listeInformation = RecuprerInformationConnexion<Dictionary<string, int>>(retourJson);
            int? nombreMail = listeInformation![0].TryGetValue("COUNT(courriel)", out var value) ? value : null;
            return nombreMail == 0 ? true : throw new CompteExistantException();
        }
        

        private async Task<bool> VerifierToutesLesConditions() => VerifierChampComplet() && await VerifierMailDoublon();

        public record EventCreationSucess() { }
    }
}
