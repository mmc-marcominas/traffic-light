using System.Text.Json.Serialization;

namespace TrafficLight;
public class ApplicationSettings
{
    public delegate Task RefreshEventHandler(ApplicationSettings s);
    public static event RefreshEventHandler? RefreshAsync;
    public readonly static string Filename = "settings.json";

    [JsonPropertyName("action")]
    public string? Action { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonIgnore]
    public bool? Resume { get; set; }

    [JsonPropertyName("resumedAt")]
    public string? ResumedAt { get; set; }

    public void Load() {
        if (RefreshAsync == null) {
            throw new Exception("RefreshSettingsAsync handler must be set before");
        }
        RefreshAsync(this);
    }
}
