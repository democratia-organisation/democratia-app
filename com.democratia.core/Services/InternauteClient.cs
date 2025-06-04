using System.Net.Http.Headers;

namespace com.democratia.Services
{
    public class InternauteClient : Client
    {
        

        public InternauteClient() : base() {}

        public async override Task<string> GetModelAsync(params object?[] parameters)
        {
            client!.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"""?request=SELECT * FROM internaute WHERE courriel=?&parameters=+["{parameters[0]}"]""");
            if (!response.IsSuccessStatusCode)
            {
                MettreAJourStatuts(response);
                throw new Exception("Requete râté");
            }
                
            else
            {
                MettreAJourStatuts(response);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
