using CommunityToolkit.Mvvm.Input;

namespace com.democratia.ViewModels
{
    public interface INavigeablleViewModel
    {

        [RelayCommand]
        public Task NavigateTapped(string commande);
    }
}
