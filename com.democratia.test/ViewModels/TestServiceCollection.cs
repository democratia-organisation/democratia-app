using com.democratia.Services;
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
}