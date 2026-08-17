using com.koyok.democratia.Lib;
using com.koyok.democratia.Domain.Models;
using System.Collections;

namespace com.koyok.democratia.Domain.Repository
{
    /// <summary>
    /// Interface qui représente un fournisseur des services de l'API.
    /// </summary>
    public interface IRepository
    {
        public Task<List<IModel>> GetModelAsync(params object?[] parameters);

        public Task<bool> CreateModelAsync(params object?[]? parameters);
        public  Task<bool> UpdateModelAsync(params object?[]? parameters);
        public Task<bool> DeleteModelAsync(params object?[]? parameters);
        public Task<byte[]?> GetImageAsync(params object?[]? parameters);
        public Task<bool> UploadImage(Guid? id, string filePath);
    }

    public interface IGroupeRepository : IRepository
    {
        public Task<bool> AjouterCreateur(Guid? idInternaute, Guid? idGroupe);
        public Task<bool> CreateJointureThemeEtGroupeAsync(Guid? idGroupe, int? idThematique, float? budgetThematique);
        public Task<List<Thematique>> GetJointureThemeEtGroupeAsync(Guid? idGroupe);
        protected Task<string> GetRoleGroupe(string rowGroupe);
    }

    public interface IInternauteRepository : IRepository
    {
        public Task<bool> DoublonEmailAsync(string email);
        public Task<bool> SaveNotification(Groupe groupe, BitArray notificationChoices, Internaute internaute);
    }

    public interface IThematiqueRepository : IRepository
    {
    }

    public interface IFakeRepository : IRepository
    {
    }

    public interface IPropositionRepository : IRepository
    {
        public Task<List<Proposition>> GetAllPropositionsAsync(params object?[] parameters);
        public List<Proposition> TrierProposition(Critere critere);
    }

    public interface ICommentaireRepository : IRepository
    {
        
    }
}

