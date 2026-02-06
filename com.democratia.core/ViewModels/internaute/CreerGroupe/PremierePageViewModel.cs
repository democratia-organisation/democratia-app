using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;

namespace com.democratia.ViewModels.internaute.CreerGroupe
{
    public partial class PremierePageViewModel : ConnectableViewModel, INavigeablleViewModel 
    {
        [ObservableProperty] public string? nomGroupe;
        [ObservableProperty] public string? thematique;
        private Groupe groupe { get; set; } = new();
        private List<string> thematiquesExistante { get; set; } = new(); // permet l'auto-complétion des thématiques déjà existantes
        private List<string> thematiquesRetenues {  get; set; } = new();
        // TODO : ajouter un attribut pour un retour message d'erreur si le nom du groupe est vide ou si aucune thématique n'est donnée
        private INavigationService navigationService;
        public PremierePageViewModel(INavigationService navigation, IEnumerable<IClient?>? clients, ILocalizationService? localizationService)
            : base(clients!.OfType<ThematiqueClient>().FirstOrDefault(), localizationService)
        {
            navigationService = navigation;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
            RemplirThematique();
        }


        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            groupe.NomGroupe = NomGroupe; // TODO : vérifier si le champ n'est pas vide
            await navigationService.GoToAsync(commande, new(){ {"groupe", groupe }, { "thematique", thematiquesRetenues } });
        }

        [RelayCommand]
        public void PrendreThematique()
        {
            thematiquesRetenues.Add(Thematique!);
            Thematique= string.Empty;

        }
        

        private async void RemplirThematique()
        {
            string listeRequete = await client!.GetModelAsync();
            List<object> thematiques = RecuprerInformationConnexion(listeRequete);
            foreach (var item in thematiques)
            {
                var thematique = JsonSerializer.Deserialize<Thematique>(item.ToString()!);
                thematiquesExistante.Add(thematique!.nom_thematique!);
            }
        }
    }
}
