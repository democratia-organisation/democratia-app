using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace com.democratia.ViewModels.groupe
{
    public partial class PropositionViewModel(IEnumerable<IClient?> clients, ILocalizationService? localizationService) : ConnectableViewModel(clients.OfType<PropositionClient>().FirstOrDefault(), localizationService), IQueryAttributable
    {
        private readonly Groupe? groupe;
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}
