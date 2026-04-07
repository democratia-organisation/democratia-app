using com.democratia.Views;
using com.democratia.Views.groupe;
using com.democratia.Views.internaute;
using com.democratia.Views.internaute.CreerGroupe;
using com.democratia.Views.internaute.gestionCompte;

namespace com.democratia
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            CreerGroupeRoute.RouteCreationGroup();
            GestionCompteRoute.RouteGestionCompte();
            GroupeRoute.Routes();
        }
    }
}
