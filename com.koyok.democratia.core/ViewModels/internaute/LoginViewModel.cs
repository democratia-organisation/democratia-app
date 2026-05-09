using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.UseCase;
using com.koyok.democratia.Domain.Extension;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;

namespace com.koyok.democratia.UI.internaute
{
    public partial class LoginViewModel(AuthenticateUseCase useCase) 
        : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty] public partial string? adresseMail { get; set; }
        [ObservableProperty] public partial string? motDePasse { get; set; }
        [ObservableProperty] public partial string? errorMessage { get; set; }
        public Internaute? modele { get; private set; }
        private readonly AuthenticateUseCase useCase = useCase;

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            if (commande == "CreationPage")
                await Shell.Current!.GoToAsync(commande);
            else
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(adresseMail)) throw new EmptyEmailFieldException();
                    else if (string.IsNullOrWhiteSpace(motDePasse)) throw new EmptyPassWordFieldException();
                    modele = await useCase.Authenticate(adresseMail!, motDePasse!);
                    Shell.Current.AppContext.Internaute = modele;
                    var parameters = new ShellNavigationQueryParameters { { "modele", modele! } };
                    await Shell.Current!.GoToAsync(commande, parameters);
                }
                catch (Exception ex)
                {
                    errorMessage = Shell.Current.AppContext.Mapper!.MappingException(ex);
                    SecureStorage.Default.RemoveAll();
                }
            }

        }
    }
}