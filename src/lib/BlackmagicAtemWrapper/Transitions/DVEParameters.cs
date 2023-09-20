//-----------------------------------------------------------------------------
// <copyright file="DVEParameters.cs">
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
    /// The DVEParameters class is used for manipulating transition settings specific to DVE parameters.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 3.2.7</remarks>
    public class DVEParameters : IBMDSwitcherTransitionDVEParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherMixEffectBlock"/>.
        /// </summary>
        private readonly IBMDSwitcherTransitionDVEParameters InternalDVEParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="DVEParameters"/> class.
        /// </summary>
        /// <param name="dveParameters">The native <seealso cref="IBMDSwitcherTransitionDVEParameters"/> from the BMDSwitcherAPI.</param>
        public DVEParameters(IBMDSwitcherTransitionDVEParameters dveParameters)
        {
            this.InternalDVEParametersReference = dveParameters ?? throw new ArgumentNullException(nameof(dveParameters));
            this.InternalDVEParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DVEParameters"/> class.
        /// </summary>
        ~DVEParameters()
        {
            this.InternalDVEParametersReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalDVEParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="DVEParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="DVEParameters"/> that received the event.</param>
        public delegate void DVEParametersEventHandler(object sender);

        /// <summary>
        /// The <see cref="Rate"/> changed.
        /// </summary>
        public event DVEParametersEventHandler OnRateChanged;

        /// <summary>
        /// The <see cref="LogoRate"/> changed.
        /// </summary>
        public event DVEParametersEventHandler OnLogoRateChanged;

        /// <summary>
        /// The <see cref="Reverse"/> flag changed.
        /// </summary>
        public event DVEParametersEventHandler OnReverseChanged;

        /// <summary>
        /// The <see cref="Flipflop"/> flag changed.
        /// </summary>
        public event DVEParametersEventHandler OnFlipFlopChanged;

        /// <summary>
        /// The <see cref="Style"/> changed.
        /// </summary>
        public event DVEParametersEventHandler OnStyleChanged;

        /// <summary>
        /// The <see cref="FillInput"/> changed.
        /// </summary>
        public event DVEParametersEventHandler OnFillInputChanged;

        /// <summary>
        /// The <see cref="CutInput"/> changed.
        /// </summary>
        public event DVEParametersEventHandler OnCutInputChanged;

        /// <summary>
        /// The <see cref="EnableKey"/> flag changed.
        /// </summary>
        public event DVEParametersEventHandler OnEnableKeyChanged;

        /// <summary>
        /// The <see cref="IsPreMultiplied"/> flag changed.
        /// </summary>
        public event DVEParametersEventHandler OnPreMultipliedChanged;

        /// <summary>
        /// The <see cref="Clip"/> changed.
        /// </summary>
        public event DVEParametersEventHandler OnClipChanged;

        /// <summary>
        /// The <see cref="Gain"/> changed.
        /// </summary>
        public event DVEParametersEventHandler OnGainChanged;

        /// <summary>
        /// The <see cref="IsInverse"/> flag changed.
        /// </summary>
        public event DVEParametersEventHandler OnInverseChanged;
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
        /// Gets or sets the logo rate in frames.
        /// </summary>
        public uint LogoRate
        {
            get { return this.GetLogoRate(); }
            set { this.SetLogoRate(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the reverse flag is set.
        /// </summary>
        public bool Reverse
        {
            get { return this.GetReverse(); }
            set { this.SetReverse(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the flipflop flag is set.
        /// </summary>
        public bool Flipflop
        {
            get { return this.GetFlipFlop(); }
            set { this.SetFlipFlop(value); }
        }

        /// <summary>
        /// Gets or sets the current DVE transition style.
        /// </summary>
        public _BMDSwitcherDVETransitionStyle Style
        {
            get { return this.GetStyle(); }
            set { this.SetStyle(value); }
        }

        /// <summary>
        /// Gets a list of the supported transition styles.
        /// </summary>
        public _BMDSwitcherDVETransitionStyle[] SupportedStyles
        {
            get { return this.GetSupportedStyles(); }
        }

        /// <summary>
        /// Gets or sets the current fill input.
        /// </summary>
        public long FillInput
        {
            get { return this.GetInputFill(); }
            set { this.SetInputFill(value); }
        }

        /// <summary>
        /// Gets or sets the current cut input.
        /// </summary>
        public long CutInput
        {
            get { return this.GetInputCut(); }
            set { this.SetInputCut(value); }
        }

        /// <summary>
        /// Gets the availability mask for the fill input.
        /// </summary>
        public _BMDSwitcherInputAvailability FillInputAvailabilityMask
        {
            get { return this.GetFillInputAvailabilityMask(); }
        }

        /// <summary>
        /// Gets the availability mask for the cut input.
        /// </summary>
        public _BMDSwitcherInputAvailability CutInputAvailabilityMask
        {
            get { return this.GetCutInputAvailabilityMask(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the enable key is set.
        /// </summary>
        public bool EnableKey
        {
            get { return this.GetEnableKey(); }
            set { this.SetEnableKey(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the pre-multiplied key is set.
        /// </summary>
        public bool IsPreMultiplied
        {
            get { return this.GetPreMultiplied(); }
            set { this.SetPreMultiplied(value); }
        }

        /// <summary>
        /// Gets ir sets the current clip value.
        /// </summary>
        public double Clip
        {
            get { return this.GetClip(); }
            set { this.SetClip(value); }
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
        /// Gets or sets a value indcating whether the inverse flag is set.
        /// </summary>
        public bool IsInverse
        {
            get { return this.GetInverse(); }
            set { this.SetInverse(value); }
        }
        #endregion

        #region IBMDSwitcherTransitionDVEParameters
        /// <summary>
        /// The GetRate method returns the current rate in frames.
        /// </summary>
        /// <returns>The current rate.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.1</remarks>
        public uint GetRate()
        {
            this.InternalDVEParametersReference.GetRate(out uint frameRate);
            return frameRate;
        }

        /// <summary>
        /// The SetRate method sets the rate in frames.
        /// </summary>
        /// <param name="frameRate">The desired rate in frames.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.2</remarks>
        public void SetRate(uint frameRate)
        {
            try
            {
                this.InternalDVEParametersReference.SetRate(frameRate);
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
        /// The GetLogoRate method returns the current logo rate in frames.
        /// </summary>
        /// <returns>The current logo rate.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.3</remarks>
        public uint GetLogoRate()
        {
            this.InternalDVEParametersReference.GetLogoRate(out uint frames);
            return frames;
        }

        /// <summary>
        /// The SetLogoRate method sets the logo rate in frames.
        /// </summary>
        /// <param name="frames">The desired logo rate in frames.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.4</remarks>
        public void SetLogoRate(uint frames)
        {
            try
            {
                this.InternalDVEParametersReference.SetLogoRate(frames);
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
        /// The GetReverse method returns the current reverse flag.
        /// </summary>
        /// <returns>The current reverse flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.5</remarks>
        public bool GetReverse()
        {
            this.InternalDVEParametersReference.GetReverse(out int reverse);
            return Convert.ToBoolean(reverse);
        }

        /// <summary>
        /// The SetReverse method sets the reverse flag.
        /// </summary>
        /// <param name="reverse">The desired reverse flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.6</remarks>
        public void SetReverse(bool reverse)
        {
            try
            {
                this.InternalDVEParametersReference.SetReverse(Convert.ToInt32(reverse));
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
        /// The GetFlipFlop method returns the current flip flop flag.
        /// </summary>
        /// <returns>The current flip flop flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.7</remarks>
        public bool GetFlipFlop()
        {
            this.InternalDVEParametersReference.GetFlipFlop(out int flipflop);
            return Convert.ToBoolean(flipflop);
        }

        /// <summary>
        /// The SetFlipFlop method sets the flip flop flag.
        /// </summary>
        /// <param name="flipflop">The desired flip flop flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.8</remarks>
        public void SetFlipFlop(bool flipflop)
        {
            try
            {
                this.InternalDVEParametersReference.SetFlipFlop(Convert.ToInt32(flipflop));
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
        /// The GetStyle method returns the current style.
        /// </summary>
        /// <returns>The current style.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.9</remarks>
        public _BMDSwitcherDVETransitionStyle GetStyle()
        {
            this.InternalDVEParametersReference.GetStyle(out _BMDSwitcherDVETransitionStyle style);
            return style;
        }

        /// <summary>
        /// The SetStyle method sets the style.
        /// </summary>
        /// <param name="style">The desired style.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.10</remarks>
        public void SetStyle(_BMDSwitcherDVETransitionStyle style)
        {
            try
            {
                this.InternalDVEParametersReference.SetStyle(style);
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
        /// The DoesSupportStyle method determines if the connected device supports a given DVE transition style.
        /// </summary>
        /// <param name="style">The DVE style to check.</param>
        /// <returns>Boolean status of the requested DVE transition style support.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.11</remarks>
        public bool DoesSupportStyle(_BMDSwitcherDVETransitionStyle style)
        {
            this.InternalDVEParametersReference.DoesSupportStyle(style, out int supported);
            return Convert.ToBoolean(supported);
        }

        /// <summary>
        /// The GetNumSupportedStyles method determines the total number of supported DVE transition styles in the connected device.
        /// </summary>
        /// <returns>Total number of DVE transition styles supported.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.12</remarks>
        public uint GetNumSupportedStyles()
        {
            this.InternalDVEParametersReference.GetNumSupportedStyles(out uint numSupportedStyles);
            return numSupportedStyles;
        }

        /// <summary>
        /// The GetSupportedStyles method retrieves a list of supported DVE transition styles supported by the connected device.
        /// </summary>
        /// <returns>List of supported DVE transition styles.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.13</remarks>
        public _BMDSwitcherDVETransitionStyle[] GetSupportedStyles()
        {
            uint numStyles = this.GetNumSupportedStyles();
            _BMDSwitcherDVETransitionStyle[] supportedStyles = new _BMDSwitcherDVETransitionStyle[numStyles - 5];
            this.InternalDVEParametersReference.GetSupportedStyles(out supportedStyles[0], numStyles);

            return supportedStyles;
        }

        /// <summary>
        /// The GetInputFill method returns the current fill input.
        /// </summary>
        /// <returns>The current fill input.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.14</remarks>
        public long GetInputFill()
        {
            this.InternalDVEParametersReference.GetInputFill(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputFill method sets the fill input.
        /// </summary>
        /// <param name="input">The desired fill input.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.15</remarks>
        public void SetInputFill(long input)
        {
            try
            {
                this.InternalDVEParametersReference.SetInputFill(input);
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
        /// The GetInputCut method returns the current cut input.
        /// </summary>
        /// <returns>The current cut input.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.16</remarks>
        public long GetInputCut()
        {
            this.InternalDVEParametersReference.GetInputCut(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputCut method sets the cut input.
        /// </summary>
        /// <param name="input">The desired cut input.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.17</remarks>
        public void SetInputCut(long input)
        {
            try
            {
                this.InternalDVEParametersReference.SetInputCut(input);
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
        /// The GetFillInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask value for fill inputs available to this DVE transition.The input availability property of an IBMDSwitcherInput can be bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this input is available for use as a fill input for this DVE transition.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.18</remarks>
        public _BMDSwitcherInputAvailability GetFillInputAvailabilityMask()
        {
            this.InternalDVEParametersReference.GetFillInputAvailabilityMask(out _BMDSwitcherInputAvailability mask);
            return mask;
        }

        /// <summary>
        /// The GetCutInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask value for cut inputs available to this DVE transition.The input availability property of an IBMDSwitcherInput can be bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this input is available for use as a cut input for this DVE transition.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.19</remarks>
        public _BMDSwitcherInputAvailability GetCutInputAvailabilityMask()
        {
            this.InternalDVEParametersReference.GetCutInputAvailabilityMask(out _BMDSwitcherInputAvailability mask);
            return mask;
        }

        /// <summary>
        /// The GetEnableKey method returns the current enableKey flag.
        /// </summary>
        /// <returns>The current enableKey flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.20</remarks>
        public bool GetEnableKey()
        {
            this.InternalDVEParametersReference.GetEnableKey(out int enableKey);
            return Convert.ToBoolean(enableKey);
        }

        /// <summary>
        /// The SetEnableKey method sets the enableKey flag.
        /// </summary>
        /// <param name="enableKey">The desired enableKey flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.21</remarks>
        public void SetEnableKey(bool enableKey)
        {
            try
            {
                this.InternalDVEParametersReference.SetEnableKey(Convert.ToInt32(enableKey));
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
        /// The GetPreMultiplied method returns the current pre-multiplied flag.
        /// </summary>
        /// <returns>The desired enableKey flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.22</remarks>
        public bool GetPreMultiplied()
        {
            this.InternalDVEParametersReference.GetPreMultiplied(out int preMultiplied);
            return Convert.ToBoolean(preMultiplied);
        }

        /// <summary>
        /// The SetPreMultiplied method sets the pre-multiplied flag.
        /// </summary>
        /// <param name="preMultiplied">The desired pre-multiplied flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.23</remarks>
        /// <bug>Title and description refer to GetPreMultiplied instead of SetPreMultiplied</bug>
        public void SetPreMultiplied(bool preMultiplied)
        { 
            try
            {
                this.InternalDVEParametersReference.SetPreMultiplied(Convert.ToInt32(preMultiplied));
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
        /// The SetClip method sets the clip value.
        /// </summary>
        /// <returns>The current clip value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.24</remarks>
        public double GetClip()
        {
            this.InternalDVEParametersReference.GetClip(out double clip);
            return clip;
        }

        /// <summary>
        /// The SetClip method sets the clip value.
        /// </summary>
        /// <param name="clip">The desired clip value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.25</remarks>
        public void SetClip(double clip)
        { 
            try
            {
                this.InternalDVEParametersReference.SetClip(clip);
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
        /// The GetGain method returns the current clip.
        /// </summary>
        /// <returns>The current gain.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.26</remarks>
        public double GetGain()
        {
            this.InternalDVEParametersReference.GetGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetGain method sets the gain.
        /// </summary>
        /// <param name="gain">The desired gain.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.27</remarks>
        public void SetGain(double gain)
        { 
            try
            {
                this.InternalDVEParametersReference.SetGain(gain);
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
        /// The GetInverse method returns the current inverse flag.
        /// </summary>
        /// <returns>The current inverse flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.28</remarks>
        public bool GetInverse()
        {
            this.InternalDVEParametersReference.GetInverse(out int inverse);
            return Convert.ToBoolean(inverse);
        }

        /// <summary>
        /// The SetInverse method sets the inverse flag.
        /// </summary>
        /// <param name="inverse">The desired inverse flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.29</remarks>
        public void SetInverse(bool inverse)
        { 
            try
            {
                this.InternalDVEParametersReference.SetInverse(Convert.ToInt32(inverse));
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

        #region IBMDSwitcherTransitionDVEParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherTransitionDVEParameters events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherTransitionDVEParametersEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 3.2.8.1</remarks>
        void IBMDSwitcherTransitionDVEParametersCallback.Notify(_BMDSwitcherTransitionDVEParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeRateChanged:
                    this.OnRateChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeLogoRateChanged:
                    this.OnLogoRateChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeReverseChanged:
                    this.OnReverseChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeFlipFlopChanged:
                    this.OnFlipFlopChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeStyleChanged:
                    this.OnStyleChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeInputFillChanged:
                    this.OnFillInputChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeInputCutChanged:
                    this.OnCutInputChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeEnableKeyChanged:
                    this.OnEnableKeyChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypePreMultipliedChanged:
                    this.OnPreMultipliedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeClipChanged:
                    this.OnClipChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeGainChanged:
                    this.OnGainChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionDVEParametersEventType.bmdSwitcherTransitionDVEParametersEventTypeInverseChanged:
                    this.OnInverseChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
