using com.koyok.democratia.Data.Repository;
using com.koyok.democratia.Domain.Extension.DelegatesHandler;
using com.koyok.democratia.Domain.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Hosting;
using System.Net.Http.Headers;
using System.Reflection;

namespace com.koyok.democratia.Domain.Extension
{
    public static class ExtentionsCollection
    {
        private static MauiAppBuilder? maui;
        private static Utils.AppContext appContext = new();

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
                    CloneHeader(request.Content.Headers, clone.Content.Headers);
                }
                CloneHeader(request.Headers, clone.Headers);

                foreach (var prop in request.Options)
                {
                    clone.Options.Set(new HttpRequestOptionsKey<object?>(prop.Key), prop.Value);
                }
                return clone;
            }
            public static void CloneHeader(HttpHeaders source, HttpHeaders destination)
            {
                foreach (var header in source)
                {
                    destination.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }

        extension(IHttpClientBuilder builder)
        {
            public IHttpClientBuilder AddAllHttpHander()
            {
                builder.AddHttpMessageHandler<AuthentificationHandler>()
                    .AddHttpMessageHandler<DebutRequete>()
                    .AddHttpMessageHandler<FinRequete>();
                return builder;
            }
        }



        extension(IServiceCollection services)
        {

            private void AddHttpExtension()
            {
                services.AddTransient<DebutRequete>();
                services.AddTransient<AuthentificationHandler>();
                services.AddTransient<FinRequete>();
            }
            public IServiceCollection AddClients()
            {
                services.AddHttpExtension();
                services.AddHttpClient<IInternauteRepository, InternauteRepository>().AddAllHttpHander();
                services.AddHttpClient<IGroupeRepository, GroupRepository>().AddAllHttpHander();
                services.AddHttpClient<IThematiqueRepository, ThematiqueRepository>().AddAllHttpHander();
                services.AddHttpClient<IPropositionRepository, PropositionRepository>().AddAllHttpHander();
#if DEBUG
                // utiliser pour avoir les clé JWT dans les middlewares 
#endif
                services.AddHttpClient("ClientBrut", static c => c.BaseAddress = GetUrl());

                return services;

            }

        }

        private static Uri GetUrl()
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
                    .GetManifestResourceStream($"com.koyok.democratia.core.{environnement}.json")!;
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

        extension(BaseRepository builder)
        {
            public Uri AffecterURL() => GetUrl();
        }

        extension(Shell shell)
        {
            public Utils.AppContext AppContext => appContext;
        }
    }
}
