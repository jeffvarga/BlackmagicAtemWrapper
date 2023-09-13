using BMDSwitcherAPI;
using System;
using System.Runtime.InteropServices;

namespace BlackmagicAtemWrapper.video
{
    public class SwitcherKeyPatternParameters : IDisposable
    {
        private readonly IBMDSwitcherKeyPatternParameters skpp;

        public SwitcherKeyPatternParameters(IBMDSwitcherKeyPatternParameters skpp) => this.skpp = skpp;

        public void Dispose()
        {
            Marshal.ReleaseComObject(skpp);
        }

        /// <summary>
        /// The GetPattern method gets the current pattern style.
        /// </summary>
        /// <returns>The current pattern style of BMDSwitcherPatternStyle.</returns>
        public _BMDSwitcherPatternStyle GetPattern()
        {
            skpp.GetPattern(out _BMDSwitcherPatternStyle pattern);
            return pattern;
        }

        /// <summary>
        /// The SetPattern method sets the pattern style.
        /// </summary>
        /// <param name="pattern">The desired BMDSwitcherPatternStyle pattern style.</param>
        public void SetPattern(_BMDSwitcherPatternStyle pattern)
        {
            skpp.SetPattern(pattern);
            return;
        }

        #region Properties
        public _BMDSwitcherPatternStyle Pattern
        {
            get { return this.GetPattern(); }
            set { this.SetPattern(value); }
        }
        #endregion
    }
}