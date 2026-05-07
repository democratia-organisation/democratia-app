using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Enumerations;

namespace com.koyok.democratia.Domain.UseCase
{
    public class GererCompteUseCase(IInternauteRepository repository)
    {
        private readonly IInternauteRepository internauteRepository = repository;

        public void ActionCompte(TypeGestion type)
        {

        }

        public void SupprimerCompte()
        {

        }
    }
}
