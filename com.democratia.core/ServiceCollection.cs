using com.democratia.Models;
using com.democratia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace com.democratia
{
    public static class ServiceCollection
    {
        /// <summary>
        /// Méthode pour ajouter les services nécessaires à l'application.
        /// </summary>
        /// <param name="services">La collection de services.</param>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<INavigationService, ShellNavigationService>();
            services.AddTransient<MainPageViewModel>();

            return services;
        }
    }
}
