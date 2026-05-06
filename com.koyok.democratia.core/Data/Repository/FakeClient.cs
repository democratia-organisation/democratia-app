using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository
{
    /// <summary>
    /// Classe utilisée pour simuler un client comportant des erreurs dans les tests unitaires.
    /// </summary>
    internal class FakeClient : BaseRepository, IFakeClient
    {
        private readonly string? fakeResponse;

        public FakeClient(HttpClient? client, string? fakeResponse) : base(client!) 
        {
            this.fakeResponse = fakeResponse;
        }
        public FakeClient() : this(null, null) { }

        public async Task<string> CreateModelAsync(params object?[]? parameters)
        {
            return await Task.FromResult(fakeResponse!);
        }


        public async Task<string> GetModelAsync(params object?[] parameters)
        {
            return await Task.FromResult(fakeResponse!);
        }

        public Task<string> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }
    }
}