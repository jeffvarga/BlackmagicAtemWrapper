//-----------------------------------------------------------------------------
// <copyright file="InputSuperSource.cs">
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

namespace BlackmagicAtemWrapper.SuperSource
{
    using System;
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The InputSuperSource object is used for manipulating settings specific to the SuperSource input.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 6.2.1</remarks>
    public class InputSuperSource : IBMDSwitcherInputSuperSourceCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherInputSuperSource"/>.
        /// </summary>
        internal IBMDSwitcherInputSuperSource InternalInputSuperSourceReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputSuperSource"/> class.
        /// </summary>
        /// <param name="inputSuperSource">The native <seealso cref="IBMDSwitcherInputSuperSource"/> from the BMDSwitcherAPI.</param>
        public InputSuperSource(IBMDSwitcherInputSuperSource inputSuperSource)
        {
            this.InternalInputSuperSourceReference = inputSuperSource;
            this.InternalInputSuperSourceReference.AddCallback(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputSuperSource"/> class.
        /// </summary>
        /// <param name="input">The <see cref="Input"/> instance from which to derive the IBMDSwitcherInputSuperSource object.</param>
        /// <exception cref="ArgumentNullException">Caller passed a null <see cref="Input"/>.</exception>
        /// <exception cref="NotSupportedException">Unable to get a reference to <seealso cref="IBMDSwitcherInputSuperSource"/>.</exception>
        internal InputSuperSource(Input input)
        {
            if (null == input) { throw new ArgumentNullException(nameof(input)); }

            this.InternalInputSuperSourceReference = input.InternalSwitcherInputReference as IBMDSwitcherInputSuperSource;

            if (this.InternalInputSuperSourceReference == null)
            {
                throw new NotSupportedException();
            }

            this.InternalInputSuperSourceReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="InputSuperSource"/> class.
        /// </summary>
        ~InputSuperSource()
        {
            this.InternalInputSuperSourceReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalInputSuperSourceReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="InputSuperSource"/>.
        /// </summary>
        /// <param name="sender">The <see cref="InputSuperSource"/> that received the event.</param>
        public delegate void InputSuperSourceEventHandler(object sender);

        /// <summary>
        /// The <see cref="InputFill"/> input changed. 
        /// </summary>
        public event InputSuperSourceEventHandler OnInputFillChanged;

        /// <summary>
        /// The <see cref="InputCut"/> input changed.
        /// </summary>
        public event InputSuperSourceEventHandler OnInputCutChanged;

        /// <summary>
        /// The <see cref="ArtOption"/> option changed.
        /// </summary>
        public event InputSuperSourceEventHandler OnArtOptionChanged;

        /// <summary>
        /// The <see cref="IsPreMultiplied"/> flag changed.
        /// </summary>
        public event InputSuperSourceEventHandler OnPreMultipliedChanged;

        /// <summary>
        /// The <see cref="Clip"/> value changed.
        /// </summary>
        public event InputSuperSourceEventHandler OnClipChanged;

        /// <summary>
        /// The <see cref="Gain"/> changed.
        /// </summary>
        public event InputSuperSourceEventHandler OnGainChanged;

        /// <summary>
        /// The <see cref="IsInverse"/> flag changed.
        /// </summary>
        public event InputSuperSourceEventHandler OnInverseChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current art cut input.
        /// </summary>
        public long InputCut
        {
            get { return this.GetInputCut(); }
            set { this.SetInputCut(value); }
        }

        /// <summary>
        /// Gets or sets the current art fill input.
        /// </summary>
        public long InputFill
        {
            get { return this.GetInputFill(); }
            set { this.SetInputFill(value); }
        }

        /// <summary>
        /// Gets the availability mask for the fill of this input.
        /// </summary>
        public _BMDSwitcherInputAvailability FillInputAvailabilityMask
        {
            get { return this.GetFillInputAvailabilityMask(); }
        }

        /// <summary>
        /// Gets the availability mask for the cut of this input.
        /// </summary>
        public _BMDSwitcherInputAvailability CutInputAvailabilityMask
        {
            get { return this.GetCutInputAvailabilityMask(); }
        }

        /// <summary>
        /// Gets or sets the current art option.
        /// </summary>
        public _BMDSwitcherSuperSourceArtOption ArtOption
        {
            get { return this.GetArtOption(); }
            set { this.SetArtOption(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the pre-multiplied flag is set.
        /// </summary>
        public bool IsPreMultiplied
        {
            get { return this.GetPreMultiplied(); }
            set { this.SetPreMultiplied(value); }
        }

        /// <summary>
        /// Gets or sets the current art clip value.
        /// </summary>
        public double Clip
        {
            get { return this.GetClip(); }
            set { this.SetClip(value); }
        }

        /// <summary>
        /// Gets or sets the current art gain.
        /// </summary>
        public double Gain
        {
            get { return this.GetGain(); }
            set { this.SetGain(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the art inverse flag is set.
        /// </summary>
        public bool IsInverse
        {
            get { return this.GetInverse(); }
            set { this.SetInverse(value); }
        }

        /// <summary>
        /// Gets a value indicating whether the switcher supports the display of borders on the SuperSource.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.17</remarks>
        public bool SupportsBorder
        {
            get
            {
                this.InternalInputSuperSourceReference.SupportsBorder(out int supportsBorder);
                return Convert.ToBoolean(supportsBorder);
            }
        }

        /// <summary>
        /// Gets an object used to enumerate the available supersource boxes for a supersource input.
        /// </summary>
        public SuperSourceBoxCollection SuperSourceBoxes
        {
            get { return new SuperSourceBoxCollection(this.InternalInputSuperSourceReference); }
        }

        /// <summary>
        /// Gets the SuperSourceBorder object.
        /// </summary>
        public SuperSourceBorder SuperSourceBorder
        {
            get { return new SuperSourceBorder(this.InternalInputSuperSourceReference as IBMDSwitcherSuperSourceBorder); }
        }
        #endregion

        #region IBMDSwitcherInputSuperSource
        /// <summary>
        /// The GetInputCut method returns the current art cut input.
        /// </summary>
        /// <returns>The current cut input.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.1</remarks>
        public long GetInputCut()
        {
            this.InternalInputSuperSourceReference.GetInputCut(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputCut method sets the art cut input.
        /// </summary>
        /// <param name="input">The desired cut input.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.2</remarks>
        public void SetInputCut(long input)
        {
            try
            {
                this.InternalInputSuperSourceReference.SetInputCut(input);
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
        /// The GetInputFill method returns the current art fill input.
        /// </summary>
        /// <returns>The current fill input.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.3</remarks>
        public long GetInputFill()
        {
            this.InternalInputSuperSourceReference.GetInputFill(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputFill method sets the art fill input.
        /// </summary>
        /// <param name="input">The desired fill input.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.4</remarks>
        public void SetInputFill(long input)
        {
            try
            {
                this.InternalInputSuperSourceReference.SetInputFill(input);
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
        /// The GetFillInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask value for fill inputs available to this supersource input.The input availability property of an IBMDSwitcherInput can be bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this input is available for use as a fill input for this supersource.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.5</remarks>
        public _BMDSwitcherInputAvailability GetFillInputAvailabilityMask()
        {
            this.InternalInputSuperSourceReference.GetFillInputAvailabilityMask(out _BMDSwitcherInputAvailability mask);
            return mask;
        }

        /// <summary>
        /// The GetCutInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask value for cut inputs available to this supersource input.The input availability property of an IBMDSwitcherInput can be bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this input is available for use as a cut input for this supersource.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.6</remarks>
        public _BMDSwitcherInputAvailability GetCutInputAvailabilityMask()
        {
            this.InternalInputSuperSourceReference.GetCutInputAvailabilityMask(out _BMDSwitcherInputAvailability mask);
            return mask;
        }

        /// <summary>
        /// The GetArtOption method returns the current art option.
        /// </summary>
        /// <returns>The current art option.</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.7</remarks>
        public _BMDSwitcherSuperSourceArtOption GetArtOption()
        {
            try
            {
                this.InternalInputSuperSourceReference.GetArtOption(out _BMDSwitcherSuperSourceArtOption artOption);
                return artOption;
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
        /// The SetArtOption method sets the art option.
        /// </summary>
        /// <param name="artOption">The desired art option.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.8</remarks>
        public void SetArtOption(_BMDSwitcherSuperSourceArtOption artOption)
        {
            try
            {
                this.InternalInputSuperSourceReference.SetArtOption(artOption);
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
        /// The GetPreMultiplied method returns the current art pre-multiplied flag.
        /// </summary>
        /// <returns>The current pre-multiplied flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.9</remarks>
        public bool GetPreMultiplied()
        {
            this.InternalInputSuperSourceReference.GetPreMultiplied(out int preMultiplied);
            return Convert.ToBoolean(preMultiplied);
        }

        /// <summary>
        /// The SetPreMultiplied method sets the art pre-multiplied flag.
        /// </summary>
        /// <param name="preMultiplied">The desired pre-multiplied flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.10</remarks>
        public void SetPreMultiplied(bool preMultiplied)
        {
            try
            {
                this.InternalInputSuperSourceReference.SetPreMultiplied(Convert.ToInt32(preMultiplied));
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
        /// The GetClip method returns the current art clip value
        /// </summary>
        /// <returns>The current clip value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.11</remarks>
        public double GetClip()
        {
            this.InternalInputSuperSourceReference.GetClip(out double clip);
            return clip;
        }

        /// <summary>
        /// The SetClip method sets the art clip value.
        /// </summary>
        /// <param name="clip">The desired clip value</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.12</remarks>
        public void SetClip(double clip)
        {
            try
            {
                this.InternalInputSuperSourceReference.SetClip(clip);
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
        /// The GetGain method returns the current art gain.
        /// </summary>
        /// <returns>The current gain.</returns>
        public double GetGain()
        {
            this.InternalInputSuperSourceReference.GetGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetGain method sets the art gain.
        /// </summary>
        /// <param name="gain">The desired gain.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.14</remarks>
        public void SetGain(double gain)
        {
            try
            {
                this.InternalInputSuperSourceReference.SetGain(gain);
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
        /// The GetInverse method returns the current art inverse flag.
        /// </summary>
        /// <returns>The current inverse flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.15</remarks>
        public bool GetInverse()
        {
            this.InternalInputSuperSourceReference.GetInverse(out int inverse);
            return Convert.ToBoolean(inverse);
        }

        /// <summary>
        /// The SetInverse method sets the art inverse flag.
        /// </summary>
        /// <param name="inverse">The desired inverse flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.1.16</remarks>
        public void SetInverse(bool inverse)
        {
            try
            {
                this.InternalInputSuperSourceReference.SetInverse(Convert.ToInt32(inverse));
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

        #region IBMDSwitcherInputSuperSourceCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherInputSuperSource events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherInputSuperSourceEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 6.2.2.1</remarks>
        void IBMDSwitcherInputSuperSourceCallback.Notify(_BMDSwitcherInputSuperSourceEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeInputFillChanged:
                    this.OnInputFillChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeInputCutChanged:
                    this.OnInputCutChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeArtOptionChanged:
                    this.OnArtOptionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypePreMultipliedChanged:
                    this.OnPreMultipliedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeClipChanged:
                    this.OnClipChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeGainChanged:
                    this.OnGainChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputSuperSourceEventType.bmdSwitcherInputSuperSourceEventTypeInverseChanged:
                    this.OnInverseChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
