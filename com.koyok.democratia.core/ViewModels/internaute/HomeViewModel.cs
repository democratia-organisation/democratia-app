using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Extension;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.UseCase;

namespace com.koyok.democratia.UI.internaute
{
    public partial class HomeViewModel(IGroupeRepository repository,
        IManipulateImage useCase) :  ObservableObject, IQueryAttributable, INotifyPropertyChanged
    {
        public Internaute? internaute;
        private readonly IGroupeRepository repository = repository;
        private readonly IManipulateImage useCase = useCase;
        private int cursor = 0;

        [ObservableProperty]
        public partial ObservableCollection<Groupe> groupes { get; set; } = [];

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
            List<Groupe> listeInformation = [];
            try
            { listeInformation = [.. (await repository.GetModelAsync(internaute!)).Cast<Groupe>()]!; }
            catch (Exception)
            { throw new ConnexionErrorException(); }
            groupes.Clear();
            int byteReaden = 0;
            foreach (var groupe in listeInformation)
            {
                groupe.image = await useCase.GetImageAsync(groupe, byteReaden, internaute!);
                byteReaden += groupe.imageSize!.Value;
                groupes.Add(groupe);
            }
        }

        [RelayCommand]
        private async Task OpenGroup(Groupe groupe)
        {
            var parameters = new ShellNavigationQueryParameters { { "groupe", groupe }, { "Image", groupe.image! }, { "modele", internaute! } };
            Shell.Current.AppContext.Groupe = groupe;
            await Shell.Current?.GoToAsync("GroupePage", parameters)!;
        }

        [RelayCommand]
        private async Task NavigateToParametre(Groupe groupe)
        {
            var parameters = new ShellNavigationQueryParameters { { "groupe", groupe }, { "modele", internaute! } };
            await Shell.Current?.GoToAsync("ParametrePage", parameters)!;
        }


        [RelayCommand]
        private async Task NavigateTapped(string commande) 
            => await Shell.Current?.GoToAsync(commande, new ShellNavigationQueryParameters{{ "modele", internaute! }})!;

        [RelayCommand]
        private async Task RefreshListGroupe()
        {
            cursor += 1;
        }
    }
}
