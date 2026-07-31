using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace com.koyok.democratia.Data.Repository.RemoteRepository
{
    internal class ThematiqueRemoteRepository(HttpClient client, IRemoteToDomain remote) : RemoteBaseRepository(client, remote), IThematiqueRepository
    {
        public async Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            var thematique = (Thematique)parameters![0]!;
            var requete = $"thematiques";
            HttpResponseMessage response;
            try
            {
                var contenu = new StringContent(JsonSerializer.Serialize(thematique), Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
                response = await client?.PostAsync(requete, contenu)!;
            }
            catch (Exception)
            {
                throw new ConnexionErrorException();
            }
            return await ExtraiteStatus(response);

        }

        public Task<bool> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IModel>> GetModelAsync(params object?[] parameters)
        {
            HttpResponseMessage response;
            try
            {
                var requete = "thematiques";

                response = await client?.GetAsync(requete)!;
                string content = await response.Content.ReadAsStringAsync();
                return [.. RecuprerInformationConnexion<Thematique>(content)];

            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }

        }

        public Task<bool> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }
    }
}
