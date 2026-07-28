using com.koyok.democratia.Lib;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Data.Repository.RemoteRepository;
using com.koyok.democratia.Data.Repository.LocalRepository;
using com.koyok.democratia.Data.DataSource.Local;

namespace com.koyok.democratia.Data.Repository.RepositoryImpl
{
    public class PropositionRepository(PropositionRemoteRepository remote, PropositionLocalRepository local)
        : BaseRepository<PropositionLocalSource>(remote,local), IPropositionRepository
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
            return await remote.GetAllPropositionsAsync(parameters);
        }

        public List<Proposition> TrierProposition(Critere critere)
        {
            return remote.TrierProposition(critere);
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