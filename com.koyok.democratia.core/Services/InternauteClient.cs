using com.koyok.democratia.Models;

namespace com.koyok.democratia.Services
{
    public class InternauteClient(HttpClient client) : Client(client), IInternauteClient
    {
        public async Task<string> CreateModelAsync(params object?[]? parameters)
        {
            HttpResponseMessage? response;

            try
            {
                var content = $$"""?request=CreerUtilisateur&parameters=["{{parameters![0]}}", "{{parameters[1]}}", "{{parameters[3]}}", "{{parameters[2]}}", "{{parameters[4]}}"]""";
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
                response = await client!.PatchAsync($"""?request=ModifInfoInternaute&parameters=["{internaute.id_internaute}","{internaute.nom_internaute}","{internaute.prenom_internaute}","{internaute.adresse_postale}","{internaute.courriel}","{internaute.hashageMDP}"]""", null);
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
                response = await client?.DeleteAsync($"?request=SupprimerInternaute&parameters=[{((Internaute)parameters![0]!).id_internaute}]")!;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
