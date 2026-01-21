using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Text.Json;
using System.Drawing;
using System.Collections.ObjectModel;
// sophie.lemoine@example.com

namespace com.democratia.ViewModels
{
    public partial class HomeViewModel : ConnectableViewModel , IQueryAttributable, INotifyPropertyChanged, INavigeablleViewModel
    {
        private Internaute? internaute;
        private readonly INavigationService? navigationService;
        public ObservableCollection<Groupe> Groupes { get; private set; } = new();
        public readonly List<Groupe> listeRecu = new();
        public HomeViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients)
            : base(clients?.OfType<GroupClient>().FirstOrDefault())
        {
            this.navigationService = navigationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = (Internaute)query["modele"];
        }

        public async void InitializeAsync()
        {

            var jsonString = string.Empty;
            try
            { jsonString = await client?.GetModelAsync(internaute!)!; }
            catch (Exception)
            { throw new Exception("Erreur de connexion inattendu"); }
            List<Dictionary<string, object>> listeInformation = RecuprerInformationConnexion(jsonString);
            listeInformation.ForEach((groupe) =>
            {
                listeRecu.Add(new Groupe(
                    ((JsonElement?)groupe["id_groupe"])?.GetInt32() ?? 0,
                    groupe["nom_groupe"]?.ToString() ?? string.Empty,
                    groupe["couleur_groupe"]?.ToString() ?? string.Empty,
                    groupe["image"]?.ToString() ?? string.Empty,
                    ((JsonElement?)groupe["budget"])?.GetSingle() ?? 0,
                    ((JsonElement?)groupe["nbj_dft_vote"])?.GetInt32() ?? 0,
                    ((JsonElement?)groupe["nbj_dft_discuss"])?.GetInt32() ?? 0,
                    ((JsonElement?)groupe["nb_signalement"])?.GetInt32() ?? 0
                    ));
            });
        }

        public async Task<ImageSource> GetImageAsync(string url)
        {
            return await ((GroupClient)client!).GetImageAsync(url)!;
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            await navigationService?.GoToAsync(commande)!;
        }
        
        [RelayCommand]
        public async Task OpenGroup(string nomGroupe) 
        { 
            var parameters = new ShellNavigationQueryParameters { { "nomGroupe", nomGroupe } };
            await navigationService?.GoToAsync("Groupe", parameters)!;
        }
    }
}
