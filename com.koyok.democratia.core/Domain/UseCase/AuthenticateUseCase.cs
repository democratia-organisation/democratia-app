using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Domain.UseCase
{
    public class AuthenticateUseCase(IInternauteRepository repository)
    {
        private readonly IInternauteRepository internauteRepository = repository;

        public Internaute Authenticate()
        {
            return new();
        }
    }
}
