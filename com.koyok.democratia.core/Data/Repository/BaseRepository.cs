using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Extension;
using com.koyok.democratia.Domain.Models;
using System.Text.Json;
using Xunit.Abstractions;

namespace com.koyok.democratia.Data.Repository
{

    public class BaseRepository : IDisposable, IXunitSerializable
    {
        protected static string? BASE_URL;
        protected string? statutsMessage;
        protected int? statuts;
        protected HttpClient? client;
        public HttpClient Client => client!;
        protected ILocalSource localSource;
        protected IRemoteSource remoteSource;
        private IRemoteToDomain remoteToDomain;
        private ILocalToDomain localToDomain;

        public bool succes {  get; private set; }
        
        protected BaseRepository(HttpClient client, 
            ILocalSource localSource, IRemoteSource remoteSource, IRemoteToDomain remoteToDomain, ILocalToDomain localToDomain)
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
            this.remoteToDomain = remoteToDomain;
            this.localToDomain = localToDomain;
        }

        public virtual List<T> RecuprerInformationConnexion<T>(string stringJson) where T : class, IModel
        {            
            var finalJson = stringJson.Trim();
            try { 
                using var doc = JsonDocument.Parse(finalJson);
                var root = doc.RootElement;
                if (!root.TryGetProperty("data", out var data))
                    throw new ConnexionErrorException(root.TryGetProperty("message", out var message)? message.ToString() : throw new Exception());
                var resultList = new List<T>();

                if (data.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in data.EnumerateArray())
                    {
                        T domainModel = remoteToDomain.Mapping<T>(item.GetRawText())!;

                        if (domainModel is not null)
                            resultList.Add(domainModel);
                    }
                }

                return resultList;
            }
            catch (Exception) { throw new FetchDataException(); }
            

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
        public virtual Task<string?> GetImageAsync(string? url) => throw new NotImplementedException();
        

        public virtual async Task<string> UploadImage(Guid? id, string filePath) => "";
        

        public void Dispose()
        {
            client!.Dispose();
            GC.SuppressFinalize(this);
        }
        
    }
}