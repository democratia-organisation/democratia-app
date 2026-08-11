using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Lib;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Data.DataSource.Local;

namespace com.koyok.democratia.Data.Repository.LocalRepository  
{
    public class PropositionLocalRepository(DataBaseCreation<PropositionLocalSource> databaseConnexion, ILocalToDomain domain)
        : LocalBaseRepository<PropositionLocalSource>(databaseConnexion, domain), IPropositionRepository
    {
        public Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Proposition>> GetAllPropositionsAsync(params object?[] parameters)
        {
            throw new NotImplementedException();
        }

        public List<Proposition> TrierProposition(Critere critere)
        {
            throw new NotImplementedException();
        }

        public  Task<bool> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public Task<List<IModel>> GetModelAsync(params object?[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}