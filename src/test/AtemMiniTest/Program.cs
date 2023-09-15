namespace AtemMiniTest
{
    using BlackmagicAtemWrapper.device;
    using BMDSwitcherAPI;
    using System;
    using System.Runtime.InteropServices;
    using BMD = BlackmagicAtemWrapper;
    using Microsoft.Win32;

    public static class ToStringExtension
    {
        public static string ToBitString<T>(this T o)
        {
            string toReturn = Convert.ToString(Convert.ToInt32(o), 2).Replace('0', '.');
            return toReturn.PadLeft(((toReturn.Length-1) / 8 + 1) * 8, '.');
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BMD.Switcher switcher = BMD.Discovery.ConnectTo("10.0.1.226");

            //Guid BMDApi = typeof(BMDSwitcherAPI.CBMDSwitcherDiscovery).GUID;
            //var o = Microsoft.Win32.Registry.GetValue($"HKEY_CLASSES_ROOT\\CLSID\\{{{BMDApi.ToString("D")}}}", null, null);

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
                Console.WriteLine("MixEffectBlock:");
                Console.WriteLine($"  ProgramInput: {meb.ProgramInput}");
                Console.WriteLine($"  PreviewInput: {meb.PreviewInput}");
            }

            foreach (BMD.Input input in switcher.Inputs)
            {
                Console.WriteLine("Inputs:");
                Console.WriteLine($"  InputId: {input.InputId}");
                Console.WriteLine($"  ShortName: {input.ShortName}");
                Console.WriteLine($"  LongName: {input.LongName}");
                Console.WriteLine($"  Input Availability: {input.InputAvailability.ToBitString()}");

                BMD.SuperSource.InputSuperSource ss = input.SuperSource;
                if (ss != null)
                {
                    Console.WriteLine("  SuperSource:");
                    Console.WriteLine($"    ArtOption: {ss.ArtOption}");
                    Console.WriteLine($"    Clip: {ss.Clip}");
                    Console.WriteLine($"    CutInputAvailabilityMask: {ss.CutInputAvailabilityMask.ToBitString()}");
                    Console.WriteLine($"    FillInputAvailabilityMask: {ss.FillInputAvailabilityMask.ToBitString()}");
                    Console.WriteLine($"    Gain: {ss.Gain}");

                    Console.WriteLine("    Boxes:");
                    foreach (BMD.SuperSource.SuperSourceBox box in ss.SuperSourceBoxes)
                    {
                        Console.WriteLine( "      Box:");
                        Console.WriteLine($"        Enabled:            {box.Enabled}");
                        Console.WriteLine($"        InputSource:        {box.InputSource}");
                        Console.WriteLine($"        PositionX:          {box.PositionX}");
                        Console.WriteLine($"        PositionY:          {box.PositionY}");
                        Console.WriteLine($"        Size:               {box.Size}");
                        Console.WriteLine($"        IsCropped:          {box.IsCropped}");
                        Console.WriteLine($"        Crop:               (({box.CropLeft}, {box.CropTop}), ({box.CropRight}, {box.CropBottom}))");
                        Console.WriteLine($"        InputAvailability:  {box.InputAvailability.ToBitString()}");
                    }

                    Console.WriteLine("    Border:");
                    Console.WriteLine($"      IsEnabled:            {ss.SuperSourceBorder.IsEnabled}");
                    Console.WriteLine($"      Bevel:                {ss.SuperSourceBorder.Bevel.ToBitString()}");
                    Console.WriteLine($"      OuterWidth:           {ss.SuperSourceBorder.OuterWidth}");
                    Console.WriteLine($"      InnerWidth:           {ss.SuperSourceBorder.InnerWidth}");
                    Console.WriteLine($"      OuterSoftness:        {ss.SuperSourceBorder.OuterSoftness}");
                    Console.WriteLine($"      InnerSoftness:        {ss.SuperSourceBorder.InnerSoftness}");
                    Console.WriteLine($"      BevelSoftness:        {ss.SuperSourceBorder.BevelSoftness}");
                    Console.WriteLine($"      BevelPosition:        {ss.SuperSourceBorder.BevelPosition}");
                    Console.WriteLine($"      Hue:                  {ss.SuperSourceBorder.Hue}");
                    Console.WriteLine($"      Saturation:           {ss.SuperSourceBorder.Saturation}");
                    Console.WriteLine($"      Luma:                 {ss.SuperSourceBorder.Luma}");
                    Console.WriteLine($"      LightSourceDirection: {ss.SuperSourceBorder.LightSourceDirection}");
                    Console.WriteLine($"      LightSourceAltitude:  {ss.SuperSourceBorder.LightSourceAltitude}");
                }
            }

            foreach (SerialPort sp in switcher.SerialPorts)
            {
                Console.WriteLine("Serial Port:");
                Console.WriteLine($"  Function: {sp.Function}");
            }
            System.Threading.Thread.Sleep(1000);
        }
    }
}
