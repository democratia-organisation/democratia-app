using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository
{
    /// <summary>
    /// Classe utilisée pour simuler un client comportant des erreurs dans les tests unitaires.
    /// </summary>
    internal class FakeRepository(HttpClient? client, string? fakeResponse, ILocalSource? localSource, IRemoteSource? remoteSource, IRemoteToDomain? remoteToDomain, ILocalToDomain? localToDomain) 
        : BaseRepository(client!, localSource!, remoteSource!, remoteToDomain!, localToDomain!), IFakeRepository
    {
        private readonly string? fakeResponse = fakeResponse;

        public FakeRepository() : this(null, null, null, null, null, null) { }

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