using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Enumerations;

namespace com.koyok.democratia.Domain.UseCase
{
    public class ClassementPropositionUseCase(IPropositionRepository repository)
    {
        private readonly IPropositionRepository propositionRepository = repository;

        public void Classer(Critere critere)
        {

        }
    }
}
