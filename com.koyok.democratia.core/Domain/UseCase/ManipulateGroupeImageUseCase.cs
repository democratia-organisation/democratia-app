using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Exception;
using Microsoft.Maui.Storage;

namespace com.koyok.democratia.Domain.UseCase
{
    public class ManipulateGroupeImageUseCase(IGroupeRepository repository) : IManipulateImage
    {

        public async Task<string> GetImageAsync(params object[] args)
        {
            var groupe = (Groupe)args[0];
            var bytesRiden = (int)args[1];
            var internaute = (Internaute)args[2];

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

        public async Task<bool> UploadImage(params object[] args)
        {
            Guid? id = (Guid?)args[0];
            string filePath = (string)args[1]; 
            if (!File.Exists(filePath)) { throw new FileNotFoundException($"Le fichier {filePath} n'existe pas."); }
            return await repository.UploadImage(id, filePath);
        }
    }
}
