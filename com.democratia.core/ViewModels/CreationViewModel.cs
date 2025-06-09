using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace com.democratia.ViewModels
{
    public partial class CreationViewModel : ObservableObject, INavigeablleViewModel
    {
        
        [ObservableProperty] private string? nomDeFamille ;

        [ObservableProperty] private string? prenom ;

        [ObservableProperty] private string? adressePostal;

        [ObservableProperty] private string? adresseMail ;

        [ObservableProperty] private string? motDePasse ;
        
        private INavigationService? navigationService;

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            throw new NotImplementedException();
        }

        internal async Task CreerInternaute()
        {
            throw new NotImplementedException("Not implemented");
        }
        
        private bool VerifierChampComplet()
        {
            throw new   NotImplementedException("Not implemented");
        }
        private bool VerifierFormatageMail()
        {
            throw new NotImplementedException("Not implemented");
        }
        private void VerifierMailDoublon()
        {
            throw new NotImplementedException("Not implemented");
        }
        private void VerifieFormattageMotDePasse()
        {
            throw new NotImplementedException("Not implemented");
        }
        private object RecupererValeurRetour()
        {
            throw new   NotImplementedException("Not implemented");
        }
        private List<Dictionary<string,object>> ConversionToObject()
        {
            throw new   NotImplementedException("Not implemented");
        }

        
    }
}
