using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.Repository.LocalRepository;
using com.koyok.democratia.Data.Repository.RemoteRepository;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository.RepositoryImpl
{
    internal class ThematiqueRepository(ThematiqueRemoteRepository remote, ThematiqueLocalRepository local)
        : BaseRepository<ThematiqueLocalSource>(remote,local), IThematiqueRepository
    {
        public  async Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            return await remote.CreateModelAsync(parameters);
        }

        public Task<bool> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IModel>> GetModelAsync(params object?[] parameters)
        {
            return await remote.GetModelAsync(parameters);
        }

        public  Task<bool> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }
    }
}
