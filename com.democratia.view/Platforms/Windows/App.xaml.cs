// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using System.Globalization;
using System.Text;

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
            this.UnhandledException += (sender, e) =>
            {
                var message = $"Source: {e.Exception.InnerException ?? e.Exception} | Erreur: {e.Exception.Message} | StackTrace: {e.Exception.StackTrace ?? ""}";
                System.Diagnostics.Debug.WriteLine($"[GLOBAL ERROR] {message}");
                using FileStream file = File.Create($"{Path.Combine(FileSystem.Current.AppDataDirectory, $"error_{DateTime.Now:HH-mm-ss_dddd-dd_MM_yyyy}.log")}");
                file.Write(Encoding.UTF8.GetBytes(message));
                file.Write(Encoding.UTF8.GetBytes(Environment.NewLine));
                file.Write(Encoding.UTF8.GetBytes(e.Exception.StackTrace ?? string.Empty));
            };
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public static void SetLocal(string langage)
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture(langage) ;
            CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture(langage);
        }
    }

}
