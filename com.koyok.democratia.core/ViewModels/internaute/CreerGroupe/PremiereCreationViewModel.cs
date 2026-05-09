using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.ComponentModel;
using System.Collections.ObjectModel;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Service;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Extension.Comparer;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Extension;


namespace com.koyok.democratia.UI.internaute.CreerGroupe
{
    public partial class PremiereCreationViewModel(ILocalizationService? LocalizationService,
        IThematiqueRepository? repository) : ObservableObject, IQueryAttributable, INotifyPropertyChanged
    {
        [ObservableProperty] public partial string? thematique { get; set; }
        [ObservableProperty] public partial string? erreurMessage { get; set; }
        [ObservableProperty] public partial ObservableCollection<Thematique>? thematiquesAffiches { get; set; }
        [ObservableProperty] public partial Thematique? thematiqueSelectionnee { get; set; }
        [ObservableProperty] public partial ObservableCollection<Thematique>? thematiquesRetenues { get; set; } = [];
        [ObservableProperty] public partial Groupe groupe { get; set; } = new();
        [ObservableProperty] public partial bool afficheCollectionView { get; set; } = false;
        private readonly IThematiqueRepository? repository = repository;
        private List<Thematique>? thematiquesExistantes = [];
        private Internaute? internaute; 


        private List<Thematique> thematiquesNouvelles { get; set; } = [];

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            if (string.IsNullOrWhiteSpace(groupe?.nomGroupe))
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
                    if (groupe.budget < themeBudget)
                    {
                        erreurMessage = LocalizationService?.GetString("budgetInsuffisant");
                        return;
                    }
                }
                catch (Exception ex) 
                {
                    erreurMessage = Shell.Current!.AppContext.Mapper!.MappingException(ex, ex.Message ?? "");
                }
                thematiquesNouvelles = [.. thematiquesRetenues.Except(thematiquesExistantes!, new ThematiqueEqualityComparer())];
                foreach (Thematique item in thematiquesNouvelles)
                {
                    await repository!.CreateModelAsync(item);
                    List<Thematique> thematiques = repository.RecuprerInformationConnexion<Thematique>(await repository.GetModelAsync());
                    item.idThematique = thematiques.Last().idThematique;
                }

                await Shell.Current.GoToAsync(commande, new ShellNavigationQueryParameters
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
            // quand ThematiquesAffiches est modifié, CollectionView.SelectedItem est mis à null, donc ThematiqueSelectionnee est null
            if (thematiqueSelectionnee  is not null ) 
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
            var thematiquesFiltrees = thematiquesExistantes!.Where(t => (bool)t.nomThematique?.ToLower()?.Contains(texteRecherche)!).ToList();
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
            string listeRequete = await repository!.GetModelAsync();
            List<Thematique> thematiques = repository.RecuprerInformationConnexion<Thematique>(listeRequete);
            thematiques.ForEach(t => thematiquesExistantes!.Add(t));
            thematiquesAffiches = [..thematiquesExistantes!];
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
            => internaute = (Internaute)query["modele"] ?? Shell.Current?.AppContext.Internaute;
        
        
    }
}
