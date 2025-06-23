using com.democratia.Utils;
using com.democratia.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Hosting;

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

#if ANDROID
            builder.ConfigureMauiHandlers(handlers =>
            {
                // Active l'AutomationId pour tous les contrôles
                handlers.AddHandler(typeof(Label), typeof(LabelHandler));
                handlers.AddHandler(typeof(Entry), typeof(EntryHandler));
                handlers.AddHandler(typeof(Button), typeof(ButtonHandler));

                LabelHandler.Mapper.AppendToMapping("AutomationId", (h, v) =>
                {
                    if (h.PlatformView is Android.Views.View view && !string.IsNullOrEmpty(v.AutomationId))
                    {
                        view.ContentDescription = v.AutomationId;
                        view.Id = global::Android.Views.View.GenerateViewId();
                    }
                });

                EntryHandler.Mapper.AppendToMapping("AutomationId", (h, v) =>
                {
                    if (h.PlatformView is Android.Views.View view && !string.IsNullOrEmpty(v.AutomationId))
                    {
                        view.ContentDescription = v.AutomationId;
                        view.Id = global::Android.Views.View.GenerateViewId();
                    }
                });

                ButtonHandler.Mapper.AppendToMapping("AutomationId", (h, v) =>
                {
                    if (h.PlatformView is Android.Views.View view && !string.IsNullOrEmpty(v.AutomationId))
                    {
                        view.ContentDescription = v.AutomationId;
                        view.Id = global::Android.Views.View.GenerateViewId();
                    }
                });
            });
#endif


#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddLogging(configure => configure.AddDebug());
#endif

            return builder.Build();
        }
    }
}
