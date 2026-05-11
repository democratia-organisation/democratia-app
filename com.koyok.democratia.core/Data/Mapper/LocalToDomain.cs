using com.koyok.democratia.Domain.Models;

namespace com.koyok.democratia.Data.Mapper.LocalToDomain
{
    public interface ILocalToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel;
    }

    public class InternauteLocalToDomain : ILocalToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            throw new NotImplementedException();
        }
    }

    public class PropositionLocalToDomain : ILocalToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            throw new NotImplementedException();
        }
    }

    public class ThematiqueLocalToDomain : ILocalToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            throw new NotImplementedException();
        }
    }

    public class GroupeLocalToDomain : ILocalToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            throw new NotImplementedException();
        }
    }
}
