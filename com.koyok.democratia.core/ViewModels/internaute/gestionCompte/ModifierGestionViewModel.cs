using com.koyok.democratia.Data.Repository;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System.ComponentModel;


namespace com.koyok.democratia.UI.internaute.gestionCompte
{
    public partial class ModifierGestionViewModel : ConnectableViewModel, INavigeablleViewModel, INotifyPropertyChanged, IQueryAttributable
    {
        private INavigationService NavigationService { get; set; }
        private InternauteRemoteSource? internaute;
        [ObservableProperty] public partial string? retourMessage {get; set; }
        [ObservableProperty] public partial InternauteRemoteSource? tempInternaute { get; set; } = new();
        [ObservableProperty] public partial string? password {get; set; }
        [ObservableProperty] public partial string? email {get; set; }
        private.Domain.Service.AppContext appContext;

        public ModifierGestionViewModel(INavigationService navigationService, ILocalizationService localizationService, 
            IEnumerable<IRepository> clients,.Domain.Service.AppContext appContext) 
            : base(clients.OfType<InternauteRepository>().FirstOrDefault(),localizationService)
        {
            this.NavigationService = navigationService;
            this.appContext = appContext;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            
            await NavigationService.GoToAsync(commande, new ShellNavigationQueryParameters { { "modele", internaute! } });

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
            internaute!.prenom_internaute = Merge(internaute.prenom_internaute, tempInternaute!.prenom_internaute);
            internaute!.nom_internaute = Merge(internaute.nom_internaute, tempInternaute!.nom_internaute);
            internaute!.adresse_postale = Merge(internaute.adresse_postale, tempInternaute!.adresse_postale);
            try
            {
                internaute!.courriel = Merge(internaute.courriel, email);
                if(!string.IsNullOrWhiteSpace(password)) internaute!.tempMDP = Merge(internaute.tempMDP, password);
            }
            catch (Exception ex) {

                retourMessage = MapExceptionMessage.MappingException(ex, LocalizationService!);
            }
        }

        private static string? Merge(string? baseValue, string? newValue) =>
            string.IsNullOrWhiteSpace(newValue) ? baseValue : newValue;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = query.TryGetValue("internaute", out var value) ?
                (InternauteRemoteSource)value : appContext.Internaute;
        }

        public record EventModificationSuccessSender() { }
    }
}
