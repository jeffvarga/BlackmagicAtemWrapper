// Crude switcher.  Instantly swaps the program output of the switcher based on the numeric input number.

using BlackmagicAtemWrapper;

Switcher switcher = Discovery.ConnectTo();
MixEffectBlock meb = switcher.MixEffectBlocks.First();

meb.OnProgramInputChanged += new MixEffectBlock.MixEventBlockEventHandler(o =>
{
    Console.WriteLine("* {0}Current program input: {1}", meb.IsInFadeToBlack ? "[FTB On] " : "", meb.ProgramInput);
});

meb.OnInFadeToBlackChanged += new MixEffectBlock.MixEventBlockEventHandler(o => {
    Console.WriteLine($"* FTB changed: {meb.IsInFadeToBlack}");
});

Console.WriteLine("Press a number to cut to that input.  Press ` to toggle FTB.  Press [esc] or q to quit.");

while (true)
{
    ConsoleKeyInfo cki = Console.ReadKey(true);

    if (cki.Key == ConsoleKey.Escape || cki.Key == ConsoleKey.Q)
    {
        break;
    }

    if (cki.KeyChar >= '0' && cki.KeyChar <= '9')
    {
        try
        {
            meb.ProgramInput = (long)(cki.KeyChar - '0');
            continue;
        }
        catch (ArgumentException) { }
    }

    if (cki.KeyChar == '`')
    {
        meb.PerformFadeToBlack();
        continue;
    }
}
