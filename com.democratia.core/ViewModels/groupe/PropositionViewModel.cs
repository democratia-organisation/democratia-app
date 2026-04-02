using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace com.democratia.ViewModels.groupe
{
    public partial class PropositionViewModel : ConnectableViewModel, IQueryAttributable
    {
        private readonly Groupe? groupe;
        public PropositionViewModel(IEnumerable<IClient?> clients, ILocalizationService? localizationService) 
            : base(clients.OfType<PropositionClient>().FirstOrDefault(), localizationService)
        {
            
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}
