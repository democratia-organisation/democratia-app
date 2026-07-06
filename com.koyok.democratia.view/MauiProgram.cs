using com.koyok.democratia.UI;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Diagnostics;
using System.Text;
using com.koyok.democratia.Domain.Extension;
using com.koyok.democratia.Domain.Service;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.view.Resources.Localization;

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
            builder.Services.AddClients();
            builder.SetUrl();

#if DEBUG
            builder.Logging.AddDebug();
            builder.Services.AddLogging(configure => configure.AddDebug());
#endif

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                GererErreur((e.ExceptionObject as Exception)!, "AppDomain.UnhandledException");
            
            TaskScheduler.UnobservedTaskException += (sender, e) => 
                GererErreur(e.Exception, "TaskScheduler.UnobservedTaskException");
           
            var app = builder.Build();
            ServiceHelper.Initialize(app.Services);
            Domain.Service.ServiceHelper.Initialize(app.Services);
            
            return app;

        }
        private static void GererErreur(Exception ex, string source)
        {
            if (ex is TooManyRequestException exception)
            {
                var compteur = exception.Delay;
                Task.Run(exception.CountdownAsync);
                Task.Run(async () => await App.Current!.Windows[0].Page!.DisplayAlertAsync(AppResources.toomanyRequest, string.Format(AppResources.DetailBusy, compteur), "OK"));
                return;
            }
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
