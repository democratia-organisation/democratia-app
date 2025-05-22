using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace com.democratia.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {

        [ObservableProperty]
        private string? adresseMail ;

        [ObservableProperty]
        private string? motDePasse ;

        [RelayCommand]
        public static async Task NavigateTapped(string commande)
        {
            if (commande == "Home") VerifierUtilisateur();
            try
            {
                if (Shell.Current.CurrentItem?.Route != commande) await Shell.Current.GoToAsync(commande);   
            }
            catch (Exception ex)
            {
                Application.Current?.Windows[0]?.Page?.DisplayAlert("Error",$"Erreur lors de la navigation : {ex.Message}","OK");
            }
        }

        private static void VerifierUtilisateur()
        {
            // TODO : vérifier si l'utilisateur existe et si le mot de passe est correct en récupérant 
            // sa version dans la base de données
            // TODO : trouver la classe pour décrypter le mot de passe
        }
    }
}
