using com.democratia.core.Services;
using com.democratia.Models;
using com.democratia.Services;
using com.democratia.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace com.democratia.core
{
    public static class ServiceCollection
    {
        /// <summary>
        /// Méthode pour ajouter les services nécessaires à l'application.
        /// </summary>
        /// <param name="services">La collection de services.</param>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Ajout des services nécessaires
            services.AddTransient<IClient, InternauteClient>();
            services.AddTransient<IViewModel, MainPageViewModel>();
            services.AddTransient<IModel, Internaute>();

            return services;
        }
    }
}
