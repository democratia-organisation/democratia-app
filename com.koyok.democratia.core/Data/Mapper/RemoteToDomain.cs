using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Domain.Models;
using System.Text.Json;

namespace com.koyok.democratia.Data.Mapper.RemoteToDomain
{
    public interface IRemoteToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel;
    }

    public class InternauteRemoteToDomain : IRemoteToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            var remoteSource =  JsonSerializer.Deserialize<InternauteRemoteSource>(source)!;
            var internaute = new Internaute(
                remoteSource.id_internaute,
                remoteSource.nom_internaute,
                remoteSource.prenom_internaute,
                remoteSource.adresse_postale,
                remoteSource.courriel,
                remoteSource.hashageMDP
            );
            return internaute as T;
        }
    }

    public class PropositionRemoteToDomain : IRemoteToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            throw new NotImplementedException();
        }
    }

    public class ThematiqueRemoteToDomain : IRemoteToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            throw new NotImplementedException();
        }
    }

    public class GroupeRemoteToDomain : IRemoteToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            throw new NotImplementedException();
        }
    }
}
