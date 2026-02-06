using com.democratia.Views.Groupe;
using com.democratia.Views.internaute;
using com.democratia.Views.internaute.CreerGroupe;

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
            Routing.RegisterRoute(nameof(DeuxiemePage), typeof(DeuxiemePage));
            Routing.RegisterRoute(nameof(TroisiemePage), typeof(TroisiemePage));
        }
    }
}
