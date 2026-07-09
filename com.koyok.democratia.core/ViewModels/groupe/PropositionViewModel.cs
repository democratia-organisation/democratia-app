using com.koyok.democratia.Domain.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using System.ComponentModel;

namespace com.koyok.democratia.UI.groupe
{
    public partial class PropositionViewModel() : ObservableObject, IQueryAttributable, INotifyPropertyChanged
    {
        private Proposition? proposition;
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            proposition = query.TryGetValue("proposition", out var data) ? (Proposition)data : throw new ArgumentException("Aucune proposition existante");
        }
    }
}
