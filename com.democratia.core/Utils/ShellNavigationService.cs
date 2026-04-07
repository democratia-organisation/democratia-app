using Microsoft.Maui.Controls;

namespace com.democratia.Utils
{
    public class ShellNavigationService : INavigationService
    {
        public Task GoToAsync(string route, ShellNavigationQueryParameters parameters)
            => Shell.Current.GoToAsync(route, parameters);
    }
}