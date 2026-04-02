namespace com.democratia.Views.groupe
{
    public static class GroupeRoute
    {
        public static void Routes()
        {
            Routing.RegisterRoute($"{nameof(GroupePage)}",typeof(GroupePage));
            Routing.RegisterRoute($"{nameof(Membre)}", typeof(Membre));
            Routing.RegisterRoute($"{nameof(NouvelleProposition)}", typeof(NouvelleProposition));
            Routing.RegisterRoute($"{nameof(Parametre)}", typeof(Parametre));
        }

    }
}
