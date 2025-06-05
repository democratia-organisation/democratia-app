using com.democratia.Models;

namespace com.democratia.Services
{
    /// <summary>
    /// Interface qui représente un fournisseur des services de l'API.
    /// </summary>
    public interface IClient
    {
        Task<string> GetModelAsync(params object?[] parameters);

    }
}

