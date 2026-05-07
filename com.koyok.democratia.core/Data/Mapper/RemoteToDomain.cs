using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Domain.Models;

namespace com.koyok.democratia.Data.Mapper.RemoteToDomain
{
    public interface IRemoteToDomain
    {
        public static abstract IModel Mapping(IRemoteSource source);
    }

    public class InternauteRemoteToDomain : IRemoteToDomain
    {
        public static IModel Mapping(IRemoteSource source)
        {
            throw new NotImplementedException();
        }
    }

    public class PropositionRemoteToDomain : IRemoteToDomain
    {
        public static IModel Mapping(IRemoteSource source)
        {
            throw new NotImplementedException();
        }
    }

    public class ThematiqueRemoteToDomain : IRemoteToDomain
    {
        public static IModel Mapping(IRemoteSource source)
        {
            throw new NotImplementedException();
        }
    }

    public class GroupeRemoteToDomain : IRemoteToDomain
    {
        public static IModel Mapping(IRemoteSource source)
        {
            throw new NotImplementedException();
        }
    }
}
