using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.ComponentModel;
using System.Text.Json;

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
        private Internaute? internaute;
        private Groupe groupe { get; set; } = new();
        private List<Thematique> thematiquesExistante { get; set; } = new(); // permet l'auto-complétion des thématiques déjà existantes
        private List<Thematique> thematiquesRetenues {  get; set; } = new();
        private List<Thematique> thematiquesNouvelles { get; set; } = new(); 
        private INavigationService navigationService;
        private ILocalizationService? localizationService;
        public PremierePageViewModel(INavigationService navigation, IEnumerable<IClient?>? clients, ILocalizationService? localizationService)
            : base(clients!.OfType<ThematiqueClient>().FirstOrDefault(), localizationService)
        {
            navigationService = navigation;
            this.localizationService = localizationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
            RemplirThematique();
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
                thematiquesNouvelles = [.. thematiquesRetenues.Except(thematiquesExistante,new ThematiqueEqualityComparer())];
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
        public void PrendreThematique()
        {
            Predicate<Thematique> predicate = t => t.nom_thematique == Thematique;
            if (thematiquesExistante.Contains(new Thematique(Thematique),new ThematiqueEqualityComparer()))
                thematiquesRetenues.Add(thematiquesExistante[thematiquesExistante.FindIndex(predicate)]);

            else thematiquesRetenues.Add(new Thematique(Thematique));
            Thematique = string.Empty;
        }

        private async void RemplirThematique()
        {
            string listeRequete = await client!.GetModelAsync();
            List<object> thematiques = RecuprerInformationConnexion(listeRequete);
            foreach (var item in thematiques)
            {
                var thematique = JsonSerializer.Deserialize<Thematique>(item.ToString()!);
                thematiquesExistante.Add(thematique!);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = (Internaute)query["modele"];
        }
    }
}
