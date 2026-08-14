// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using com.koyok.democratia.Lib;
using Microsoft.UI.Xaml;
using Microsoft.Windows.PushNotifications;
using Microsoft.Windows.AppLifecycle;
using System.Globalization;
using System.Text;

namespace com.koyok.democratia.WinUI
{
    
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        public static string? AZURE_KEY;
        public static readonly CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        public readonly INotificationRegistrationService notificationRegistrationService;
        public readonly IDeviceInstallationService deviceInstallationService;
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App(INotificationRegistrationService notificationRegistrationService, IDeviceInstallationService deviceInstallationService)
        {
            this.notificationRegistrationService = notificationRegistrationService;
            this.deviceInstallationService = deviceInstallationService;
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
#if !DEBUG
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
            await SetupWindowsPushNotificationsAsync();
        }

        private async Task SetupWindowsPushNotificationsAsync()
        {
            PushNotificationManager.Default.PushReceived += (sender, e) =>
            {
                var payload = e.Payload;

            };
            if (deviceInstallationService.NotificationsSupported)
            {
                PushNotificationManager.Default.Register();
                AppInstance.GetCurrent().GetActivatedEventArgs();
            }

            var channelOperation = await PushNotificationManager.Default.CreateChannelAsync(new Guid(AZURE_KEY!));
            

            if (channelOperation.Status == PushNotificationChannelStatus.CompletedSuccess)
            {
                var channel = channelOperation.Channel;
                string channelUri = channel.Uri.ToString();
                deviceInstallationService.Token = channelUri;

                await notificationRegistrationService.RefreshRegistrationAsync();
            }
        }
#endif
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public static void SetLocal(string langage)
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture(langage) ;
            CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture(langage);
        }
    }

}
