using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace com.koyok.democratia.Data.Repository.RemoteRepository
{
    internal class GroupeRemoteRepository(HttpClient client, IRemoteToDomain remote) : RemoteBaseRepository(client, remote), IGroupeRepository
    {
        public async Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            var groupe = (Groupe)parameters![0]!;
            var stringContent = new StringContent(JsonSerializer.Serialize(groupe), new MediaTypeHeaderValue("application/json"));
            var requete = "groupes";

            HttpResponseMessage? response;
            try
            {
                response = await client!.PostAsync(requete, stringContent);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await ExtraiteStatus(response);
        }

        public async Task<bool> CreateJointureThemeEtGroupeAsync(Guid? idGroupe, int? idThematique, float? budgetThematique)
        {
            List<object> arguments = [idGroupe!, idThematique!, budgetThematique!];
            var stringContent = new StringContent(JsonSerializer.Serialize(arguments), new MediaTypeHeaderValue("application/json"));
            var requete = $"groupes/theme";

            HttpResponseMessage? response;
            try
            {
                response = await client!.PostAsync(requete, stringContent);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await ExtraiteStatus(response);
        }

        public async Task<List<Thematique>> GetJointureThemeEtGroupeAsync(Guid? idGroupe)
        {
            var requete = $"groupes/{idGroupe}/thematiqueJoin";

            HttpResponseMessage? response;
            try
            {
                response = await client!.GetAsync(requete);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            string content = await response.Content.ReadAsStringAsync();
            return RecuprerInformationConnexion<Thematique>(content);
        }

        public async override Task<bool> UploadImage(Guid? id, string filePath)
        {
            var requete = $"groupes/publierImage/{id}";

            HttpResponseMessage? response;

            try
            {
                byte[] imageBytes = await File.ReadAllBytesAsync(filePath);
                using var multipartContent = new MultipartFormDataContent();
                var byteContent = new ByteArrayContent(imageBytes);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                multipartContent.Add(byteContent, "image", "upload.jpg");

                response = await client!.PostAsync(requete, multipartContent);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await ExtraiteStatus(response);
        }

        public async override Task<byte[]?> GetImageAsync(params object?[]? parameters)
        {
            var idInternaute = ((Internaute)parameters![0]!).idInternaute;
            var requete = $"groupes/obtenirImageGroupes/{idInternaute}";
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
            return await response.Content.ReadAsByteArrayAsync();
        }



        public Task<bool> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IModel>> GetModelAsync(params object?[] parameters)
        {
            var requete = $"groupes/{((Internaute)parameters[0]!).idInternaute}";

            HttpResponseMessage? response;
            try
            {
                response = await client!.GetAsync(requete);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            string content = await response.Content.ReadAsStringAsync();
            content = await GetRoleGroupe(content);
            return [.. RecuprerInformationConnexion<Groupe>(content)];
        }

        public Task<bool> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AjouterCreateur(Guid? id_internaute, Guid? id_groupe)
        {
            var notificationId = 1;
            List<object?> data = [id_groupe, id_internaute, (int)Role.Administrateur, notificationId];
            var stringContent = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
            var requete = $"groupes/internaute";

            HttpResponseMessage? response;
            try
            {
                response = await client!.PostAsync(requete, stringContent);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await ExtraiteStatus(response);
        }

        public async Task<string> GetRoleGroupe(string rowGroupe)
        {
            try
            {
                var jsonArray = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(rowGroupe)!;
                var content = jsonArray["data"].Deserialize<List<Dictionary<string, JsonElement>>>()!;
                foreach (var item in content)
                {
                    item["is_admin"] = JsonSerializer.SerializeToElement(JsonSerializer.Deserialize<int>(item["id_role"]!.ToString()!) == (int)Role.Administrateur);
                    item.Remove("id_role");
                }
                jsonArray["data"] = JsonSerializer.SerializeToElement(content);
                return JsonSerializer.Serialize(jsonArray); ;
            }
            catch (Exception) { throw new FetchDataException(); }
        }
    }
}
