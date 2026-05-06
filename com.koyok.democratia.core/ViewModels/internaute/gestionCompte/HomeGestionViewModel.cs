using com.koyok.democratia.Utils;
using com.koyok.democratia.Models;
using com.koyok.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace com.koyok.democratia.ViewModels.internaute.gestionCompte
{

    public partial class HomeGestionViewModel : ConnectableViewModel, IQueryAttributable, INotifyPropertyChanged, INavigeablleViewModel
    {
        [ObservableProperty] private string? retourMessage;

        public Internaute? internaute;
        private Services.AppContext appContext;
        private bool _isNavigating = false;
        private readonly INavigationService navigationService1;
        private readonly WeakReferenceMessenger weakReferenceMessenger;
        public HomeGestionViewModel(IEnumerable<IClient> clients, ILocalizationService? localizationService, INavigationService navigationService, Services.AppContext context) 
            : base(clients.OfType<InternauteClient>().FirstOrDefault(), localizationService)
        {
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
            navigationService1 = navigationService;
            weakReferenceMessenger = WeakReferenceMessenger.Default;
            appContext = context;
        }

        [RelayCommand]
        private void ActionInternaute() =>
            weakReferenceMessenger.Send<EventSuppression, string>(TypeEventSuppression.Send.ToString());

        
        [RelayCommand]
        private async Task SupprimerCompte()
        {
            await client?.DeleteModelAsync(internaute)!;
            if (((InternauteClient)client!).succes)
                weakReferenceMessenger.Send<EventSuppression, string>(TypeEventSuppression.Sucess.ToString());
            else
                RetourMessage = LocalizationService?.GetString("connexionErreur");
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
                await navigationService1.GoToAsync(commande, parameter);
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
