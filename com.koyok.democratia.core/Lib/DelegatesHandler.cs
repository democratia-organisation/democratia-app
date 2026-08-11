using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Extension;
using Microsoft.Maui.Storage;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace com.koyok.democratia.Lib
{

    public class AuthentificationHandler(IHttpClientFactory factory) : DelegatingHandler
    {
        private readonly IHttpClientFactory _factory = factory;

        private async Task<HttpResponseMessage> RefreshKeys(CancellationToken ct)
        {
            string email = await SecureStorage.Default.GetAsync(SecureStorageKeys.IdInternaute.ToString()) ?? string.Empty;
            var brutClient = _factory.CreateClient("ClientBrut");
#if DEBUG
            brutClient.Timeout = TimeSpan.FromSeconds(60 * 5);
#elif !DEBUG
            brutClient.Timeout = TimeSpan.FromSeconds(10);
#endif
            var resp = await brutClient.PostAsync("users/refresh", new StringContent(JsonSerializer.Serialize(new List<string>([email])), Encoding.UTF8, new MediaTypeHeaderValue("application/json")), ct);
            return resp;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                HttpRequestMessage clone = await request.CloneRequest();
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                    throw new TooManyRequestException((int)response.Headers.RetryAfter!.Delta!.Value.TotalSeconds);
                else if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.PreconditionFailed)
                {
                    string reponse = await response.Content.ReadAsStringAsync(cancellationToken);
                    string message = JsonSerializer.Deserialize<Dictionary<string, object>>(reponse)!["message"].ToString()!;
                    string authorisation = response.RequestMessage!.Headers.Authorization!.Parameter!;
                    string refreshKey = (await SecureStorage.Default.GetAsync(SecureStorageKeys.REFRESH.ToString()))!;
                    if (message == "Token expiré" && authorisation == refreshKey)
                    {
                        await SecureStorage.Default.SetAsync(SecureStorageKeys.is_refresh_key_fresh.ToString(), $"{false}");
                        return await base.SendAsync(clone, cancellationToken);
                    }
                    
                    else if (message == "Entête incorrect" || message == "Utilisateur incorérent" || message == "La clé n'est pas la bonne")
                    {
                        HttpResponseMessage responseToken = await RefreshKeys(cancellationToken);
                        if (!responseToken.IsSuccessStatusCode)
                        {
#if DEBUG
                            string contente = await response.Content.ReadAsStringAsync(cancellationToken);
#endif
                            throw new ConnexionErrorException();
                        }

                        else
                        {
                            var réponse = await responseToken.Content.ReadFromJsonAsync<Dictionary<string, object>>(cancellationToken);
                            string key = JsonSerializer.Deserialize<Dictionary<string, string>>(réponse!["data"].ToString()!)![SecureStorageKeys.API_KEY.ToString()];
                            string refresh = JsonSerializer.Deserialize<Dictionary<string, string>>(réponse!["data"].ToString()!)![SecureStorageKeys.REFRESH.ToString()];
                            Task taskApi = SecureStorage.Default.SetAsync(SecureStorageKeys.API_KEY.ToString(), key);
                            Task taskRefresh = SecureStorage.Default.SetAsync(SecureStorageKeys.REFRESH.ToString(), refresh);
                            Task taskIsFresh = SecureStorage.Default.SetAsync(SecureStorageKeys.is_refresh_key_fresh.ToString(), $"{true}");
                            await Task.WhenAll(taskApi, taskRefresh, taskIsFresh);

                            return await base.SendAsync(clone, cancellationToken);
                        }
                    }
                    else
                    {
                        throw new ConnexionErrorException();
                    }
                }
                else throw new ConnexionErrorException();
            }
            else
                return response;
        }
    }
    public class DebutRequete : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            bool isParsed = bool.TryParse(await SecureStorage.Default.GetAsync(SecureStorageKeys.is_refresh_key_fresh.ToString()), out bool isFresh);
            if (!isParsed)
                return await base.SendAsync(request, cancellationToken);

            if (isFresh && await SecureStorage.Default.GetAsync(SecureStorageKeys.REFRESH.ToString()) is string refresh)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", refresh);

            else if (await SecureStorage.Default.GetAsync(SecureStorageKeys.API_KEY.ToString()) is string apiKey)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            return await base.SendAsync(request, cancellationToken);
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
                string content = await response.Content.ReadAsStringAsync(cancellationToken);
#endif
                if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.TooManyRequests || response.StatusCode == HttpStatusCode.PreconditionFailed)
                    return response;
                else
                    throw new ConnexionErrorException();
            }
            else return response;
        }
    }
}
