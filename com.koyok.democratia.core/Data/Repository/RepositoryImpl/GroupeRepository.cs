using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.Repository.LocalRepository;
using com.koyok.democratia.Data.Repository.RemoteRepository;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository.RepositoryImpl
{
    internal class GroupeRepository(GroupeRemoteRepository remote, GroupeLocalRepository local) 
        : BaseRepository<GroupeLocalSource>(remote, local), IGroupeRepository
    {
        public  async Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            return await remote.CreateModelAsync(parameters);
        }

        public async Task<bool> CreateJointureThemeEtGroupeAsync(Guid? idGroupe, int? idThematique, float? budgetThematique)
        {
            return await remote.CreateJointureThemeEtGroupeAsync(idGroupe, idThematique, budgetThematique);
        }

        public async Task<List<Thematique>> GetJointureThemeEtGroupeAsync(Guid? idGroupe)
        {
            return await remote.GetJointureThemeEtGroupeAsync(idGroupe);
        }

        public override async Task<bool> UploadImage(Guid? id, string filePath)
        {
            throw new NotImplementedException();
        }

        public async override Task<byte[]?> GetImageAsync(params object?[]? parameters)
        {
            return await remote.GetImageAsync(parameters);
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

        public  async Task<bool> AjouterCreateur(Guid? id_internaute, Guid? id_groupe)
        {
            return await remote.AjouterCreateur(id_internaute, id_groupe);
        }

        public async Task<string> GetRoleGroupe(string rowGroupe)
        {
            return await remote.GetRoleGroupe(rowGroupe);
        }
    }
}
