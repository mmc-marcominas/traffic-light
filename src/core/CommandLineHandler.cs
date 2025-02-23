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
        await RunSelectedVersion(version, cts);
    }

    private static void ShowHelper() {
        Console.WriteLine($"{Environment.NewLine}Usage: {CommandlineName} [options]");
        Console.WriteLine($"{Environment.NewLine}Options:");
        Console.WriteLine("  --help                             Display this help message");
        Console.WriteLine("  --version=versionNumber            Required: specify implementation version");
        Console.WriteLine($"{Environment.NewLine}Example:");
        Console.WriteLine($"  {CommandlineName} --version=v1 {Environment.NewLine}");
        Console.WriteLine($"Available versions:");
        Console.WriteLine($" v1 - simple dictionary implementation");
        Console.WriteLine($" v2 - vehicle, pedestrian and intermitent implementation with enum");
        Environment.Exit(0);
    }

    private static async Task RunSelectedVersion(string version, CancellationTokenSource cts) {
        Dictionary<string, IRunner> options = new() {
            {"v2", new EnumVersionRunner()},
            {"v1", new DictionaryVersionRunner()},
        };

        if (!options.TryGetValue(version, out var result)) {
            Console.WriteLine($"Version not found");
            Environment.Exit(1);
        }

        await result.Run(cts);
    }
}
