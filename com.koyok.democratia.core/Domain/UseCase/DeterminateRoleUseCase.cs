using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Domain.UseCase
{
    public class DeterminateRoleUseCase(IInternauteRepository repository)
    {
        private readonly IInternauteRepository repository = repository;

        public void Determinate(Role role)
        {

        }
    }
}
