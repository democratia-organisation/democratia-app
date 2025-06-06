using Microsoft.Maui.Controls;

public class ShellNavigationService : INavigationService
{
    public Task GoToAsync(string route, ShellNavigationQueryParameters? parameters = null)
    {
        // Ensure parameters is not null by initializing it if necessary
        parameters ??= new ShellNavigationQueryParameters();

        // Example implementation of navigation logic
        return Shell.Current.GoToAsync(route, parameters);
    }
}