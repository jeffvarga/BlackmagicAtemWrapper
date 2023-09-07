namespace AtemMiniTest
{
    using BMD=BlackmagicAtemWrapper;

    class Program
    {
        static void Main(string[] args)
        {
            BMD.Switcher switcher = BMD.Discovery.Connect("");

            foreach (BMD.MixEffectBlock meb in switcher.MixEffectBlocks)
            {
                meb.PerformFadeToBlack();
            }
            System.Threading.Thread.Sleep(1000);

        }
    }
}
