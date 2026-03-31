using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace com.democratia.ViewModels.groupe
{
    public partial class GroupeViewModel : ConnectableViewModel, INavigeablleViewModel, IQueryAttributable
    {
        [ObservableProperty] private ImageSource? image;
        [ObservableProperty] private Groupe? groupe;
        [ObservableProperty] private ObservableCollection<Proposition>? propositions;
        private INavigationService? navigationService;

        public GroupeViewModel(IEnumerable<IClient> clients, INavigationService? navigationService) : base(clients.FirstOrDefault(), null)
        {
            this.navigationService = navigationService;
            WeakReferenceMessenger.Default.Register<GroupeViewModel,EventEndScroll,string>(this, TypeEventScroll.EndScroll.ToString() ,UpdateList);
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            await navigationService?.GoToAsync(commande)!;
        }

        public async Task ChargerProposition()
        {
            var propositionClient = ServiceHelper.GetService<IPropositionClient>();
            var response = await ((PropositionClient)propositionClient!).GetAllPropositionsAsync(Groupe!.IdGroupe);
            var stringpropositions = RecuprerInformationConnexion(response)!;            
            Propositions = stringpropositions.Count == 0 ?[] : 
                [..JsonSerializer.Deserialize<List<Proposition>>(stringpropositions.ToString()!)!];
        }

        public async void GetImageAsync(string? url) => Image = await client!.GetImageAsync(url);

        [RelayCommand]
        public async Task OpenGroup(string nomGroupe)
        {
            var parameters = new ShellNavigationQueryParameters { { "nomGroupe", nomGroupe }, { "modele", Groupe! } };
            await navigationService?.GoToAsync("GroupePage", parameters)!;
        }

        private void UpdateList(object _, EventEndScroll __)
        {
            throw new NotImplementedException();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Groupe = query.TryGetValue("modele", out var value) ? (Groupe)value : new();
        }

        public record class EventEndScroll {}
        public enum TypeEventScroll
        {
            EndScroll,
            RechargeScoll
        }
    }
}
