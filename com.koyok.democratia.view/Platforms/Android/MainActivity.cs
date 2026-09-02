using Android.App;
using Android.Content.PM;
using Android.OS;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Lib;
using Firebase;
using Firebase.Messaging;

namespace com.koyok.democratia
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        IDeviceInstallationService? deviceInstallationService;
        INotificationRegistrationRepository? notificationRegistrationService;
        IDeviceInstallationService IDeviceInstallationService => deviceInstallationService ??= IPlatformApplication.Current!.Services.GetService<IDeviceInstallationService>()!;
        INotificationRegistrationRepository INotificationRegistrationRepository => notificationRegistrationService ??= IPlatformApplication.Current!.Services.GetService<INotificationRegistrationRepository>()!;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var firebase = FirebaseApp.InitializeApp(this);
            if (IDeviceInstallationService?.NotificationsSupported == true)
                FirebaseMessaging.Instance.Register();
        }

        public async void OnSuccess(Java.Lang.Object? result)
        {
            IDeviceInstallationService?.Token = result!.ToString();
            string? key = await SecureStorage.Default.GetAsync(SecureStorageKeys.API_KEY.ToString());
            if (key == null) INotificationRegistrationRepository!.SerializeDevice(IDeviceInstallationService!);
            else INotificationRegistrationRepository?.RegisterDeviceAsync();
        }
    }
}
