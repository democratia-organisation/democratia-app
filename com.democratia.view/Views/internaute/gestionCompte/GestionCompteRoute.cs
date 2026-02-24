
namespace com.democratia.Views.internaute.gestionCompte
{
    internal static class GestionCompteRoute
    {
        public static void RouteGestionCompte()
        {
            Routing.RegisterRoute(nameof(CreationPage), typeof(CreationPage));
            Routing.RegisterRoute(nameof(HomeGestionPage), typeof(HomeGestionPage));
            Routing.RegisterRoute(nameof(ModifierGestionPage), typeof(ModifierGestionPage));
            Routing.RegisterRoute(nameof(PreferencePage), typeof(PreferencePage));

        }
    }
}
