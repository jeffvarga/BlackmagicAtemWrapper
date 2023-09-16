//-----------------------------------------------------------------------------
// <copyright file="KeyChromaParameters.cs">
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
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// <para>The ChromaParameters class is used for manipulating settings specific to the chroma type key.</para>
    /// <para>If a switcher is capable of using advanced chroma key, then this interface will not be available.Only if IBMDSwitcherKey::DoesSupportAdvancedChroma returns false, does the switcher support this IBMDSwitcherKeyChromaParameters interface.</para>
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 5.2.6</remarks>
    public class ChromaParameters : IBMDSwitcherKeyChromaParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherKeyChromaParameters"/>.
        /// </summary>
        private readonly IBMDSwitcherKeyChromaParameters InternalKeyChromaParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaParameters"/> class.
        /// </summary>
        /// <param name="switcherKeyChromaParameters">The native <seealso cref="IBMDSwitcherKeyChromaParameters"/> from the BMDSwitcherAPI.</param>
        public ChromaParameters(IBMDSwitcherKeyChromaParameters switcherKeyChromaParameters)
        {
            this.InternalKeyChromaParametersReference = switcherKeyChromaParameters;
            this.InternalKeyChromaParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ChromaParameters"/> class.
        /// </summary>
        ~ChromaParameters()
        {
            this.InternalKeyChromaParametersReference.RemoveCallback(this);
            Marshal.ReleaseComObject(this.InternalKeyChromaParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="ChromaParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="ChromaParameters"/> that received the event.</param>
        public delegate void KeyChromaParametersEventHandler(object sender);

        /// <summary>
        /// The <see cref="Hue"/> value changed.
        /// </summary>
        public event KeyChromaParametersEventHandler OnHueChanged;

        /// <summary>
        /// The <see cref="Gain"/> value changed.
        /// </summary>
        public event KeyChromaParametersEventHandler OnGainChanged;

        /// <summary>
        /// The <see cref="YSuppress"/> value changed.
        /// </summary>
        public event KeyChromaParametersEventHandler OnYSuppressChanged;

        /// <summary>
        /// The <see cref="Lift"/> value changed.
        /// </summary>
        public event KeyChromaParametersEventHandler OnLiftChanged;

        /// <summary>
        /// The <see cref="IsNarrow"/> flag changed.
        /// </summary>
        public event KeyChromaParametersEventHandler OnNarrowChanged;
        #endregion

        #region Properties
        /// <summary>
        ///  Gets or sets the current hue value.
        /// </summary>
        public double Hue
        {
            get { return this.GetHue(); }
            set { this.SetHue(value); }
        }

        /// <summary>
        /// Gets or sets the current gain value.
        /// </summary>
        public double Gain
        {
            get { return this.GetGain(); }
            set { this.SetGain(value); }
        }

        /// <summary>
        /// Gets or sets the y-suppress value.
        /// </summary>
        public double YSuppress
        {
            get { return this.GetYSuppress(); }
            set { this.SetYSuppress(value); }
        }

        /// <summary>
        /// Gets or sets the current lift value.
        /// </summary>
        public double Lift
        {
            get { return this.GetLift(); }
            set { this.SetLift(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Narrow flag is set.
        /// </summary>
        public bool IsNarrow
        {
            get { return this.GetNarrow(); }
            set { this.SetNarrow(value); }
        }
        #endregion

        #region IBMDSwitcherKeyChromaParameters
        /// <summary>
        /// The GetHue method gets the current hue value.
        /// </summary>
        /// <returns>The current hue value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.6.1</remarks>
        public double GetHue()
        {
            this.InternalKeyChromaParametersReference.GetHue(out double hue);
            return hue;
        }

        /// <summary>
        /// The SetHue method sets the hue value.
        /// </summary>
        /// <param name="hue">The desired hue value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.6.2</remarks>
        public void SetHue(double hue)
        { 
            try
            {
                this.InternalKeyChromaParametersReference.SetHue(hue);
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
        /// The SetGain method sets the gain value.
        /// </summary>
        /// <returns>The desired gain value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.6.3</remarks>
        public double GetGain()
        {
            this.InternalKeyChromaParametersReference.GetGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetGain method sets the gain value.
        /// </summary>
        /// <param name="gain">The desired gain value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.6.4</remarks>
        public void SetGain(double gain)
        {
            try
            {
                this.InternalKeyChromaParametersReference.SetGain(gain);
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
        /// The GetYSuppress method gets the current y-suppress value.
        /// </summary>
        /// <returns>The current y-suppress value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.6.5</remarks>
        public double GetYSuppress()
        {
            this.InternalKeyChromaParametersReference.GetYSuppress(out double ySuppress);
            return ySuppress;
        }

        /// <summary>
        /// The SetYSuppress method sets the y-suppress value.
        /// </summary>
        /// <param name="ySuppress">The desired ySuppress value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.6.6</remarks>
        public void SetYSuppress(double ySuppress)
        {
            try
            {
                this.InternalKeyChromaParametersReference.SetYSuppress(ySuppress);
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
        /// The GetLift method gets the current lift value.
        /// </summary>
        /// <returns>The current lift value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.6.7</remarks>
        public double GetLift()
        {
            this.InternalKeyChromaParametersReference.GetLift(out double lift);
            return lift;
        }

        /// <summary>
        /// The SetLift method sets the lift value.
        /// </summary>
        /// <param name="lift">The desired lift value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.6.8</remarks>
        public void SetLift(double lift)
        {
            try
            {
                this.InternalKeyChromaParametersReference.SetLift(lift);
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
        /// The GetNarrow method gets the current narrow flag. 
        /// </summary>
        /// <returns>The current narrow flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.6.9</remarks>
        public bool GetNarrow()
        {
            this.InternalKeyChromaParametersReference.GetNarrow(out int narrow);
            return Convert.ToBoolean(narrow);
        }

        /// <summary>
        /// The SetNarrow method sets the narrow flag.
        /// </summary>
        /// <param name="narrow">The desired narrow flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.6.10</remarks>
        public void SetNarrow(bool narrow)
        {
            try
            {
                this.InternalKeyChromaParametersReference.SetNarrow(Convert.ToInt32(narrow));
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

        #region IBMDSwitcherKeyChromaParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherKeyChromaParameters events occur, such as property changes.</para>
        /// <para> This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherKeyChromaParametersEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.7.1</remarks>
        void IBMDSwitcherKeyChromaParametersCallback.Notify(_BMDSwitcherKeyChromaParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherKeyChromaParametersEventType.bmdSwitcherKeyChromaParametersEventTypeHueChanged:
                    this.OnHueChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyChromaParametersEventType.bmdSwitcherKeyChromaParametersEventTypeGainChanged:
                    this.OnGainChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyChromaParametersEventType.bmdSwitcherKeyChromaParametersEventTypeYSuppressChanged:
                    this.OnYSuppressChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyChromaParametersEventType.bmdSwitcherKeyChromaParametersEventTypeLiftChanged:
                    this.OnLiftChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyChromaParametersEventType.bmdSwitcherKeyChromaParametersEventTypeNarrowChanged:
                    this.OnNarrowChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
