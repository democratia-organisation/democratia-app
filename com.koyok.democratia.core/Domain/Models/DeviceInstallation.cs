using System.Text.Json.Serialization;

namespace com.koyok.democratia.Domain.Models;

public class DeviceInstallation : IModel
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

