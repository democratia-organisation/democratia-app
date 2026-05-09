using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;

namespace com.koyok.democratia.Data.Repository
{
    internal class ThematiqueRepository(HttpClient client, IEnumerable<ILocalSource> localSources, IEnumerable<IRemoteSource> remoteSources) 
        : BaseRepository(client, 
            localSources.OfType<ThematiqueLocalSource>().FirstOrDefault()!, 
            remoteSources.OfType<ThematiqueRemoteSource>().FirstOrDefault()!), IThematiqueRepository
    {
        public async Task<string> CreateModelAsync(params object?[]? parameters)
        {
            var thematique = (Thematique)parameters![0]!;
            var requete = $"""
                ?request=INSERT INTO thematique (nom_thematique)
                VALUES (?);
                &parameters=["{thematique.nomThematique}"]
                """;
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
                var content = "?request=SELECT * FROM thematique ORDER BY id_thematique&parameters=[]";
                
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
