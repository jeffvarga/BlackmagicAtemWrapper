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
    using BMDSwitcherAPI;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The AudioMixer class is the root object for all original audio mixing control and feedback.
    /// </summary>
    /// <remarks>Wraps Blackmagic Switcher SDK - 7.5.1</remarks>
    public class FairlightAudioMixer
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
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FairlightAudioMixer"/> class.
        /// </summary>
        ~FairlightAudioMixer()
        {
            Marshal.ReleaseComObject(this.InternalFairlightAudioMixerReference);
        }

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
        #endregion
    }
}
