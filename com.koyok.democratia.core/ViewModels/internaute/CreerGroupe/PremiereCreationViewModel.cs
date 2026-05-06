using com.koyok.democratia.Models;
using com.koyok.democratia.Services;
using com.koyok.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.ComponentModel;
using System.Text.Json;
using System.Collections.ObjectModel;


namespace com.koyok.democratia.ViewModels.internaute.CreerGroupe
{
    public partial class PremiereCreationViewModel : ConnectableViewModel, INavigeablleViewModel , IQueryAttributable, INotifyPropertyChanged
    {
        [ObservableProperty] public partial string? thematique { get; set; }
        [ObservableProperty] public partial string? erreurMessage { get; set; }
        [ObservableProperty] public partial ObservableCollection<Thematique>? thematiquesAffiches { get; set; }
        [ObservableProperty] public partial Thematique? thematiqueSelectionnee { get; set; }
        [ObservableProperty] public partial ObservableCollection<Thematique>? thematiquesRetenues { get; set; }
        [ObservableProperty] public partial Groupe groupe { get; set; } = new();
        [ObservableProperty] public partial bool afficheCollectionView { get; set; } = false;
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
            if (string.IsNullOrWhiteSpace(groupe?.NomGroupe))
            {
                erreurMessage = LocalizationService?.GetString("nomGroupeRequis");
                return;
            }
            else if (thematiquesRetenues!.Count == 0)
            {
                erreurMessage = LocalizationService?.GetString("thematiqueRequise");
                return;
            }
            else
            {
                try
                {
                    var themeBudget = thematiquesRetenues.Sum(t =>
                    {
                        if (!t.budget.HasValue) throw new EmptyRequiredFieldException(nameof(t));
                        return t.budget;
                    });
                    if (groupe.Budget < themeBudget)
                    {
                        erreurMessage = LocalizationService?.GetString("budgetInsuffisant");
                        return;
                    }
                }
                catch (EmptyRequiredFieldException ex)
                {
                    erreurMessage = MapExceptionMessage.MappingException(ex, LocalizationService!, ex.Message);
                    return;
                }
                catch (Exception ex) {
                    erreurMessage = MapExceptionMessage.MappingException(ex, LocalizationService!);
                
                }
                thematiquesNouvelles = [.. thematiquesRetenues.Except(thematiquesExistantes!, new ThematiqueEqualityComparer())];
                foreach (Thematique item in thematiquesNouvelles)
                {
                    await client!.CreateModelAsync(item);
                    List<Thematique> thematiques = RecuprerInformationConnexion<Thematique>(await client.GetModelAsync());
                    item.id_thematique = thematiques.Last().id_thematique;
                }

                await navigationService.GoToAsync(commande, new()
                { 
                    { "groupe",  groupe},
                    { "thematique", thematiquesRetenues },
                    { "internaute",internaute! }
                });
            }
        }

        [RelayCommand]
        private void PrendreThematique() => AjoutThematique(new Thematique(thematique));

        [RelayCommand]
        private void PrendreThematiqueSelectionnee()
        {
            if(thematiqueSelectionnee  is not null ) // quand ThematiquesAffiches est modifié, CollectionView.SelectedItem est mis à null, donc ThematiqueSelectionnee est null
            {
                AjoutThematique(thematiqueSelectionnee);
                thematiquesExistantes!.Remove(thematiqueSelectionnee);
                thematiquesAffiches!.Clear();
                thematiquesExistantes.ForEach(t => thematiquesAffiches.Add(t));
            }
        }

        [RelayCommand]
        private void AfficherThematiquesMatch() 
        {

            if(!string.IsNullOrWhiteSpace(thematique) && !afficheCollectionView) afficheCollectionView = true;
            else if (string.IsNullOrWhiteSpace(thematique) && afficheCollectionView) afficheCollectionView = false;
            string texteRecherche = thematique?.ToLower() ?? string.Empty;
            var thematiquesFiltrees = thematiquesExistantes!.Where(t => (bool)t.nom_thematique?.ToLower()?.Contains(texteRecherche)!).ToList();
            thematiquesAffiches!.Clear();
            thematiquesFiltrees.ForEach(t => thematiquesAffiches.Add(t));

        }



        private void AjoutThematique(Thematique thematiqueItem)
        {
            if (!thematiquesRetenues!.Contains(thematiqueItem, new ThematiqueEqualityComparer()))
                thematiquesRetenues!.Add(thematiqueItem);

            thematique = string.Empty;
        }

        public async Task RemplirThematique()
        {
            string listeRequete = await client!.GetModelAsync();
            List<Thematique> thematiques = RecuprerInformationConnexion<Thematique>(listeRequete);
            thematiques.ForEach(t => thematiquesExistantes!.Add(t));
            thematiquesAffiches = [..thematiquesExistantes!];
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = query.TryGetValue("modele", out var internauteObj) ? (Internaute)internauteObj : null;
            internaute ??= await RetrouverModele<Internaute>();
        }
        
    }
}
