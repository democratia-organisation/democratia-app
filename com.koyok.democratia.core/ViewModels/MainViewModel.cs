using com.koyok.democratia.Domain.Enumerations;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.UseCase;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using System.ComponentModel;
using AppContext = com.koyok.democratia.Domain.Utils.AppContext;

namespace com.koyok.democratia.UI
{
    public partial class MainViewModel(AuthenticateUseCase useCase,
        AppContext context) : ObservableObject, INotifyPropertyChanged
    {
        private readonly AuthenticateUseCase useCase = useCase;
        private readonly AppContext context = context;
        [ObservableProperty]
        public partial bool loading { get; set; } = true;

        [ObservableProperty]
        public partial bool isConnected { get; set; }
        
        [RelayCommand]
        private async Task Connect()
        {
            string? identifiant = await SecureStorage.Default.GetAsync(SecureStorageKeys.IdInternaute.ToString());
            string? motDePasse = await SecureStorage.Default.GetAsync(SecureStorageKeys.MotDePasseInternaute.ToString());
            if (identifiant is null || motDePasse is null)
            {
                isConnected = false;
                return;
            }
            else
            {
                Internaute internaute = await useCase.Authenticate(identifiant, motDePasse);
                context.Internaute = internaute;
                isConnected = true;

            }
            
        }

    }
}
