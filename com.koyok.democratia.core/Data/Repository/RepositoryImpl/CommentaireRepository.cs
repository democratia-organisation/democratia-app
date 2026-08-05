using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.Repository.LocalRepository;
using com.koyok.democratia.Data.Repository.RemoteRepository;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using System.Text;
using System.Text.Json;

namespace com.koyok.democratia.Data.Repository.RepositoryImpl
{
    internal class CommentaireRepository(CommentaireRemoteRepository remote, CommentaireLocalRepository localBaseRepository) 
        : BaseRepository<CommentaireLocalSource>(remote, localBaseRepository), ICommentaireRepository
    {
        public Task<bool> CreateModelAsync(params object?[]? parameters)
        {
            return remote.CreateModelAsync(parameters);
        }

        public Task<bool> DeleteModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IModel>> GetModelAsync(params object?[] parameters)
        {
            var internaute = (Internaute)parameters![2]!;
            List<Commentaire> commentaires = [..(await remote.GetModelAsync(parameters)).Cast<Commentaire>()];
            commentaires.ForEach(commentaire => commentaire.himself = commentaire.idInternaute == internaute.idInternaute);
            return [..commentaires.Cast<IModel>()];
        }

        public  Task<bool> UpdateModelAsync(params object?[]? parameters)
        {
            throw new NotImplementedException();
        }
    }
}
