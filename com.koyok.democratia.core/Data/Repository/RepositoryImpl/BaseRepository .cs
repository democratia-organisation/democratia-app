using com.koyok.democratia.Data.Repository.LocalRepository;
using com.koyok.democratia.Data.Repository.RemoteRepository;

namespace com.koyok.democratia.Data.Repository.RepositoryImpl
{

    public class BaseRepository<T> where T : new()
    {
        protected readonly RemoteBaseRepository remoteBaseRepository;
        protected readonly LocalBaseRepository<T> localBaseRepository;

        public bool succes {  get; private set; }
        
        protected BaseRepository(RemoteBaseRepository remoteBaseRepository, LocalBaseRepository<T> localBaseRepository)
        {
            this.remoteBaseRepository = remoteBaseRepository;
            this.localBaseRepository = localBaseRepository;
        }

        /// <summary>
        /// fonction qui permet de changer le port de l'API.
        /// Utilisée pour les tests unitaires afin de simuler une erreur de connexion internet.
        /// </summary>
        /// <param name="port">le numéro de port</param>
        public void SetPort(int port) => throw new NotImplementedException(); 
        

        protected void MettreAJourStatuts(HttpResponseMessage? response)
        {
            throw new NotImplementedException();
        }

        // vouer à ne pas être implémenté ici mais dans les repositories qui en ont besoin
        public virtual Task<byte[]?> GetImageAsync(params object?[]? parameters) => throw new NotImplementedException();
        public virtual async Task<bool> UploadImage(Guid? id, string filePath) => true;
        
    }
}