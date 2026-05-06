using CommunityToolkit.Mvvm.Input;

namespace com.koyok.democratia.ViewModels
{
    public interface INavigeablleViewModel
    {

        [RelayCommand]
        public Task NavigateTapped(string commande);
    }
}
