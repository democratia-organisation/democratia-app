using com.democratia.Views;
using com.democratia.view.Views;

namespace com.democratia
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(Home), typeof(Home));
            Routing.RegisterRoute(nameof(Creation), typeof(Creation));
            Routing.RegisterRoute(nameof(Groupe), typeof(Groupe));
        }
    }
}
