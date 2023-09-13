using BMDSwitcherAPI;
using System;
using System.Runtime.InteropServices;

namespace BlackmagicAtemWrapper
{
    public class SwitcherKeyFlyParameters : IDisposable
    {
        private readonly IBMDSwitcherKeyFlyParameters skpp;

        public SwitcherKeyFlyParameters(IBMDSwitcherKeyFlyParameters skpp) => this.skpp = skpp;

        public void Dispose()
        {
            Marshal.ReleaseComObject(skpp);
        }

        public double GetSizeX()
        {
            skpp.GetSizeX(out double multiplierX);
            return multiplierX;
        }

        public void SetSizeX(double multiplierX)
        {
            skpp.SetSizeX(multiplierX);
            return;
        }

        public double GetSizeY()
        {
            skpp.GetSizeY(out double multiplierY);
            return multiplierY;
        }

        public void SetSizeY(double multiplierY)
        {
            skpp.SetSizeX(multiplierY);
            return;
        }

        public double SizeX
        {
            get { return this.GetSizeX(); }
            set { this.SetSizeX(value); }
        }

        public double SizeY
        {
            get { return this.GetSizeY(); }
            set { this.SetSizeY(value); }
        }

    }
}