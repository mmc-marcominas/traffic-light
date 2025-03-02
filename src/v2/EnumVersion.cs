namespace TrafficLight;
public class EnumVersionRunner: IRunner {
    private string InitialState = string.Empty;
    
    public async Task Run(CancellationTokenSource cts)
    {
        Console.WriteLine("Setting refresh settings handler");
        var settings = GetApplicationSettings();

        SetApplicationSettingsRefreshAsync(cts);
        settings.Load();

        while (settings != null && !string.IsNullOrEmpty(settings.Type)) {
            Console.WriteLine($"{settings.Type} is running.");
            await Runner.RunAsync(settings, cts);

            if (settings.Resume.HasValue && settings.Resume.Value) {
                settings.UpdateSettings(ApplicationSettings.Filename);
                break;
            }
        }
    }

    public void SetInitialState(string state, CancellationTokenSource cts) {
        InitialState = state;
    }

    private ApplicationSettings GetApplicationSettings() {
        var settings = new ApplicationSettings();

        if (!string.IsNullOrEmpty(InitialState)) {
            settings.Type = InitialState;
            settings.ResumedAt = string.Empty;
            settings.UpdateSettings(ApplicationSettings.Filename);
        }

        return settings;
    }

    private static void SetApplicationSettingsRefreshAsync(CancellationTokenSource cts) {
        ApplicationSettings.RefreshAsync += async (e) => {
            while (!cts.Token.IsCancellationRequested) {
                e.Refresh(ApplicationSettings.Filename);
                await Task.Delay(250, cts.Token.GetLinkedTokenSource());
            }
        };
    }
}
