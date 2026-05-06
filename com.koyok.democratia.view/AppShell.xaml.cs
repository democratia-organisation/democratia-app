using com.koyok.democratia.UI;
using com.koyok.democratia.UI.groupe;
using com.koyok.democratia.UI.internaute;
using com.koyok.democratia.UI.internaute.CreerGroupe;
using com.koyok.democratia.UI.internaute.gestionCompte;

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
