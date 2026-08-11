using com.koyok.democratia.Data.DataSource;
using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Domain.Models;

namespace com.koyok.democratia.Data.Mapper.LocalToDomain
{
    public interface ILocalToDomain
    {
        public IModel Mapping(ILocalSource source);
        public ILocalSource ReversMapping(IModel model);
    }

    public class InternauteLocalToDomain : ILocalToDomain
    {
        public IModel Mapping(ILocalSource source)
        {
            var localSource = (InternauteLocalSource)source;
            return new Internaute(localSource.IdInternaute, localSource.NomInternaute, localSource.PrenomInternaute, localSource.AdressePostale, localSource.Courriel, localSource.HashageMDP);
        }

        public ILocalSource ReversMapping(IModel model)
        {
            var internaute = (Internaute)model;
            return new InternauteLocalSource
            {
                AdressePostale = internaute.adressePostale,
                PrenomInternaute = internaute.prenomInternaute,
                NomInternaute = internaute.nomInternaute,
                Courriel = internaute.courriel,
                HashageMDP = internaute.hashageMDP,
                IdInternaute = internaute.idInternaute,
            };
        }
    }

    public class PropositionLocalToDomain : ILocalToDomain
    {
        public IModel Mapping(ILocalSource source)
        {
            throw new NotImplementedException();
        }

        public ILocalSource ReversMapping(IModel model)
        {
            throw new NotImplementedException();
        }
    }

    public class ThematiqueLocalToDomain : ILocalToDomain
    {
        public IModel Mapping(ILocalSource source)
        {
            throw new NotImplementedException();
        }

        public ILocalSource ReversMapping(IModel model)
        {
            throw new NotImplementedException();
        }
    }

    public class GroupeLocalToDomain : ILocalToDomain
    {
        public IModel Mapping(ILocalSource source)
        {
            throw new NotImplementedException();
        }

        public ILocalSource ReversMapping(IModel model)
        {
            throw new NotImplementedException();
        }
    }

    public class CommentaireLocalToDomain : ILocalToDomain
    {
        public IModel Mapping(ILocalSource source)
        {
            throw new NotImplementedException();
        }

        public ILocalSource ReversMapping(IModel model)
        {
            throw new NotImplementedException();
        }
    }
}
