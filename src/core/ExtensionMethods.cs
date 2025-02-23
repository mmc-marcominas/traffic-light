namespace TrafficLight;

public static class ExtensionMethods {
    public static void SetCancelKeyPress(this CancellationTokenSource cts) {
        Console.WriteLine("Setting cancelation handler");
        Console.CancelKeyPress += (sender, e) => {
            e.Cancel = true;
            cts.Cancel();
        };
    }

    public static void SetKeysFromArgs(this Dictionary<string, string> options, string[] args) {
        foreach (var arg in args) {
            var keyValue = arg.Split('=');
            options[keyValue[0]] = keyValue.Length == 2 ? keyValue[1] : string.Empty;
        }
    }

    public static string GetKey(this Dictionary<string, string> options, string option, string[] validOption, string defaultValue = "") {
        if (!options.TryGetValue(option, out string? optionValue))
            optionValue = defaultValue;

        if (!validOption.Contains(optionValue)) {
            Console.WriteLine($"{Environment.NewLine}Error:");
            Console.WriteLine($"  Invalid value - {option} expect: \"{String.Join("\" or \"", validOption)}\" and received \"{optionValue}\"");
            Environment.Exit(0);
        }

        return optionValue;
    }
}
