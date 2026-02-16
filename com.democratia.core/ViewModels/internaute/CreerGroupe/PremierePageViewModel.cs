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
        [ObservableProperty] private ObservableCollection<Thematique> thematiquesAffiches;
        [ObservableProperty] private Thematique? thematiqueSelectionnee;
        [ObservableProperty] private ObservableCollection<Thematique> thematiquesRetenues;
        private List<Thematique> thematiquesExistantes;
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
                thematiquesExistantes = new List<Thematique>();
                thematiquesRetenues = new ObservableCollection<Thematique>();
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
            else if (ThematiquesRetenues.Count == 0)
            {
                ErreurMessage = localizationService?.GetString("thematiqueRequise");
                return;
            }
            else
            {
                var themeBudget = ThematiquesRetenues.Sum(t => t.budget ?? 0);
                groupe.Budget = Budget ?? 0;
                if (groupe.Budget < themeBudget)
                {
                    ErreurMessage = localizationService?.GetString("budgetInsuffisant");
                    return;
                }
                groupe.NomGroupe = NomGroupe;
                groupe.NombreDeJourDiscuss = NombreJourDiscussion ?? 0;
                groupe.NombreDeJourVote = NombreJourVote ?? 0;
                thematiquesNouvelles = [.. ThematiquesRetenues.Except(thematiquesExistantes, new ThematiqueEqualityComparer())];
                foreach (Thematique item in thematiquesNouvelles)
                {
                    await client!.CreateModelAsync(item);
                    List<object> thematiques = RecuprerInformationConnexion(await client.GetModelAsync());
                    item.id_thematique = JsonSerializer.Deserialize<Thematique>(thematiques.Last().ToString()!)!.id_thematique;
                }

                await navigationService.GoToAsync(commande, new()
                { 
                    { "groupe",  groupe },
                    { "thematique", ThematiquesRetenues },
                    { "internaute",internaute! }
                });
            }
        }

        [RelayCommand]
        private void PrendreThematique() => AjoutThematique(new Thematique(Thematique));

        [RelayCommand]
        private void PrendreThematiqueSelectionnee()
        {
            if(ThematiqueSelectionnee  is not null ) // quand ThematiquesAffiches est modifié, CollectionView.SelectedItem est mis à null, donc ThematiqueSelectionnee est null
            {
                AjoutThematique(ThematiqueSelectionnee);
                thematiquesExistantes.Remove(ThematiqueSelectionnee);
                ThematiquesAffiches.Clear();
                thematiquesExistantes.ForEach(t => ThematiquesAffiches.Add(t));
            }
        }

        [RelayCommand]
        private void AfficherThematiquesMatch() 
        {
            string texteRecherche = Thematique?.ToLower() ?? string.Empty;
            var thematiquesFiltrees = thematiquesExistantes.Where(t => t.nom_thematique?.ToLower().Contains(texteRecherche) == true).ToList();
            ThematiquesAffiches.Clear();
            thematiquesFiltrees.ForEach(t => ThematiquesAffiches.Add(t));

        }



        private void AjoutThematique(Thematique thematiqueItem)
        {
            if (!ThematiquesRetenues.Contains(thematiqueItem, new ThematiqueEqualityComparer()))
                ThematiquesRetenues.Add(thematiqueItem);

            Thematique = string.Empty;
        }

        private async void RemplirThematique()
        {
            string listeRequete = await client!.GetModelAsync();
            List<object> thematiques = RecuprerInformationConnexion(listeRequete);
            foreach (var item in thematiques)
            {
                var thematique = JsonSerializer.Deserialize<Thematique>(item.ToString()!);
                thematiquesExistantes.Add(thematique!);

            }
            ThematiquesAffiches = [..thematiquesExistantes];
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = query.TryGetValue("internaute", out var internauteObj) ? (Internaute)internauteObj : null;
            if (internaute is null)
                internaute = await RetrouverModele<Internaute>();
        }
        
    }
}
