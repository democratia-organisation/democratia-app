
using Microsoft.Maui.Controls;

namespace com.democratia.ViewModels;
public interface INavigationService
{
    Task GoToAsync(string route, ShellNavigationQueryParameters? parameters = null);
}