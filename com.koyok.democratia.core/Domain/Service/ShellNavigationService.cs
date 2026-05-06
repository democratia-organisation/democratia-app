using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace com.koyok.democratia.Domain.Service
{
    public class ShellNavigationService : INavigationService
    {
        public Task GoToAsync(string route, ShellNavigationQueryParameters? parameters = null)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (parameters == null)
                    {
                        parameters = [];
                    }
                    await Shell.Current.GoToAsync(route, false ,parameters);
                }
                catch (System.Exception)
                {
                    throw;
                }
            });
            return Task.CompletedTask;
        }
    }
}