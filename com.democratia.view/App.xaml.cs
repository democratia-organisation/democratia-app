using System.Diagnostics;

namespace com.democratia
{
    public partial class App : Application
    {
        public App()
        {
            
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                Debug.WriteLine($"CRASH: {e.ExceptionObject}");
            };
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }



    }
}
