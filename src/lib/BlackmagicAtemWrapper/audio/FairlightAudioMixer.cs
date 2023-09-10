//-----------------------------------------------------------------------------
// <copyright file="AudioMixer.cs">
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

namespace BlackmagicAtemWrapper.audio
{
    using System;
    using System.Runtime.InteropServices;
    using BMDSwitcherAPI;

    /// <summary>
    /// The AudioMixer class is the root object for all original audio mixing control and feedback.
    /// </summary>
    /// <remarks>Wraps Blackmagic Switcher SDK - 7.5.1</remarks>
    public class FairlightAudioMixer : IBMDSwitcherFairlightAudioMixerCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherFairlightAudioMixer"/>.
        /// </summary>
        private readonly IBMDSwitcherFairlightAudioMixer InternalFairlightAudioMixerReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="FairlightAudioMixer"/> class.
        /// </summary>
        /// <param name="audioMixer">The native <seealso cref="IBMDSwitcherFairlightAudioMixer"/> from the BMDSwitcherAPI.</param>
        public FairlightAudioMixer(IBMDSwitcherFairlightAudioMixer audioMixer)
        {
            if(audioMixer == null)
            {
                throw new System.ArgumentNullException(nameof(audioMixer));
            }

            this.InternalFairlightAudioMixerReference = audioMixer;
            this.InternalFairlightAudioMixerReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FairlightAudioMixer"/> class.
        /// </summary>
        ~FairlightAudioMixer()
        {
            this.InternalFairlightAudioMixerReference.RemoveCallback(this);
            Marshal.ReleaseComObject(this.InternalFairlightAudioMixerReference);
        }

        #region Events
        /// <summary>
        /// Handles a <see cref="FairlightAudioMixer"/> event.
        /// </summary>
        /// <param name="sender">The object that received the event.</param>
        public delegate void FairlightAudioMixerEventHandler(object sender);

        /// <summary>Handles MasterOutLevelNotification.</summary>
        /// <param name="sender">The object that received the event.</param>
        /// <param name="numLevels">The number of levels of the master out.</param>
        /// <param name="levels">The current dB levels of the master out.</param>
        /// <param name="numPeakLevels">The number of peak levels of the master out.</param>
        /// <param name="peakLevels">The highest encountered peak dB level of the master out since the last reset.</param>
        /// <remarks>Blackmagic Switcher SDK - 7.5.2.2</remarks>
        public delegate void FairlighAudioMixerMasterOutLevelEventHandler(object sender, uint numLevels, double levels, uint numPeakLevels, double peakLevels);

        /// <summary>
        /// The <see cref="MasterOutFaderGain"/> changed.
        /// </summary>
        public event FairlightAudioMixerEventHandler OnMasterOutFaderGainChanged;

        /// <summary>
        /// The <see cref="OnMasterOutFollowFadeToBlackChanged"/> changed.
        /// </summary>
        public event FairlightAudioMixerEventHandler OnMasterOutFollowFadeToBlackChanged;

        /// <summary>
        /// The <see cref="DoesAudioFollowVideoCrossfadeTransition"/> flag changed.
        /// </summary>
        public event FairlightAudioMixerEventHandler OnAudioFollowVideoCrossfadeTransitionChanged;

        /// <summary>
        /// The <see cref="MicTalkbackGain"/> changed.
        /// </summary>
        public event FairlightAudioMixerEventHandler OnMicTalkbackGainChanged;

        /// <summary>
        /// <para>The OnAudioMixerMasterOutLevelChanged event is called periodically to report the current dB levels and the last known peak levels.These peak levels can be reset using IBMDSwitcherFairlightAudioMixer::ResetMasterOutPeakLevels.</para>
        /// <para>Note that this is an opt-in subscription.Enable or disable receiving these calls using IBMDSwitcherFairlightAudioMixer::SetAllLevelNotificationsEnabled.</para>
        /// </summary>
        public event FairlighAudioMixerMasterOutLevelEventHandler OnAudioMixerMasterOutLevelChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current program out gain value.
        /// </summary>
        public double MasterOutFaderGain
        {
            get { return this.GetMasterOutFaderGain(); }
            set { this.SetMasterOutFaderGain(value); }
        }

        /// <summary>
        /// Get or sets a value indicating the current master out follow fade to black state.
        /// </summary>
        public bool DoesMasterOutFollowFadeToBlack
        {
            get { return this.GetMasterOutFollowFadeToBlack(); }
            set { this.SetMasterOutFollowFadeToBlack(value); }
        }

        /// <summary>
        /// Gets or sets the current audio follow video crossfade transition state.
        /// </summary>
        public bool DoesAudioFollowVideoCrossfadeTransition
        {
            get { return this.GetAudioFollowVideoCrossfadeTransition(); }
            set { this.SetAudioFollowVideoCrossfadeTransition(value); }
        }

        /// <summary>
        /// Gets or sets the current talkback mic gain.
        /// </summary>
        public double MicTalkbackGain
        {
            get { return this.GetMicTalkbackGain(); }
            set { this.SetMicTalkbackGain(value); }
        }
        #endregion

        #region IBMDSwitcherAudio
        /// <summary>
        /// The GetMasterOutFaderGain method returns the current gain value.
        /// </summary>
        /// <returns>The current gain value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.1.2</remarks>
        public double GetMasterOutFaderGain()
        {
            this.InternalFairlightAudioMixerReference.GetMasterOutFaderGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetMasterOutFaderGain method sets the gain to apply to the master out.
        /// </summary>
        /// <param name="gain">The desired gain value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.1.3</remarks>
        public void SetMasterOutFaderGain(double gain)
        {
            try
            {
                this.InternalFairlightAudioMixerReference.SetMasterOutFaderGain(gain);
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
        /// The GetMasterOutFollowFadeToBlack method returns the current follow fade to black state. When enabled the master out audio will fade in unity with a fade to black transition.
        /// </summary>
        /// <returns>The current follow fade to black state.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.1.4</remarks>
        public bool GetMasterOutFollowFadeToBlack()
        {
            this.InternalFairlightAudioMixerReference.GetMasterOutFollowFadeToBlack(out int follow);
            return Convert.ToBoolean(follow);
        }

        /// <summary>
        /// The SetMasterOutFollowFadeToBlack method sets the current follow fade to black state. When enabled the master out audio will fade in unity with a fade to black transition.
        /// </summary>
        /// <param name="follow">The desired follow fade to black state.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.1.5</remarks>
        public void SetMasterOutFollowFadeToBlack(bool follow)
        {
            try
            {
                this.InternalFairlightAudioMixerReference.SetMasterOutFollowFadeToBlack(Convert.ToInt32(follow));
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
        /// The GetAudioFollowVideoCrossfadeTransition method returns the current follow video with crossfade transition state.When enabled the audio will crossfade with the video. 
        /// </summary>
        /// <returns>The current follow video with crossfade transition state.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.1.6</remarks>
        public bool GetAudioFollowVideoCrossfadeTransition()
        {
            this.InternalFairlightAudioMixerReference.GetAudioFollowVideoCrossfadeTransition(out int transition);
            return Convert.ToBoolean(transition);
        }

        /// <summary>
        /// The SetAudioFollowVideoCrossfadeTransition method sets the current follow video with crossfade transition state.When enabled the audio will crossfade with the video.
        /// </summary>
        /// <param name="transition">The desired follow video with crossfade transition state.</param>
        /// <exception cref="FailedException">Failed.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.1.7</remarks>
        public void SetAudioFollowVideoCrossfadeTransition(bool transition)
        {
            try
            {
                this.InternalFairlightAudioMixerReference.SetAudioFollowVideoCrossfadeTransition(Convert.ToInt32(transition));
            }
            catch (COMException e)
            {
                if (FailedException.IsFailedException(e.ErrorCode))
                {
                    throw new FailedException(e);
                }

                throw;
            }

            return;
        }

        /// <summary>
        /// <para>The SetAllLevelNotificationsEnabled method enables level statistics for the Fairlight mixer inputs and outputs.</para>
        /// <para>Receiving level notifications are an opt-in subscription, affecting the callbacks IBMDSwitcherFairlightAudioMixerCallback::MasterOutLevelNotification, IBMDSwitcherFairlightAudioSourceCallback::OutputLevelNotification, IBMDSwitcherFairlightAudioDynamicsProcessorCallback::InputLevelNotification, IBMDSwitcherFairlightAudioDynamicsProcessorCallback::OutputLevelNotification, IBMDSwitcherFairlightAudioLimiterCallback::GainReductionLevelNotification, IBMDSwitcherFairlightAudioCompressorCallback::GainReductionLevelNotification and IBMDSwitcherFairlightAudioExpanderCallback::GainReductionLevelNotification</para>
        /// </summary>
        /// <param name="enabled">Whether to enable notifications.</param>
        /// <exception cref="FailedException">Failure</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.1.8</remarks>
        public void SetAllLevelNotificationsEnabled(bool enabled)
        {
            try
            {
                this.InternalFairlightAudioMixerReference.SetAllLevelNotificationsEnabled(Convert.ToInt32(enabled));
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
        /// The ResetMasterOutPeakLevels method resets the switcher’s master out peak level statistics.
        /// </summary>
        /// <exception cref="FailedException">Failed.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.1.9</remarks>
        public void ResetMasterOutPeakLevels()
        {
            try
            {
                this.InternalFairlightAudioMixerReference.ResetMasterOutPeakLevels();
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
        /// The ResetAllPeakLevels method resets peak level statistics for all Fairlight audio mixer inputs and outputs.
        /// </summary>
        /// <exception cref="FailedException">Failed.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.1.10</remarks>
        public void ResetAllPeakLevels()
        {
            try
            {
                this.InternalFairlightAudioMixerReference.ResetAllPeakLevels();
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
        /// Gets the gain for the talkback mic.
        /// </summary>
        /// <returns>The gain level for the talkback mic.</returns>
        /// <bug>Not in documentation</bug>
        public double GetMicTalkbackGain()
        {
            this.InternalFairlightAudioMixerReference.GetMicTalkbackGain(out double gain);
            return gain;
        }

        /// <summary>
        /// Sets the gain for the talkback mic.
        /// </summary>
        /// <param name="gain">The intended gain level for the talkback mic.</param>
        /// <exception cref="FailedException">Failed.</exception>
        /// <bug>Not in documentation.</bug>
        public void SetMicTalkbackGain(double gain)
        {
            try
            {
                this.SetMicTalkbackGain(gain);
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

        #region IBMDSwitcherFairlightAudioMixerCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherFairlightAudioMixer events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller. </para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherFairlightAudioMixerEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 7.5.2.1</remarks>
        void IBMDSwitcherFairlightAudioMixerCallback.Notify(_BMDSwitcherFairlightAudioMixerEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherFairlightAudioMixerEventType.bmdSwitcherFairlightAudioMixerEventTypeMasterOutFaderGainChanged:
                    this.OnMasterOutFaderGainChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioMixerEventType.bmdSwitcherFairlightAudioMixerEventTypeMasterOutFollowFadeToBlackChanged:
                    this.OnMasterOutFollowFadeToBlackChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioMixerEventType.bmdSwitcherFairlightAudioMixerEventTypeAudioFollowVideoCrossfadeTransitionChanged:
                    this.OnAudioFollowVideoCrossfadeTransitionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioMixerEventType.bmdSwitcherFairlightAudioMixerEventTypeMicTalkbackGainChanged:
                    this.OnMicTalkbackGainChanged?.Invoke(this);
                    break;
            }
        }

        /// <summary>
        /// <para>The MasterOutLevelNotification method is called periodically to report the current dB levels and the last known peak levels.These peak levels can be reset using IBMDSwitcherFairlightAudioMixer::ResetMasterOutPeakLevels.</para>
        /// <para>Note that this is an opt-in subscription.Enable or disable receiving these calls using IBMDSwitcherFairlightAudioMixer::SetAllLevelNotificationsEnabled.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="numLevels">The number of levels of the master out.</param>
        /// <param name="levels">The current dB levels of the master out.</param>
        /// <param name="numPeakLevels">The number of peak levels of the master out.</param>
        /// <param name="peakLevels">The highest encountered peak dB level of the master out since the last reset.</param>
        /// <bug>levels and peakLevels are set as [in,out]</bug>
        /// <remarks>Blackmagic Switcher SDK - 7.5.2.2</remarks>
        void IBMDSwitcherFairlightAudioMixerCallback.MasterOutLevelNotification(uint numLevels, ref double levels, uint numPeakLevels, ref double peakLevels)
        {
            this.OnAudioMixerMasterOutLevelChanged?.Invoke(this, numLevels, levels, numPeakLevels, peakLevels);
        }
        #endregion
    }
}
