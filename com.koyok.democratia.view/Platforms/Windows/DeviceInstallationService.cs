using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Lib;
using Microsoft.Windows.PushNotifications;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.System.Profile;

namespace com.koyok.democratia.WinUI
{
    internal class DeviceInstallationService : IDeviceInstallationService
    {
        public string? Token { get; set; }

        public bool NotificationsSupported => PushNotificationManager.IsSupported();

        public string GetDeviceId()
        {
            SystemIdentificationInfo systemId = SystemIdentification.GetSystemIdForPublisher();

            
            if (systemId.Source != SystemIdentificationSource.None)
            {
                byte[] idBytes = systemId.Id.ToArray();
                return Convert.ToHexString(idBytes);
            }

            return string.Empty;
        }

        private string? GetNotificationsSupportError()
        {
            if (!NotificationsSupported)
            {
                return "Notification pas supporté";
            }
            else return "Notification supporté mais le token n'est pas disponible";
        }

        public DeviceInstallation GetDeviceInstallation(params string[] tags)
        {
            if (!NotificationsSupported)
                throw new Exception(GetNotificationsSupportError());

            if (string.IsNullOrWhiteSpace(Token))
                throw new Exception("Unable to resolve token for WNS");
            var installation = new DeviceInstallation
            {
                InstallationId = GetDeviceId(),
                Platform = "wns",
                PushChannel = Token
            };

            installation.Tags.AddRange(tags);

            return installation;
        }
    }
}
