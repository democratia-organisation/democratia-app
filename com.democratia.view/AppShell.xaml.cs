using com.democratia.view.Views;
using com.democratia.Views.groupe;
using com.democratia.Views.groupe.decideur;
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
            BasePageRoute.RouteBase();
            CreerGroupeRoute.RouteCreationGroup();
            GestionCompteRoute.RouteGestionCompte();
            DecideurBaseRoute.RouteBase();
            GroupeRoute.Routes();
        }
    }
}
