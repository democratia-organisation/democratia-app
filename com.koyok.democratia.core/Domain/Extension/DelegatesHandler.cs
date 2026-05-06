using com.koyok.democratia.Domain.Utils;
using Microsoft.Maui.Storage;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace com.koyok.democratia.Domain.Extension.DelegatesHandler
{
    public enum SecureStorageKeys
    {
        API_KEY,
        REFRESH,
        is_refresh_key_fresh
    }
    public class AuthentificationHandler(IHttpClientFactory factory) : DelegatingHandler
    {
        private readonly IHttpClientFactory _factory = factory;
        private int _maxRetryAttempts = 0;
        private readonly static int MAX_RETRY_ATTEMPTS = 3;

        private async Task<HttpResponseMessage> RefreshKeys(CancellationToken ct)
        {
            string email = await SecureStorage.Default.GetAsync("id_internaute") ?? string.Empty;
            var brutClient = _factory.CreateClient("ClientBrut");
#if DEBUG
            brutClient.Timeout = TimeSpan.FromSeconds(60*5);
#elif !DEBUG
            brutClient.Timeout = TimeSpan.FromSeconds(10);
#endif
            var resp = await brutClient.GetAsync($"""?request=login&parameters=["{email}"]""", ct);
            return resp;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                HttpRequestMessage clone = await request.CloneRequest();
                if (response.StatusCode == HttpStatusCode.TooManyRequests )
                {
                    if (_maxRetryAttempts < MAX_RETRY_ATTEMPTS)
                    {
                        Thread.Sleep((int)response.Headers.RetryAfter!.Delta!.Value.TotalMilliseconds);
                        _maxRetryAttempts++;
                        return await base.SendAsync(clone, cancellationToken);
                    }
                    else
                    {
                        throw new ConnexionErrorException();
                    }

                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    string reponse = await response.Content.ReadAsStringAsync(cancellationToken);
                    string message = JsonSerializer.Deserialize<Dictionary<string, object>>(reponse)!["message"].ToString()!;
                    if (message == "Token expiré")
                    {
                        string authorisation = response.RequestMessage!.Headers.Authorization!.Parameter!;
                        if (authorisation.Equals(await SecureStorage.Default.GetAsync(SecureStorageKeys.REFRESH.ToString())))
                            await SecureStorage.Default.SetAsync(SecureStorageKeys.is_refresh_key_fresh.ToString(), $"{false}");
                        else
                            response.StatusCode = HttpStatusCode.Unauthorized;
                        return await base.SendAsync(clone,cancellationToken);
                    }

                    else if (message == "Entête incorrect" || message == "Utilisateur incorérent")
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
                            await SecureStorage.Default.SetAsync(SecureStorageKeys.API_KEY.ToString(), key);
                            await SecureStorage.Default.SetAsync(SecureStorageKeys.REFRESH.ToString(), refresh);
                            await SecureStorage.Default.SetAsync(SecureStorageKeys.is_refresh_key_fresh.ToString(), $"{true}");

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
            
            else if(await SecureStorage.Default.GetAsync(SecureStorageKeys.API_KEY.ToString()) is string apiKey)
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
                if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.TooManyRequests)
                    return response;
                else
                    throw new ConnexionErrorException();
            }
            else return response;
        }
    }
}
