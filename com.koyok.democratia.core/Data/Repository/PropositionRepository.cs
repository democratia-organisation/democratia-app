using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository
{
    public class PropositionRepository(HttpClient client, IEnumerable<ILocalSource> localSources, IEnumerable<IRemoteSource> remoteSources,
        IEnumerable<IRemoteToDomain> remotes, IEnumerable<ILocalToDomain> domains)
        : BaseRepository(client,
            localSources.OfType<PropositionLocalSource>().FirstOrDefault()!,
            remoteSources.OfType<PropositionRemoteSource>().FirstOrDefault()!,
            remotes.OfType<PropositionRemoteToDomain>().FirstOrDefault()!,
            domains.OfType<PropositionLocalToDomain>().FirstOrDefault()!), IPropositionRepository
    {
        public Task<string> CreateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetAllPropositionsAsync(params object?[] parameters)
        {
            HttpResponseMessage? response;
            string? requete = $""""

                ?request=SELECT BIN_TO_UUID(id_groupe) AS id_groupe,id_proposition
                    budget,
                    date_publication,
                    description_proposition,
                    id_proposition,
                    id_thematique,
                    nb_signalement,
                    titre_proposition
                FROM proposition
                WHERE id_groupe = UUID_TO_BIN(?)
                &parameters=["{parameters[0]!}"]
                """";
            try
            {
                response = await client!.GetAsync(requete);
            } catch (HttpRequestException)
            {
                throw new ConnexionErrorException();
            }
            return await response!.Content.ReadAsStringAsync();
        }

        public Task<string> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        Task<string> GetModelAsync(params object?[] parameters)
        {
            throw new NotImplementedException();
        }

        Task<string> IRepository.GetModelAsync(params object?[] parameters)
        {
            return GetModelAsync(parameters);
        }
    }
}