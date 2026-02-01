using com.democratia.core.Utils;
using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using Crypt = BCrypt.Net.BCrypt;

namespace com.democratia.ViewModels
{

    public partial class GestionCompteViewModel : ConnectableViewModel, IQueryAttributable, INotifyPropertyChanged
    {
        [ObservableProperty] private string? retourMessage;
        [ObservableProperty] public string? prenom_internaute;
        [ObservableProperty] public string? nom_internaute;
        [ObservableProperty] public string? adresse_postal;
        [ObservableProperty] public string? courriel;
        [ObservableProperty] public string? hashageMdp;


        public Internaute? internaute;
        private readonly ILocalizationService? localizationService;
        public GestionCompteViewModel(IEnumerable<IClient> clients, ILocalizationService? localizationService) 
            : base(clients.OfType<InternauteClient>().FirstOrDefault(), localizationService)
        {
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        [RelayCommand]
        public void ActionInternaute(string action)
        {
            RetourMessage = action;
        }

        [RelayCommand]
        public async Task ModifierInternaute()
        {
            RecupererInformations();
            Verification.VerifierFormatage(internaute!.courriel!, new(@"^[\w.\+\-]+@[\w\-]+\.[A-Za-z]{2,}$"));
            Verification.VerifierFormatage(internaute!.hashageMDP!, new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$"));
            await HasherMotDePasse();
            await client?.UpdateModelAsync(internaute)!;
            EnregistrerModele(internaute!);
            RetourEcran("Modification");
        }

        [RelayCommand]
        public async Task SupprimerCompte()
        {
            await client?.DeleteModelAsync(internaute)!;
            RetourEcran("Suppression");
        }

        private void RetourEcran(string action)
        {
            if (((InternauteClient)client!).succes) RetourMessage = $"OK {action}";
            else RetourMessage = $"Erreur {action}";
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("modele", out var valeur)) internaute = (Internaute)valeur;
            else internaute = await RetrouverModele<Internaute>();
        }


        private async Task HasherMotDePasse() => await Task.Run(() => internaute!.hashageMDP = Crypt.HashPassword(internaute!.hashageMDP!));


        private void RecupererInformations()
        {
            internaute!.prenom_internaute = Merge(internaute.prenom_internaute, Prenom_internaute);
            internaute!.nom_internaute = Merge(internaute.nom_internaute, Nom_internaute);
            internaute!.adresse_postal = Merge(internaute.adresse_postal, Adresse_postal);
            internaute!.courriel = Merge(internaute.courriel, Courriel);
            internaute!.hashageMDP = Merge(internaute.hashageMDP, HashageMdp);
        }

        private static string? Merge(string? baseValue, string? newValue) =>
            string.IsNullOrEmpty(newValue) ? baseValue : newValue;

    }
}
