using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using System.Net.Http.Headers;

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
            Groupe groupe = (Groupe)parameters![0]!;
            
            var requete = $"""
                ?request=INSERT INTO groupe (id_groupe,nom_groupe,couleur_groupe,budget,nbj_dft_vote,nbj_dft_discuss) VALUES (UUID_TO_BIN(?,0),?,?,?,?,?)&parameters=["{groupe.idGroupe}","{groupe.nomGroupe}", "{Uri.EscapeDataString(groupe.couleurGroupe!)}", "{groupe.budget}", "{groupe.nombreDeJourVote}", "{groupe.nombreDeJourDiscuss}"]
                """;
            
            HttpResponseMessage? response;
            try
            {
                response = await client!.PostAsync(requete,null);

            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateJointureThemeEtGroupeAsync(Guid? idGroupe, int? idThematique, float? budgetThematique)
        {
            var requete = $"""
                ?request=INSERT INTO theme_groupe (id_groupe, id_thematique, budget_thematique)
                VALUES (UUID_TO_BIN(?,0),?,?);
                &parameters=["{idGroupe}", "{idThematique}", "{budgetThematique}"]
                """;
            
            HttpResponseMessage? response;
            try
            {
                response = await client!.PostAsync(requete, null);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetJointureThemeEtGroupeAsync(Guid? idGroupe)
        {
            var requete = $"""
                ?request=SELECT budget_thematique,
                    BIN_TO_UUID(tg.id_groupe) AS id_groupe,
                    tg.id_thematique,
                    nom_thematique,
                    g.budget
                FROM theme_groupe tg
                    INNER JOIN thematique t ON tg.id_thematique = t.id_thematique
                    INNER JOIN groupe g ON g.id_groupe = tg.id_groupe  WHERE tg.id_groupe=UUID_TO_BIN(?,1)
                &parameters=["{idGroupe}"]
                """;

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
            var requete = $"""?request=publierImage&parameters=["{id}"]""";

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

        public async override Task<MemoryStream?> GetImageAsync(string? url)
        {
            var requete = $"""?request=obtenirImage&parameters=["{url}"]""";

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
                return new MemoryStream(imageBytes);
            }
        }

        

        public Task<string> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetModelAsync(params object?[] parameters)
        {
            var requete = $"""
                ?request=SELECT BIN_TO_UUID(g.id_groupe, 1) as id, nom_groupe, couleur_groupe, g.image, budget, nb_signalement, nbj_dft_discuss, nbj_dft_vote  FROM groupe g  INNER JOIN infos_membre ifo ON g.id_groupe = ifo.id_groupe WHERE ifo.id_internaute=?
                &parameters=["{((Internaute)parameters[0]!).idInternaute}"]
                """;
            
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
            var requete = $"""
                ?request=INSERT INTO infos_membre (
                    id_groupe,
                    id_internaute,
                    id_role,
                    id_notification
                  )
                VALUES (
                    UUID_TO_BIN(?,0),
                    ?,
                    ?,
                    ?
                  )
                &parameters=["{id_groupe}", "{id_internaute}", "{adminId}", "{notificationId}"]
                """;
            
            HttpResponseMessage? response;
            try
            {
                response = await client!.PostAsync(requete, null);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Erreur de connexion inattendu", ex);
            }

            return await response.Content.ReadAsStringAsync();
        }

        public Task<string> GetGroupesAsync(Internaute internaute)
        {
            throw new NotImplementedException();
        }
    }
}
