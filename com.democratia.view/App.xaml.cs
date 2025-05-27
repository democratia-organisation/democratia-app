using System.Diagnostics;

namespace com.democratia
{
    public partial class App : Application
    {
        public App()
        {
            
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                Current?.Windows[0]?.Page?.DisplayAlert("Erreur","Une erreur inattendu est survenue. Voici le message : \n" + e.ExceptionObject.ToString(),"OK");
                Current?.Quit();
            };
        }

        protected override Window CreateWindow(IActivationState? activationState) =>new(new AppShell());

    }
}
