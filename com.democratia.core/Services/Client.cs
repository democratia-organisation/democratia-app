using System.Net.Http.Headers;

namespace com.democratia.Services
{
    public abstract class Client : IClient
    {
        protected static readonly string BASE_URL = "https://projets.iut-orsay.fr/saes3-mmarti32/API/rest.php";
        protected string? statutsMessage;
        protected int? statuts;
        protected HttpClient? client;

        protected Client()
        {
            // TODO : peut-être mettre un timeout si le temps d'attente est vraiment invivable côté client
            client = new() { BaseAddress = new(BASE_URL) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            statutsMessage = string.Empty;
            statuts = 0;
        }

        protected async Task<string> GetMethode()
        {
            var response = await client!.GetAsync("?request=getMethode");
            response.EnsureSuccessStatusCode();
            MettreAJourStatuts(response);
            return await response.Content.ReadAsStringAsync();
        }

        public void SetPort(int port)
        {
            client!.BaseAddress = new Uri($"https://projets.iut-orsay.fr:{port}/saes3-mmarti32/API/rest.php");
        }

        public abstract Task<string> GetModelAsync(params object?[] parameters);

        protected void MettreAJourStatuts(HttpResponseMessage? response)
        {
            statuts = (int)response!.StatusCode;
            statutsMessage = response.ReasonPhrase;
        }
    }
}
