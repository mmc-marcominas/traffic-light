namespace TrafficLight;

public class TrafficLightBase {
    protected string CurrentType { get; set; } = string.Empty;
    protected string? ResumedAt { get; set; } = null;

    protected async Task<bool> DisplayLight(object state, ApplicationSettings settings, CancellationToken token) {
        var stateName = $"{Enum.GetName(state.GetType(), state)}";
        
        if (!string.IsNullOrWhiteSpace(ResumedAt)) {
            if (ResumedAt != stateName) {
                return true;
            }
            ResumedAt = null;
        }

        await Runner.ValidateSettingsAction(settings, token);
        if (settings.Type?.ToLower() != CurrentType) {
            return false;
        }

        var duration = TimeSpan.FromSeconds((int)state);
        var delayToken = token.GetLinkedTokenSource();

        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.WriteLine($"{stateName}, duration: {duration.Seconds}{new string(' ', 10)}");

        if (!HasCancellationRequested(stateName, settings, token)) {
            await Task.Delay(duration, delayToken);
        }

        return !HasCancellationRequested(stateName, settings, token) && !delayToken.IsCancellationRequested;
    }

    protected static bool HasCancellationRequested(string stateName, ApplicationSettings settings, CancellationToken token) {
        if (token.IsCancellationRequested) {
            settings.ResumedAt = stateName;
            Console.WriteLine($"Operation canceled on {settings.Type} {settings.ResumedAt} light");
            settings.Resume = true;
        }
        return token.IsCancellationRequested;
    }

    protected void ValidateResumedAt(ApplicationSettings settings) {
        Console.WriteLine($"{Environment.NewLine}");
        if (!string.IsNullOrWhiteSpace(settings.ResumedAt)) {
            ResumedAt = $"{settings.ResumedAt}";
            settings.ResumedAt = null;
            settings.UpdateSettings(ApplicationSettings.Filename);
        }
    }
}
