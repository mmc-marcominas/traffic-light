namespace TrafficLight;
public class EnumVersionRunner: IRunner {
    public async Task Run(CancellationTokenSource cts) {
        Console.WriteLine("Setting refresh settings handler");

        var settings = new ApplicationSettings{
            Action = "run",
            Type= "vehicle",
        };

        while (settings != null && !string.IsNullOrEmpty(settings.Type)) {
            Console.WriteLine($"{settings.Type} is running.");
            await Runner.RunAsync(settings, cts);
        }

    }
}
