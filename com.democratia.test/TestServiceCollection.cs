using com.democratia.Services;

namespace com.democratia.test;

public static class TestServiceCollection
{
    public static IServiceProvider CreateTestServiceProviderForMainViewModel()
    {
        var services = new ServiceCollection();

        
        return services.BuildServiceProvider();
    }

    public static IServiceProvider CreateTestServiceProviderForCreationViewModel()
    {
        var services = new ServiceCollection();

        return services.BuildServiceProvider();
    }

    public static IServiceProvider CreateFakeServiceProviderForMainViewModel(string? fakeResponse)
    {
        var services = new ServiceCollection();

        
        return services.BuildServiceProvider();
    }

    public static void CreateInternauteServiceProviderForClientTest(IServiceProvider sp)
    {
        var services = new ServiceCollection();
        

    }

    public static void CreateTestServiceProviderForClients()
    {
    }
}