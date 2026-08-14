using com.koyok.democratia.Lib;
using Foundation;
using System.Globalization;
using UIKit;
using UserNotifications;

namespace com.koyok.democratia
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {

        INotificationRegistrationService? _notificationRegistrationService;
        IDeviceInstallationService? _deviceInstallationService;
        INotificationRegistrationService NotificationRegistrationService =>
            _notificationRegistrationService ??=  IPlatformApplication.Current!.Services.GetService<INotificationRegistrationService>()!;

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
        Task CompleteRegistrationAsync(NSData deviceToken)
        {
            DeviceInstallationService.Token = deviceToken.ToHexString();
            return NotificationRegistrationService.RefreshRegistrationAsync();
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
            throw new Exception($"Failed to register for remote notifications: {error.LocalizedDescription}");
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
