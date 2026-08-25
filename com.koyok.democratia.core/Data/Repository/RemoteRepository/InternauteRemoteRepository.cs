using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Exception;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Collections;

namespace com.koyok.democratia.Data.Repository.RemoteRepository
{
    public class InternauteRemoteRepository(HttpClient client, IRemoteToDomain remote) : RemoteBaseRepository(client, remote), IInternauteRepository
    {
        public async Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            HttpResponseMessage? response;

            try
            {
                var internaute = (Internaute)parameters![0]!;
                var request = "users";
                var stringContent = new StringContent(JsonSerializer.Serialize(internaute));
                response = await client!.PostAsync(request, stringContent);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await ExtraiteStatus(response);
        }

        public async Task<List<IModel>> GetModelAsync(params object?[] parameters)
        {
            HttpResponseMessage? response;
            try
            {
                var requete = $"""users/login""";
                string jsonContent = JsonSerializer.Serialize(parameters);
                var contenu = new StringContent(jsonContent, Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
                response = await client!.PostAsync(requete, contenu);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }

            string content = await response.Content.ReadAsStringAsync();
            return [.. RecuprerInformationConnexion<Internaute>(content)];
        }

        public async Task<bool> DoublonEmailAsync(string email)
        {


            HttpResponseMessage? response;
            try
            {
                response = await client!.GetAsync($"users/{email}/doublon");
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }

            string content = await response.Content.ReadAsStringAsync();
            var sucess = (bool)JsonSerializer.Deserialize<Dictionary<string, object>>(content)!["sucess"];
            if (!sucess) throw new ConnexionErrorException();
            var tableau = JsonSerializer.Deserialize<Dictionary<string, object>>(content);
            var reponse = tableau!["data"] as List<Dictionary<string, object>>;
            return int.Parse(reponse![0]["COUNT(courriel)"].ToString()!) == 0;
        }

        public async Task<bool> UpdateModelAsync(params object?[]? parameters)
        {

            HttpResponseMessage? response;
            var internaute = (Internaute)parameters![0]!;
            try
            {
                var contenu = new StringContent(JsonSerializer.Serialize(internaute));
                response = await client!.PatchAsync("users", contenu);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await ExtraiteStatus(response);
        }

        public async Task<bool> DeleteModelAsync(params object?[]? parameters)
        {

            HttpResponseMessage? response;
            try
            {
                response = await client?.DeleteAsync($"users/{((Internaute)parameters![0]!).idInternaute}")!;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await ExtraiteStatus(response);
        }

        public async Task<bool> SaveNotification(Groupe groupe, BitArray notificationChoices, Internaute internaute)
        {
            if (notificationChoices.Length > 16)
                throw new ArgumentException("Le BitArray ne doit pas dépasser 16 bits.");

            byte[] bytes = new byte[2];
            notificationChoices.CopyTo(bytes, 0);
            List<ushort> notificationsConverties = [BitConverter.ToUInt16(bytes, 0)];
            HttpResponseMessage? response;
            try
            {
                
                response = await client!.PatchAsync($"notifications/choixUtilisateur/{groupe.idGroupe}/{internaute.idInternaute}", new StringContent(JsonSerializer.Serialize(notificationsConverties)));
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await ExtraiteStatus(response);
        }
    }
}
