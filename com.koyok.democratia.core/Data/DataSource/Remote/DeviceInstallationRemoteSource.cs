using System.Text.Json.Serialization;

namespace com.koyok.democratia.Data.DataSource.Remote;

public class DeviceInstallationRemoteSource : IRemoteSource
{
    [JsonPropertyName("installationId")]
    public string? InstallationId { get; set; }

    [JsonPropertyName("platform")]
    public string? Platform { get; set; }

    [JsonPropertyName("pushChannel")]
    public string? PushChannel { get; set; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; } = new List<string>();
}

