//-----------------------------------------------------------------------------
// <copyright file="FairlightAudioEqualizer.cs">
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

    /// <summary>
    /// The <see cref="FairlightAudioEqualizer"/> class is used for manipulating the Fairlight audio equalizer interface.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 7.5.9</remarks>
    public class FairlightAudioEqualizer : IBMDSwitcherFairlightAudioEqualizerCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherFairlightAudioEqualizer"/>.
        /// </summary>
        private readonly IBMDSwitcherFairlightAudioEqualizer InternalFairlightAudioEqualizerReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="FairlightAudioEqualizer"/> class.
        /// </summary>
        /// <param name="audioMixer">The native <seealso cref="IBMDSwitcherFairlightAudioEqualizer"/> from the BMDSwitcherAPI.</param>
        public FairlightAudioEqualizer(IBMDSwitcherFairlightAudioEqualizer audioMixer)
        {
            this.InternalFairlightAudioEqualizerReference = audioMixer ?? throw new System.ArgumentNullException(nameof(audioMixer));
            this.InternalFairlightAudioEqualizerReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FairlightAudioEqualizer"/> class.
        /// </summary>
        ~FairlightAudioEqualizer()
        {
            this.InternalFairlightAudioEqualizerReference.RemoveCallback(this);
            Marshal.ReleaseComObject(this.InternalFairlightAudioEqualizerReference);
        }

        #region Events
        /// <summary>
        /// Handles a <see cref="FairlightAudioEqualizer"/> event.
        /// </summary>
        /// <param name="sender">The object that received the event.</param>
        public delegate void FairlightAudioEqualizerEventHandler(object sender);

        /// <summary>
        /// The <see cref="Enabled"/> flag changed.
        /// </summary>
        public event FairlightAudioEqualizerEventHandler OnEnabledChanged;

        /// <summary>
        /// The <see cref="Gain"/> value changed.
        /// </summary>
        public event FairlightAudioEqualizerEventHandler OnGainChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the equalizer is enabled.
        /// </summary>
        /// <seealso cref="GetEnabled"/>
        /// <seealso cref="SetEnabled(bool)"/>
        public bool Enabled
        {
            get { return this.GetEnabled(); }
            set { this.SetEnabled(value); }
        }

        /// <summary>
        /// Gets or sets the equalizer gain.
        /// </summary>
        /// <seealso cref="GetGain"/>
        /// <seealso cref="SetGain(double)"/>
        public double Gain
        {
            get { return this.GetGain(); }
            set { this.SetGain(value); }
        }

        /// <summary>
        /// Gets the associated equalizer bands.
        /// </summary>
        public FairlightAudioEqualizerBandCollection EqualizerBands => new(this.InternalFairlightAudioEqualizerReference);
        #endregion

        #region IBMDSwitcherFairlightAudioEqualizer
        /// <summary>
        /// The GetEnabled method returns the current equalizer enabled flag.
        /// </summary>
        /// <returns>The current equalizer enabled flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.9.1</remarks>
        public bool GetEnabled()
        {
            this.InternalFairlightAudioEqualizerReference.GetEnabled(out int enabled);
            return Convert.ToBoolean(enabled);
        }

        /// <summary>
        /// The SetEnabled method sets the equalizer enabled flag.
        /// </summary>
        /// <param name="enabled">The desired equalizer enabled flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.9.2</remarks>
        public void SetEnabled(bool enabled)
        { 
            try
            {
                this.InternalFairlightAudioEqualizerReference.SetEnabled(Convert.ToInt32(enabled));
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
        /// The GetGain method returns the current gain value applied to the Fairlight audio source by the equalizer.
        /// </summary>
        /// <returns>The gain currently applied to the Fairlight audio source by the equalizer.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.9.3</remarks>
        public double GetGain()
        {
            this.InternalFairlightAudioEqualizerReference.GetGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetGain method sets the gain value to apply to the Fairlight audio source.
        /// </summary>
        /// <param name="gain">The gain to apply to the Fairlight audio source by the equalizer.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.9.4</remarks>
        public void SetGain(double gain)
        { 
            try
            {
                this.InternalFairlightAudioEqualizerReference.SetGain(gain);
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
        /// The Reset method resets the equalizer to its default state.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.9.5</remarks>
        public void Reset()
        { 
            try
            {
                this.InternalFairlightAudioEqualizerReference.Reset();
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

        #region IBMDSwitcherFairlightAudioEqualizerCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherFairlightAudioEqualizer events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherFairlightAudioDynamics ProcessorEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 7.5.10.1</remarks>
        void IBMDSwitcherFairlightAudioEqualizerCallback.Notify(_BMDSwitcherFairlightAudioEqualizerEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherFairlightAudioEqualizerEventType.bmdSwitcherFairlightAudioEqualizerEventTypeEnabledChanged:
                    this.OnEnabledChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioEqualizerEventType.bmdSwitcherFairlightAudioEqualizerEventTypeGainChanged:
                    this.OnGainChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
