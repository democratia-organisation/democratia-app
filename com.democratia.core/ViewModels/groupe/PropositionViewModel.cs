using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace com.democratia.ViewModels.groupe
{
    public partial class PropositionViewModel : ConnectableViewModel, IQueryAttributable
    {
        [ObservableProperty] private DateOnly _dateFinDiscussion;
        [ObservableProperty] private DateOnly _dateVote;
        private readonly Groupe? groupe;
        public PropositionViewModel(IEnumerable<IClient?> clients, ILocalizationService? localizationService) 
            : base(clients.OfType<PropositionClient>().FirstOrDefault(), localizationService)
        {
            int jourDiscussion = groupe!.NombreDeJourDiscuss ?? 1;
            int jourVote = groupe.NombreDeJourVote ?? 1;
            
            _dateFinDiscussion = DateOnly.FromDateTime(DateTime.Parse(DateTime.Now.Date.ToString("d MMMM yyyy"))).AddDays(jourDiscussion);
            _dateVote = DateOnly.FromDateTime(DateTime.Parse(DateTime.Now.Date.ToString("d MMMM yyyy"))).AddDays(jourVote);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            throw new NotImplementedException();
        }
    }
}
