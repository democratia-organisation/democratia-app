using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using System.Text;
using System.Text.Json;

namespace com.koyok.democratia.Data.Repository.RemoteRepository
{
    internal class CommentaireRemoteRepository(HttpClient client, IRemoteToDomain domain) : RemoteBaseRepository(client, domain), ICommentaireRepository
    {
        public async Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            Commentaire commentaire = (Commentaire)parameters![0]!;
            var content = new StringContent(JsonSerializer.Serialize(commentaire), Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                response = await client!.PostAsync("/commentaires/", content);
                return await ExtraiteStatus(response);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating commentaire: {ex.Message}");
            }
        }

        public Task<bool> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IModel>> GetModelAsync(params object?[] parameters)
        {
            var groupe = ((Groupe)parameters![0]!).idGroupe;
            var propostion = ((Proposition)parameters[1]!).idProposition;
            var requete = $"/commentaires/{groupe}/{propostion}";
            HttpResponseMessage response;
            try
            {
                response = await client!.GetAsync(requete);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la création de la requête HTTP : {ex.Message}");
            }
            string content = await response.Content.ReadAsStringAsync();
            return [.. RecuprerInformationConnexion<Commentaire>(content)];
        }

        public  Task<bool> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }
    }
}
