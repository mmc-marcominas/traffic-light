namespace TrafficLight;

public class EnumVersionRunner: IRunner {
    public async Task Run(CancellationTokenSource cts) {
        Console.WriteLine($"{Environment.NewLine}EnumVersionRunner.Run() execution");

        await Task.Run(() => Thread.Sleep(1));
        Console.WriteLine("Version not implemented yet");
    }
}
