using com.koyok.democratia.Data.Mapper.LocalToDomain;

namespace com.koyok.democratia.Data.Repository.LocalRepository
{

    public class LocalBaseRepository<T> where T : new()
    {
        protected ILocalToDomain localToDomain;
        protected DataBaseCreation<T> databaseConnexion;

        public bool succes {  get; private set; }

        
        protected LocalBaseRepository(DataBaseCreation<T> databaseConnexion, ILocalToDomain localToDomain)
        {             
            this.databaseConnexion = databaseConnexion;
            this.localToDomain = localToDomain;
        }
        
        // vouer à ne pas être implémenté ici mais dans les repositories qui en ont besoin
        public virtual Task<byte[]?> GetImageAsync(params object?[]? parameters) => throw new NotImplementedException();
        

        public virtual async Task<bool> UploadImage(Guid? id, string filePath) => true;
        

        
        
    }
}