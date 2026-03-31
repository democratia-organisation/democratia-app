using Microsoft.Extensions.DependencyInjection;

namespace com.democratia.Utils
{
    /// <summary>
    /// classe qui permet d'avoir les dépendances injectés dans tout l'assembly com.democratia.view
    /// </summary>
    public static class ServiceHelper
    {
        public static IServiceProvider? Services { get; private set; }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;
        }

        public static T? GetService<T>() => Services!.GetService<T>();
    }
}
