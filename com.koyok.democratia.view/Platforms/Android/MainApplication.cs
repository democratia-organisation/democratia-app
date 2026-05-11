using Android.App;
using Android.Runtime;
using com.koyok.democratia.Utils;
using com.koyok.democratia.UI;
using System.Globalization;

namespace com.koyok.democratia
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public static readonly CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();


        public static void SetLocal(string langage)
        {

            CultureInfo ci = CultureInfo.CreateSpecificCulture(langage);

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
}

