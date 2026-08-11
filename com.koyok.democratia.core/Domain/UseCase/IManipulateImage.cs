namespace com.koyok.democratia.Domain.UseCase
{
    public interface IManipulateImage
    {
        public Task<string> GetImageAsync(params object[] args);
        public Task<bool> UploadImage(params object[] args);
    }
}
