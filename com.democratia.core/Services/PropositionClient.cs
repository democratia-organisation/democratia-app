using com.democratia.Models;
using com.democratia.Utils;

namespace com.democratia.Services
{
    public class PropositionClient(HttpClient client) : Client(client), IPropositionClient
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

                ?request=SELECT bin_to_uuid(id_groupe) as id_groupe,
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

        Task<string> IClient.GetModelAsync(params object?[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}