using BlackmagicAtemWrapper;

// Connect via USB
Switcher switcher = Discovery.ConnectTo();

// Enumerate all of the inputs
foreach (Input input in switcher.Inputs)
{
    Console.WriteLine($"Input #{input.InputId}");
    Console.WriteLine($"  Short name:      {input.ShortName}");
    Console.WriteLine($"  Long name:       {input.LongName}");
    Console.WriteLine($"  Port type:       {input.PortType}");
    Console.WriteLine($"  Program tallied: {input.IsProgramTallied}");
    Console.WriteLine($"  Preview tallied: {input.IsPreviewTallied}");
}
