using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Domain.Models;

namespace com.koyok.democratia.Data.Mapper.LocalToDomain
{
    public interface ILocalToDomain
    {
        public static abstract IModel Mapping(ILocalSource source);
    }

    public class InternauteLocalToDomain : ILocalToDomain
    {
        public static IModel Mapping(ILocalSource source)
        {
            throw new NotImplementedException();
        }
    }

    public class PropositionLocalToDomain : ILocalToDomain
    {
        public static IModel Mapping(ILocalSource source)
        {
            throw new NotImplementedException();
        }
    }

    public class ThematiqueLocalToDomain : ILocalToDomain
    {
        public static IModel Mapping(ILocalSource source)
        {
            throw new NotImplementedException();
        }
    }

    public class GroupeLocalToDomain : ILocalToDomain
    {
        public static IModel Mapping(ILocalSource source)
        {
            throw new NotImplementedException();
        }
    }
}
