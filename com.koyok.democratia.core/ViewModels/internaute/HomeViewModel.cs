using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Extension;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Data.Repository;

namespace com.koyok.democratia.UI.internaute
{
    public partial class HomeViewModel(IGroupeRepository repository) :  ObservableObject, IQueryAttributable, INotifyPropertyChanged
    {
        public Internaute? internaute;
        private readonly IGroupeRepository repository = repository;
        private int cursor = 0;

        [ObservableProperty]
        public partial ObservableCollection<Tuple<Groupe, string, ICommand>> groupes { get; set; } = [];

        [ObservableProperty]
        public partial bool isRefreshing { get; set; } = false;

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
            => internaute = query.TryGetValue("modele", out var data) ? (Internaute)data : Shell.Current.AppContext.Internaute ;
        

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
            { jsonString = await repository.GetModelAsync(internaute!)!; }
            catch (Exception)
            { throw new ConnexionErrorException(); }
            List<Groupe> listeInformation = repository.RecuprerInformationConnexion<Groupe>(jsonString);
            groupes.Clear();
            foreach (var groupe in listeInformation)
            {
                string image = await GetImageAsync(groupe.image);
                groupes.Add(new Tuple<Groupe, string, ICommand>(groupe, image, OpenGroupCommand));
            }
        }

        [RelayCommand]
        private async Task OpenGroup(Tuple<Groupe, string, ICommand> tuple)
        {
            var parameters = new ShellNavigationQueryParameters { { "groupe", tuple.Item1! }, { "Image", tuple.Item2! }, { "modele", internaute! } };
            Shell.Current.AppContext.Groupe = tuple.Item1;
            Shell.Current.AppContext.ImageSourceGroupe = tuple.Item2;
            await Shell.Current?.GoToAsync("GroupePage", parameters)!;
        }


        private async Task<string> GetImageAsync(string? url)
        {
            var imageStream = await repository.GetImageAsync(url);
            if (imageStream != null)
                return imageStream;
            else
                return "default_group_image.png";
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande) 
            => await Shell.Current?.GoToAsync(commande, new ShellNavigationQueryParameters{{ "modele", internaute! }})!;

        [RelayCommand]
        private async Task RefreshListGroupe()
        {
            cursor += 1;
        }
    }
}
