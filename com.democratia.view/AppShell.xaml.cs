using com.democratia.Views;
using com.democratia.view.Views;

namespace com.democratia
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Home), typeof(Home));
            Routing.RegisterRoute($"{nameof(MainPage)}/{nameof(Creation)}", typeof(Creation));
            Routing.RegisterRoute($"{nameof(Home)}/{nameof(Groupe)}", typeof(Groupe));
            Routing.RegisterRoute($"{nameof(Home)}/{nameof(GestionCompte)}", typeof(GestionCompte));
            Routing.RegisterRoute($"{nameof(Home)}/{nameof(CreerGroupe)}", typeof(CreerGroupe));
        }
    }
}
