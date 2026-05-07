using Microsoft.Maui.Controls;
using Xunit.Abstractions;

namespace com.koyok.democratia.Domain.Repository
{
    /// <summary>
    /// Interface qui représente un fournisseur des services de l'API.
    /// </summary>
    public interface IRepository : IXunitSerializable
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
        public List<T> RecuprerInformationConnexion<T>(string stringJson);
    }

    public interface IGroupeRepository : IRepository
    {
        public Task<string> CreateJointureThemeEtGroupeAsync(Guid? idGroupe, int? idThematique, float? budgetThematique);
        public Task<string> GetJointureThemeEtGroupeAsync(Guid? idGroupe);
    }

    public interface IInternauteRepository : IRepository
    {
        public Task<string> DoublonEmailAsync(string email);
    }

    public interface IThematiqueRepository : IRepository
    {
    }

    public interface IFakeRepository : IRepository
    {
    }

    public interface IPropositionRepository : IRepository
    {
        public Task<string> GetAllPropositionsAsync(params object?[] parameters);
    }
}

