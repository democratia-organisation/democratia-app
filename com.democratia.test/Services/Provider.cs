
using com.democratia.Services;

namespace com.democratia.test.Services
{
    public class Provider
    {

        public readonly IEnumerable<IClient?>? clients;

        public Provider(IEnumerable<IClient?>? clients)
        {
            
            this.clients = clients;
        }
    }
}
