using com.koyok.democratia.Resources.Localization;
using com.koyok.democratia.UI;
using com.koyok.democratia.UI.groupe;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Diagnostics;
using System.Text;
using com.koyok.democratia.core.Domain.Utils;
using com.koyok.democratia.core.Domain.Extension;

namespace com.koyok.democratia
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldEnableSnackbarOnWindows(true);
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<ILocalizationService, LocalizationService>();
            builder.Services.AddServices();
            builder.SetUrl();

#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddLogging(configure => configure.AddDebug());
#endif

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                LogErreur((e.ExceptionObject as Exception)!, "AppDomain.UnhandledException");
            
            TaskScheduler.UnobservedTaskException += (sender, e) => 
                LogErreur(e.Exception, "TaskScheduler.UnobservedTaskException");
           

            var app = builder.Build();
            Directory.CreateDirectory(Path.Combine(FileSystem.Current.CacheDirectory, "cache"));
            ServiceHelper.Initialize(app.Services);
            core.Domain.Utils.ServiceHelper.Initialize(app.Services);

            return app;

        }
        private static void LogErreur(Exception ex, string source)
        {
            var message = $"Source: {source} | Erreur: {ex.Message} | StackTrace: {ex.StackTrace ?? ""}";

            Debug.WriteLine($"[GLOBAL ERROR] {message}");
            if (ex?.StackTrace != null)
                Debug.WriteLine(ex.StackTrace);

            using FileStream file = File.Create($"{Path.Combine(FileSystem.Current.AppDataDirectory,$"error_{DateTime.Now:HH-mm-ss_dddd-dd_MM_yyyy}.log")}");
            file.Write(Encoding.UTF8.GetBytes(message));
            file.Write(Encoding.UTF8.GetBytes(Environment.NewLine));
            file.Write(Encoding.UTF8.GetBytes(ex?.StackTrace ?? string.Empty));
        }
        
    }
}
