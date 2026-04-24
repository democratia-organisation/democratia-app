using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace com.democratia.ViewModels.internaute
{
    public partial class HomeViewModel : ConnectableViewModel , IQueryAttributable, INotifyPropertyChanged, INavigeablleViewModel
    {
        public Internaute? internaute;
        private readonly INavigationService? navigationService;
        private readonly Services.AppContext context;
        private int cursor = 0;

        [ObservableProperty]
        private ObservableCollection<Tuple<Groupe, ImageSource, ICommand>> groupes = [];

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
        private async Task InitialisationListe()
        {
            if (isRefreshing)
            {
                await InitializeAsync();
                isRefreshing = false;
            }
            else return;
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
            foreach (var groupe in listeInformation)
            {
                ImageSource image = await GetImageAsync(groupe.Image);
                Groupes.Add(new Tuple<Groupe, ImageSource, ICommand>(groupe, image, OpenGroupCommand));
            }
        }

        [RelayCommand]
        private async Task OpenGroup(Tuple<Groupe, ImageSource, ICommand> tuple)
        {
            var parameters = new ShellNavigationQueryParameters { { "groupe", tuple.Item1! }, { "Image", tuple.Item2! }, { "modele", internaute! } };
            context.Groupe = tuple.Item1;
            await navigationService?.GoToAsync("GroupePage", parameters)!;
        }


        private async Task<ImageSource> GetImageAsync(string? url) => (await client!.GetImageAsync(url))!;

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
