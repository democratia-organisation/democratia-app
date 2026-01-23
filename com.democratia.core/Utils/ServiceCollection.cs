using com.democratia.Services;
using com.democratia.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Hosting;
using System.Reflection;

namespace com.democratia.Utils
{
    public static class ServiceCollection
    {
        private static MauiAppBuilder? maui;
        

        extension(IServiceCollection services)
        {
            /// <summary>
            /// Méthode pour ajouter les services nécessaires à l'application.
            /// </summary>
            /// <param name="services">La collection de services.</param>
            public IServiceCollection AddServices()
            {
                services.AddSingleton<INavigationService, ShellNavigationService>();
                services.AddSingleton<IClient, InternauteClient>();
                services.AddSingleton<IClient, GroupClient>();
                services.AddTransient<MainPageViewModel>();
                services.AddTransient<CreationViewModel>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<GestionCompteViewModel>();
                services.AddSingleton<INavigeablleViewModel, MainPageViewModel>();
                services.AddSingleton<INavigeablleViewModel, CreationViewModel>();

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
                if (DeviceInfo.Current.DeviceType == DeviceType.Virtual && DeviceInfo.Current.Platform == DevicePlatform.Android)
                    url = new(maui!.GetAppSetting("ANDROID_URL_VIRTUAL"));
                else if (DeviceInfo.Current.Platform == DevicePlatform.Android && DeviceInfo.Current.DeviceType == DeviceType.Physical)
                    url = new(maui!.GetAppSetting("ANDROID_URL_PHYSICAL"));
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
