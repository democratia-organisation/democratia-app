using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace com.democratia.ViewModels.groupe
{
    public partial class GroupeViewModel : ConnectableViewModel, INavigeablleViewModel
    {
        [ObservableProperty] private ImageSource? image;
        private INavigationService? navigationService;

        public GroupeViewModel(IEnumerable<IClient> clients, INavigationService? navigationService) : base(clients.FirstOrDefault(), null)
        {
            this.navigationService = navigationService;
        }

        public Task NavigateTapped(string commande)
        {
            throw new NotImplementedException();
        }

        public async void GetImageAsync(string? url) => Image = await client!.GetImageAsync(url);

        [RelayCommand]
        public async Task OpenGroup(string nomGroupe)
        {
            var parameters = new ShellNavigationQueryParameters { { "nomGroupe", nomGroupe } };
            await navigationService?.GoToAsync("Groupe", parameters)!;
        }
    }
}
