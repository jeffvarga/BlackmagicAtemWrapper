//-----------------------------------------------------------------------------
// <copyright file="DipParameters.cs">
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
    /// The DipParameters class is used for manipulating transition settings specific to dip parameters.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 3.2.3</remarks>
    public class DipParameters : IBMDSwitcherTransitionDipParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherMixEffectBlock"/>.
        /// </summary>
        private readonly IBMDSwitcherTransitionDipParameters InternalDipParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="DipParameters"/> class.
        /// </summary>
        /// <param name="dipParameters">The native <seealso cref="IBMDSwitcherTransitionDipParameters"/> from the BMDSwitcherAPI.</param>
        public DipParameters(IBMDSwitcherTransitionDipParameters dipParameters)
        {
            this.InternalDipParametersReference = dipParameters ?? throw new ArgumentNullException(nameof(dipParameters));
            this.InternalDipParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DipParameters"/> class.
        /// </summary>
        ~DipParameters()
        {
            this.InternalDipParametersReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalDipParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="DipParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="DipParameters"/> that received the event.</param>
        public delegate void DipParametersEventHandler(object sender);

        /// <summary>
        /// The <see cref="Rate"/> changed.
        /// </summary>
        public event DipParametersEventHandler OnRateChanged;

        /// <summary>
        /// The <see cref="Input"/> changed.
        /// </summary>
        public event DipParametersEventHandler OnInputChanged;
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

        /// <summary>
        /// Gets or sets the current dip input.
        /// </summary>
        /// <bug>3.2.3 public member functions table does not list Get/SetInputDip</bug>
        public long Input
        {
            get { return this.GetInputDip(); }
            set { this.SetInputDip(value); }
        }
        #endregion

        #region IBMDSwitcherTransitionDipParameters
        /// <summary>
        /// The GetRate method returns the current rate in frames.
        /// </summary>
        /// <returns>The current rate.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.3.1</remarks>
        public uint GetRate()
        {
            this.InternalDipParametersReference.GetRate(out uint frameRate);
            return frameRate;
        }

        /// <summary>
        /// The SetRate method sets the rate in frames.
        /// </summary>
        /// <param name="frameRate">The desired rate in frames.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.3.2</remarks>
        public void SetRate(uint frameRate)
        {
            try
            {
                this.InternalDipParametersReference.SetRate(frameRate);
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

        /// <summary>
        /// The GetInputDip method returns the current dip input.
        /// </summary>
        /// <returns>The current dip input.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.3.3</remarks>
        public long GetInputDip()
        {
            this.InternalDipParametersReference.GetInputDip(out long inputDip);
            return inputDip;
        }

        /// <summary>
        /// The SetInputDip method sets the dip input.
        /// </summary>
        /// <param name="input">The desired dip input.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.3.4</remarks>
        public void SetInputDip(long input)
        { 
            try
            {
                this.InternalDipParametersReference.SetInputDip(input);
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

        #region IBMDSwitcherTransitionDipParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherTransitionDipParameters events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherTransitionDipParametersEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 3.2.4.1</remarks>
        void IBMDSwitcherTransitionDipParametersCallback.Notify(_BMDSwitcherTransitionDipParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherTransitionDipParametersEventType.bmdSwitcherTransitionDipParametersEventTypeRateChanged:
                    this.OnRateChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDipParametersEventType.bmdSwitcherTransitionDipParametersEventTypeInputDipChanged:
                    this.OnInputChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
