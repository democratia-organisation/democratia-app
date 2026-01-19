using com.democratia.Services;
using com.democratia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace com.democratia.Utils
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
            services.AddSingleton<IClient, InternauteClient>();
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<CreationViewModel>();
            services.AddSingleton<INavigeablleViewModel, MainPageViewModel>();
            services.AddSingleton<INavigeablleViewModel, CreationViewModel>();

            return services;
        }
    }
}
