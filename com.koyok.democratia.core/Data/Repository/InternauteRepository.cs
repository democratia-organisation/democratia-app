using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace com.koyok.democratia.Data.Repository
{
    public class InternauteRepository(HttpClient client, IEnumerable<ILocalSource> localSources, IEnumerable<IRemoteSource> remoteSources,
        IEnumerable<IRemoteToDomain> remotes, IEnumerable<ILocalToDomain> domains)
        : BaseRepository(client,
            localSources.OfType<InternauteLocalSource>().FirstOrDefault()!,
            remoteSources.OfType<InternauteRemoteSource>().FirstOrDefault()!,
            remotes.OfType<InternauteRemoteToDomain>().FirstOrDefault()!,
            domains.OfType<InternauteLocalToDomain>().FirstOrDefault()!), IInternauteRepository
    {
        public async Task<string> CreateModelAsync(params object?[]? parameters)
        {
            HttpResponseMessage? response;

            try
            {
                var internaute = (Internaute)parameters![0]!;
                var content = "users";
                var stringContent = new StringContent(JsonSerializer.Serialize(internaute));
                response = await client!.PostAsync(content,stringContent);
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
                var requete = $"""users/login""";
                string jsonContent = JsonSerializer.Serialize(parameters);
                var content = new StringContent(jsonContent, Encoding.UTF8,new MediaTypeHeaderValue("application/json"));
                response = await client!.PostAsync(requete,content);
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
                response = await client!.GetAsync($"users/{email}/doublon");
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
                var content = new StringContent(JsonSerializer.Serialize(internaute));
                response = await client!.PatchAsync("users/", content);
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
                response = await client?.DeleteAsync($"users/{((Internaute)parameters![0]!).idInternaute}")!;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
