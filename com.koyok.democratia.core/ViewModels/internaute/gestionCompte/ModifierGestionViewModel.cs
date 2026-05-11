using com.koyok.democratia.Domain.Enumerations;
using com.koyok.democratia.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using com.koyok.democratia.Domain.Extension;
using com.koyok.democratia.Domain.UseCase;


namespace com.koyok.democratia.UI.internaute.gestionCompte
{
    public partial class ModifierGestionViewModel(InsertionCompteUseCase? useCase) 
        : ObservableObject, INotifyPropertyChanged, IQueryAttributable
    {
        private Internaute? internaute;
        [ObservableProperty] public partial string? retourMessage {get; set; }
        [ObservableProperty] public partial Internaute? tempInternaute { get; set; } = new();
        [ObservableProperty] public partial string? password {get; set; }
        [ObservableProperty] public partial string? email {get; set; }
        private readonly InsertionCompteUseCase? _useCase = useCase;

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        => await Shell.Current?.GoToAsync(commande, new ShellNavigationQueryParameters { { "modele", internaute! } })!;

        [RelayCommand]
        private async Task ModifierInternaute()
        {
            try
            {
                RecupererInformations();
                await _useCase!.InsertAsync(TypeGestion.MODIFIER, internaute!);
            }
            catch (Exception ex) 
            {
                retourMessage = Shell.Current?.AppContext.Mapper!.MappingException(ex);
            }
            WeakReferenceMessenger.Default.Send<EventModificationSuccessSender>();
        }


        private void RecupererInformations()
        {
            internaute!.prenomInternaute = Merge(internaute.prenomInternaute, tempInternaute!.prenomInternaute);
            internaute!.nomInternaute = Merge(internaute.nomInternaute, tempInternaute!.nomInternaute);
            internaute!.adressePostale = Merge(internaute.adressePostale, tempInternaute!.adressePostale);
            try
            {
                internaute!.courriel = Merge(internaute.courriel, email);
                if(!string.IsNullOrWhiteSpace(password)) internaute!.tempMDP = Merge(internaute.tempMDP, password);
            }
            catch
            {

                throw;
            }
        }

        private static string? Merge(string? baseValue, string? newValue) 
            => string.IsNullOrWhiteSpace(newValue) ? baseValue : newValue;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
         => internaute = (Internaute)query["modele"] ?? Shell.Current?.AppContext.Internaute;
        

        public record EventModificationSuccessSender() { }
    }
}
