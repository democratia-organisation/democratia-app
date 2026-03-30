using Microsoft.Maui.Controls;
using Xunit.Abstractions;

namespace com.democratia.Services
{
    /// <summary>
    /// Interface qui représente un fournisseur des services de l'API.
    /// </summary>
    public interface IClient : IXunitSerializable
    {
        public Task<string> GetModelAsync(params object?[] parameters);

        /// <summary>
        /// fonction qui permet de changer le port de l'API.
        /// Utilisée pour les tests unitaires afin de simuler une erreur de connexion internet.
        /// </summary>
        /// <param name="port">le numéro de port</param>
        public void SetPort(int port);
        public Task<string> CreateModelAsync(params object?[]? parameters);
        public Task<string> UpdateModelAsync(params object?[]? parameters);
        public Task<string> DeleteModelAsync(params object?[]? parameters);
        public Task<ImageSource?> GetImageAsync(string? url);
        public Task<string> UploadImage(Guid? id, string filePath);
    }

    public interface IGroupeClient : IClient
    {
    }

    public interface IInternauteClient : IClient
    {
    }

    public interface IThematiqueClient : IClient
    {
    }

    public interface IFakeClient : IClient
    {
    }
}

