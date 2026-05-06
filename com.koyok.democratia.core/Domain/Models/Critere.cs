using System.Diagnostics.CodeAnalysis;

namespace com.koyok.democratia.core.Domain.Models
{
    public enum Critere
    {
        POPULARITE,
        PRIX,
        REACTIONS

    }

    public record class PropositionPoulariteComparer() : IEqualityComparer<PropositionRemoteSource>
    {
        public bool Equals(PropositionRemoteSource? x, PropositionRemoteSource? y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] PropositionRemoteSource obj)
        {
            throw new NotImplementedException();
        }
    }

    public record class PropositionPrixComparer() : IEqualityComparer<PropositionRemoteSource>
    {
        public bool Equals(PropositionRemoteSource? x, PropositionRemoteSource? y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] PropositionRemoteSource obj)
        {
            throw new NotImplementedException();
        }
    }

    public record class PropositionReactionsComparer() : IEqualityComparer<PropositionRemoteSource>
    {
        public bool Equals(PropositionRemoteSource? x, PropositionRemoteSource? y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] PropositionRemoteSource obj)
        {
            throw new NotImplementedException();
        }
    }

}
