using com.democratia.Services;
using com.democratia.test.Services;
using com.democratia.ViewModels;

public static class TestServiceCollection
{
    public static IServiceProvider CreateTestServiceProviderForMainViewModel()
    {
        var services = new ServiceCollection();

        services.AddSingleton<INavigationService, ShellNavigationService>();
        services.AddSingleton<IClient, InternauteClient>();
        services.AddTransient<MainPageViewModel>();

        return services.BuildServiceProvider();
    }

    public static IServiceProvider CreateTestServiceProviderForClients()
    {
        var services = new ServiceCollection();

        // TODO : ajouter d'autres clients si nécessaire
        services.AddSingleton<IClient,InternauteClient>();
        services.AddTransient<Provider>();

        return services.BuildServiceProvider();
    }
}