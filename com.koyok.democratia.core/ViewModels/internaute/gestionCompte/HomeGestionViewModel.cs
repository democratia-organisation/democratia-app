using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using com.koyok.democratia.Data.Repository;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Service;

namespace com.koyok.democratia.UI.internaute.gestionCompte
{

    public partial class HomeGestionViewModel(Domain.Utils.AppContext context
        ,ILocalizationService localizationService) : ObservableObject, IQueryAttributable, INotifyPropertyChanged
    {
        [ObservableProperty] public partial string? retourMessage { get; set; }

        public Internaute? internaute;
        private readonly ILocalizationService localizationService = localizationService;
        private readonly Domain.Utils.AppContext appContext = context;
        private bool _isNavigating = false;
        private readonly WeakReferenceMessenger weakReferenceMessenger = WeakReferenceMessenger.Default;

        [RelayCommand]
        private void ActionInternaute() =>
            weakReferenceMessenger.Send<EventSuppression, string>(TypeEventSuppression.Send.ToString());

        
        [RelayCommand]
        private async Task SupprimerCompte()
        {
            await client?.DeleteModelAsync(internaute)!;
            if (((InternauteRepository)client!).succes)
                weakReferenceMessenger.Send<EventSuppression, string>(TypeEventSuppression.Sucess.ToString());
            else
                retourMessage = localizationService?.GetString("connexionErreur");
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("modele", out var valeur)) internaute = (Internaute)valeur;
            else internaute = appContext.Internaute ;
        }

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
