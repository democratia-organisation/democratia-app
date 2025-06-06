using com.democratia.Services;

namespace com.democratia.test.Services
{
    public class ClientTest
    {
        private IServiceProvider? _serviceProvider;

        public static IEnumerable<object[]> ClientData =>
            new List<object[]>
            {
                    new object[] { new InternauteClient(), "modadary56@gmail.com" },
            };

        [Theory]
        [MemberData(nameof(ClientData))]
        public async Task GetMethodeTest(IClient client, params object?[]? parameters)
        {
            // Arrange
            _serviceProvider = TestServiceCollection.CreateTestServiceProviderForClients();
            var provider = _serviceProvider.GetRequiredService<Provider>();
            var clients = provider.clients;
            var testeur = clients!.FirstOrDefault(c => c?.GetType() == client.GetType());

            // Act
            var result = await testeur?.GetModelAsync(parameters![0])!;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

        [Theory]
        [MemberData(nameof(ClientData))]
        public async Task GetMethodeErroConnexoTest(IClient client, params object?[]? parameters)
        {
            // Arrange
            _serviceProvider = TestServiceCollection.CreateTestServiceProviderForClients();
            var provider = _serviceProvider.GetRequiredService<Provider>();
            var clients = provider.clients;
            var testeur = clients!.FirstOrDefault(c => c?.GetType() == client.GetType());
            testeur?.SetPort(1234); // Port incorrect pour provoquer une erreur de connexion

            // Assert
            // TODO : si le timeout est configuré, il faut modifier le test qui doit attendre une exception de type TaskCanceledException
            var result = await Assert.ThrowsAsync<HttpRequestException>(async () => await testeur!.GetModelAsync(parameters![0]));
            Assert.Equal("Erreur de connexion inattendu", result.Message);
        }
    }
}
