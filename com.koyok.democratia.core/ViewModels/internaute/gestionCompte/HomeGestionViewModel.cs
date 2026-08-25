using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Lib;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Extension;

namespace com.koyok.democratia.UI.internaute.gestionCompte
{

    public partial class HomeGestionViewModel(ILocalizationService localizationService, 
        IInternauteRepository internauteRepository, INotificationRegistrationRepository? notificationRegistrationService) : ObservableObject, IQueryAttributable, INotifyPropertyChanged
    {
        [ObservableProperty] public partial string? retourMessage { get; set; }
        private readonly IInternauteRepository? internauteRepository = internauteRepository;
        private Internaute? internaute;
        private readonly ILocalizationService localizationService = localizationService;
        private readonly INotificationRegistrationRepository? notificationRegistrationService = notificationRegistrationService;
        private bool _isNavigating = false;
        private readonly WeakReferenceMessenger weakReferenceMessenger = WeakReferenceMessenger.Default;

        [RelayCommand]
        private void ActionInternaute() =>
            weakReferenceMessenger.Send<EventSuppression, string>(SuceffulyEnum.Send.ToString());
        [RelayCommand]
        private void Deconnexion()
        {
            weakReferenceMessenger.Send<EventDeconnexion, string>(SuceffulyEnum.Send.ToString());
        }

        
        [RelayCommand]
        private async Task SupprimerCompte()
        {
            try
            {
                await internauteRepository?.DeleteModelAsync(internaute)!;
                weakReferenceMessenger.Send<EventSuppression, string>(SuceffulyEnum.Sucess.ToString());
            }
            catch (Exception ex)
            {
                retourMessage = Shell.Current.AppContext.Mapper!.MappingException(ex);
            }

        }

        [RelayCommand]
        private async Task Deconnecter()
        {
            try
            {
                await notificationRegistrationService?.DeregisterDeviceAsync()!;
            }
            catch (Exception ex)
            {
                retourMessage = Shell.Current.AppContext.Mapper!.MappingException(ex);
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
         => internaute = (Internaute)query["modele"] ?? Shell.Current?.AppContext.Internaute;
        

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task NavigateTapped(string commande)
        {
            if (_isNavigating) return;
            _isNavigating = true;
            var parameter = new ShellNavigationQueryParameters { { "modele", internaute! } };
            try
            {
                await Shell.Current?.GoToAsync(commande, parameter)!;
            }
            finally
            {
                _isNavigating = false;
            }
        }
        public enum SuceffulyEnum
        {
            Sucess,
            Send
        }
        public record EventSuppression() { }
        public record EventDeconnexion() { }
    }
}
