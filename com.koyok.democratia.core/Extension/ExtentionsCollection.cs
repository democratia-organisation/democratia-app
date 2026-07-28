using com.koyok.democratia.Lib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Hosting;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Reflection;
using com.koyok.democratia.Data.Repository.RemoteRepository;

namespace com.koyok.democratia.Extension
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
                    destination.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        /// <summary>
        /// Remplace les éléments de la collection
        /// </summary>
        /// <typeparam name="T">la classe des éléments de la collection</typeparam>
        /// <param name="models">this</param>
        /// <param name="newElements">Les nouveaux éléments à ajouter.</param>
        /// <param name="customAdding">Une action à exécuter pour chaque élément ajouté. 
        /// Pas besoin d'appeller Add pour ajouter l'élément.
        /// </param>
        public static void RemplacerElements<T>(this ObservableCollection<T> models, IEnumerable<T> newElements, Action<T>? customAdding = null)
        {
            models.Clear();
            foreach (var item in newElements)
            {
                customAdding?.Invoke(item);
                models.Add(item);
            }
        }

        /// <summary>
        /// Remplace les éléments de la collection
        /// </summary>
        /// <typeparam name="T">la classe des éléments de la collection</typeparam>
        /// <param name="models">this</param>
        /// <param name="newElements">Les nouveaux éléments à ajouter.</param>
        /// <param name="customAdding">Une fonction asynchrone à exécuter pour chaque élément ajouté. 
        /// Pas besoin d'appeller Add pour ajouter l'élément.
        /// </param>
        /// <returns>la tâche asynchrone</returns>
        public static async Task RemplacerElementsAsync<T>(this ObservableCollection<T> models, IEnumerable<T> newElements, Func<T, Task>? customAdding = null)
        {
            models.Clear();
            foreach (var item in newElements)
            {
                await customAdding?.Invoke(item)!;
                models.Add(item);
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
                services.AddHttpClient<InternauteRemoteRepository>().AddAllHttpHander();
                services.AddHttpClient<GroupeRemoteRepository>().AddAllHttpHander();
                services.AddHttpClient<ThematiqueRemoteRepository>().AddAllHttpHander();
                services.AddHttpClient<PropositionRemoteRepository>().AddAllHttpHander();
                services.AddHttpClient<CommentaireRemoteRepository>().AddAllHttpHander();
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
            builder.AddJSonSettings("appsettings.production");
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

        extension(RemoteBaseRepository builder)
        {
            public Uri AffecterURL() => GetUrl();
        }
    }

    public static class ShellExtension
    {
        private static Lib.AppContext appContext = new(new(ServiceHelper.GetService<ILocalizationService>()!));

        extension(Shell shell)
        {
            public Lib.AppContext AppContext => appContext;
        }
    }
}
