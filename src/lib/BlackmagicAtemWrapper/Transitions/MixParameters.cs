//-----------------------------------------------------------------------------
// <copyright file="MixParameters.cs">
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

namespace BlackmagicAtemWrapper.Transitions
{
    using System;
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The MixParameters class is used for manipulating transition settings specific to mix parameters.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 3.2.1</remarks>
    public class MixParameters : IBMDSwitcherTransitionMixParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherMixEffectBlock"/>.
        /// </summary>
        private readonly IBMDSwitcherTransitionMixParameters InternalMixParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="MixParameters"/> class.
        /// </summary>
        /// <param name="mixParameters">The native <seealso cref="IBMDSwitcherTransitionMixParameters"/> from the BMDSwitcherAPI.</param>
        public MixParameters(IBMDSwitcherTransitionMixParameters mixParameters)
        {
            this.InternalMixParametersReference = mixParameters ?? throw new ArgumentNullException(nameof(mixParameters));
            this.InternalMixParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MixParameters"/> class.
        /// </summary>
        ~MixParameters()
        {
            this.InternalMixParametersReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalMixParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="MixParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="MixParameters"/> that received the event.</param>
        public delegate void MixParametersEventHandler(object sender);

        /// <summary>
        /// The <see cref="Rate"/> changed.
        /// </summary>
        public event MixParametersEventHandler OnRateChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current rate in frames.
        /// </summary>
        public uint Rate
        {
            get { return this.GetRate(); }
            set { this.SetRate(value); }
        }
        #endregion

        #region IBMDSwitcherTransitionMixParameters
        /// <summary>
        /// The GetRate method returns the current rate in frames.
        /// </summary>
        /// <returns>The current rate.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.1.1</remarks>
        public uint GetRate()
        {
            this.InternalMixParametersReference.GetRate(out uint frameRate);
            return frameRate;
        }

        /// <summary>
        /// The SetRate method sets the rate in frames.
        /// </summary>
        /// <param name="frameRate">The desired rate in frames.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.1.2</remarks>
        public void SetRate(uint frameRate)
        {
            try
            {
                this.InternalMixParametersReference.SetRate(frameRate);
                return;
            }
            catch (COMException e)
            {
                if (FailedException.IsFailedException(e.ErrorCode))
                {
                    throw new FailedException(e);
                }

                throw;
            }
        }
        #endregion

        #region IBMDSwitcherTransitionMixParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherTransitionMixParameters events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherTransitionMixParametersEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 3.2.2.1</remarks>
        void IBMDSwitcherTransitionMixParametersCallback.Notify(_BMDSwitcherTransitionMixParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherTransitionMixParametersEventType.bmdSwitcherTransitionMixParametersEventTypeRateChanged:
                    this.OnRateChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
