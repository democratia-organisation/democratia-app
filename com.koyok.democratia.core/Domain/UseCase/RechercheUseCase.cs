namespace com.koyok.democratia.Domain.UseCase
{
    public class RechercheUseCase<T>(IEnumerable<T> enumerable, EqualityComparer<T> comparer)
    {
        private readonly T item;
        private readonly EqualityComparer<T> comparer = comparer;
        private readonly IEnumerable<T> collection = enumerable;

        public void Recherche()
        {

        }
    }
}
