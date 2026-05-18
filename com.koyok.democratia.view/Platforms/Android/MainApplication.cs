using Android.App;
using Android.Runtime;
using System.Globalization;

namespace com.koyok.democratia
{
    [Application]
    public class MainApplication(IntPtr handle, JniHandleOwnership ownership) : MauiApplication(handle, ownership)
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

