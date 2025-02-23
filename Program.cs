// // See https://aka.ms/new-console-template for more information

using TrafficLight;

var cts = new CancellationTokenSource();
cts.SetCancelKeyPress();

await CommandLineHandler.Handle(args, cts);

Console.WriteLine($"{Environment.NewLine}Disponsing cancelation source");
cts.Dispose();

Console.WriteLine("Finishing gracefully");
Environment.Exit(0);
