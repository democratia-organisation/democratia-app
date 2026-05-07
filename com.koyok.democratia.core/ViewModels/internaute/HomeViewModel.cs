using com.koyok.democratia.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using com.koyok.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Data.Repository;
using com.koyok.democratia.core.Domain.Service;

namespace com.koyok.democratia.UI.internaute
{
    public partial class HomeViewModel : ConnectableViewModel , IQueryAttributable, INotifyPropertyChanged, INavigeablleViewModel
    {
        public InternauteRemoteSource? internaute;
        private readonly INavigationService? Shell.Current;
        private readonly.core.Domain.Utils.AppContext context;
        private int cursor = 0;

        [ObservableProperty]
        public partial ObservableCollection<Tuple<Groupe, ImageSource, ICommand>> groupes { get; set; } = [];

        [ObservableProperty]
        public partial bool isRefreshing { get; set; } = false;

        public HomeViewModel(INavigationService? Shell.Current, IEnumerable<Repository?>? clients, ILocalizationService? localizationService,.core.Domain.Utils.AppContext context)
            : base(clients?.OfType<GroupRepository>().FirstOrDefault(), localizationService)
        {
            this.Shell.Current = Shell.Current;
            client ??= clients?.OfType<FakeRepository>().FirstOrDefault();
            this.context = context;
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = query.TryGetValue("modele", out var user) ? (InternauteRemoteSource)user : context.Internaute ;
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
            groupes.Clear();
            foreach (var groupe in listeInformation)
            {
                ImageSource image = await GetImageAsync(groupe.Image);
                groupes.Add(new Tuple<Groupe, ImageSource, ICommand>(groupe, image, OpenGroupCommand));
            }
        }

        [RelayCommand]
        private async Task OpenGroup(Tuple<Groupe, ImageSource, ICommand> tuple)
        {
            var parameters = new ShellNavigationQueryParameters { { "groupe", tuple.Item1! }, { "Image", tuple.Item2! }, { "modele", internaute! } };
            context.Groupe = tuple.Item1;
            context.ImageSourceGroupe = tuple.Item2;
            await Shell.Current?.GoToAsync("GroupePage", parameters)!;
        }


        private async Task<ImageSource> GetImageAsync(string? url) => (await client!.GetImageAsync(url))!;

        [RelayCommand]
        public async Task NavigateTapped(string commande) => 
            await Shell.Current?.GoToAsync(commande, new (){{ "modele", internaute! }})!;

        [RelayCommand]
        private async Task RefreshListGroupe()
        {
            cursor += 1;
        }



    }
}
