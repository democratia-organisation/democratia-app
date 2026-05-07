using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Data.Repository
{
    public class PropositionRepository(HttpClient client, PropositionLocalSource localSource, PropositionRemoteSource remoteSource) 
        : BaseRepository(client, localSource, remoteSource), IPropositionRepository
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

                ?request=SELECT BIN_TO_UUID(id_groupe) AS id_groupe,
                    budget,
                    date_publication,
                    description_proposition,
                    id_proposition,
                    id_thematique,
                    nb_signalement,
                    titre_proposition
                FROM proposition
                WHERE id_groupe = UUID_TO_BIN(?, 1)
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

        Task<string> Domain.Repository.Repository.GetModelAsync(params object?[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}