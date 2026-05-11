using com.koyok.democratia.Domain.Models;
using System.Diagnostics.CodeAnalysis;

namespace com.koyok.democratia.Domain.Extension.Comparer
{
    
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


    public record class ThematiqueEqualityComparer : IEqualityComparer<Thematique>
    {
        public bool Equals(Thematique? x, Thematique? y)
        {
            if (x is null || y is null) return false;
            return x.nomThematique?.ToLower() == y.nomThematique?.ToLower();
        }
        public int GetHashCode(Thematique obj)
        {
            return obj.nomThematique?.GetHashCode() ?? 0;
        }
    }

}
