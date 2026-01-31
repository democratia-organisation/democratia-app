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


        private Internaute? internaute;
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
            internaute = (Internaute)query["modele"];
        }

        private async Task HasherMotDePasse() => await Task.Run(() => internaute!.hashageMDP = Crypt.HashPassword(internaute!.hashageMDP!));


        private void RecupererInformations()
        {
            string?[] attributsInterne = { Prenom_internaute, Nom_internaute, Adresse_postal, Courriel, HashageMdp };
            string?[] attributsDeBase = { internaute?.prenom_internaute, internaute?.nom_internaute, internaute?.adresse_postal, internaute?.courriel, internaute?.hashageMDP };
            for (int i = 0; i < attributsInterne.Length; i++)
                ModifierAttribut(ref attributsDeBase[i], attributsInterne[i]);
        }

        private void ModifierAttribut(ref string? attributDeBase, string? attributInterne) =>
            attributDeBase = string.IsNullOrEmpty(attributInterne) ? attributDeBase : attributInterne;

    }
}
