using com.democratia.Services;

namespace com.democratia.test.Services
{
    public class Provider(IEnumerable<IClient?>? clients)
    {

        public readonly IEnumerable<IClient?>? clients = clients;
    }
}
