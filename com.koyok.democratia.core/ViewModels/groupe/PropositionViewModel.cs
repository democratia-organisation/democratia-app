using com.koyok.democratia.Models;
using com.koyok.democratia.Services;
using com.koyok.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace com.koyok.democratia.ViewModels.groupe
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
