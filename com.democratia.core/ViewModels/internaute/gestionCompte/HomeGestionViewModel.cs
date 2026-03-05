using com.democratia.Utils;
using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace com.democratia.ViewModels.internaute.gestionCompte
{

    public partial class HomeGestionViewModel : ConnectableViewModel, IQueryAttributable, INotifyPropertyChanged, INavigeablleViewModel
    {
        [ObservableProperty] private string? retourMessage;

        public Internaute? internaute;
        private bool _isNavigating = false;
        private readonly INavigationService navigationService1;
        private readonly WeakReferenceMessenger weakReferenceMessenger;
        public HomeGestionViewModel(IEnumerable<IClient> clients, ILocalizationService? localizationService, INavigationService navigationService) 
            : base(clients.OfType<InternauteClient>().FirstOrDefault(), localizationService)
        {
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
            navigationService1 = navigationService;
            weakReferenceMessenger = WeakReferenceMessenger.Default;
        }

        [RelayCommand]
        private void ActionInternaute() =>
            weakReferenceMessenger.Send<EventSuppressionSend>();

        
        [RelayCommand]
        private async Task SupprimerCompte()
        {
            await client?.DeleteModelAsync(internaute)!;
            if (((InternauteClient)client!).succes)
                weakReferenceMessenger.Send<EventSuppressionSuccess>();
            else
                RetourMessage = LocalizationService?.GetString("connexionErreur");
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("modele", out var valeur)) internaute = (Internaute)valeur;
            else internaute = await RetrouverModele<Internaute>();
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task NavigateTapped(string commande)
        {
            if (_isNavigating) return;
            _isNavigating = true;
            var parameter = commande == "ModifierGestionPage" ?
                new ShellNavigationQueryParameters { { "internaute", internaute! } } : null;
            try
            {
                await navigationService1.GoToAsync(commande, parameter);
            }
            finally
            {
                _isNavigating = false;
            }
        }
        public record EventSuppressionSend() { }
        public record EventSuppressionSuccess() { }
    }
}
