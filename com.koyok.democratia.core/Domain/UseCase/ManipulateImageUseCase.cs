using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Exception;
using Microsoft.Maui.Storage;

namespace com.koyok.democratia.Domain.UseCase
{
    public class ManipulateImageUseCase(IRepository repository)
    {
        private readonly IRepository repository = repository;

        public async Task<string> GetImageAsync(Groupe groupe, int bytesRiden, Internaute internaute)
        {
            string fileName = $"palette_{Math.Abs(internaute.nomInternaute!.GetHashCode())}.jpg";
            string palettePath = Path.Combine(FileSystem.CacheDirectory, fileName);
            if (!File.Exists(palettePath))
            {
                var response = await repository.GetImageAsync(internaute)!;
                if (response!.Length == 0) throw new ConnexionErrorException();
                await File.WriteAllBytesAsync(palettePath, response);
            }
            byte[] fullImages = await File.ReadAllBytesAsync(palettePath);
            byte[] finalImage = [..fullImages.Skip(bytesRiden).Take(groupe.imageSize!.Value)];
            string finalImagePath = Path.Combine(FileSystem.CacheDirectory, $"image_groupe_of_{groupe.idGroupe}_internaute_{internaute.idInternaute}");
            await File.WriteAllBytesAsync(finalImagePath, finalImage);
            return finalImagePath;
        }

        public async Task<string> UploadImage(Guid? id, string filePath)
        {
            return await repository.UploadImage(id, filePath);
        }
    }
}
