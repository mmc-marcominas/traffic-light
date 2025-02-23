using System.Text;
using System.Text.Json;

namespace TrafficLight;

public static class ExtensionMethods {
    public static void SetCancelKeyPress(this CancellationTokenSource cts) {
        Console.WriteLine("Setting cancelation handler");
        Console.CancelKeyPress += (sender, e) => {
            e.Cancel = true;
            cts.Cancel();
        };
    }
}
