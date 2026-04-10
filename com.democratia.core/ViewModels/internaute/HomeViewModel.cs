using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using com.democratia.Utils;

namespace com.democratia.ViewModels.internaute
{
    public partial class HomeViewModel : ConnectableViewModel , IQueryAttributable, INotifyPropertyChanged, INavigeablleViewModel
    {
        public Internaute? internaute;
        private readonly INavigationService? navigationService;
        private readonly Services.AppContext context;
        private int cursor = 0;
        public ObservableCollection<Groupe> Groupes { get; private set; } = [];

        public HomeViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients, ILocalizationService? localizationService, Services.AppContext context)
            : base(clients?.OfType<GroupClient>().FirstOrDefault(), localizationService)
        {
            this.navigationService = navigationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
            this.context = context;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = query.TryGetValue("modele", out var user) ? (Internaute)user : context.Internaute ;
        }


        [RelayCommand]
        private async Task InitializeAsync()
        {
            var jsonString = string.Empty;
            try
            { jsonString = await client?.GetModelAsync(internaute!)!; }
            catch (Exception)
            { throw new ConnexionErrorException(); }
            List<Groupe> listeInformation = RecuprerInformationConnexion<Groupe>(jsonString);
            Groupes.Clear();
            listeInformation.ForEach(groupe => Groupes.Add(groupe));
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande) => 
            await navigationService?.GoToAsync(commande, new (){{ "modele", internaute! }})!;

        [RelayCommand]
        private async Task RefreshListGroupe()
        {
            cursor += 1;
        }



    }
}
