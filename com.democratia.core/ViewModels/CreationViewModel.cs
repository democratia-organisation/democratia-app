using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace com.democratia.ViewModels
{
    public partial class CreationViewModel : ObservableObject
    {
        
        [ObservableProperty] private string? nomDeFamille ;

        [ObservableProperty] private string? prenom ;

        [ObservableProperty] private string? adressePostal;

        [ObservableProperty] private string? adresseMail ;

        [ObservableProperty] private string? motDePasse ;

        [RelayCommand]
        private void Inscription()
        {
            // TODO : Implémenter la logique d'inscription
        }




    }
}
