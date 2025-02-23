namespace TrafficLight;

public class Intermitent: TrafficLightBase, ITrafficLight {
    private enum TrafficLightState {
        Ambar = 1, _ = 1,
    }
    public async Task RunAsync(ApplicationSettings settings, CancellationTokenSource cts) {
        CurrentType = TrafficLightTypes.intermitent;
        ValidateResumedAt(settings);

        while (
            settings.Type?.ToLower() == CurrentType &&
            await DisplayLight(TrafficLightState.Ambar, settings, cts.Token) && 
            await DisplayLight(TrafficLightState._, settings, cts.Token)) {
        }
    }
}
