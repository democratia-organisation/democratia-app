using Microsoft.Maui.Controls;

namespace com.democratia.Utils
{
    public class ShellNavigationService : INavigationService
    {
        public Task GoToAsync(string route, ShellNavigationQueryParameters? parameters = null)
        {
            // Ensure parameters is not null by initializing it if necessary
            parameters ??= [];

            return Shell.Current.GoToAsync(route, parameters);
        }
    }
}