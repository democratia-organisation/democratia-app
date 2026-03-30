using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace com.democratia.ViewModels.groupe
{
    public partial class GroupeViewModel : ConnectableViewModel, INavigeablleViewModel
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

        public async void GetImageAsync(string? url) => Image = await client!.GetImageAsync(url);

        [RelayCommand]
        public async Task OpenGroup(string nomGroupe)
        {
            var parameters = new ShellNavigationQueryParameters { { "nomGroupe", nomGroupe } };
            await navigationService?.GoToAsync("Groupe", parameters)!;
        }

        private void UpdateList(object _, EventEndScroll __)
        {

        }

        public record class EventEndScroll {}
        public enum TypeEventScroll
        {
            EndScroll,
            RechargeScoll
        }
    }
}
