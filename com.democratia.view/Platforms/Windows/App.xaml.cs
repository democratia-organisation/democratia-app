// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.Globalization;

namespace com.democratia.WinUI
{
    
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        public static readonly CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public static void SetLocal(string langage)
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture(langage) ;
            CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture(langage);
        }
    }

}
