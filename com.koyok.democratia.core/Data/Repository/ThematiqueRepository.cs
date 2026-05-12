using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository
{
    internal class ThematiqueRepository(HttpClient client, IEnumerable<ILocalSource> localSources, IEnumerable<IRemoteSource> remoteSources,
        IEnumerable<IRemoteToDomain> remotes, IEnumerable<ILocalToDomain> domains)
        : BaseRepository(client,
            localSources.OfType<ThematiqueLocalSource>().FirstOrDefault()!,
            remoteSources.OfType<ThematiqueRemoteSource>().FirstOrDefault()!,
            remotes.OfType<ThematiqueRemoteToDomain>().FirstOrDefault()!,
            domains.OfType<ThematiqueLocalToDomain>().FirstOrDefault()!), IThematiqueRepository
    {
        public async Task<string> CreateModelAsync(params object?[]? parameters)
        {
            var thematique = (Thematique)parameters![0]!;
            var requete = $"thematiques/{thematique.nomThematique}";
            HttpResponseMessage response;
            try
            {
                response = await client?.PostAsync(requete, null)!;
            }
            catch (Exception) { 
                throw new ConnexionErrorException();
            }
            return await response.Content.ReadAsStringAsync();

        }

        public Task<string> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetModelAsync(params object?[] parameters)
        {
            HttpResponseMessage response;
            try
            {
                var content = "thematiques";
                
                response = await client?.GetAsync(content)!;
                return await response.Content.ReadAsStringAsync();

            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }

        }

        public Task<string> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }
    }
}
