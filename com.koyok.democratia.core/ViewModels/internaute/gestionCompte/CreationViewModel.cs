using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using com.koyok.democratia.Domain.Utils;
using com.koyok.democratia.Data.Repository;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Service;
using com.koyok.democratia.Domain.Repository;
using AppContext = com.koyok.democratia.Domain.Utils.AppContext;
using com.koyok.democratia.Domain.Exception;

namespace com.koyok.democratia.UI.internaute.gestionCompte
{
    public partial class CreationViewModel(IInternauteRepository? repository, AppContext? context) : ObservableObject
    {

        [ObservableProperty] public partial Internaute? internaute { get; set; } = new();
        [ObservableProperty] public partial string? retourMessage { get; set; }
        [ObservableProperty] public partial string? password { get; set; } // passowrd tempon afin d'éviter le set à chaque écriture dans la varible
        [ObservableProperty] public partial string? email { get; set; }
        private readonly IInternauteRepository? _internauteRepository = repository;
        private readonly AppContext? _context = context;
        

        public CreationViewModel() : this(null, null) { }

        [RelayCommand]
        public async Task NavigateTapped(string commande) 
            => await Shell.Current?.GoToAsync(commande, new ShellNavigationQueryParameters { { "modele", internaute! } })!;

        [RelayCommand]
        private async Task CreerInternauteTapped()
        {
            try
            {
                await CreerInternaute();
            }
            catch (Exception ex)
            {
                retourMessage = _context!.Mapper!.MappingException(ex);
            }
            WeakReferenceMessenger.Default.Send<EventCreationSucess>();
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
            if (!(!string.IsNullOrWhiteSpace(internaute!.adressePostale) &&
              !string.IsNullOrWhiteSpace(internaute!.nomInternaute) &&
              !string.IsNullOrWhiteSpace(internaute!.prenomInternaute) &&
              !string.IsNullOrWhiteSpace(internaute!.tempMDP) &&
              !string.IsNullOrWhiteSpace(internaute!.courriel)))
                throw new EmptyRequiredFieldException();
            else return true;
        }
        
        private async Task<bool> VerifierMailDoublon()
        {
            string retourJson = await ((InternauteRepository?)client)?.DoublonEmailAsync(internaute!.courriel!)!;
            List<Dictionary<string, int>>? listeInformation = RecuprerInformationConnexion<Dictionary<string, int>>(retourJson);
            int? nombreMail = listeInformation![0].TryGetValue("COUNT(courriel)", out var value) ? value : null;
            return nombreMail == 0 ? true : throw new CompteExistantException();
        }
        

        private async Task<bool> VerifierToutesLesConditions() => VerifierChampComplet() && await VerifierMailDoublon();

        public record EventCreationSucess() { }
    }
}
