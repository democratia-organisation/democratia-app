using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Lib;
using Foundation;
using System.Diagnostics;
using System.Globalization;
using UIKit;
using UserNotifications;

namespace com.koyok.democratia
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        INotificationRegistrationRepository? _notificationRegistrationService;
        IDeviceInstallationService? _deviceInstallationService;
        INotificationRegistrationRepository NotificationRegistrationService =>
            _notificationRegistrationService ??=  IPlatformApplication.Current!.Services.GetService<INotificationRegistrationRepository>()!;

        IDeviceInstallationService DeviceInstallationService =>
            _deviceInstallationService ??= IPlatformApplication.Current!.Services.GetService<IDeviceInstallationService>()!;
        public static readonly CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
#if !DEBUG
        [Export("application:didFinishLaunchingWithOptions:")]
        public override bool FinishedLaunching(UIApplication application, NSDictionary? launchOptions)
        {
            if (DeviceInstallationService.NotificationsSupported)
            {
                UNUserNotificationCenter.Current.RequestAuthorization(
                    UNAuthorizationOptions.Alert |
                    UNAuthorizationOptions.Badge |
                    UNAuthorizationOptions.Sound,
                    (approvalGranted, error) =>
                    {
                        if (approvalGranted && error == null)
                        {
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                UIApplication.SharedApplication.RegisterForRemoteNotifications();
                            });
                        }
                    });
            }

            return base.FinishedLaunching(application, launchOptions);
        }
        async Task CompleteRegistrationAsync(NSData deviceToken)
        {
            DeviceInstallationService.Token = deviceToken.ToHexString();
            string? key = await SecureStorage.Default.GetAsync(SecureStorageKeys.API_KEY.ToString());
            if (key == null) NotificationRegistrationService.SerializeDevice(DeviceInstallationService);
            else await NotificationRegistrationService.RefreshRegistrationAsync();
        }
        [Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
        public void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            CompleteRegistrationAsync(deviceToken)
                .ContinueWith((task) =>
                {
                    if (task.IsFaulted)
                        throw task.Exception;
                });
        }

        [Export("application:didFailToRegisterForRemoteNotificationsWithError:")]
        public void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            Debug.WriteLine(error.Description);
        }
#endif

        public static void SetLocal(string langage)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(langage);

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
}
