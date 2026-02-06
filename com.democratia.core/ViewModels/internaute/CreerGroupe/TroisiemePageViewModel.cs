using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace com.democratia.ViewModels.internaute.CreerGroupe
{
    public class TroisiemePageViewModel : ConnectableViewModel, INavigeablleViewModel , IQueryAttributable, INotifyPropertyChanged
    {

        private INavigationService service;
        private Groupe? groupe;
        private List<string>? thematiques { get; set; }
        public TroisiemePageViewModel(IEnumerable<IClient?>? clients, ILocalizationService? localizationService, INavigationService service) 
            : base(clients?.FirstOrDefault(), localizationService)
        {
            this.service = service;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = (Groupe)query["groupe"];
            thematiques = (List<string>)query["thematique"];
        }

        public Task NavigateTapped(string commande)
        {
            throw new NotImplementedException();
        }
    }
}
