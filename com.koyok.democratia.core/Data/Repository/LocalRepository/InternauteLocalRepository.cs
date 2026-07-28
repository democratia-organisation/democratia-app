using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository.LocalRepository
{
    public class InternauteLocalRepository(DataBaseCreation<InternauteLocalSource> databaseConnexion, IEnumerable<ILocalToDomain> domains)
        : LocalBaseRepository<InternauteLocalSource>(databaseConnexion,
            domains.OfType<InternauteLocalToDomain>().FirstOrDefault()!), IInternauteRepository
    {
        public  async Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            var internaute = ((Internaute)parameters![0]!);
            var localInternaute = (InternauteLocalSource)this.localToDomain.ReversMapping(internaute);
            int rowAdded = await this.databaseConnexion.database!.InsertOrReplaceAsync(localInternaute);
            return rowAdded == 1;
        }


        public async Task<List<IModel>> GetModelAsync(params object?[] parameters)
        {
            string email = (string)parameters![0]!;
            var internauteLocals = await this.databaseConnexion!.database!.Table<InternauteLocalSource>().Where(source => source.Courriel == email).ToListAsync();
            List<IModel>? internautes = [];
            internauteLocals.ForEach(internauteLocal => internautes.Add(localToDomain.Mapping(internauteLocal)!));
            return internautes;
        }

        public async Task<bool> DoublonEmailAsync(string email)
        {

            throw new NotImplementedException();
        }

        public async Task<bool> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteModelAsync(params object?[]? parameters)
        {

            throw new NotImplementedException();
        }
    }
}
