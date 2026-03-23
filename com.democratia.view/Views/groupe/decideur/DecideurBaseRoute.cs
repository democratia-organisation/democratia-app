namespace com.democratia.Views.groupe.decideur
{
    internal static class DecideurBaseRoute
    {
        public static void RouteBase()
        {
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            
        }
    }
}
