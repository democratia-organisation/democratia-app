using com.koyok.democratia.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace com.koyok.democratia.UI.groupe
{
    public partial class PropositionViewModel() : ObservableObject, IQueryAttributable
    {
        private readonly Groupe? groupe;
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}
