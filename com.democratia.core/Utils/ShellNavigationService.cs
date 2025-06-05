using Microsoft.Maui.Controls;

public class ShellNavigationService : INavigationService
{
    public Task GoToAsync(string route, ShellNavigationQueryParameters? parameters) => Shell.Current.GoToAsync(route,parameters);
}