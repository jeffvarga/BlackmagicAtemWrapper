//-----------------------------------------------------------------------------
// <copyright file="FairlightAudioSource.cs">
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

namespace BlackmagicAtemWrapper.Audio
{
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The FairlightAudioSource class is used for manipulating the settings of an audio source for the Fairlight audio mixer.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 7.5.7</remarks>
    public class FairlightAudioSource : IBMDSwitcherFairlightAudioSourceCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherFairlightAudioSource"/>.
        /// </summary>
        private readonly IBMDSwitcherFairlightAudioSource InternalFairlightAudioSourceReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="FairlightAudioSource"/> class.
        /// </summary>
        /// <param name="fairlightAudioSource">The native <seealso cref="IBMDSwitcherFairlightAudioSource"/> from the BMDSwitcherAPI.</param>
        public FairlightAudioSource(IBMDSwitcherFairlightAudioSource fairlightAudioSource)
        {
            this.InternalFairlightAudioSourceReference = fairlightAudioSource;
            this.InternalFairlightAudioSourceReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the FairlightAudioSource class.
        /// </summary>
        ~FairlightAudioSource()
        {
            this.InternalFairlightAudioSourceReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalFairlightAudioSourceReference);
        }

        #region Events
        /// <summary>
        /// Handles a <see cref="FairlightAudioSource"/> event.
        /// </summary>
        /// <param name="sender">The object that received the event.</param>
        public delegate void FairlightAudioSourceEventHandler(object sender);

        /// <summary>Handles <see cref="IBMDSwitcherFairlightAudioSourceCallback.OutputLevelNotification"/>.</summary>
        /// <param name="sender">The object that received the event.</param>
        /// <param name="numLevels">The number of output levels.</param>
        /// <param name="levels">The current output dB levels.</param>
        /// <param name="numPeakLevels">The number of output peak levels.</param>
        /// <param name="peakLevels">The highest encountered output peak level.</param>
        public delegate void FairlightAudioSourceOutputLevelEventHandler(object sender, uint numLevels, double levels, uint numPeakLevels, double peakLevels);

        /// <summary>
        /// The <see cref="IsActive"/> property of the audio source has changed.
        /// </summary>
        public event FairlightAudioSourceEventHandler OnIsActiveChanged;

        /// <summary>
        /// The <see cref="MaxDelayFrames"/> of the audio source has changed.
        /// </summary>
        public event FairlightAudioSourceEventHandler OnMaxDelayFramesChanged;

        /// <summary>
        /// The <see cref="DelayFrames"/> of the audio source has changed.
        /// </summary>
        public event FairlightAudioSourceEventHandler OnDelayFramesChanged;

        /// <summary>
        /// The <see cref="InputGain"/> of the audio source has changed.
        /// </summary>
        public event FairlightAudioSourceEventHandler OnInputGainChanged;

        /// <summary>
        /// The <see cref="StereoSimulationIntensity"/> of the audio source has changed.
        /// </summary>
        public event FairlightAudioSourceEventHandler OnStereoSimulationIntensityChanged;

        /// <summary>
        /// The <see cref="Pan"/> of the audio source has changed.
        /// </summary>
        public event FairlightAudioSourceEventHandler OnPanChanged;

        /// <summary>
        /// The <see cref="FaderGain"/> of the audio source has changed.
        /// </summary>
        public event FairlightAudioSourceEventHandler OnFaderGainChanged;

        /// <summary>
        /// The <see cref="MixOption"/> of the audio source has changed.
        /// </summary>
        public event FairlightAudioSourceEventHandler OnMixOptionChanged;

        /// <summary>
        /// The <see cref="IsMixedIn"/> of the audio source has changed.
        /// </summary>
        public event FairlightAudioSourceEventHandler OnIsMixedInChanged;

        /// <summary>
        /// <para>The OutputLevelNotification method is called periodically to report the current dB output levels and the last known peak levels.These peak levels can be reset using IBMDSwitcherFairlightAudioSource::ResetOutputPeakLevels.</para>
        /// <para>Note that this is an opt-in subscription.Enable or disable receiving these calls using IBMDSwitcherFairlightAudioMixer::SetAllLevelNotificationsEnabled.</para>
        /// </summary>
        public event FairlightAudioSourceOutputLevelEventHandler OnOutputLevelNotification;
        #endregion

        #region Properties
        /// <summary>
        /// <para>Gets a value indicating whether the Fairlight audio source is currently active.</para>
        /// <para>Audio sources can become inactive when the configuration property of the <see cref="FairlightAudioInput"/> is changed. When a source is not active, it can not be used to manipulate audio on the switcher.</para>
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.1</remarks>
        public bool IsActive
        {
            get
            {
                this.InternalFairlightAudioSourceReference.IsActive(out int active);
                return Convert.ToBoolean(active);
            }
        }

        /// <summary>
        /// Gets a value indicating the current source type.
        /// </summary>
        public _BMDSwitcherFairlightAudioSourceType SourceType
        {
            get { return this.GetSourceType(); }
        }

        /// <summary>
        /// Gets the maximum delay frames.
        /// </summary>
        public ushort MaxDelayFrames
        {
            get { return this.GetMaxDelayFrames(); }
        }

        /// <summary>
        /// Gets or sets the current delay frames.
        /// </summary>
        public ushort DelayFrames
        {
            get { return this.GetDelayFrames(); }
            set { this.SetDelayFrames(value); }
        }

        /// <summary>
        /// Gets or sets the current input gain value.
        /// </summary>
        public double InputGain
        {
            get { return this.GetInputGain(); }
            set { this.SetInputGain(value); }
        }

        /// <summary>
        /// Gets a value indicating whether the Fairlight audio source has stereo simulation available.
        /// </summary>
        public bool HasStereoSimulation
        {
            get
            {
                this.InternalFairlightAudioSourceReference.HasStereoSimulation(out int hasStereoSimulation);
                return Convert.ToBoolean(hasStereoSimulation);
            }
        }

        /// <summary>
        /// Gets or sets the current stereo-simulation-intensity percentage.
        /// </summary>
        public double StereoSimulationIntensity
        {
            get { return this.GetStereoSimulationIntensity(); }
            set { this.SetStereoSimulationIntensity(value); }
        }

        /// <summary>
        /// Gets or sets the current pan value.
        /// </summary>
        public double Pan
        {
            get { return this.GetPan(); }
            set { this.SetPan(value); }
        }

        /// <summary>
        /// Gets or sets the current fader gain value.
        /// </summary>
        public double FaderGain
        {
            get { return this.GetFaderGain(); }
            set { this.SetFaderGain(value); }
        }

        /// <summary>
        /// Gets the supported mix options.
        /// </summary>
        public _BMDSwitcherFairlightAudioMixOption SupportedMixOptions
        {
            get { return this.GetSupportedMixOptions(); }
        }

        /// <summary>
        /// Gets or sets the current mix option.
        /// </summary>
        public _BMDSwitcherFairlightAudioMixOption MixOption
        {
            get { return this.GetMixOption(); }
            set { this.SetMixOption(value); }
        }

        /// <summary>
        /// Gets a value indicating whether the Fairlight audio source is currently being mixed into the program out.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.19</remarks>
        public bool IsMixedIn
        {
            get
            {
                this.InternalFairlightAudioSourceReference.IsMixedIn(out int isMixedIn);
                return Convert.ToBoolean(isMixedIn);
            }
        }

        /// <summary>
        /// Gets the ID of this AudioSource interface.
        /// </summary>
        public long Id
        {
            get { return this.GetId(); }
        }
        #endregion

        #region IBMDSwitcherFairlightAudioSource
        /// <summary>
        /// The GetSourceType method indicates the type of Fairlight audio source.
        /// </summary>
        /// <returns>The current Fairlight audio source type.</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.2</remarks>
        public _BMDSwitcherFairlightAudioSourceType GetSourceType()
        {
            try
            {
                this.InternalFairlightAudioSourceReference.GetSourceType(out _BMDSwitcherFairlightAudioSourceType type);
                return type;
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
        /// The GetMaxDelayFrames method returns the maximum delay frames.
        /// </summary>
        /// <returns>Maximum number of delay frames.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.3</remarks>
        public ushort GetMaxDelayFrames()
        {
            this.InternalFairlightAudioSourceReference.GetMaxDelayFrames(out ushort maxDelay);
            return maxDelay;
        }

        /// <summary>
        /// The GetDelayFrames method returns the current number of delay frames applied to the Fairlight audio source.
        /// </summary>
        /// <returns>The current number of delay frames applied to the Fairlight audio source.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.4</remarks>
        public ushort GetDelayFrames()
        {
            this.InternalFairlightAudioSourceReference.GetDelayFrames(out ushort delay);
            return delay;
        }

        /// <summary>
        /// The SetDelayFrames method sets the number of delay frames to apply to the Fairlight audio source.
        /// </summary>
        /// <param name="delay">The number of delay frames to apply to the Fairlight audio source.</param>
        /// <exception cref="FailedException">Failure. This can happen if the source is no longer active.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.5</remarks>
        public void SetDelayFrames(ushort delay)
        {
            try
            {
                this.InternalFairlightAudioSourceReference.SetDelayFrames(delay);
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
        /// The GetInputGain method returns the current input gain applied to the Fairlight audio source.
        /// </summary>
        /// <returns>The gain currently applied to the Fairlight audio source.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.6</remarks>
        public double GetInputGain()
        {
            this.InternalFairlightAudioSourceReference.GetInputGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetInputGain method sets the input gain of a Fairlight audio source.
        /// </summary>
        /// <param name="gain">The gain to apply to the audio source.</param>
        /// <exception cref="FailedException">Failure. This can happen if the source is no longer active.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.7</remarks>
        public void SetInputGain(double gain)
        {
            try
            {
                this.InternalFairlightAudioSourceReference.SetInputGain(gain);
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
        /// The GetStereoSimulationIntensity method returns the current intensity of the stereo simulation applied to the Fairlight audio source.
        /// </summary>
        /// <returns>The current stereo-simulation-intensity percentage applied to the Fairlight audio source.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.9</remarks>
        public double GetStereoSimulationIntensity()
        {
            this.InternalFairlightAudioSourceReference.GetStereoSimulationIntensity(out double intensity);
            return intensity;
        }

        /// <summary>
        /// The SetStereoSimulationIntensity method sets the intensity of the stereo simulation to apply to the Fairlight audio source.
        /// </summary>
        /// <param name="intensity">The desired stereo simulation intensity to apply to the Fairlight audio source.</param>
        /// <exception cref="FailedException">Failure. This can happen if the source is no longer active.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.10</remarks>
        public void SetStereoSimulationIntensity(double intensity)
        {
            try
            {
                this.InternalFairlightAudioSourceReference.SetStereoSimulationIntensity(intensity);
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

        // TODO: GetEffect 7.5.7.11
        public void GetEffect<EffectType>(out EffectType effectObject)
        {
            Guid guid;
            IntPtr ppv;

            if (typeof(EffectType) == typeof(Effects.FairlightAudioDynamicsProcessor))
            {
                guid = typeof(IBMDSwitcherFairlightAudioDynamicsProcessor).GUID;
                this.InternalFairlightAudioSourceReference.GetEffect(ref guid, out ppv);
                effectObject = (EffectType) Convert.ChangeType(new Effects.FairlightAudioDynamicsProcessor(Marshal.GetObjectForIUnknown(ppv) as IBMDSwitcherFairlightAudioDynamicsProcessor), typeof(EffectType));
            }
            else
            {
                throw new NotSupportedException(typeof(EffectType).ToString());
            }

            return;
        }

        /// <summary>
        /// The GetPan method returns the current pan value applied to the Fairlight audio source.
        /// </summary>
        /// <returns>The pan currently applied to the Fairlight audio source</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.12</remarks>
        public double GetPan()
        {
            this.InternalFairlightAudioSourceReference.GetPan(out double pan);
            return pan;
        }

        /// <summary>
        /// The SetPan method sets the pan value to apply to the Fairlight audio source.
        /// </summary>
        /// <param name="pan">The pan to apply to the Fairlight audio source.</param>
        /// <exception cref="FailedException">Failure. This can happen if the source is no longer active.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.13</remarks>
        public void SetPan(double pan)
        {
            try
            {
                this.InternalFairlightAudioSourceReference.SetPan(pan);
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
        /// The GetFaderGain method returns the current fader gain value applied to the Fairlight audio source.
        /// </summary>
        /// <returns>The fader gain currently applied to the Fairlight audio source.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.14</remarks>
        public double GetFaderGain()
        {
            this.InternalFairlightAudioSourceReference.GetFaderGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetFaderGain method sets the fader gain value to apply to the Fairlight audio source.
        /// </summary>
        /// <param name="gain">The fader gain value to apply to the Fairlight audio source.</param>
        /// <exception cref="FailedException">Failure. This can happen if the source is no longer active.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.15</remarks>
        public void SetFaderGain(double gain)
        {
            try
            {
                this.InternalFairlightAudioSourceReference.SetFaderGain(gain);
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
        /// The GetSupportedMixOptions method returns the supported mix options.
        /// </summary>
        /// <returns>The available mix options.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.16</remarks>
        public _BMDSwitcherFairlightAudioMixOption GetSupportedMixOptions()
        {
            this.InternalFairlightAudioSourceReference.GetSupportedMixOptions(out _BMDSwitcherFairlightAudioMixOption mixOptions);
            return mixOptions;
        }

        /// <summary>
        /// The GetMixOption method returns the current mix option.
        /// </summary>
        /// <returns>The current mix option.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.17</remarks>
        public _BMDSwitcherFairlightAudioMixOption GetMixOption()
        {
            this.InternalFairlightAudioSourceReference.GetMixOption(out _BMDSwitcherFairlightAudioMixOption mixOption);
            return mixOption;
        }

        /// <summary>
        /// The SetMixOption method sets the mix option.
        /// </summary>
        /// <param name="mixOption">The desired mix option.</param>
        /// <exception cref="FailedException">Failure. This can happen if the source is no longer active.</exception>
        /// <exception cref="ArgumentException">The mixOption is not a valid identifier.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.18</remarks>
        public void SetMixOption(_BMDSwitcherFairlightAudioMixOption mixOption)
        {
            try
            {
                this.InternalFairlightAudioSourceReference.SetMixOption(mixOption);
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
        /// The ResetOutputPeakLevels method resets peak statistics for the Fairlight audio source.
        /// </summary>
        /// <exception cref="FailedException">Failure. This can happen if the source is no longer active.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.20</remarks>
        public void ResetOutputPeakLevels()
        {
            try
            {
                this.InternalFairlightAudioSourceReference.ResetOutputPeakLevels();
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
        /// The GetId method returns the BMDSwitcherFairlightAudioSourceId of the Fairlight audio source.
        /// </summary>
        /// <returns>BMDSwitcherFairlightAudioSourceId identifier for the current Fairlight audio source.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.7.21</remarks>
        public long GetId()
        {
            this.InternalFairlightAudioSourceReference.GetId(out long sourceId);
            return sourceId;
        }
        #endregion

        #region IBMDSwitcherFairlightAudioSourceCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherFairlightAudioSource events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherFairlightAudioSourceEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 7.5.8.1</remarks>
        void IBMDSwitcherFairlightAudioSourceCallback.Notify(_BMDSwitcherFairlightAudioSourceEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherFairlightAudioSourceEventType.bmdSwitcherFairlightAudioSourceEventTypeIsActiveChanged:
                    this.OnIsActiveChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioSourceEventType.bmdSwitcherFairlightAudioSourceEventTypeMaxDelayFramesChanged:
                    this.OnMaxDelayFramesChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioSourceEventType.bmdSwitcherFairlightAudioSourceEventTypeDelayFramesChanged:
                    this.OnDelayFramesChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioSourceEventType.bmdSwitcherFairlightAudioSourceEventTypeInputGainChanged:
                    this.OnInputGainChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioSourceEventType.bmdSwitcherFairlightAudioSourceEventTypeStereoSimulationIntensityChanged:
                    this.OnStereoSimulationIntensityChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioSourceEventType.bmdSwitcherFairlightAudioSourceEventTypePanChanged:
                    this.OnPanChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioSourceEventType.bmdSwitcherFairlightAudioSourceEventTypeFaderGainChanged:
                    this.OnFaderGainChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioSourceEventType.bmdSwitcherFairlightAudioSourceEventTypeMixOptionChanged:
                    this.OnMixOptionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioSourceEventType.bmdSwitcherFairlightAudioSourceEventTypeIsMixedInChanged:
                    this.OnIsMixedInChanged?.Invoke(this);
                    break;
            }

            return;
        }

        /// <summary>
        /// <para>The OutputLevelNotification method is called periodically to report the current dB output levels and the last known peak levels.These peak levels can be reset using IBMDSwitcherFairlightAudioSource::ResetOutputPeakLevels.</para>
        /// <para>Note that this is an opt-in subscription.Enable or disable receiving these calls using IBMDSwitcherFairlightAudioMixer::SetAllLevelNotificationsEnabled.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller</para>
        /// </summary>
        /// <param name="numLevels">The number of output levels.</param>
        /// <param name="levels">The current output dB levels.</param>
        /// <param name="numPeakLevels">The number of output peak levels.</param>
        /// <param name="peakLevels">The highest encountered output peak level.</param>
        /// <remarks>Blackmagic Switcher SDK - 7.5.8.2</remarks>
        void IBMDSwitcherFairlightAudioSourceCallback.OutputLevelNotification(uint numLevels, ref double levels, uint numPeakLevels, ref double peakLevels)
        {
            this.OnOutputLevelNotification?.Invoke(this, numLevels, levels, numPeakLevels, peakLevels);
            return;
        }
        #endregion
    }
}
