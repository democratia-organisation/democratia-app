using com.democratia.Services;

namespace com.democratia.test.Services
{
    public class ClientTest
    {
        private IServiceProvider? _serviceProvider;

        public static TheoryData<Client, string> ClientData => new()
        {
            { new InternauteClient(), "modadary56@gmail.com" }
        };

        [Theory]
        [MemberData(nameof(ClientData))]
        public async Task GetMethodeTest(Client client, params object?[]? parameters)
        {
            _serviceProvider = TestServiceCollection.CreateTestServiceProviderForClients();
            var provider = _serviceProvider.GetRequiredService<Provider>();
            var clients = provider.clients;
            var testeur = clients!.FirstOrDefault(c => c?.GetType() == client.GetType());

            
            var result = await testeur?.GetModelAsync(parameters![0])!;

            
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

        [Theory]
        [MemberData(nameof(ClientData))]
        public async Task GetMethodeErroConnexoTest(Client client, params object?[]? parameters)
        {
            
            _serviceProvider = TestServiceCollection.CreateTestServiceProviderForClients();
            var provider = _serviceProvider.GetRequiredService<Provider>();
            var clients = provider.clients;
            var testeur = clients!.FirstOrDefault(c => c?.GetType() == client.GetType());
            testeur?.SetPort(1234); // Port incorrect pour provoquer une erreur de connexion

            
            // TODO : si le timeout est configuré, il faut modifier le test qui doit attendre une exception de type TaskCanceledException
            var result = await Assert.ThrowsAsync<HttpRequestException>(async () => await testeur!.GetModelAsync(parameters![0]));
            Assert.Equal("Erreur de connexion inattendu", result.Message);
        }
    }
}
