using com.democratia.Utils;
using com.democratia.view.Views;
using com.democratia.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace com.democratia
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddServices();
            builder.Services.AddTransient<Creation>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<Home>();
            builder.Services.AddTransient<GestionCompte>();

#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddLogging(configure => configure.AddDebug());
#endif

            return builder.Build();

        }
    }
}
