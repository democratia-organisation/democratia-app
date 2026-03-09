

using com.democratia.Models;

namespace com.democratia.Services
{
    public class InternauteClient : Client, IClient
    {
        public InternauteClient() : base() { }

        public async Task<string> CreateModelAsync(params object?[]? parameters)
        {
            await DebutRequete();

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

            return await FinRequete(response);
        }

        public async Task<string> GetModelAsync(params object?[] parameters)
        {
            await DebutRequete();

            HttpResponseMessage? response;
            try
            {
                response = await client!.GetAsync(
                    $$"""
                        ?request=SELECT * FROM internaute WHERE courriel=?&parameters=["{{parameters[0]}}"]
                        """);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }

            return await FinRequete(response);
        }

        public async Task<string> DoublonEmailAsync(string email)
        {
            await DebutRequete();

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

            return await FinRequete(response);
        }

        public async Task<string> UpdateModelAsync(params object?[]? parameters)
        {
            await DebutRequete();
            HttpResponseMessage? response;
            var internaute = (Internaute)parameters![0]!;
            try
            {
                response = await client!.PatchAsync($"""?request=ModifInfoInternaute&parameters=["{internaute.id_internaute}","{internaute.nom_internaute}","{internaute.prenom_internaute}","{internaute.adresse_postale}","{internaute.courriel}","{internaute.hashageMDP}"]""", null);
            }
            catch (HttpRequestException ex) {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await FinRequete(response);
        }

        public async Task<string> DeleteModelAsync(params object?[]? parameters)
        {
            await DebutRequete();
            HttpResponseMessage? response;
            try
            {
                response = await client?.DeleteAsync($"?request=SupprimerInternaute&parameters=[{((Internaute)parameters![0]!).id_internaute}]")!;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await FinRequete(response);
        }
    }
}
