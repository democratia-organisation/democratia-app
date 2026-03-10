using com.democratia.Utils;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit.Abstractions;

namespace com.democratia.Services
{

    public class Client
    {
        protected static string? BASE_URL;
        protected string? statutsMessage;
        protected int? statuts;
        protected HttpClient? client;
        private static readonly string API_KEY = "API_KEY";

        public bool succes {  get; private set; }
        
        protected Client()
        {
            statutsMessage = string.Empty;
            statuts = 0;
            client = new()
            {
                BaseAddress = this.AffecterURL(),
#if DEBUG
                Timeout = TimeSpan.FromSeconds(60*7)
#elif !DEBUG
                Timeout = TimeSpan.FromSeconds(10)
#endif
            };
        }

        protected async Task<string> GetMethode()
        {
            await DebutRequete();
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

        public async void Deserialize(IXunitSerializationInfo info)
        {
            BASE_URL = info.GetValue<string>("BaseUrl");
            statutsMessage = info.GetValue<string>("StatutsMessage");
            statuts = info.GetValue<int?>("Statuts");
            client = new HttpClient { BaseAddress = new Uri(BASE_URL) };
            await DebutRequete();
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
                if(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Conflict)
                {
                    string initialRequest = response!.RequestMessage!.RequestUri!.OriginalString.Split("php")[1];
                    HttpMethod methode = response!.RequestMessage.Method;
                    return await GenerateJWTKey(initialRequest, methode);
                }
                throw new ConnexionErrorException();
            } 
            else
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
        }

        private async Task<string> GenerateJWTKey(string content, HttpMethod method)
        {
            await DebutRequete();
            try
            {
                string email = await SecureStorage.Default.GetAsync("id_internaute") ?? string.Empty;
                HttpResponseMessage response = await client!.GetAsync($"""?request=login&parameters=["{email}"]""");
                MettreAJourStatuts(response);
                if (!response.IsSuccessStatusCode)
                {
                    string contente = await response.Content.ReadAsStringAsync();
                    throw new ConnexionErrorException();
                }

                else
                {
                    var réponse = await response.Content.ReadFromJsonAsync<Dictionary<string,object>>();
                    string key = JsonSerializer.Deserialize<Dictionary<string, string>>(réponse!["data"].ToString()!)![API_KEY];
                    string refresh = JsonSerializer.Deserialize<Dictionary<string, string>>(réponse!["data"].ToString()!)!["REFRESH"];
                    await SecureStorage.Default.SetAsync(API_KEY, key);
                    await SecureStorage.Default.SetAsync("REFRESH", refresh);
                    await DebutRequete();
                    response = method.Method switch
                    {
                        "GET" => await client!.GetAsync(content),
                        "POST" => await client!.PostAsync(content, null),
                        "PUT" => await client!.PutAsync(content, null),
                        "PATCH" => await client!.PatchAsync(content, null),
                        "DELETE" => await client!.DeleteAsync(content),
                        _ => throw new InvalidOperationException("Méthode HTTP non supportée")
                    };
                    return await FinRequete(response);
                }
            }
            catch (Exception) { throw; }
        }

        protected async Task DebutRequete()
        {
            client!.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (await SecureStorage.Default.GetAsync(API_KEY) is string refresh)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", refresh);
                return;
            }
            if (await SecureStorage.Default.GetAsync(API_KEY) is string key)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", key);
        }

        public async Task<ImageSource?> GetImageAsync(string? url)
        {
            var requete = $"""?request=obtenirImage&parameters=["{url}"]""";
            await DebutRequete();
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
            await DebutRequete();
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