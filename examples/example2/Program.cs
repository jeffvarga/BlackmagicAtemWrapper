using BlackmagicAtemWrapper;

// Connect via USB
Switcher switcher = Discovery.ConnectTo();

// Grab the first MixerEffect block
MixEffectBlock meb = switcher.MixEffectBlocks.First();

// Set the preview input to the first input (often black)
meb.PreviewInput = switcher.Inputs.First().InputId;
// Set the program input to the second input (often the first physical input)
meb.ProgramInput = switcher.Inputs.Skip(1).First().InputId;

// Performs an auto transition
meb.PerformAutoTransition();

/// Wait - ATEM will stop transitioning if the API disconnects, as it would without the sleep.
System.Threading.Thread.Sleep(1000);
