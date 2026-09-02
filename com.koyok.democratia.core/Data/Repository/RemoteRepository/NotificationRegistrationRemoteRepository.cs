using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Lib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Text;
using System.Text.Json;

namespace com.koyok.democratia.Data.Repository.RemoteRepository;

public class NotificationRegistrationRemoteRepository(HttpClient client, IRemoteToDomain remoteToDomain) : 
    RemoteBaseRepository(client, remoteToDomain) , INotificationRegistrationRepository
{
    const string RequestUrl = "notifications";
    private static string PATHDEVICE = Path.Combine(FileSystem.Current.AppDataDirectory, "device.json");

    IDeviceInstallationService? _deviceInstallationService;

    IDeviceInstallationService DeviceInstallationService =>
        _deviceInstallationService ??= _deviceInstallationService = Application.Current!.Windows[0].Page!.Handler!.MauiContext!.Services.GetService<IDeviceInstallationService>()!;

    public async Task DeregisterDeviceAsync()
    {
        var cachedToken = await SecureStorage.GetAsync(SecureStorageKeys.CachedDeviceTokenKey.ToString());

        if (cachedToken == null)
            return;

        var deviceId = DeviceInstallationService?.GetDeviceId();

        if (string.IsNullOrWhiteSpace(deviceId))
            throw new Exception("Unable to resolve an ID for the device.");

        await client!.DeleteAsync($"{RequestUrl}/{deviceId}");

        SecureStorage.Remove(SecureStorageKeys.CachedDeviceTokenKey.ToString());
        SecureStorage.Remove(SecureStorageKeys.CachedTagsKey.ToString());
    }

    public async Task RegisterDeviceAsync(params string[] tags)
    {
        DeviceInstallation? deviceInstallation;
        string? key = await SecureStorage.Default.GetAsync(SecureStorageKeys.API_KEY.ToString());

        if (File.Exists(PATHDEVICE) && key == null)
        {
            string devicestring = Encoding.UTF8.GetString(File.ReadAllBytes(PATHDEVICE));
            deviceInstallation = remoteToDomain.Mapping(devicestring) as DeviceInstallation;
        }
        else
         deviceInstallation = DeviceInstallationService?.GetDeviceInstallation(tags);
        string? idInternaute = await SecureStorage.Default.GetAsync(SecureStorageKeys.IdInternaute.ToString());
        if (idInternaute == null) return;

        HttpResponseMessage response; 
        try
        {
            response = await client!.PatchAsync($"{RequestUrl}/{idInternaute}/{deviceInstallation!.InstallationId}", new StringContent(JsonSerializer.Serialize(deviceInstallation), Encoding.UTF8, "application/json"));
        }
        catch (Exception)
        {
            throw new ConnexionErrorException("Failed to register device installation.");
        }
        Task tokenTask = SecureStorage.SetAsync(SecureStorageKeys.CachedDeviceTokenKey.ToString(), deviceInstallation!.PushChannel!);
        Task tagsTask = SecureStorage.SetAsync(SecureStorageKeys.CachedTagsKey.ToString(), JsonSerializer.Serialize(tags));

        await Task.WhenAll(tokenTask, tagsTask);
    }

    public async Task RefreshRegistrationAsync()
    {
        var cachedTokenTask = SecureStorage.GetAsync(SecureStorageKeys.CachedDeviceTokenKey.ToString());

        var serializedTagsTask = SecureStorage.GetAsync(SecureStorageKeys.CachedTagsKey.ToString());

        await Task.WhenAll(cachedTokenTask, serializedTagsTask);

        string? cachedToken = await cachedTokenTask;
        string? serializedTagsResult = await serializedTagsTask;

        if (string.IsNullOrWhiteSpace(cachedToken) ||
            string.IsNullOrWhiteSpace(serializedTagsResult) ||
            string.IsNullOrWhiteSpace(_deviceInstallationService!.Token) ||
            cachedToken == DeviceInstallationService.Token)
            return;

        var tags = JsonSerializer.Deserialize<string[]>(serializedTagsResult);

        await RegisterDeviceAsync(tags!);
    }

    public void SerializeDevice(IDeviceInstallationService device)
    {
        string devicestring = JsonSerializer.Serialize(device);
        using FileStream file = File.Create(PATHDEVICE);
        file.Write(Encoding.UTF8.GetBytes(devicestring));
    }
}
