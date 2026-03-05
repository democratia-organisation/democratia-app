using com.democratia.Services;
using com.democratia.Utils;
using com.democratia.test.Localization;

namespace com.democratia.test.Services
{
    public class ClientTest
    {
        private readonly IServiceProvider? _serviceProvider;

        public static TheoryData<Client, string> ClientData => new()
        {
            // TODO : ajouter les autres clients à tester quand ils seront implémentés
            { new InternauteClient(), "modadary56@gmail.com" }
        };

        public ClientTest()
        {
            _serviceProvider = TestServiceCollection.CreateTestServiceProviderForClients();
        }


        [Theory]
        [MemberData(nameof(ClientData))]
        public async Task GetMethodeTest(Client client, params object?[]? parameters)
        {
            Provider provider = _serviceProvider!.GetRequiredService<Provider>();
            IEnumerable<IClient?>? clients = provider.clients;
            IClient? testeur = clients!.FirstOrDefault(c => c?.GetType() == client.GetType());
            string result = await testeur?.GetModelAsync(parameters![0])!;
            Assert.NotNull(result);
            Assert.IsType<string>(result);
        }

        [Theory]
        [MemberData(nameof(ClientData))]
        public async Task GetMethodeErrorConnexoTest(Client client, params object?[]? parameters)
        {

            Provider provider = _serviceProvider!.GetRequiredService<Provider>();
            IEnumerable<IClient?>? clients = provider.clients;
            IClient? testeur = clients!.FirstOrDefault(c => c?.GetType() == client.GetType());
            testeur?.SetPort(1234); // Port incorrect pour provoquer une erreur de connexion
            ConnexionErrorException result = await Assert.ThrowsAsync<ConnexionErrorException>(async () => await testeur!.GetModelAsync(parameters![0]));
            Assert.Equal(AppResources.connexionErreur, result.Message);
        }
    }
}
