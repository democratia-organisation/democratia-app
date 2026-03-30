using com.democratia.Services;
using com.democratia.test.Services;
using com.democratia.ViewModels.internaute;
using com.democratia.Utils;
using com.democratia.ViewModels.internaute.gestionCompte;

namespace com.democratia.test;

public static class TestServiceCollection
{
    public static IServiceProvider CreateTestServiceProviderForMainViewModel()
    {
        var services = new ServiceCollection();

        services.AddSingleton<INavigationService, ShellNavigationService>();
        services.AddSingleton<IClient, InternauteClient>();
        services.AddTransient<MainViewModel>();

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
        var services = new ServiceCollection();

        services.AddSingleton<INavigationService, ShellNavigationService>();
        services.AddSingleton<IFakeClient>(sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = factory.CreateClient(nameof(IInternauteClient));
            return new FakeClient(httpClient,fakeResponse);
        });
        services.AddTransient<MainViewModel>();

        return services.BuildServiceProvider();
    }

    public static IServiceProvider CreateTestServiceProviderForClients()
    {
        var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
        services.AddSingleton<IClient, InternauteClient>();
        services.AddTransient<Provider>();

        return services.BuildServiceProvider();
    }
}