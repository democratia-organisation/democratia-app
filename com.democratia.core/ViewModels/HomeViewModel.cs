using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace com.democratia.ViewModels
{
    public partial class HomeViewModel : ConnectableViewModel , IQueryAttributable, INotifyPropertyChanged, INavigeablleViewModel
    {
        private Internaute? internaute;
        private readonly INavigationService? navigationService;
        public ObservableCollection<Groupe> Groupes { get; private set; } = [];
        public readonly List<Groupe> listeRecu = [];
        public HomeViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients)
            : base(clients?.OfType<GroupClient>().FirstOrDefault())
        {
            this.navigationService = navigationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.TryGetValue("modele", out var valeur)) internaute = (Internaute)valeur ;
            else internaute = await RetrouverModele<Internaute>();
        }

        public async void InitializeAsync()
        {

            var jsonString = string.Empty;
            try
            { jsonString = await client?.GetModelAsync(internaute!)!; }
            catch (Exception)
            { throw new Exception("Erreur de connexion inattendu"); }
            List<object> listeInformation = RecuprerInformationConnexion(jsonString);
            Groupes.Clear();
            listeInformation.ForEach(groupe => Groupes.Add(JsonSerializer.Deserialize<Groupe>(groupe.ToString()!)!));

        }

        public async Task<ImageSource> GetImageAsync(string url)
        {
            return await ((GroupClient)client!).GetImageAsync(url)!;
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            ShellNavigationQueryParameters? parameters = commande == "/Home/GestionCompte" ? new ShellNavigationQueryParameters
            {
                { "modele", internaute! }
            } : null;
            await navigationService?.GoToAsync(commande,parameters)!;

        }
        
        [RelayCommand]
        public async Task OpenGroup(string nomGroupe) 
        { 
            var parameters = new ShellNavigationQueryParameters { { "nomGroupe", nomGroupe } };
            await navigationService?.GoToAsync("Groupe", parameters)!;
        }
    }
}
