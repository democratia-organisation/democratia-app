using com.koyok.democratia.Domain.Repository;
using Microsoft.Maui.Storage;

namespace com.koyok.democratia.Domain.UseCase
{
    public class ManipulateImageUseCase(IGroupeRepository repository)
    {
        private readonly IGroupeRepository repository = repository;

        public async Task<string?> GetImageAsync(string url)
        {
            var response = await repository.GetImageAsync(url)!;
            string fileName = $"img_{Math.Abs(url.GetHashCode())}.jpg";
            string localFilePath = Path.Combine(FileSystem.CacheDirectory, fileName);
            if (File.Exists(localFilePath))
            {
                return localFilePath;
            }
            else
            {
                if (response!.Length == 0) return null;
                await File.WriteAllBytesAsync(localFilePath, response);
                return localFilePath;
            }
        }

        public async Task<string> UploadImage(Guid? id, string filePath)
        {
            return await repository.UploadImage(id, filePath);
        }
    }
}
