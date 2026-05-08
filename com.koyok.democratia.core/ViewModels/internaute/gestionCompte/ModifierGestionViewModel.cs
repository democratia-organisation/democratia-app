using com.koyok.democratia.Data.Repository;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System.ComponentModel;


namespace com.koyok.democratia.UI.internaute.gestionCompte
{
    public partial class ModifierGestionViewModel(Domain.Utils.AppContext appContext) 
        : ObservableObject, INotifyPropertyChanged, IQueryAttributable
    {
        private Internaute? internaute;
        [ObservableProperty] public partial string? retourMessage {get; set; }
        [ObservableProperty] public partial Internaute? tempInternaute { get; set; } = new();
        [ObservableProperty] public partial string? password {get; set; }
        [ObservableProperty] public partial string? email {get; set; }
        private Domain.Utils.AppContext appContext = appContext;

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            
            await Shell.Current?.GoToAsync(commande, new ShellNavigationQueryParameters { { "modele", internaute! } })!;

        }

        [RelayCommand]
        private async Task ModifierInternaute()
        {
            RecupererInformations();
            if(!string.IsNullOrWhiteSpace(password)) await Verification.HasherMotDePasse(internaute!);
            await client?.UpdateModelAsync(internaute)!;
            EnregistrerModele(internaute!);
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
            catch (Exception ex) 
            {

                retourMessage = appContext.Mapper!.MappingException(ex);
            }
        }

        private static string? Merge(string? baseValue, string? newValue) =>
            string.IsNullOrWhiteSpace(newValue) ? baseValue : newValue;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = query.TryGetValue("internaute", out var value) ?
                (Internaute)value : appContext.Internaute;
        }

        public record EventModificationSuccessSender() { }
    }
}
