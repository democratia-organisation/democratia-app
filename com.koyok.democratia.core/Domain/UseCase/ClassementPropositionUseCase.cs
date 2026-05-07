using com.koyok.democratia.Domain.Repository;

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
