using System.Diagnostics.CodeAnalysis;

namespace com.koyok.democratia.Models
{
    public enum Critere
    {
        POPULARITE,
        PRIX,
        REACTIONS

    }

    public record class PropositionPoulariteComparer() : IEqualityComparer<Proposition>
    {
        public bool Equals(Proposition? x, Proposition? y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] Proposition obj)
        {
            throw new NotImplementedException();
        }
    }

    public record class PropositionPrixComparer() : IEqualityComparer<Proposition>
    {
        public bool Equals(Proposition? x, Proposition? y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] Proposition obj)
        {
            throw new NotImplementedException();
        }
    }

    public record class PropositionReactionsComparer() : IEqualityComparer<Proposition>
    {
        public bool Equals(Proposition? x, Proposition? y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] Proposition obj)
        {
            throw new NotImplementedException();
        }
    }

}
