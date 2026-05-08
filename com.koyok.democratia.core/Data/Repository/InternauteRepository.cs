using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository
{
    public class InternauteRepository(HttpClient client, InternauteLocalSource localSource, InternauteRemoteSource remoteSource) 
        : BaseRepository(client, localSource, remoteSource), IInternauteRepository
    {
        public async Task<string> CreateModelAsync(params object?[]? parameters)
        {
            HttpResponseMessage? response;

            try
            {
                var internaute = (Internaute)parameters![0]!;
                var content = $$"""?request=CreerUtilisateur&parameters=["{{internaute.nomInternaute}}", "{{internaute.prenomInternaute}}", "{{internaute.courriel}}", "{{internaute.adressePostale}}", "{{internaute.hashageMDP}}"]""";
                response = await client!.PostAsync(content, null);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetModelAsync(params object?[] parameters)
        {
            HttpResponseMessage? response;
            try
            {
                var requete = parameters.Length == 1 ?
                    $"""
                        ?request=SELECT * FROM internaute WHERE courriel=?&parameters=["{parameters[0]}"]
                        """ :
                    $"""
                        ?request={parameters[1]}&parameters=["{parameters[0]}"]
                        """;
                response = await client!.GetAsync(requete);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DoublonEmailAsync(string email)
        {
            

            HttpResponseMessage? response;
            try
            {
                response = await client!.GetAsync(
                    $$"""
                        ?request=SELECT COUNT(courriel) FROM internaute WHERE courriel=?&parameters=["{{email}}"]
                        """);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateModelAsync(params object?[]? parameters)
        {
            
            HttpResponseMessage? response;
            var internaute = (Internaute)parameters![0]!;
            try
            {
                response = await client!.PatchAsync($"""?request=ModifInfoInternaute&parameters=["{internaute.idInternaute}","{internaute.nomInternaute}","{internaute.prenomInternaute}","{internaute.adressePostale}","{internaute.courriel}","{internaute.hashageMDP}"]""", null);
            }
            catch (HttpRequestException ex) {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteModelAsync(params object?[]? parameters)
        {
            
            HttpResponseMessage? response;
            try
            {
                response = await client?.DeleteAsync($"?request=SupprimerInternaute&parameters=[{((Internaute)parameters![0]!).idInternaute}]")!;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
