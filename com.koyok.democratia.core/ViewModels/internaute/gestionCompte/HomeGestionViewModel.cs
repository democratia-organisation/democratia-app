using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using com.koyok.democratia.Data.Repository;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Service;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Extension;

namespace com.koyok.democratia.UI.internaute.gestionCompte
{

    public partial class HomeGestionViewModel(ILocalizationService localizationService, 
        IInternauteRepository internauteRepository) : ObservableObject, IQueryAttributable, INotifyPropertyChanged
    {
        [ObservableProperty] public partial string? retourMessage { get; set; }
        private readonly IInternauteRepository? internauteRepository = internauteRepository;
        private Internaute? internaute;
        private readonly ILocalizationService localizationService = localizationService;
        private bool _isNavigating = false;
        private readonly WeakReferenceMessenger weakReferenceMessenger = WeakReferenceMessenger.Default;

        [RelayCommand]
        private void ActionInternaute() =>
            weakReferenceMessenger.Send<EventSuppression, string>(TypeEventSuppression.Send.ToString());

        
        [RelayCommand]
        private async Task SupprimerCompte()
        {
            await internauteRepository?.DeleteModelAsync(internaute)!;
            if (((InternauteRepository)internauteRepository!).succes)
                weakReferenceMessenger.Send<EventSuppression, string>(TypeEventSuppression.Sucess.ToString());
            else
                retourMessage = localizationService?.GetString("connexionErreur");
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
        public enum TypeEventSuppression
        {
            Sucess,
            Send
        }
        public record EventSuppression() { }
    }
}
