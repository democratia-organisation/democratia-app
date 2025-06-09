using System.Net.Http.Headers;

namespace com.democratia.Services
{
    public class InternauteClient : Client
    {
        

        public InternauteClient() : base() {}

        public override Task<string> CreateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException("Not implemented");
        }

        public async override Task<string> GetModelAsync(params object?[] parameters)
        {
            DebutRequete();

            HttpResponseMessage? response; 
            try
            { response = await client!.GetAsync($"""?request=SELECT * FROM internaute WHERE courriel=?&parameters=+["{parameters[0]}"]""");}
            catch (Exception ex) when (ex is HttpRequestException)
            { throw new HttpRequestException("Erreur de connexion inattendu", ex); }
            
            return await FinRequete(response);

        }
    }
}
