using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using Crypt = BCrypt.Net.BCrypt;


namespace com.democratia.ViewModels.internaute.gestionCompte
{
    public partial class ModifierGestionPageViewModel : ConnectableViewModel, INavigeablleViewModel, INotifyPropertyChanged, IQueryAttributable
    {
        private INavigationService NavigationService { get; set; }
        private ILocalizationService localizationService { get; set; }
        [ObservableProperty] private string? retourMessage;
        private Internaute? internaute;
        [ObservableProperty] private Internaute? tempInternaute = new();

        public ModifierGestionPageViewModel(INavigationService navigationService, ILocalizationService localizationService, IEnumerable<IClient> clients) 
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
            bool estUnBonEmail = Verification.VerifierFormatage(internaute!.courriel!, new(@"^[\w.\+\-]+@[\w\-]+\.[A-Za-z]{2,}$"));
            if (!estUnBonEmail)
            {
                RetourMessage = localizationService?.GetString("InvalidEmailFormat");
                return;
            }
            bool estUnBonMotDePasse;
            if (!string.IsNullOrEmpty(TempInternaute!.hashageMDP) && internaute.hashageMDP != TempInternaute!.hashageMDP)
            {
                estUnBonMotDePasse = Verification.VerifierFormatage(TempInternaute!.hashageMDP, new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$"));
                if (!estUnBonMotDePasse)
                {
                    RetourMessage = localizationService?.GetString("InvalidPasswordFormat");
                    return;
                }
                internaute!.hashageMDP = Merge(internaute.hashageMDP, TempInternaute!.hashageMDP);
                await HasherMotDePasse();
            }
            await client?.UpdateModelAsync(internaute)!;
            EnregistrerModele(internaute!);
            WeakReferenceMessenger.Default.Send<EventModificationSuccessSender>();
        }

        private async Task HasherMotDePasse() => await Task.Run(() => internaute!.hashageMDP = Crypt.HashPassword(internaute!.hashageMDP!));

        private void RecupererInformations()
        {
            internaute!.prenom_internaute = Merge(internaute.prenom_internaute, TempInternaute!.prenom_internaute);
            internaute!.nom_internaute = Merge(internaute.nom_internaute, TempInternaute!.nom_internaute);
            internaute!.adresse_postale = Merge(internaute.adresse_postale, TempInternaute!.adresse_postale);
            internaute!.courriel = Merge(internaute.courriel, TempInternaute!.courriel);
        }

        private static string? Merge(string? baseValue, string? newValue) =>
            string.IsNullOrEmpty(newValue) ? baseValue : newValue;

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = query.TryGetValue("internaute", out var value) ? 
                (Internaute)value : await RetrouverModele<Internaute>();
        }

        public record EventModificationSuccessSender() { }
    }
}
