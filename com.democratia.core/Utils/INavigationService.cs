using Microsoft.Maui.Controls;

namespace com.democratia.Utils;
public interface INavigationService
{
    Task GoToAsync(string route, ShellNavigationQueryParameters? parameters = null);
}