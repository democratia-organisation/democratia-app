using com.democratia.Utils;
using Microsoft.Maui.Storage;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace com.democratia.Services
{
    public class DebutRequete : DelegatingHandler
    {
        private static readonly string API_KEY = "API_KEY";
        private static readonly string REFRESH = "REFRESH";
        private static readonly string IS_FRESH = "is_refresh_key_fresh";
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            bool isParsed = bool.TryParse(await SecureStorage.Default.GetAsync(IS_FRESH), out bool isFresh);
            if (!isParsed)
                return await base.SendAsync(request, cancellationToken);
            
            if (isFresh && await SecureStorage.Default.GetAsync(REFRESH) is string refresh)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refresh);
            
            else if(await SecureStorage.Default.GetAsync(API_KEY) is string apiKey)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            
            return await base.SendAsync(request, cancellationToken);
        }
    }


    public class AuthentificationHandler(IHttpClientFactory factory) : DelegatingHandler
    {
        private static readonly string API_KEY = "API_KEY";
        private static readonly string REFRESH = "REFRESH";
        private static readonly string IS_FRESH = "is_refresh_key_fresh";

        private readonly IHttpClientFactory _factory = factory;

        private async Task<HttpResponseMessage> RefreshKeys(CancellationToken ct)
        {
            string email = await SecureStorage.Default.GetAsync("id_internaute") ?? string.Empty;
            var brutClient = _factory.CreateClient("ClientBrut");
            var resp = await brutClient.GetAsync($"""?request=login&parameters=["{email}"]""", ct);
            return resp;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                HttpRequestMessage clone = await request.CloneRequest();
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    var authorisation = response.RequestMessage!.Headers.Authorization!;
                    if (authorisation.Equals(await SecureStorage.Default.GetAsync(REFRESH)))
                        await SecureStorage.Default.SetAsync(IS_FRESH, $"{false}");
                    else
                        response.StatusCode = HttpStatusCode.Unauthorized;
                    return await base.SendAsync(clone, cancellationToken);

                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {

                    HttpResponseMessage responseToken = await RefreshKeys(cancellationToken);
                    if (!responseToken.IsSuccessStatusCode)
                    {
#if DEBUG
                        string contente = await response.Content.ReadAsStringAsync();
#endif
                        throw new ConnexionErrorException();
                    }

                    else
                    {
                        var réponse = await responseToken.Content.ReadFromJsonAsync<Dictionary<string, object>>();
                        string key = JsonSerializer.Deserialize<Dictionary<string, string>>(réponse!["data"].ToString()!)![API_KEY];
                        string refresh = JsonSerializer.Deserialize<Dictionary<string, string>>(réponse!["data"].ToString()!)![REFRESH];
                        await SecureStorage.Default.SetAsync(API_KEY, key);
                        await SecureStorage.Default.SetAsync(REFRESH, refresh);
                        await SecureStorage.Default.SetAsync(IS_FRESH, $"{true}");

                        return await base.SendAsync(clone, cancellationToken);
                    }
                }
                else if ((int)response.StatusCode == 412 && request.RequestUri!.Query == $"?request=relogin&parameters=[%22{await SecureStorage.Default.GetAsync("id_internaute")}%22]")
                    return response;
                else throw new ConnexionErrorException();
            }
            else
                return response;
        }
    }
    public class FinRequete : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
#if DEBUG
                string content = await response.Content.ReadAsStringAsync();
#endif
                if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Conflict)
                    return response;
                else if (response.StatusCode == HttpStatusCode.PreconditionFailed && request.RequestUri!.Query == $"?request=relogin&parameters=[%22{await SecureStorage.Default.GetAsync("id_internaute")}%22]")
                {
                    SecureStorage.Default.RemoveAll();
                    return response;
                }

                else
                    throw new ConnexionErrorException();
            }
            else
                return response;
        }
    }
}
