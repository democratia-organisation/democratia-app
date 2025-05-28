using System.Net.Http.Headers;
using System.Text.Json.Nodes;

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
            client = new() {BaseAddress = new(BASE_URL)};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            statutsMessage = string.Empty;
            statuts = 0;
        }

        public abstract JsonArray GetInstance();


        public abstract bool SuprimmerInstance(int id);
        
    }
}
