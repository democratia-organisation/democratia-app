using com.koyok.democratia.UI;
using Foundation;
using System.Globalization;

namespace com.koyok.democratia
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        public static readonly CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public static void SetLocal(string langage)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(langage);

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
}
