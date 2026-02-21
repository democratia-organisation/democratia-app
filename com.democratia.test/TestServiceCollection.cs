using com.democratia.Services;
using com.democratia.test.Services;
using com.democratia.ViewModels.internaute;
using com.democratia.Utils;
using com.democratia.ViewModels.internaute.gestionCompte;

namespace com.democratia.test;

public static class TestServiceCollection // TODO : ajouter l'assembly com.democratia.view pour utiliser la classe AppResources
{
    public static IServiceProvider CreateTestServiceProviderForMainViewModel()
    {
        var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

        services.AddSingleton<INavigationService, ShellNavigationService>();
        services.AddSingleton<IClient, InternauteClient>();
        services.AddTransient<MainPageViewModel>();

        return services.BuildServiceProvider();
    }

    public static IServiceProvider CreateTestServiceProviderForCreationViewModel()
    {
        var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

        services.AddSingleton<INavigationService, ShellNavigationService>();
        services.AddSingleton<IClient, InternauteClient>();
        services.AddTransient<CreationViewModel>();

        return services.BuildServiceProvider();
    }

    public static IServiceProvider CreateFakeServiceProviderForMainViewModel(string? fakeResponse)
    {
        var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

        services.AddSingleton<INavigationService, ShellNavigationService>();
        services.AddSingleton<IClient>(sp => new FakeClient(fakeResponse));
        services.AddTransient<MainPageViewModel>();

        return services.BuildServiceProvider();
    }

    public static IServiceProvider CreateTestServiceProviderForClients()
    {
        var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

        // TODO : ajouter d'autres clients si nécessaire
        services.AddSingleton<IClient, InternauteClient>();
        services.AddTransient<Provider>();

        return services.BuildServiceProvider();
    }
}