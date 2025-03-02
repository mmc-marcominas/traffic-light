namespace TrafficLight;
public class DictionaryVersionRunner: IRunner {
    private static readonly Dictionary<string, Settings> _trafficLight = new() {
        { "go", new Settings { State = "Green", Delay = TimeSpan.FromSeconds(4) } },
        { "attention", new Settings { State = "Amber", Delay = TimeSpan.FromSeconds(1) } },
        { "stop", new Settings { State = "Red", Delay = TimeSpan.FromSeconds(2) } }
    };


    public async Task Run(CancellationTokenSource cts) {
        Console.WriteLine($"{Environment.NewLine}DictionaryVersionRunner.Run() execution");
        await DisplayLight(cts.Token);
    }

    public void SetInitialState(string type, CancellationTokenSource cts) {
        Console.WriteLine($"{Environment.NewLine}DictionaryVersionRunner not implements state execution control");
    }

    private async Task DisplayLight(CancellationToken token) {
        try {
            Console.WriteLine($"{Environment.NewLine}");
            while (true) {
                foreach (var item in _trafficLight) {
                    Console.SetCursorPosition(0, Console.CursorTop -1);
                    Console.WriteLine($"{item.Value.State} - {item.Key}{new string(' ', 20)}");
                    await Task.Delay(item.Value.Delay, token);
                }
            }
        }
        catch (OperationCanceledException) {
            Console.WriteLine($"{Environment.NewLine}Operation canceled");
        }
    }
}
