using System.Text.Json.Serialization;

namespace TrafficLight;
public class ApplicationSettings
{
    public readonly static string Filename = "settings.json";

    [JsonPropertyName("action")]
    public string? Action { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonIgnore]
    public bool? Resume { get; set; }

    [JsonPropertyName("resumedAt")]
    public string? ResumedAt { get; set; }
}
