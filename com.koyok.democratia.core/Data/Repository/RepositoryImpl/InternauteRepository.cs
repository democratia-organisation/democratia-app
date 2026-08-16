using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.Repository.LocalRepository;
using com.koyok.democratia.Data.Repository.RemoteRepository;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using System.Collections;

namespace com.koyok.democratia.Data.Repository.RepositoryImpl
{
    public class InternauteRepository(InternauteRemoteRepository remote, InternauteLocalRepository local)
        : BaseRepository<InternauteLocalSource>(remote,local), IInternauteRepository
    {
        public  async Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            return await remote.CreateModelAsync(parameters);
        }

        public async Task<List<IModel>> GetModelAsync(params object?[] parameters)
        {
            
            List<IModel> liste = await local.GetModelAsync(parameters);
            if (liste.Count < 1)
            {
                liste = await remote.GetModelAsync(parameters);
                foreach (var item in liste)
                {
                    if (!(await local.CreateModelAsync(item))) throw new Exception();
                }
            }
            return liste;
        }

        public async Task<bool> DoublonEmailAsync(string email)
        {

            return await remote.DoublonEmailAsync(email);
        }

        public async Task<bool> UpdateModelAsync(params object?[]? parameters)
        {
            return await remote.UpdateModelAsync(parameters);
        }

        public async Task<bool> DeleteModelAsync(params object?[]? parameters)
        {

            return await remote.DeleteModelAsync(parameters);
        }

        public async Task<bool> SaveNotification(Groupe groupe, BitArray notificationChoices, Internaute internaute)
        {
            return await remote.SaveNotification(groupe, notificationChoices, internaute);
        }
    }
}
