using com.koyok.democratia.Domain.Models;

namespace com.koyok.democratia.Lib;

public interface IDeviceInstallationService
{
    string? Token { get; set; }
    bool NotificationsSupported { get; }
    string GetDeviceId();
    DeviceInstallation GetDeviceInstallation(params string[] tags);
}

