using com.koyok.democratia.core.Data.Repository;
using com.koyok.democratia.core.Domain.Repository;
using com.koyok.democratia.core.Domain.Utils;
using com.koyok.democratia.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace com.koyok.democratia.UI.groupe
{
    public partial class PropositionViewModel(IEnumerable<IRepository?> clients, ILocalizationService? localizationService) : ConnectableViewModel(clients.OfType<PropositionRepository>().FirstOrDefault(), localizationService), IQueryAttributable
    {
        private readonly Groupe? groupe;
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}
