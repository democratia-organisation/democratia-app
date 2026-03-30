using com.democratia.Services;
using com.democratia.ViewModels.groupe;
using com.democratia.ViewModels.internaute;
using com.democratia.ViewModels.internaute.CreerGroupe;
using com.democratia.ViewModels.internaute.gestionCompte;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Hosting;
using System.Net.Http.Headers;
using System.Reflection;

namespace com.democratia.Utils
{
    public static class ExtentionsCollection
    {
        private static MauiAppBuilder? maui;

        extension(HttpRequestMessage request)
        {
            public async Task<HttpRequestMessage> CloneRequest()
            {
                var clone = new HttpRequestMessage(request.Method, request.RequestUri)
                {
                    Version = request.Version
                };

                if (request.Content != null)
                {
                    var contentStream = new MemoryStream();
                    await request.Content.CopyToAsync(contentStream);
                    contentStream.Position = 0;
                    clone.Content = new StreamContent(contentStream);
                    request.CloneHeader(request.Content.Headers, clone.Content.Headers);
                }
                request.CloneHeader(request.Headers, clone.Headers);

                foreach (var prop in request.Options)
                {
                    clone.Options.Set(new HttpRequestOptionsKey<object?>(prop.Key), prop.Value);
                }
                return clone;
            }
            public void CloneHeader(HttpHeaders source, HttpHeaders destination)
            {
                foreach (var header in source)
                {
                    destination.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }


        extension(IServiceCollection services)
        {
            /// <summary>
            /// Méthode pour ajouter les services nécessaires à l'application.
            /// </summary>
            /// <returns>Retourne la collection de services après l'ajout des services.</returns>
            public IServiceCollection AddServices()
            {
                services.AddSingleton<INavigationService, ShellNavigationService>();
                services.AddClients();
                services.AddClient();
                services.AddTransientViewModel();

                return services;
            }

            private IServiceCollection AddClients()
            {
                services.AddHttpExtension();
                services.AddHttpClient<IInternauteClient, InternauteClient>()
                    .AddHttpMessageHandler<DebutRequete>()
                    .AddHttpMessageHandler<AuthentificationHandler>()
                    .AddHttpMessageHandler<FinRequete>();
                services.AddHttpClient<IGroupeClient, GroupClient>()
                    .AddHttpMessageHandler<DebutRequete>()
                    .AddHttpMessageHandler<AuthentificationHandler>()
                    .AddHttpMessageHandler<FinRequete>();
                services.AddHttpClient<IThematiqueClient, ThematiqueClient>()
                    .AddHttpMessageHandler<DebutRequete>()
                    .AddHttpMessageHandler<AuthentificationHandler>()
                    .AddHttpMessageHandler<FinRequete>();
                services.AddHttpClient("ClientBrut", c => {
                    Uri url;
#if DEBUG
                    if (DeviceInfo.Current.Platform == DevicePlatform.Android)
                        url = new(maui!.GetAppSetting("VIRTUAL_URL"));
                    else
                        url = new(maui!.GetAppSetting("API_URL"));
#elif !DEBUG
                url = new(maui!.GetAppSetting("API_URL"));
#endif

                    c.BaseAddress = url;
                });

                return services;

            }

            public IServiceCollection AddClient()
            {
               
                services.AddTransient<IClient>(s => s.GetRequiredService<IInternauteClient>());
                services.AddTransient<IClient>(s => s.GetRequiredService<IGroupeClient>());
                services.AddTransient<IClient>(s => s.GetRequiredService<IThematiqueClient>());

                return services;
            }

            private IServiceCollection AddTransientViewModel()
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
                services.AddTransient<ViewModels.groupe.decideur.HomeViewModel>();

                return services;
            }

            private void AddHttpExtension()
            {
                services.AddTransient<DebutRequete>();
                services.AddTransient<AuthentificationHandler>();
                services.AddTransient<FinRequete>();
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
