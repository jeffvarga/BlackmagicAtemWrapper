//-----------------------------------------------------------------------------
// <copyright file="FairlightAudioEqualizerBand.cs">
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


namespace BlackmagicAtemWrapper.Audio.Effects
{
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Channels;

    /// <summary>
    /// The <see cref="FairlightAudioEqualizerBand"/> class is used for manipulating the Fairlight audio equalizer bands.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 7.5.12</remarks>
    public class FairlightAudioEqualizerBand : IBMDSwitcherFairlightAudioEqualizerBandCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherFairlightAudioEqualizerBand"/>.
        /// </summary>
        private readonly IBMDSwitcherFairlightAudioEqualizerBand InternalFairlightAudioEqualizerBandReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="FairlightAudioEqualizerBand"/> class.
        /// </summary>
        /// <param name="audioMixer">The native <seealso cref="IBMDSwitcherFairlightAudioEqualizerBand"/> from the BMDSwitcherAPI.</param>
        public FairlightAudioEqualizerBand(IBMDSwitcherFairlightAudioEqualizerBand audioMixer)
        {
            this.InternalFairlightAudioEqualizerBandReference = audioMixer ?? throw new System.ArgumentNullException(nameof(audioMixer));
            this.InternalFairlightAudioEqualizerBandReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FairlightAudioEqualizerBand"/> class.
        /// </summary>
        ~FairlightAudioEqualizerBand()
        {
            this.InternalFairlightAudioEqualizerBandReference.RemoveCallback(this);
            Marshal.ReleaseComObject(this.InternalFairlightAudioEqualizerBandReference);
        }

        #region Events
        /// <summary>
        /// Handles a <see cref="FairlightAudioEqualizerBand"/> event.
        /// </summary>
        /// <param name="sender">The object that received the event.</param>
        public delegate void FairlightAudioEqualizerBandEventHandler(object sender);

        /// <summary>
        /// The <see cref="Enabled"/> flag changed.
        /// </summary>
        public event FairlightAudioEqualizerBandEventHandler OnEnabledChanged;

        /// <summary>
        /// The <see cref="Shape"/> changed.
        /// </summary>
        public event FairlightAudioEqualizerBandEventHandler OnShapeChanged;

        /// <summary>
        /// The <see cref="FrequencyRange"/>
        /// </summary>
        public event FairlightAudioEqualizerBandEventHandler OnFrequencyRangeChanged;

        /// <summary>
        /// The <see cref="Frequency"/> value changed.
        /// </summary>
        public event FairlightAudioEqualizerBandEventHandler OnFrequencyChanged;

        /// <summary>
        /// The <see cref="Gain"/> value changed.
        /// </summary>
        public event FairlightAudioEqualizerBandEventHandler OnGainChanged;

        /// <summary>
        /// The <see cref="QFactor"/> value has changed.
        /// </summary>
        public event FairlightAudioEqualizerBandEventHandler OnQFactorChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the equalizer band is enabled.
        /// </summary>
        /// <seealso cref="GetEnabled"/>
        /// <seealso cref="SetEnabled(bool)"/>
        public bool Enabled
        {
            get { return this.GetEnabled(); }
            set { this.SetEnabled(value); }
        }

        /// <summary>
        /// Gets the supported equalizer band shapes.
        /// </summary>
        /// <seealso cref="GetSupportedShapes"/>
        public _BMDSwitcherFairlightAudioEqualizerBandShape SupportedShapes
        {
            get { return this.GetSupportedShapes(); }
        }

        /// <summary>
        /// Gets or sets the current equalizer band shape.
        /// </summary>
        /// <seealso cref="GetShape"/>
        /// <seealso cref="SetShape(_BMDSwitcherFairlightAudioEqualizerBandShape)"/>
        public _BMDSwitcherFairlightAudioEqualizerBandShape Shape
        {
            get { return this.GetShape(); }
            set { this.SetShape(value); }
        }

        /// <summary>
        /// Gets the supported frequency ranges of this band.
        /// </summary>
        /// <seealso cref="GetSupportedFrequencyRanges"/>
        public _BMDSwitcherFairlightAudioEqualizerBandFrequencyRange SupportedFrequencyRanges
        {
            get { return this.GetSupportedFrequencyRanges(); }
        }

        /// <summary>
        /// Gets or sets the current frequency range.
        /// </summary>
        /// <seealso cref="GetFrequencyRange"/>
        /// <seealso cref="SetFrequencyRange(_BMDSwitcherFairlightAudioEqualizerBandFrequencyRange)"/>
        public _BMDSwitcherFairlightAudioEqualizerBandFrequencyRange FrequencyRange
        {
            get { return this.GetFrequencyRange(); }
            set { this.SetFrequencyRange(value); }
        }

        /// <summary>
        /// Gets or sets the equalizer band's frequency
        /// </summary>
        /// <seealso cref="GetFrequency"/>
        /// <seealso cref="SetFrequency(uint)"/>
        public uint Frequency
        {
            get { return this.GetFrequency(); }
            set { this.SetFrequency(value); }
        }

        /// <summary>
        /// Gets or sets the band gain.
        /// </summary>
        /// <seealso cref="GetGain"/>
        /// <seealso cref="SetGain(double)"/>
        public double Gain
        {
            get { return this.GetGain(); }
            set { this.SetGain(value); }
        }

        /// <summary>
        /// Gets or sets the band Q factor
        /// </summary>
        /// <seealso cref="GetQFactor"/>
        /// <seealso cref="SetQFactor(double)"/>
        public double QFactor
        {
            get { return this.GetQFactor(); }
            set { this.SetQFactor(value); }
        }
        #endregion

        #region IBMDSwitcherFairlightAudioEqualizerBand
        /// <summary>
        /// The GetEnabled method returns the current equalizer band enabled flag.
        /// </summary>
        /// <returns>The current equalizer band enabled flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.1</remarks>
        public bool GetEnabled()
        {
            this.InternalFairlightAudioEqualizerBandReference.GetEnabled(out int enabled);
            return Convert.ToBoolean(enabled);
        }

        /// <summary>
        /// The SetEnabled method sets the equalizer band enabled flag.
        /// </summary>
        /// <param name="enabled">The desired equalizer band enabled flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.2</remarks>
        public void SetEnabled(bool enabled)
        { 
            try
            {
                this.InternalFairlightAudioEqualizerBandReference.SetEnabled(Convert.ToInt32(enabled));
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
        /// The GetSupportedShapes method returns the supported equalizer band shapes.
        /// </summary>
        /// <returns>The available shapes.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.3</remarks>
        public _BMDSwitcherFairlightAudioEqualizerBandShape GetSupportedShapes()
        {
            this.InternalFairlightAudioEqualizerBandReference.GetSupportedShapes(out _BMDSwitcherFairlightAudioEqualizerBandShape shapes);
            return shapes;
        }

        /// <summary>
        /// The GetShape method returns the current equalizer band shape.
        /// </summary>
        /// <returns>The current shape.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.4</remarks>
        public _BMDSwitcherFairlightAudioEqualizerBandShape GetShape()
        {
            this.InternalFairlightAudioEqualizerBandReference.GetShape(out _BMDSwitcherFairlightAudioEqualizerBandShape shape);
            return shape;
        }

        /// <summary>
        /// The SetShape method sets the equalizer band shape.
        /// </summary>
        /// <param name="shape">The desired shape.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.5</remarks>
        public void SetShape(_BMDSwitcherFairlightAudioEqualizerBandShape shape)
        { 
            try
            {
                this.InternalFairlightAudioEqualizerBandReference.SetShape(shape);
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
        /// The GetSupportedFrequencyRanges method returns the available frequency ranges for the equalizer band.
        /// </summary>
        /// <returns>The available frequency ranges.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.6</remarks>
        public _BMDSwitcherFairlightAudioEqualizerBandFrequencyRange GetSupportedFrequencyRanges()
        {
            this.InternalFairlightAudioEqualizerBandReference.GetSupportedFrequencyRanges(out _BMDSwitcherFairlightAudioEqualizerBandFrequencyRange ranges);
            return ranges;
        }

        /// <summary>
        /// The GetFrequencyRange method returns the current frequency range of the equalizer band.
        /// </summary>
        /// <returns>The current frequency range.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.7</remarks>
        public _BMDSwitcherFairlightAudioEqualizerBandFrequencyRange GetFrequencyRange()
        {
            this.InternalFairlightAudioEqualizerBandReference.GetFrequencyRange(out _BMDSwitcherFairlightAudioEqualizerBandFrequencyRange range);
            return range;
        }

        /// <summary>
        /// The SetFrequencyRange method sets the frequency range of the equalizer band.
        /// </summary>
        /// <param name="range">The desired frequency range.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.8</remarks>
        public void SetFrequencyRange(_BMDSwitcherFairlightAudioEqualizerBandFrequencyRange range)
        { 
            try
            {
                this.InternalFairlightAudioEqualizerBandReference.SetFrequencyRange(range);
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
        /// The GetFrequencyRangeMinMax method gets the minimum and maximum frequencies of a specified BMDSwitcherFairlightAudioEqualizerBandFrequencyRange.
        /// </summary>
        /// <param name="range">The desired frequency range.</param>
        /// <param name="minFreq">The current minimum frequency.</param>
        /// <param name="maxFreq">The current maximum frequency.</param>
        /// <exception cref="ArgumentException">The <paramref name="range"/> is not a valid identifier.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.9</remarks>
        public void GetFrequencyRangeMinMax(_BMDSwitcherFairlightAudioEqualizerBandFrequencyRange range, out uint minFreq, out uint maxFreq)
        {
            this.InternalFairlightAudioEqualizerBandReference.GetFrequencyRangeMinMax(range, out minFreq, out maxFreq);
            return;
        }

        /// <summary>
        /// The GetFrequency method returns the current frequency of the equalizer band.
        /// </summary>
        /// <returns>The current frequency.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.10</remarks>
        public uint GetFrequency()
        {
            this.InternalFairlightAudioEqualizerBandReference.GetFrequency(out uint freq);
            return freq;
        }

        /// <summary>
        /// The SetFrequency method sets the frequency of the equalizer band.
        /// </summary>
        /// <param name="freq">The desired frequency.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.11</remarks>
        public void SetFrequency(uint freq)
        { 
            try
            {
                this.InternalFairlightAudioEqualizerBandReference.SetFrequency(freq);
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
        /// The GetGain method returns the current gain of the equalizer band.
        /// </summary>
        /// <returns>The current gain</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.12</remarks>
        public double GetGain()
        {
            this.InternalFairlightAudioEqualizerBandReference.GetGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetGain method sets the gain of the equalizer band.
        /// </summary>
        /// <param name="gain">The desired gain.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.13</remarks>
        public void SetGain(double gain)
        { 
            try
            {
                this.InternalFairlightAudioEqualizerBandReference.SetGain(gain);
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
        /// The GetQFactor method returns the current Q factor of the equalizer band.
        /// </summary>
        /// <returns>The current Q Factor.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.14</remarks>
        public double GetQFactor()
        {
            this.InternalFairlightAudioEqualizerBandReference.GetQFactor(out double value);
            return value;
        }

        /// <summary>
        /// The SetQFactor method sets the Q factor of the equalizer band.
        /// </summary>
        /// <param name="value">The desired Q Factor</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.15</remarks>
        public void SetQFactor(double value)
        { 
            try
            {
                this.InternalFairlightAudioEqualizerBandReference.SetQFactor(value);
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
        /// The Reset method resets the equalizer band to its default state. 
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.12.16</remarks>
        public void Reset()
        { 
            try
            {
                this.InternalFairlightAudioEqualizerBandReference.Reset();
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

        #region IBMDSwitcherFairlightAudioEqualizerBandCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherFairlightAudioEqualizerBand events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherFairlightAudioDynamics ProcessorEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 7.5.13.1</remarks>
        void IBMDSwitcherFairlightAudioEqualizerBandCallback.Notify(_BMDSwitcherFairlightAudioEqualizerBandEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherFairlightAudioEqualizerBandEventType.bmdSwitcherFairlightAudioEqualizerBandEventTypeEnabledChanged:
                    this.OnEnabledChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioEqualizerBandEventType.bmdSwitcherFairlightAudioEqualizerBandEventTypeShapeChanged:
                    this.OnShapeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioEqualizerBandEventType.bmdSwitcherFairlightAudioEqualizerBandEventTypeFrequencyRangeChanged:
                    this.OnFrequencyRangeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioEqualizerBandEventType.bmdSwitcherFairlightAudioEqualizerBandEventTypeFrequencyChanged:
                    this.OnFrequencyChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioEqualizerBandEventType.bmdSwitcherFairlightAudioEqualizerBandEventTypeGainChanged:
                    this.OnGainChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioEqualizerBandEventType.bmdSwitcherFairlightAudioEqualizerBandEventTypeQFactorChanged:
                    this.OnQFactorChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
