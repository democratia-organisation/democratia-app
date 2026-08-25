using Android.App;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Lib;
using Firebase.Messaging;

namespace PushNotificationsDemo.Platforms.Android;

[Service(Exported = false)]
[IntentFilter([ "com.google.firebase.MESSAGING_EVENT" ])]
public class PushNotificationFirebaseMessagingService : FirebaseMessagingService
{
    
    INotificationRegistrationRepository? _notificationRegistrationService;
    IDeviceInstallationService? _deviceInstallationService;

    INotificationRegistrationRepository NotificationRegistrationService =>
        _notificationRegistrationService ??= IPlatformApplication.Current!.Services.GetService<INotificationRegistrationRepository>()!;

    IDeviceInstallationService DeviceInstallationService =>
        _deviceInstallationService ??= IPlatformApplication.Current!.Services.GetService<IDeviceInstallationService>()!;

    public override void OnRegistered(string token)
    {
        base.OnRegistered(token);
        DeviceInstallationService.Token = token;

        NotificationRegistrationService.RefreshRegistrationAsync()
            .ContinueWith((task) =>
            {
                if (task.IsFaulted)
                    throw task.Exception;
            });
    }
}
