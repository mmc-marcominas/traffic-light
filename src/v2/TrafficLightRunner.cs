namespace TrafficLight;
public static class Runner {
    public static async Task RunAsync(ApplicationSettings settings, CancellationTokenSource cts) {
        cts.Token.ValidateCancellationRequest();
        var trafficLight = Factory.GetTrafficLight(settings.Type);
        await trafficLight.RunAsync(settings, cts);
    }

    public static async Task ValidateSettingsAction(ApplicationSettings settings, CancellationToken token) {
        await Task.Run(() => ValidateStopAction(settings, token), token);
    }

    private static void ValidateStopAction(ApplicationSettings settings, CancellationToken token) {
        if (settings.Action?.ToLower() == "stop") {
            Console.WriteLine("Finishing gracefully by stop action");
            Environment.Exit(0);
        }
        token.ValidateCancellationRequest();
    }
}
