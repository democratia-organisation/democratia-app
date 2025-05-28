
using com.democratia.core.Services;
using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace com.democratia.ViewModels
{
    public partial class MainPageViewModel : ObservableObject, IViewModel
    {
        
        public IClient? client { get; }

        [ObservableProperty]
        private string? adresseMail;

        [ObservableProperty]
        private string? motDePasse;

        [ObservableProperty]
        private string? errorMessage;

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
             
            try
            {
                if (commande == "Home") ConnecterInternaute();
                if (Shell.Current.CurrentItem?.Route != commande) await Shell.Current.GoToAsync(commande);
            }
            catch (Exception)
            {
                ErrorMessage = $"Erreur lors de la navigation vers {commande}";
            }
        }

        public Internaute ConnecterInternaute()
        {
            
            return new Internaute(null, null, null, null, AdresseMail, null);
            // TODO : vérifier si l'utilisateur existe et si le mot de passe est correct en récupérant 
            // sa version dans la base de données
            // TODO : trouver la classe pour décrypter le mot de passe
        }
    }
}
