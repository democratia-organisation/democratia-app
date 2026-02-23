namespace com.democratia.Views.internaute.CreerGroupe
{
    public static class CreerGroupeRoute
    {
        public static void RouteCreationGroup()
        {
            Routing.RegisterRoute(nameof(PremiereCreationPage), typeof(PremiereCreationPage));
            Routing.RegisterRoute(nameof(DeuxiemeCreationPage), typeof(DeuxiemeCreationPage));
            Routing.RegisterRoute(nameof(TroisiemeCreationPage), typeof(TroisiemeCreationPage));

        }
    }
}
