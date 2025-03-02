namespace TrafficLight;
public class EnumVersionRunner: IRunner {
    public async Task Run(CancellationTokenSource cts) {
        Console.WriteLine("Setting refresh settings handler");
        ApplicationSettings.RefreshAsync += async (e) => {
            while (!cts.Token.IsCancellationRequested) {
                e.Refresh(ApplicationSettings.Filename);
                await Task.Delay(250, cts.Token.GetLinkedTokenSource());
            }
        };

        var settings = new ApplicationSettings();
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
}
