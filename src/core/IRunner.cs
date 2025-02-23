namespace TrafficLight;

public interface IRunner {
    Task Run(CancellationTokenSource cts);
}
