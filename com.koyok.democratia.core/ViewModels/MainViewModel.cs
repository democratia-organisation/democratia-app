using com.koyok.democratia.Domain.Enumerations;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.UseCase;
using com.koyok.democratia.Domain.Extension;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.ComponentModel;

namespace com.koyok.democratia.UI
{
    public partial class MainViewModel(AuthenticateUseCase useCase) : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        public partial bool isConnected { get; set; }

        [RelayCommand]
        private async Task Connect()
        {
            string? identifiant = await SecureStorage.Default.GetAsync(SecureStorageKeys.IdInternaute.ToString());
            string? motDePasse = await SecureStorage.Default.GetAsync(SecureStorageKeys.MotDePasseInternaute.ToString());
            if (identifiant is null || motDePasse is null)
                isConnected = false;
            else
            {
                try
                {
                    Internaute? internaute = await useCase.Authenticate(identifiant, motDePasse);
                    isConnected = internaute != null;
                    await SecureStorage.Default.SetAsync(SecureStorageKeys.isConnected.ToString(), $"{isConnected}");
                    if(isConnected) Shell.Current.AppContext.Internaute = internaute;
                }
                catch (Exception) {
                    SecureStorage.Default.RemoveAll();
                }   
            }
        }

    }
}
