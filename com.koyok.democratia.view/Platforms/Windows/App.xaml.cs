// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

using com.koyok.democratia.Lib;
using Microsoft.UI.Xaml;
using Windows.Networking.PushNotifications;
using System.Globalization;
using System.Text;
using Windows.UI.Notifications;
using com.koyok.democratia.Domain.Repository;

namespace com.koyok.democratia.WinUI
{
    
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        public static readonly CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        public INotificationRegistrationRepository? notificationRegistrationService;
        public IDeviceInstallationService? deviceInstallationService;

        public INotificationRegistrationRepository NotificationRegistrationService => notificationRegistrationService ??= IPlatformApplication.Current!.Services.GetService<INotificationRegistrationRepository>()!;
        public IDeviceInstallationService DeviceInstallationService => deviceInstallationService ??= IPlatformApplication.Current!.Services.GetService<IDeviceInstallationService>()!;
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
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
#if !DEBUG
            await SetupWindowsPushNotificationsAsync();
#endif
        }

        private async Task SetupWindowsPushNotificationsAsync()
        {
            try
            {
                PushNotificationChannel channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();

                DeviceInstallationService.Token = channel.Uri;

                channel.PushNotificationReceived += (sender, e) =>
                {
                    RawNotification notification = e.RawNotification;
                    ToastNotification toastNotification = e.ToastNotification;
                    TileNotification tileNotification = e.TileNotification;
                    BadgeNotification badgeNotification = e.BadgeNotification;
                };
                string? key = await SecureStorage.Default.GetAsync(SecureStorageKeys.API_KEY.ToString());  
                if (key == null) NotificationRegistrationService.SerializeDevice(DeviceInstallationService);
                else await NotificationRegistrationService.RegisterDeviceAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ERROR] Failed to register device for push notifications: {ex.Message}");
            }
        }
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public static void SetLocal(string langage)
        {
            CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture(langage) ;
            CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture(langage);
        }
    }

}
