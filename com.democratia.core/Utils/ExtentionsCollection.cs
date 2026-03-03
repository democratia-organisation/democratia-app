using com.democratia.Services;
using com.democratia.ViewModels.internaute;
using com.democratia.ViewModels.internaute.gestionCompte;
using com.democratia.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Hosting;
using System.Reflection;
using com.democratia.ViewModels.internaute.CreerGroupe;
using com.democratia.ViewModels.groupe;

namespace com.democratia.Utils
{
    public static class ExtentionsCollection
    {
        private static MauiAppBuilder? maui;
        

        extension(IServiceCollection services)
        {
            /// <summary>
            /// Méthode pour ajouter les services nécessaires à l'application.
            /// </summary>
            /// <returns>Retourne la collection de services après l'ajout des services.</returns>
            public IServiceCollection AddServices()
            {
                services.AddViewModels();
                services.AddSingleton<INavigationService, ShellNavigationService>();
                services.AddClients();
                services.AddTransientViewModel();

                return services;
            }

            public IServiceCollection AddClients()
            {
                services.AddSingleton<IClient, InternauteClient>();
                services.AddSingleton<IClient, GroupClient>();
                services.AddSingleton<IClient, ThematiqueClient>();
                return services;

            }

            public IServiceCollection AddTransientViewModel()
            {
                services.AddTransient<MainViewModel>();
                services.AddTransient<CreationViewModel>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<HomeGestionViewModel>();
                services.AddTransient<PremiereCreationViewModel>();
                services.AddTransient<DeuxiemePageViewModel>();
                services.AddTransient<TroisiemeCreationViewModel>();
                services.AddTransient<GroupeViewModel>();
                services.AddTransient<ModifierGestionViewModel>();
                services.AddTransient<PreferenceViewModel>();

                return services;
            }

            public IServiceCollection AddViewModels()
            {
                services.AddSingleton<INavigeablleViewModel, MainViewModel>();
                services.AddSingleton<INavigeablleViewModel, CreationViewModel>();
                services.AddSingleton<INavigeablleViewModel, HomeViewModel>();
                services.AddSingleton<INavigeablleViewModel, PremiereCreationViewModel>();
                services.AddSingleton<INavigeablleViewModel, DeuxiemePageViewModel>();
                services.AddSingleton<INavigeablleViewModel, GroupeViewModel>();
                services.AddSingleton<INavigeablleViewModel, ModifierGestionViewModel>();

                return services;
            }
        }
        extension(MauiAppBuilder builder)
        {
            public void SetUrl()
            {
                maui = builder;
            }
            private void AddJSonSettings(string environnement)
            {
                using Stream stream = Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream($"com.democratia.core.{environnement}.json")!;
                IConfigurationRoot config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
                builder.Configuration.AddConfiguration(config);
            }
            private void AddSettings()
            {
#if DEBUG
                builder.AddJSonSettings("appsettings.developpement");
#elif !DEBUG
            builder.AddJSonSettings("appsettings.productiont");
            // ajouter la configuration pour du https
#endif
                builder.AddJSonSettings("appsettings");
            }

            internal string GetAppSetting(string nom_cle)
            {
                builder.AddSettings();
                return builder.Configuration.GetValue<string>(nom_cle)!;
            }
        }
        extension(Client builder)
        {
            public Uri AffecterURL()
            {
                Uri url;
#if DEBUG
                if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                    url = new(maui!.GetAppSetting("VIRTUAL_URL"));
                else
                    url = new(maui!.GetAppSetting("API_URL"));
#elif !DEBUG
                url = new(maui!.GetAppSetting("API_URL"));
#endif
                return url;
            }
        }
    }
}
