using Microsoft.Maui.Controls;

namespace com.koyok.democratia.core.Domain.Utils
{
    public interface INavigationService
    {
        Task GoToAsync(string route, ShellNavigationQueryParameters? parameters = null);
    }
}