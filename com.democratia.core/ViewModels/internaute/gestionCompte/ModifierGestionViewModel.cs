using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System.ComponentModel;


namespace com.democratia.ViewModels.internaute.gestionCompte
{
    public partial class ModifierGestionViewModel : ConnectableViewModel, INavigeablleViewModel, INotifyPropertyChanged, IQueryAttributable
    {
        private INavigationService NavigationService { get; set; }
        private ILocalizationService localizationService { get; set; }
        private Internaute? internaute;
        [ObservableProperty] private string? retourMessage;
        [ObservableProperty] private Internaute? tempInternaute = new();
        [ObservableProperty] private string? password;
        [ObservableProperty] private string? email;

        public ModifierGestionViewModel(INavigationService navigationService, ILocalizationService localizationService, IEnumerable<IClient> clients) 
            : base(clients.OfType<InternauteClient>().FirstOrDefault(),localizationService)
        {
            this.NavigationService = navigationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
            this.localizationService = localizationService;
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            
            await NavigationService.GoToAsync(commande);

        }

        [RelayCommand]
        private async Task ModifierInternaute()
        {
            RecupererInformations();
            if(!string.IsNullOrWhiteSpace(Password)) await Verification.HasherMotDePasse(internaute!);
            await client?.UpdateModelAsync(internaute)!;
            EnregistrerModele(internaute!);
            WeakReferenceMessenger.Default.Send<EventModificationSuccessSender>();
        }

        

        private void RecupererInformations()
        {
            internaute!.prenom_internaute = Merge(internaute.prenom_internaute, TempInternaute!.prenom_internaute);
            internaute!.nom_internaute = Merge(internaute.nom_internaute, TempInternaute!.nom_internaute);
            internaute!.adresse_postale = Merge(internaute.adresse_postale, TempInternaute!.adresse_postale);
            try
            {
                internaute!.courriel = Merge(internaute.courriel, Email);
                if(!string.IsNullOrWhiteSpace(Password)) internaute!.tempMDP = Merge(internaute.tempMDP, Password);
            }
            catch (Exception ex) {

                RetourMessage = MapExceptionMessage.MappingException(ex, localizationService!);
            }
        }

        private static string? Merge(string? baseValue, string? newValue) =>
            string.IsNullOrWhiteSpace(newValue) ? baseValue : newValue;

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = query.TryGetValue("internaute", out var value) ? 
                (Internaute)value : await RetrouverModele<Internaute>();
        }

        public record EventModificationSuccessSender() { }
    }
}
