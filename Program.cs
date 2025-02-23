// // See https://aka.ms/new-console-template for more information

using TrafficLight;

var cts = new CancellationTokenSource();
cts.SetCancelKeyPress();

await DictionaryVersionRunner.Run(cts);

Console.WriteLine($"{Environment.NewLine}Disponsing cancelation source");
cts.Dispose();

Console.WriteLine("Finishing gracefully");
Environment.Exit(0);
