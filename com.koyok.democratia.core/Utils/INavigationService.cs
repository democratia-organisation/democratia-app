using Microsoft.Maui.Controls;

namespace com.koyok.democratia.Utils
{
    public interface INavigationService
    {
        Task GoToAsync(string route, ShellNavigationQueryParameters? parameters = null);
    }
}