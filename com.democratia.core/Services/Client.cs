using System.Net.Http.Headers;
using Xunit.Abstractions;

namespace com.democratia.Services
{

    public class Client
    {
        protected static string? BASE_URL;
        protected string? statutsMessage;
        protected int? statuts;
        protected HttpClient? client;
        
        protected Client()
        {
            // TODO : peut-être mettre un timeout si le temps d'attente est vraiment invivable côté client
            BASE_URL = "http://localhost:80/rest.php"; // TODO : trouver un autre hébergeur pour l'api et utiliser les variables d'environnement pour y mettre le lien
            client = new() { BaseAddress = new(BASE_URL) };
            statutsMessage = string.Empty;
            statuts = 0;
        }

        protected async Task<string> GetMethode()
        {
            DebutRequete();
            HttpResponseMessage response = await client!.GetAsync("?request=getMethode");
            return await FinRequete(response);
        }

        /// <summary>
        /// fonction qui permet de changer le port de l'API.
        /// Utilisée pour les tests unitaires afin de simuler une erreur de connexion internet.
        /// </summary>
        /// <param name="port">le numéro de port</param>
        public void SetPort(int port)
        {
            client!.BaseAddress = new Uri($""); // TODO : héberger tout seul et surtout caché les liens
        }


        protected void MettreAJourStatuts(HttpResponseMessage? response)
        {
            statuts = (int)response!.StatusCode;
            statutsMessage = response.ReasonPhrase;
        }

        public void Deserialize(IXunitSerializationInfo info)
        {
            BASE_URL = info.GetValue<string>("BaseUrl");
            statutsMessage = info.GetValue<string>("StatutsMessage");
            statuts = info.GetValue<int?>("Statuts");

            client = new HttpClient { BaseAddress = new Uri(BASE_URL) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("BaseUrl", BASE_URL);
            info.AddValue("StatutsMessage", statutsMessage);
            info.AddValue("Statuts", statuts);
        }

        protected async Task<string> FinRequete(HttpResponseMessage response)
        {
            MettreAJourStatuts(response);
            if (!response.IsSuccessStatusCode) throw new Exception("Requete râté");

            else
                return await response.Content.ReadAsStringAsync();
        }

        protected void DebutRequete()
        {
            client!.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
