namespace TrafficLight;

public interface ITrafficLight {
    Task RunAsync(ApplicationSettings settings, CancellationTokenSource cts);
}
