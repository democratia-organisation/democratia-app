using com.democratia.Services;
using System.Text.Json.Nodes;

namespace com.democratia.core.Services
{
    public class InternauteClient : IClient
    {
        protected static readonly string BASE_URL = "https://projets.iut-orsay.fr/saes3-mmarti32/API/rest.php";
        private string? statusMessage;
        private int status;
        private readonly HttpClient? client;

        public InternauteClient()
        {
            // Initialize the HttpClient if needed, or use dependency injection in a real application
            client = new() { BaseAddress = new(BASE_URL)};
            statusMessage = string.Empty;
            status = 0;
        }

        public JsonArray GetInstance()
        {
            
            throw new NotImplementedException("Not implemented");
        }

        public bool SuprimmerInstance(int id)
        {
            throw new NotImplementedException();
        }
    }
}
