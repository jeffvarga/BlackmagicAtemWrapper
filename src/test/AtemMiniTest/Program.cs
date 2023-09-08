namespace AtemMiniTest
{
    using System;
    using BMD=BlackmagicAtemWrapper;

    class Program
    {
        static void Main(string[] args)
        {
            BMD.Switcher switcher = BMD.Discovery.ConnectTo("");

            foreach (BMD.MixEffectBlock meb in switcher.MixEffectBlocks)
            {
                meb.PerformFadeToBlack();
            }

            foreach (BMD.SerialPort sp in switcher.SerialPorts)
            {
                Console.WriteLine("Got a serial port!");
            }
            System.Threading.Thread.Sleep(1000);
        }
    }
}
