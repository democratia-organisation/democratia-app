using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace com.democratia.ViewModels.internaute.CreerGroupe
{
    public partial class PremierePageViewModel : ConnectableViewModel, INavigeablleViewModel 
    {
        [ObservableProperty] public string? nomGroupe;
        private INavigationService navigationService;
        public PremierePageViewModel(INavigationService navigation, IEnumerable<IClient?>? clients, ILocalizationService? localizationService) : base(clients!.FirstOrDefault(), localizationService)
        {
            navigationService = navigation;
        }


        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            await navigationService.GoToAsync(commande);
        }
    }
}
