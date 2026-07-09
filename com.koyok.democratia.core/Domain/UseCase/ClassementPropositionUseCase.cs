using com.koyok.democratia.Lib;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Domain.UseCase
{
    public class ClassementPropositionUseCase(List<Proposition> propositions, IPropositionRepository propositionRepository)
    {
        private static readonly int MAX_PROPOSITIONS = 10;
        private readonly List<Proposition> propositions = propositions;
        private readonly IPropositionRepository propositionRepository = propositionRepository;

        public List<Proposition> Classer(Critere critere)
        {
            if (propositions.Count == MAX_PROPOSITIONS)
            {
                return critere switch
                {
                    Critere.POPULARITE => [.. propositions.OrderByDescending(p => p.Popularite)],
                    Critere.PRIX => [.. propositions.OrderBy(p => p.Prix)],
                    Critere.REACTIONS => [.. propositions.OrderByDescending(p => p.Reactions)],
                    _ => throw new ArgumentException("Critère de classement non valide")
                };
            }
            else
            {
               return propositionRepository.TrierProposition(critere);
            }

        }
    }
}
