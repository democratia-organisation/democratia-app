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
    public partial class PremiereCreationViewModel : ConnectableViewModel, INavigeablleViewModel , IQueryAttributable, INotifyPropertyChanged
    {
        [ObservableProperty] private string? thematique;
        [ObservableProperty] private string? erreurMessage;
        [ObservableProperty] private ObservableCollection<Thematique>? thematiquesAffiches;
        [ObservableProperty] private Thematique? thematiqueSelectionnee;
        [ObservableProperty] private ObservableCollection<Thematique>? thematiquesRetenues;
        [ObservableProperty] private Groupe groupe = new();
        [ObservableProperty] private bool afficheCollectionView = false;
        private List<Thematique>? thematiquesExistantes;
        private Internaute? internaute;

        
        private List<Thematique> thematiquesNouvelles { get; set; } = []; 
        private INavigationService navigationService;
        public PremiereCreationViewModel(INavigationService navigation, IEnumerable<IClient?>? clients, ILocalizationService? LocalizationService)
            : base(clients!.OfType<ThematiqueClient>().FirstOrDefault(), LocalizationService)
        {
            navigationService = navigation;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
            thematiquesExistantes = [];
            thematiquesRetenues = [];
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            if (string.IsNullOrWhiteSpace(Groupe?.NomGroupe))
            {
                ErreurMessage = LocalizationService?.GetString("nomGroupeRequis");
                return;
            }
            else if (ThematiquesRetenues!.Count == 0)
            {
                ErreurMessage = LocalizationService?.GetString("thematiqueRequise");
                return;
            }
            else
            {
                try
                {
                    var themeBudget = ThematiquesRetenues.Sum(t =>
                    {
                        if (!t.budget.HasValue) throw new EmptyRequiredFieldException(nameof(t));
                        return t.budget;
                    });
                    if (Groupe.Budget < themeBudget)
                    {
                        ErreurMessage = LocalizationService?.GetString("budgetInsuffisant");
                        return;
                    }
                }
                catch (EmptyRequiredFieldException ex)
                {
                    ErreurMessage = MapExceptionMessage.MappingException(ex, LocalizationService!, ex.Message);
                    return;
                }
                catch (Exception ex) {
                    ErreurMessage = MapExceptionMessage.MappingException(ex, LocalizationService!);
                
                }
                thematiquesNouvelles = [.. ThematiquesRetenues.Except(thematiquesExistantes!, new ThematiqueEqualityComparer())];
                foreach (Thematique item in thematiquesNouvelles)
                {
                    await client!.CreateModelAsync(item);
                    List<object> thematiques = RecuprerInformationConnexion(await client.GetModelAsync());
                    item.id_thematique = JsonSerializer.Deserialize<Thematique>(thematiques.Last().ToString()!)!.id_thematique;
                }

                await navigationService.GoToAsync(commande, new()
                { 
                    { "groupe",  Groupe},
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
                thematiquesExistantes!.Remove(ThematiqueSelectionnee);
                ThematiquesAffiches!.Clear();
                thematiquesExistantes.ForEach(t => ThematiquesAffiches.Add(t));
            }
        }

        [RelayCommand]
        private void AfficherThematiquesMatch() 
        {

            if(!string.IsNullOrWhiteSpace(Thematique) && !AfficheCollectionView) AfficheCollectionView = true;
            else if (string.IsNullOrWhiteSpace(Thematique) && AfficheCollectionView) AfficheCollectionView = false;
            string texteRecherche = Thematique?.ToLower() ?? string.Empty;
            var thematiquesFiltrees = thematiquesExistantes!.Where(t => (bool)t.nom_thematique?.ToLower()?.Contains(texteRecherche)!).ToList();
            ThematiquesAffiches!.Clear();
            thematiquesFiltrees.ForEach(t => ThematiquesAffiches.Add(t));

        }



        private void AjoutThematique(Thematique thematiqueItem)
        {
            if (!ThematiquesRetenues!.Contains(thematiqueItem, new ThematiqueEqualityComparer()))
                ThematiquesRetenues!.Add(thematiqueItem);

            Thematique = string.Empty;
        }

        public async Task RemplirThematique()
        {
            string listeRequete = await client!.GetModelAsync();
            List<object> thematiques = RecuprerInformationConnexion(listeRequete);
            foreach (var item in thematiques)
            {
                var thematique = JsonSerializer.Deserialize<Thematique>(item.ToString()!);
                thematiquesExistantes!.Add(thematique!);

            }
            ThematiquesAffiches = [..thematiquesExistantes!];
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = query.TryGetValue("internaute", out var internauteObj) ? (Internaute)internauteObj : null;
            internaute ??= await RetrouverModele<Internaute>();
        }
        
    }
}
