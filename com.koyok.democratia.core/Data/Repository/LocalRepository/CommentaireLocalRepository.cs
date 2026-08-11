using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository.LocalRepository
{
    internal class CommentaireLocalRepository(DataBaseCreation<CommentaireLocalSource> databaseConnexion, ILocalToDomain domain) :
        LocalBaseRepository<CommentaireLocalSource>(databaseConnexion, domain), ICommentaireRepository
    {
        public Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public Task<List<IModel>> GetModelAsync(params object?[] parameters)
        {
            throw new NotImplementedException();
        }

        public  Task<bool> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }
    }
}
