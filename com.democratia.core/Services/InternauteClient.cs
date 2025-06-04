using System.Text.Json.Nodes;

namespace com.democratia.Services
{
    public class InternauteClient : Client
    {
        

        public InternauteClient() : base() {}

        
        public override JsonArray GetInstance(int id)
        {
            throw new NotImplementedException();
        }

        public override bool SuprimmerInstance(int id)
        {
            throw new NotImplementedException();
        }
    }
}
