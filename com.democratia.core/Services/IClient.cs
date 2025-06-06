using Xunit.Abstractions;

namespace com.democratia.Services
{
    /// <summary>
    /// Interface qui représente un fournisseur des services de l'API.
    /// </summary>
    public interface IClient : IXunitSerializable
    {
        Task<string> GetModelAsync(params object?[] parameters);
        public void SetPort(int port);

    }
}

