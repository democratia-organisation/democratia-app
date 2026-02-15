using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Text.Json;
using System.Collections.ObjectModel;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;

namespace com.democratia.ViewModels.internaute
{
    public partial class HomeViewModel : ConnectableViewModel , IQueryAttributable, INotifyPropertyChanged, INavigeablleViewModel
    {
        private Internaute? internaute;
        [ObservableProperty] private ImageSource? _image;
        private readonly INavigationService? navigationService;
        private readonly ILocalizationService? localizationService;
        private readonly TaskCompletionSource<bool> _internautePret = new(false);
        public ObservableCollection<Groupe> Groupes { get; private set; } = [];
        public readonly List<Groupe> listeRecu = [];
        public HomeViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients, ILocalizationService? localizationService)
            : base(clients?.OfType<GroupClient>().FirstOrDefault(), localizationService)
        {
            this.navigationService = navigationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
            this.localizationService = localizationService;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.TryGetValue("modele", out var valeur)) internaute = (Internaute)valeur ;
            else internaute = await RetrouverModele<Internaute>();
            _internautePret.TrySetResult(true);
        }

        public async void InitializeAsync()
        {
            
            await _internautePret.Task; // code pour résoudre un race condition error
            var jsonString = string.Empty;
            try
            { jsonString = await client?.GetModelAsync(internaute!)!; }
            catch (Exception)
            { throw new Exception($"{localizationService?.GetString("connexionErreur")}"); }
            List<object> listeInformation = RecuprerInformationConnexion(jsonString);
            Groupes.Clear();
            listeInformation.ForEach(groupe => Groupes.Add(JsonSerializer.Deserialize<Groupe>(groupe.ToString()!)!));

        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            ShellNavigationQueryParameters? parameters = commande == "/Home/GestionCompte" || commande == $"PremierePage" ? new ShellNavigationQueryParameters
            {
                { "modele", internaute! }
            } : null;
            await navigationService?.GoToAsync(commande,parameters)!;

        }
    }
}
