using com.koyok.democratia.Data.Repository;

namespace com.koyok.democratia.Domain.UseCase
{
    public class ManipulateImageUseCase(BaseRepository repository)
    {
        private readonly BaseRepository repository = repository;

        public async Task<MemoryStream> GetImageAsync(string url)
        {

            return (await repository.GetImageAsync(url))!;
        }

        public async Task<string> UploadImage(Guid? id, string filePath)
        {
            return await repository.UploadImage(id, filePath);
        }
    }
}
