﻿namespace AtemMiniTest
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

            Console.WriteLine("Downstream Keys");
            foreach (BMD.Keyers.DownstreamKey dsk in switcher.DownstreamKeys)
            {
                Console.WriteLine("  Key:");
                Console.WriteLine($"    OnAir: {dsk.OnAir}");
                Console.WriteLine($"    InputCut: {dsk.InputCut}");
                Console.WriteLine($"    InputFill: {dsk.InputFill}");
                Console.WriteLine($"    FillInputAvailabilityMask: {dsk.FillInputAvailabilityMask.ToBitString()}");
                Console.WriteLine($"    CutInputAvailabilityMask: {dsk.CutInputAvailabilityMask.ToBitString()}");
                Console.WriteLine($"    Tie: {dsk.Tie}");
                Console.WriteLine($"    Rate: {dsk.Rate}");
                Console.WriteLine($"    IsTransitioning: {dsk.IsTransitioning}");
                Console.WriteLine($"    IsAutoTransitioning: {dsk.IsAutoTransitioning}");
                Console.WriteLine($"    IsTransitionTowardsOnAir: {dsk.IsTransitionTowardsOnAir}");
                Console.WriteLine($"    FramesRemaining: {dsk.FramesRemaining}");
                Console.WriteLine($"    PreMultiplied: {dsk.PreMultiplied}");
                Console.WriteLine($"    Clip: {dsk.Clip}");
                Console.WriteLine($"    Gain: {dsk.Gain}");
                Console.WriteLine($"    IsInverse: {dsk.IsInverse}");
                Console.WriteLine($"    IsMasked: {dsk.IsMasked}");
                Console.WriteLine($"    Mask: (({dsk.MaskLeft}, {dsk.MaskTop}), ({dsk.MaskRight}, {dsk.MaskBottom}))");
            }

            foreach (BMD.MixEffectBlock meb in switcher.MixEffectBlocks)
            {
                Console.WriteLine("MixEffectBlock:");
                Console.WriteLine($"  ProgramInput: {meb.ProgramInput}");
                Console.WriteLine($"  PreviewInput: {meb.PreviewInput}");

                foreach (BMD.Keyers.Key key in meb.SwitcherKeys)
                {
                    Console.WriteLine("    Key:");
                    BMD.Keyers.DVEParameters dp = key.DVEParameters;
                    Console.WriteLine("      DVE:");
                    Console.WriteLine($"        IsBorderEnabled: {dp.IsBorderEnabled}");

                    BMD.Keyers.FlyParameters fp = key.FlyParameters;
                    Console.WriteLine("      Fly:");
                    Console.WriteLine($"        FlyEnabled: {fp.FlyEnabled}");
                    Console.WriteLine($"        CanFly: {fp.CanFly}");
                    Console.WriteLine($"        PositionX: {fp.PositionX}");
                    Console.WriteLine($"        PositionY: {fp.PositionY}");
                    Console.WriteLine($"        SizeX: {fp.SizeX}");
                    Console.WriteLine($"        SizeY: {fp.SizeY}");

                    Console.WriteLine("        KeyFrames:");
                    foreach (_BMDSwitcherFlyKeyFrame keyFrame in typeof(_BMDSwitcherFlyKeyFrame).GetEnumValues())
                    {
                        Console.WriteLine($"          {keyFrame}: {fp.IsKeyFrameStored(keyFrame)}");
                        if (fp.IsAtKeyFrames().HasFlag(keyFrame))
                        {
                            var x = fp.GetKeyFrameParameters(_BMDSwitcherFlyKeyFrame.bmdSwitcherFlyKeyFrameFull);
                        }
                    }

                    Console.WriteLine($"      DoesSupportAdvancedChromaKey: {key.DoesSupportAdvancedChroma}");
                }
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
