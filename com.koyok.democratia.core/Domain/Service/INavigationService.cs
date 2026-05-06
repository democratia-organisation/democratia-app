using Microsoft.Maui.Controls;

namespace com.koyok.democratia.Domain.Service
{
    public interface INavigationService
    {
        Task GoToAsync(string route, ShellNavigationQueryParameters? parameters = null);
    }
}