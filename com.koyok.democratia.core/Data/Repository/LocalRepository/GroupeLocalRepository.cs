using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository.LocalRepository
{
    internal class GroupeLocalRepository(DataBaseCreation<GroupeLocalSource> databaseConnexion, IEnumerable<ILocalToDomain> domains) 
        : LocalBaseRepository<GroupeLocalSource>(databaseConnexion, domains.OfType<GroupeLocalToDomain>().FirstOrDefault()!), IGroupeRepository
    {
        public  async Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateJointureThemeEtGroupeAsync(Guid? idGroupe, int? idThematique, float? budgetThematique)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Thematique>> GetJointureThemeEtGroupeAsync(Guid? idGroupe)
        {
            throw new NotImplementedException();
        }

        public async override Task<bool> UploadImage(Guid? id, string filePath)
        {
            throw new NotImplementedException();
        }

        public async override Task<byte[]?> GetImageAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        

        public Task<bool> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IModel>> GetModelAsync(params object?[] parameters)
        {
            throw new NotImplementedException();
        }

        public  Task<bool> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public  async Task<bool> AjouterCreateur(int? id_internaute, Guid? id_groupe)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetRoleGroupe(string rowGroupe)
        {
            throw new NotImplementedException();
        }
    }
}
