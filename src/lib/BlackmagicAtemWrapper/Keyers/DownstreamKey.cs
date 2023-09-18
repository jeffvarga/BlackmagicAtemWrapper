//-----------------------------------------------------------------------------
// <copyright file="DownstreamKey.cs">
//   Copyright (c) 2023 Jeff Varga
//
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in all
//   copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//   SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------------

namespace BlackmagicAtemWrapper.Keyers
{
    using System;
    using System.Runtime.InteropServices;
    using BMDSwitcherAPI;

    /// <summary>
    ///  The DownstreamKey class is used for managing the settings of a downstream key.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 5.2.19</remarks>
    public class DownstreamKey : IBMDSwitcherDownstreamKeyCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherDownstreamKey"/>
        /// </summary>
        private readonly IBMDSwitcherDownstreamKey InternalDownstreamKeyReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownstreamKey" /> class.
        /// </summary>
        /// <param name="downstreamKey">The native <seealso cref="IBMDSwitcherDownstreamKey"/> from the BMDSwitcherAPI.</param>
        public DownstreamKey(IBMDSwitcherDownstreamKey downstreamKey)
        {
            this.InternalDownstreamKeyReference = downstreamKey;
            this.InternalDownstreamKeyReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DownstreamKey"/> class.
        /// </summary>
        ~DownstreamKey()
        {
            this.InternalDownstreamKeyReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalDownstreamKeyReference);
        }

        void IBMDSwitcherDownstreamKeyCallback.Notify(_BMDSwitcherDownstreamKeyEventType eventType)
        {
            throw new NotImplementedException();
        }
    }
}
