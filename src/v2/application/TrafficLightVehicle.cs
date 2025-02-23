namespace TrafficLight;

public class TrafficLightVehicle: TrafficLightBase, ITrafficLight {
    private enum TrafficLightState {
        Green = 4, Ambar = 1, Red = 2,
    }
    public async Task RunAsync(ApplicationSettings settings, CancellationTokenSource cts) {
        CurrentType = TrafficLightTypes.vehicle;
        ValidateResumedAt(settings);
      
        while (
            settings.Type?.ToLower() == CurrentType &&
            await DisplayLight(TrafficLightState.Green, settings, cts.Token) && 
            await DisplayLight(TrafficLightState.Ambar, settings, cts.Token) && 
            await DisplayLight(TrafficLightState.Red, settings, cts.Token)) {
        }
    }
}
