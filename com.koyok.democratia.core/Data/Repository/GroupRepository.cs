using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using Microsoft.Maui.Storage;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace com.koyok.democratia.Data.Repository
{
    internal class GroupRepository(HttpClient client, IEnumerable<ILocalSource> localSources, IEnumerable<IRemoteSource> remoteSources,
        IEnumerable<IRemoteToDomain> remotes, IEnumerable<ILocalToDomain> domains) 
        : BaseRepository(client, 
            localSources.OfType<GroupeLocalSource>().FirstOrDefault()!, 
            remoteSources.OfType<GroupeRemoteSource>().FirstOrDefault()!,
            remotes.OfType<GroupeRemoteToDomain>().FirstOrDefault()!,
            domains.OfType<GroupeLocalToDomain>().FirstOrDefault()!), IGroupeRepository
    {
        public async Task<string> CreateModelAsync(params object?[]? parameters)
        {
            var groupe = (Groupe)parameters![0]!;

            var stringContent = new StringContent(JsonSerializer.Serialize(groupe),new MediaTypeHeaderValue("application/json"));
            
            var requete = "groupes";
            
            HttpResponseMessage? response;
            try
            {
                response = await client!.PostAsync(requete,stringContent);

            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateJointureThemeEtGroupeAsync(Guid? idGroupe, int? idThematique, float? budgetThematique)
        {
            List<object> arguments = [idGroupe!,idThematique!,budgetThematique!];
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
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetJointureThemeEtGroupeAsync(Guid? idGroupe)
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
            return await response.Content.ReadAsStringAsync();
        }

        public async override Task<string> UploadImage(Guid? id, string filePath)
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
            return await response.Content.ReadAsStringAsync();
        }

        public async override Task<string?> GetImageAsync(string? url)
        {
            var requete = $"groupes/obtenirImageGroupe/{url}";
            string fileName = $"img_{Math.Abs(requete.GetHashCode())}.jpg";
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, fileName);
            if (File.Exists(localFilePath))
            {
                return localFilePath;
            }
            else
            {
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
                    await File.WriteAllBytesAsync(localFilePath, imageBytes);
                    return localFilePath;
                }
            }


        }

        

        public Task<string> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetModelAsync(params object?[] parameters)
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

            return await response.Content.ReadAsStringAsync();
            
        }

        public Task<string> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AjouterCreateur(int? id_internaute, Guid? id_groupe)
        {
            var adminId = 2;
            var notificationId = 1;
            List<object?> data = [id_groupe, id_internaute, adminId, notificationId];
            var stringContent = new StringContent(JsonSerializer.Serialize(data),Encoding.UTF8,new MediaTypeHeaderValue("application/json"));
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

            return await response.Content.ReadAsStringAsync();
        }
    }
}
