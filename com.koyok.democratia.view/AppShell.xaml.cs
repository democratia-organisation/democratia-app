using com.koyok.democratia.Views;
using com.koyok.democratia.Views.groupe;
using com.koyok.democratia.Views.internaute;
using com.koyok.democratia.Views.internaute.CreerGroupe;
using com.koyok.democratia.Views.internaute.gestionCompte;

namespace com.koyok.democratia
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
