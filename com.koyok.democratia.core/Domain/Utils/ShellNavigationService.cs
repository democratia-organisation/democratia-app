using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System.Data;

namespace com.koyok.democratia.Domain.Utils
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
                catch (Exception)
                {
                    throw;
                }
            });
            return Task.CompletedTask;
        }
    }
}