using Microsoft.Maui.Controls;

public interface INavigationService
{
    Task GoToAsync(string route, ShellNavigationQueryParameters? parameters);
}