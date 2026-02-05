using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace com.democratia.ViewModels.internaute.CreerGroupe
{
    public partial class PremierePageViewModel : ConnectableViewModel, INavigeablleViewModel 
    {
        [ObservableProperty] public string? nomGroupe;
        [ObservableProperty] public string? thematique;
        private Groupe groupe { get; set; } = new();
        private List<string> thematiquesExistante { get; set; } = new();
        private List<string> nouvellesThematiques {  get; set; } = new();
        private INavigationService navigationService;
        public PremierePageViewModel(INavigationService navigation, IEnumerable<IClient?>? clients, ILocalizationService? localizationService) : base(clients!.FirstOrDefault(), localizationService)
        {
            navigationService = navigation;
            // TODO : remplir la liste avec toutes les thématiques enregistrés dans la bd
        }


        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            groupe.NomGroupe = NomGroupe;
            await navigationService.GoToAsync(commande);
        }

        [RelayCommand]
        public void PrendreThematique()
        {

            if (!thematiquesExistante!.Contains(Thematique!)) nouvellesThematiques.Add(Thematique!);
        }
    }
}
