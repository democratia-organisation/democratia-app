using com.democratia.Resources.Localization;
using com.democratia.Utils;
using com.democratia.Views;
using com.democratia.Views.internaute;
using com.democratia.Views.internaute.CreerGroupe;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace com.democratia
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddServices();
            builder.Services.AddPages();
            builder.Services.AddSingleton<ILocalizationService, LocalizationService>();
            builder.SetUrl();

#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddLogging(configure => configure.AddDebug());
#endif

            return builder.Build();

        }
        extension(IServiceCollection builder)
        {
            public IServiceCollection AddPages()
            {
                builder.AddTransient<Creation>();
                builder.AddTransient<MainPage>();
                builder.AddTransient<Home>();
                builder.AddTransient<GestionCompte>();
                builder.AddTransient<PremierePage>();
                builder.AddTransient<DeuxiemePage>();
                builder.AddTransient<TroisiemePage>();

                return builder;
            }
        }
    }
}
