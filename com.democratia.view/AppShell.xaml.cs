using com.democratia.Views.internaute;

namespace com.democratia
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Home), typeof(Home));
            Routing.RegisterRoute(nameof(Creation), typeof(Creation));
            Routing.RegisterRoute(nameof(Groupe), typeof(Groupe));
            Routing.RegisterRoute(nameof(GestionCompte), typeof(GestionCompte));
            Routing.RegisterRoute(nameof(PremierePage), typeof(PremierePage));
        }
    }
}
