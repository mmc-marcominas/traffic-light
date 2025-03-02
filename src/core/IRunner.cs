namespace TrafficLight;

public interface IRunner {
    Task Run(CancellationTokenSource cts);
    void SetInitialState(string state, CancellationTokenSource cts);
}
