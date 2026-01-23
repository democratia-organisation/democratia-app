using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace com.democratia.ViewModels
{

    public partial class GestionCompteViewModel : ConnectableViewModel, IQueryAttributable, INotifyPropertyChanged
    {
        [ObservableProperty] private string? retourMessage;
        private Internaute? internaute;
        public GestionCompteViewModel(IEnumerable<IClient> clients) 
            : base(clients.OfType<InternauteClient>().FirstOrDefault())
        {
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
        }

        [RelayCommand]
        public void ActionInternaute(string action)
        {
            RetourMessage = action;
        }

        [RelayCommand]
        public async Task ModifierInternaute()
        {
            // TODO : binder les labels avec des proopréié de cette classe
            // TODO :  implémenter la requete pour modification 
            await client?.UpdateModelAsync(internaute)!;
            RetourEcran("Modification");
        }

        [RelayCommand]
        public async Task SupprimerCompte()
        {
            await client?.DeleteModelAsync(internaute)!;
            RetourEcran("Suppression");
        }

        private void RetourEcran(string action)
        {
            if (((InternauteClient)client!).succes) RetourMessage = $"OK {action}";
            else RetourMessage = $"Erreur {action}";
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = (Internaute)query["modele"];
        }
    }
}
