

namespace com.democratia.Services
{
    internal class ThematiqueClient : Client, IClient
    {
        public Task<string> CreateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
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
                var content = "?request=SELECT * FROM thematique&parameters=[]";
                DebutRequete();
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
