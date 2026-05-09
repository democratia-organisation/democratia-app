using System.Net.Http.Headers;
using Xunit.Abstractions;
using com.koyok.democratia.Domain.Extension;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using System.Text.Json;

namespace com.koyok.democratia.Data.Repository
{

    public class BaseRepository : IDisposable, IXunitSerializable
    {
        protected static string? BASE_URL;
        protected string? statutsMessage;
        protected int? statuts;
        protected HttpClient? client;
        protected ILocalSource localSource;
        protected IRemoteSource remoteSource;

        public bool succes {  get; private set; }
        
        protected BaseRepository(HttpClient client, ILocalSource localSource, IRemoteSource remoteSource)
        {
            statutsMessage = string.Empty;
            statuts = 0;
            this.client = client;
            this.client.BaseAddress = this.AffecterURL();
#if DEBUG
            this.client.Timeout = TimeSpan.FromSeconds(60 * 7);
#elif !DEBUG
            this.client.Timeout = TimeSpan.FromSeconds(10);
#endif
            this.localSource = localSource;
            this.remoteSource = remoteSource;
        }

        public virtual List<T> RecuprerInformationConnexion<T>(string stringJson)
        {
            Dictionary<string, object> dictionnary;
            var finalJson = stringJson.Trim();
            try { dictionnary = JsonSerializer.Deserialize<Dictionary<string, object>>(finalJson)!; }
            catch (Exception) { throw new FetchDataException(); }
            var rawElement = dictionnary.TryGetValue("data", out var data) ? data.ToString() : throw new FetchDataException();
            return JsonSerializer.Deserialize<List<T>>(rawElement!)!;

        }

        /// <summary>
        /// fonction qui permet de changer le port de l'API.
        /// Utilisée pour les tests unitaires afin de simuler une erreur de connexion internet.
        /// </summary>
        /// <param name="port">le numéro de port</param>
        public void SetPort(int port) => client!.BaseAddress = new Uri($"http://localhost:81/rest.php"); 
        

        protected void MettreAJourStatuts(HttpResponseMessage? response)
        {
            statuts = (int)response!.StatusCode;
            statutsMessage = response.ReasonPhrase;
            succes = response.IsSuccessStatusCode;
        }

        public async void Deserialize(IXunitSerializationInfo info)
        {
            BASE_URL = info.GetValue<string>("BaseUrl");
            statutsMessage = info.GetValue<string>("StatutsMessage");
            statuts = info.GetValue<int?>("Statuts");
            client = new HttpClient { BaseAddress = new Uri(BASE_URL) };
            
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("BaseUrl", BASE_URL);
            info.AddValue("StatutsMessage", statutsMessage);
            info.AddValue("Statuts", statuts);
        }

        // vouer à ne pas être implémenté ici mais dans les repositories qui en ont besoin
        public virtual async Task<MemoryStream?> GetImageAsync(string? url) => new ();
        

        public virtual async Task<string> UploadImage(Guid? id, string filePath) => "";
        

        public void Dispose()
        {
            client!.Dispose();
            GC.SuppressFinalize(this);
        }
        
    }
}