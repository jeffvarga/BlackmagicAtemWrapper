namespace AtemMiniTest
{
    using BlackmagicAtemWrapper.device;
    using BMDSwitcherAPI;
    using System;
    using BMD = BlackmagicAtemWrapper;

    class Program
    {
        static void Main(string[] args)
        {
            BMD.Switcher switcher = BMD.Discovery.ConnectTo("");

            try
            {
                Identity identity = switcher.Identity;

                Console.WriteLine("Identity:");
                Console.WriteLine($"  UniqueId: {identity.UniqueId}");
                Console.WriteLine($"  DeviceName: {identity.DeviceName}");
                Console.WriteLine($"  MdnsName: {identity.MdnsName}");
                Console.WriteLine($"  IpAddress: {identity.IpAddress}");
                Console.WriteLine();
            }
            catch { };

            foreach (BMD.MixEffectBlock meb in switcher.MixEffectBlocks)
            {
                meb.PerformFadeToBlack();
                meb.PerformAutoTransition();
            }

            foreach (SerialPort sp in switcher.SerialPorts)
            {
                Console.WriteLine("Got a serial port!");
            }
            System.Threading.Thread.Sleep(1000);
        }
    }
}
