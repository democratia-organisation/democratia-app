using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace com.democratia.ViewModels
{

    public partial class GestionCompteViewModel : ConnectableViewModel, IQueryAttributable, INotifyPropertyChanged, INavigeablleViewModel
    {
        [ObservableProperty] private string? retourMessage;
        private Internaute? internaute;
        private readonly INavigationService navigationService;
        public GestionCompteViewModel(INavigationService navigationService, IEnumerable<IClient> clients) 
            : base(clients.OfType<InternauteClient>().FirstOrDefault())
        {
            this.navigationService = navigationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        [RelayCommand]
        public void ActionInternaute(string action)
        {
            RetourMessage = action;

        }


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = (Internaute)query["modele"];
        }

        [RelayCommand]
        public Task NavigateTapped(string commande)
        {
            throw new NotImplementedException();
        }
    }
}
