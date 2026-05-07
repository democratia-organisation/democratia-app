using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.Domain.UseCase
{
    public class CreerGroupeUseCase(IGroupeRepository repository)
    {
        private IGroupeRepository repository = repository;

        public Groupe Creer()
        {
            return new();
        }
    }
}
