using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.ComponentModel;
using System.Text.Json;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace com.democratia.ViewModels.internaute.CreerGroupe
{
    public partial class PremierePageViewModel : ConnectableViewModel, INavigeablleViewModel , IQueryAttributable, INotifyPropertyChanged
    {
        [ObservableProperty] private string? nomGroupe;
        [ObservableProperty] private int? nombreJourVote;
        [ObservableProperty] private int? nombreJourDiscussion;
        [ObservableProperty] private string? thematique;
        [ObservableProperty] private float? budget;
        [ObservableProperty] private string? erreurMessage;
        [ObservableProperty] private ObservableCollection<Thematique> thematiquesExistantes;
        [ObservableProperty] private Thematique? thematiqueSelectionnee;
        private List<Thematique> thematiquesRetenues;
        private Internaute? internaute;

        private Groupe groupe { get; set; } = new();
        private List<Thematique> thematiquesNouvelles { get; set; } = new(); 
        private INavigationService navigationService;
        private ILocalizationService? localizationService;
        public PremierePageViewModel(INavigationService navigation, IEnumerable<IClient?>? clients, ILocalizationService? localizationService)
            : base(clients!.OfType<ThematiqueClient>().FirstOrDefault(), localizationService)
        {
            navigationService = navigation;
            this.localizationService = localizationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
            if(thematiquesExistantes is null && thematiquesRetenues is null) // verification car le viewModel est instancié à chaque fois que la page est affiché
            {
                thematiquesExistantes = new ObservableCollection<Thematique>();
                thematiquesRetenues = new List<Thematique>();
                RemplirThematique();
            }
                
        }


        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            if (string.IsNullOrEmpty(NomGroupe))
            {
                ErreurMessage = localizationService?.GetString("nomGroupeRequis");
                return;
            }
            else if (thematiquesRetenues.Count == 0)
            {
                ErreurMessage = localizationService?.GetString("thematiqueRequise");
                return;
            }
            else
            {
                groupe.NomGroupe = NomGroupe;
                groupe.NombreDeJourDiscuss = NombreJourDiscussion ?? 0;
                groupe.NombreDeJourVote = NombreJourVote ?? 0;
                groupe.Budget = Budget ?? 0;
                thematiquesNouvelles = thematiquesRetenues.Except(ThematiquesExistantes, new ThematiqueEqualityComparer()).ToList();
                foreach (Thematique item in thematiquesNouvelles)
                {
                    await client!.CreateModelAsync(item);
                    List<object> thematiques = RecuprerInformationConnexion(await client.GetModelAsync());
                    item.id_thematique = JsonSerializer.Deserialize<Thematique>(thematiques.Last().ToString()!)!.id_thematique;
                }
                await navigationService.GoToAsync(commande, new() { { "groupe", groupe }, { "thematique", thematiquesRetenues }, {"internaute", internaute! } });
            }
        }

        [RelayCommand]
        public void PrendreThematique() => AjoutThematique(new Thematique(Thematique));

        [RelayCommand]
        public void PrendreThematiqueSelectionnee()
        {
            if(ThematiqueSelectionnee  is not null ) // quand ThematiquesExistantes est modifié, CollectionView.SelectedItem est mis à null, donc ThematiqueSelectionnee est null
            {
                AjoutThematique(ThematiqueSelectionnee);
                ThematiquesExistantes.Remove(ThematiqueSelectionnee);
            }
        }



        private void AjoutThematique(Thematique thematiqueItem)
        {
            if (!thematiquesRetenues.Contains(thematiqueItem, new ThematiqueEqualityComparer()))
                thematiquesRetenues.Add(thematiqueItem);

            Thematique = string.Empty;
        }

        private async void RemplirThematique()
        {
            string listeRequete = await client!.GetModelAsync();
            List<object> thematiques = RecuprerInformationConnexion(listeRequete);
            foreach (var item in thematiques)
            {
                var thematique = JsonSerializer.Deserialize<Thematique>(item.ToString()!);
                ThematiquesExistantes.Add(thematique!);

            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query) => internaute = (Internaute)query["modele"];
        
    }
}
