namespace TrafficLight;

public class Pedestrian: TrafficLightBase, ITrafficLight {
    private enum TrafficLightState {
        Green = 4, Red = 3,
    }
    public async Task RunAsync(ApplicationSettings settings, CancellationTokenSource cts) {
        CurrentType = TrafficLightTypes.pedestrian;
        ValidateResumedAt(settings);

        while (
            settings.Type?.ToLower() == CurrentType &&
            await DisplayLight(TrafficLightState.Green, settings, cts.Token) && 
            await DisplayLight(TrafficLightState.Red, settings, cts.Token)) {
        }
    }
}
