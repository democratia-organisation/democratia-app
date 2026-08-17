using System.Text.Json.Serialization;

namespace com.koyok.democratia.Data.DataSource.Remote;

public class DeviceInstallationRemoteSource : IRemoteSource
{
    [JsonPropertyName("device_id")]
    public string? InstallationId { get; set; }

    [JsonPropertyName("type_device")]
    public string? Platform { get; set; }

    [JsonPropertyName("token")]
    public string? PushChannel { get; set; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new List<string>();
}

