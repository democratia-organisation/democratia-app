using CommunityToolkit.Mvvm.Input;

namespace com.koyok.democratia.UI
{
    public interface INavigeablleViewModel
    {

        [RelayCommand]
        public Task NavigateTapped(string commande);
    }
}
