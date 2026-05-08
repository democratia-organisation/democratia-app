using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.UseCase;
using com.koyok.democratia.Domain.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.ComponentModel;
using AppContext = com.koyok.democratia.Domain.Utils.AppContext;

namespace com.koyok.democratia.UI.internaute
{
    public partial class LoginViewModel(AuthenticateUseCase useCase, AppContext context) 
        : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty] public partial string? adresseMail { get; set; }
        [ObservableProperty] public partial string? motDePasse { get; set; }
        [ObservableProperty] public partial string? errorMessage { get; set; }
        public Internaute? modele { get; private set; }
        private AppContext contexte = context;
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
                    modele = await ConnecterInternaute();
                }
                catch (Exception ex)
                {
                    errorMessage = contexte.Mapper!.MappingException(ex);
                }

                contexte!.Internaute = modele;
                var parameters = new ShellNavigationQueryParameters { { "modele", modele! } };
                await Shell.Current!.GoToAsync(commande, parameters);
            }

        }

        internal async Task<Internaute?> ConnecterInternaute()
        {

            if (string.IsNullOrWhiteSpace(adresseMail)) throw new EmptyEmailFieldException();
            else if (string.IsNullOrWhiteSpace(motDePasse)) throw new EmptyPassWordFieldException();
            try
            {
                return await useCase.Authenticate(adresseMail!, motDePasse!);
            }
            catch (Exception)
            { throw; }
        }
    }
}