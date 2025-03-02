namespace TrafficLight;
public static class CommandLineHandler {
    public static string CommandlineName = "TrafficLight";

    public static async Task Handle(string[] args, CancellationTokenSource cts) {
        Dictionary<string, string> options = new();
        options.SetKeysFromArgs(args);

        if (options.ContainsKey("--help") || 
           !options.ContainsKey("--version")) {
            ShowHelper();
        }

        var version = options.GetKey("--version", new string[] { "v1", "v2" });
        var validStates = new string[] { "vehicle", "pedestrian", "intermitent", "" };
        var state = options.GetKey("--state", validStates);
        await RunSelectedVersion(version, state, cts);
    }

    private static void ShowHelper() {
        var newLine = Environment.NewLine;
        Console.WriteLine($"{newLine}Usage: {CommandlineName} [options]");
        Console.WriteLine($"{newLine}Options:");
        Console.WriteLine("  --help                             Display this help message");
        Console.WriteLine("  --version=versionNumber            Required: specify implementation version");
        Console.WriteLine("  --state=stateType                  Optional: specify traffic light state");
        Console.WriteLine($"{newLine}Examples:");
        Console.WriteLine($"  {CommandlineName} --version=v1");
        Console.WriteLine($"  {CommandlineName} --version=v2 --state=vehicle");
        Console.WriteLine($"{newLine}Available versions:");
        Console.WriteLine($"  v1 - simple dictionary implementation");
        Console.WriteLine($"  v2 - vehicle, pedestrian and intermitent implementation with enum");
        Console.WriteLine($"{newLine}Available states:");
        Console.WriteLine($"  vehicle, pedestrian or intermitent {newLine}");
        Environment.Exit(0);
    }

    private static async Task RunSelectedVersion(string version, string state, CancellationTokenSource cts) {
        Dictionary<string, IRunner> options = new() {
            {"v1", new DictionaryVersionRunner()},
            {"v2", new EnumVersionRunner()},
        };

        if (!options.TryGetValue(version, out var result)) {
            Console.WriteLine($"Version not found");
            Environment.Exit(1);
        }

        if (!string.IsNullOrEmpty(state)) {
            Console.WriteLine($"State {state} received");
            result.SetInitialState(state, cts);
        }

        await result.Run(cts);
    }
}
