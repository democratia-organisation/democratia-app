using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Lib;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository.RemoteRepository
{
    public class PropositionRemoteRepository(HttpClient client, IRemoteToDomain remote) : RemoteBaseRepository(client, remote), IPropositionRepository
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
            HttpResponseMessage? response;
            string? requete = $"propositions/{parameters![0]}";
            try
            {
                response = await client!.GetAsync(requete);
            } catch (HttpRequestException)
            {
                throw new ConnexionErrorException();
            }
            string content = await response.Content.ReadAsStringAsync();
            return [.. RecuprerInformationConnexion<Proposition>(content)];
        }

        public List<Proposition> TrierProposition(Critere critere)
        {
            throw new NotImplementedException();
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