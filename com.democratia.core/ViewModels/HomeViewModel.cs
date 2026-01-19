using com.democratia.Services;

namespace com.democratia.ViewModels
{
    public partial class HomeViewModel : ConnectableViewModel
    {
        public HomeViewModel(IClient? client) : base(client)
        {

        }
    }
}
