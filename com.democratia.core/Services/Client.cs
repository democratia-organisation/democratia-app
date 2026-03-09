using com.democratia.Utils;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.IO.Pipelines;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace com.democratia.Services
{

    public class Client
    {
        protected static string? BASE_URL;
        protected string? statutsMessage;
        protected int? statuts;
        protected HttpClient? client;
        private static string API_KEY = "API_KEY";
        
        public bool succes {  get; private set; }
        
        protected Client()
        {
            statutsMessage = string.Empty;
            statuts = 0;
            client = new()
            {
                BaseAddress = this.AffecterURL(),
#if DEBUG
                Timeout = TimeSpan.FromSeconds(60)
#elif !DEBUG
                Timeout = TimeSpan.FromSeconds(10)
#endif
            };
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
        public void SetPort(int port) => client!.BaseAddress = new Uri($"http://localhost:81/rest.php"); 
        

        protected void MettreAJourStatuts(HttpResponseMessage? response)
        {
            statuts = (int)response!.StatusCode;
            statutsMessage = response.ReasonPhrase;
            succes = response.IsSuccessStatusCode;
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
            if (!response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized) 
                    return await GenerateJWTKey(response.Content);
                
                throw new ConnexionErrorException();
            } 

            else
                return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GenerateJWTKey(HttpContent content)
        {
            DebutRequete();
            try
            {
                HttpResponseMessage response = await client!.GetAsync("?request=login&parameters=[]");
                MettreAJourStatuts(response);
                if (!response.IsSuccessStatusCode)
                {
                    string contente = await response.Content.ReadAsStringAsync();
                    throw new ConnexionErrorException();
                }

                else
                {
                    var réponse = await response.Content.ReadFromJsonAsync<Dictionary<string,object>>();
                    string key = réponse!["data"].ToString()!;
                    await SecureStorage.Default.SetAsync(API_KEY, key);
                    DebutRequete();
                    response = await client.GetAsync(content.ToString());
                    return await FinRequete(response);


                }

            }
            catch (Exception) { throw; }
            

        }

        protected async void DebutRequete()
        {
            client!.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string key = (await SecureStorage.Default.GetAsync(API_KEY))!;
            client.DefaultRequestHeaders.Add("Beares", key);
        }

        public async Task<ImageSource?> GetImageAsync(string? url)
        {
            var requete = $"""?request=obtenirImage&parameters=["{url}"]""";
            DebutRequete();
            HttpResponseMessage? response;
            try
            {
                response = await client!.GetAsync(requete);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            MettreAJourStatuts(response);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new ConnexionErrorException();
            }

            else
            {
                byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                if (imageBytes.Length == 0) return null;
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }

        }

        public async Task<string> UploadImage(Guid? id, string filePath)
        {
            var requete = $"""?request=publierImage&parameters=["{id}"]""";
            DebutRequete();
            HttpResponseMessage? response;
            
            try
            {
                byte[] imageBytes = await File.ReadAllBytesAsync(filePath);
                using var multipartContent = new MultipartFormDataContent();
                var byteContent = new ByteArrayContent(imageBytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                multipartContent.Add(byteContent, "image", "upload.jpg");

                response = await client!.PostAsync(requete,multipartContent);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await FinRequete(response);
        }
    }
}
