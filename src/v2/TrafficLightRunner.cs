namespace TrafficLight;
public static class Runner {
    public static async Task RunAsync(ApplicationSettings settings, CancellationTokenSource cts) {
        cts.Token.ValidateCancellationRequest();
        var trafficLight = Factory.GetTrafficLight(settings.Type);
        await trafficLight.RunAsync(settings, cts);
    }

    public static async Task ValidateSettingsAction(ApplicationSettings settings, CancellationToken token) {
        ValidateStopAction(settings, token);
        await  ValidatePauseAction(settings, token);
    }

    private static void ValidateStopAction(ApplicationSettings settings, CancellationToken token) {
        if (settings.Action?.ToLower() == "stop") {
            Console.WriteLine("Finishing gracefully by stop action");
            Environment.Exit(0);
        }
        token.ValidateCancellationRequest();
    }

    private static async Task ValidatePauseAction(ApplicationSettings settings, CancellationToken token) {
        TimeSpan duration = TimeSpan.FromMilliseconds(250);
        while (settings.Action?.ToLower() == "pause") {
            Console.SetCursorPosition(60, Console.CursorTop -1);
            Console.WriteLine($"{settings.Type} is paused.");
            token.ValidateCancellationRequest();
            await Task.Delay(duration, token.GetLinkedTokenSource());
        }
        Console.SetCursorPosition(60, Console.CursorTop -1);
        Console.WriteLine($"                         ");
    }
}
