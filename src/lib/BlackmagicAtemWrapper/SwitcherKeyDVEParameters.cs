using BMDSwitcherAPI;
using System;
using System.Runtime.InteropServices;

namespace BlackmagicAtemWrapper
{
    public class SwitcherKeyDVEParameters : IDisposable
    {
        private readonly IBMDSwitcherKeyDVEParameters skdp;

        public SwitcherKeyDVEParameters(IBMDSwitcherKeyDVEParameters skdp) => this.skdp = skdp;

        public void Dispose()
        {
            Marshal.ReleaseComObject(skdp);
        }

        public bool GetShadow()
        {
            skdp.GetShadow(out int shadowOn);
            return shadowOn != 0;
        }

        public bool Shadow
        {
            get { return this.GetShadow(); }
        }
    }
}