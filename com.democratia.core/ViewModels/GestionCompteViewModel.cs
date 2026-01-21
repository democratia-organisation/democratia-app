using com.democratia.Models;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace com.democratia.ViewModels
{

    public partial class GestionCompteViewModel : ConnectableViewModel, IQueryAttributable, INotifyPropertyChanged
    {
        [ObservableProperty] private string? retourMessage;
        private Internaute? internaute;
        public GestionCompteViewModel(IClient? client) : base(client)
        {
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            internaute = (Internaute)query["modele"];
        }

    }
}
