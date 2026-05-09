using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Extension;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Enumerations;
using com.koyok.democratia.Domain.UseCase;

namespace com.koyok.democratia.UI.internaute.gestionCompte
{
    public partial class CreationViewModel(InsertionCompteUseCase? useCase) : ObservableObject
    {

        [ObservableProperty] public partial Internaute? internaute { get; set; } = new();
        [ObservableProperty] public partial string? retourMessage { get; set; }
        [ObservableProperty] public partial string? email { get; set; }
        private readonly InsertionCompteUseCase? _useCase = useCase;

        public CreationViewModel() : this(null) { }

        [RelayCommand]
        public async Task NavigateTapped(string commande) 
            => await Shell.Current?.GoToAsync(commande, new ShellNavigationQueryParameters { { "modele", internaute! } })!;

        [RelayCommand]
        private async Task CreerInternauteTapped()
        {
            try
            {
                if (VerifierChampComplet())
                {
                    internaute!.courriel = email;
                    await _useCase!.InsertAsync(TypeGestion.AJOUTER, internaute!);

                }
            }
            catch (Exception ex)
            {
                retourMessage = Shell.Current?.AppContext.Mapper!.MappingException(ex);
            }
            WeakReferenceMessenger.Default.Send<EventCreationSucess>();
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

        public record EventCreationSucess() { }
    }
}
