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
            Routing.RegisterRoute(nameof(Creation), typeof(Creation));
            Routing.RegisterRoute(nameof(Groupe), typeof(Groupe));
            Routing.RegisterRoute(nameof(HomeGestionPage), typeof(HomeGestionPage));
            Routing.RegisterRoute(nameof(PremierePage), typeof(PremierePage));
            Routing.RegisterRoute(nameof(DeuxiemePage), typeof(DeuxiemePage));
            Routing.RegisterRoute(nameof(TroisiemePage), typeof(TroisiemePage));
            Routing.RegisterRoute(nameof(ModifierGestionPage),typeof(ModifierGestionPage));
            Routing.RegisterRoute(nameof(PreferencePage), typeof(PreferencePage));
        }
    }
}
