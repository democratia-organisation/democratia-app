using com.democratia.Models;
using com.democratia.Utils;

namespace com.democratia.Services
{
    internal class ThematiqueClient : Client, IClient
    {
        public async Task<string> CreateModelAsync(params object?[]? parameters)
        {
            var thematique = (Thematique)parameters![0]!;
            var requete = $"""
                ?request=INSERT INTO thematique (nom_thematique)
                VALUES (?);
                &parameters=["{thematique.nom_thematique}"]
                """;
            HttpResponseMessage response;
            try
            {
                response = await client?.PostAsync(requete, null)!;
            }
            catch (Exception) { 
                throw new ConnexionErrorException();
            }
            return await FinRequete(response);

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
                await DebutRequete();
                response = await client?.GetAsync(content)!;
                return await FinRequete(response);

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
