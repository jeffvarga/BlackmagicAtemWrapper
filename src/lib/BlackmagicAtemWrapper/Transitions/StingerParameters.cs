//-----------------------------------------------------------------------------
// <copyright file="StingerParameters.cs">
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
    /// The StingerParameters class is used for manipulating transition settings specific to stinger parameters.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 3.2.9</remarks>
    public class StingerParameters : IBMDSwitcherTransitionStingerParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherMixEffectBlock"/>.
        /// </summary>
        private readonly IBMDSwitcherTransitionStingerParameters InternalStingerParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="StingerParameters"/> class.
        /// </summary>
        /// <param name="stingerParameters">The native <seealso cref="IBMDSwitcherTransitionStingerParameters"/> from the BMDSwitcherAPI.</param>
        public StingerParameters(IBMDSwitcherTransitionStingerParameters stingerParameters)
        {
            this.InternalStingerParametersReference = stingerParameters ?? throw new ArgumentNullException(nameof(stingerParameters));
            this.InternalStingerParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="StingerParameters"/> class.
        /// </summary>
        ~StingerParameters()
        {
            this.InternalStingerParametersReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalStingerParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="StingerParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="StingerParameters"/> that received the event.</param>
        public delegate void StingerParametersEventHandler(object sender);

        /// <summary>
        /// The <see cref="Source"/> value changed.
        /// </summary>
        public event StingerParametersEventHandler OnSourceChanged;

        /// <summary>
        /// The <see cref="IsPreMultiplied"/> flag changed.
        /// </summary>
        public event StingerParametersEventHandler OnPreMultipliedChanged;

        /// <summary>
        /// The <see cref="Clip"/> value changed.
        /// </summary>
        public event StingerParametersEventHandler OnClipChanged;

        /// <summary>
        /// The <see cref="Gain"/> value changed.
        /// </summary>
        public event StingerParametersEventHandler OnGainChanged;

        /// <summary>
        /// The <see cref="IsInverse"/> flag changed.
        /// </summary>
        public event StingerParametersEventHandler OnInverseChanged;

        /// <summary>
        /// The <see cref="Preroll"/> value changed.
        /// </summary>
        public event StingerParametersEventHandler OnPrerollChanged;

        /// <summary>
        /// The <see cref="ClipDuration"/> value changed.
        /// </summary>
        public event StingerParametersEventHandler OnClipDurationChanged;

        /// <summary>
        /// The <see cref="TriggerPoint"/> value changed.
        /// </summary>
        public event StingerParametersEventHandler OnTriggerPointChanged;

        /// <summary>
        /// The <see cref="MixRate"/> value changed.
        /// </summary>
        public event StingerParametersEventHandler OnMixRateChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current source.
        /// </summary>
        public _BMDSwitcherStingerTransitionSource Source
        {
            get { return this.GetSource(); }
            set { this.SetSource(value); }
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

        /// <summary>
        /// Gets or sets the current number of preroll frames.
        /// </summary>
        public uint Preroll
        {
            get { return this.GetPreroll(); }
            set { this.SetPreroll(value); }
        }

        /// <summary>
        /// Gets or sets the current clip duration in frames.
        /// </summary>
        public uint ClipDuration
        {
            get { return this.GetClipDuration(); }
            set { this.SetClipDuration(value); }
        }

        /// <summary>
        /// Gets or sets the current trigger point.
        /// </summary>
        public uint TriggerPoint
        {
            get { return this.GetTriggerPoint(); }
            set { this.SetTriggerPoint(value); }
        }

        /// <summary>
        /// Gets or sets the current mix rate.
        /// </summary>
        public uint MixRate
        {
            get { return this.GetMixRate(); }
            set { this.SetMixRate(value); }
        }
        #endregion

        #region IBMDSwitcherTransitionStingerParameters
        /// <summary>
        /// The GetSource method returns the current source.
        /// </summary>
        /// <returns>The current source.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.1</remarks>
        public _BMDSwitcherStingerTransitionSource GetSource()
        {
            this.InternalStingerParametersReference.GetSource(out _BMDSwitcherStingerTransitionSource src);
            return src;
        }

        /// <summary>
        /// The SetSource method sets the rate in frames.
        /// </summary>
        /// <param name="src">The desired source.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.2</remarks>
        public void SetSource(_BMDSwitcherStingerTransitionSource src)
        {
            try
            {
                this.InternalStingerParametersReference.SetSource(src);
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
        /// <returns>The current pre-multiplied flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.3</remarks>
        public bool GetPreMultiplied()
        {
            this.InternalStingerParametersReference.GetPreMultiplied(out int preMultiplied);
            return Convert.ToBoolean(preMultiplied);
        }

        /// <summary>
        /// The SetPreMultiplied method sets the pre-multiplied flag.
        /// </summary>
        /// <param name="preMultiplied">The desired pre-multiplied flag.</param>
        /// <exception cref="FailedException">The desired pre-multiplied flag.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.4</remarks>
        public void SetPreMultiplied(bool preMultiplied)
        {
            try
            {
                this.InternalStingerParametersReference.SetPreMultiplied(Convert.ToInt32(preMultiplied));
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
        /// The GetClip method returns the current clip value.
        /// </summary>
        /// <returns>The current clip value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.5</remarks>
        public double GetClip()
        {
            this.InternalStingerParametersReference.GetClip(out double clip);
            return clip;
        }

        /// <summary>
        /// The SetClip method sets the clip value.
        /// </summary>
        /// <param name="clip">The desired clip value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.6</remarks>
        public void SetClip(double clip)
        {
            try
            {
                this.InternalStingerParametersReference.SetClip(clip);
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
        /// The GetGain method returns the current gain.
        /// </summary>
        /// <returns>The current gain.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.7</remarks>
        public double GetGain()
        {
            this.InternalStingerParametersReference.GetGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetGain method sets the gain.
        /// </summary>
        /// <param name="gain">The desired gain.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.8</remarks>
        public void SetGain(double gain)
        {
            try
            {
                this.InternalStingerParametersReference.SetGain(gain);
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
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.9</remarks>
        public bool GetInverse()
        {
            this.InternalStingerParametersReference.GetInverse(out int inverse);
            return Convert.ToBoolean(inverse);
        }

        /// <summary>
        /// The SetInverse method sets the inverse flag.
        /// </summary>
        /// <param name="inverse">The desired inverse flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.10</remarks>
        public void SetInverse(bool inverse)
        {
            try
            {
                this.InternalStingerParametersReference.SetInverse(Convert.ToInt32(inverse));
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
        /// The GetPreroll method returns the current pre-roll.
        /// </summary>
        /// <returns>The current pre-roll in frames.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.11</remarks>
        public uint GetPreroll()
        {
            this.InternalStingerParametersReference.GetPreroll(out uint frames);
            return frames;
        }

        /// <summary>
        /// The SetPreroll method sets the pre-roll.
        /// </summary>
        /// <param name="frames">The desired pre-roll in frames.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.12</remarks>
        public void SetPreroll(uint frames)
        {
            try
            {
                this.InternalStingerParametersReference.SetPreroll(frames);
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
        /// The GetClipDuration method returns the current clip duration.
        /// </summary>
        /// <returns>The current clip duration in frames.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.13</remarks>
        public uint GetClipDuration()
        {
            this.InternalStingerParametersReference.GetClipDuration(out uint frames);
            return frames;
        }

        /// <summary>
        /// The SetClipDuration method sets the clip duration.
        /// </summary>
        /// <param name="frames">The desired clip duration in frames.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.14</remarks>
        public void SetClipDuration(uint frames)
        {
            try
            {
                this.InternalStingerParametersReference.SetClipDuration(frames);
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
        /// The GetTriggerPoint method returns the current trigger point.
        /// </summary>
        /// <returns>The current trigger point in frames.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.15</remarks>
        public uint GetTriggerPoint()
        {
            this.InternalStingerParametersReference.GetTriggerPoint(out uint frames);
            return frames;
        }

        /// <summary>
        /// The SetTriggerPoint method sets the trigger point.
        /// </summary>
        /// <param name="frames">The desired trigger point in frames.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.16</remarks>
        public void SetTriggerPoint(uint frames)
        {
            try
            {
                this.InternalStingerParametersReference.SetTriggerPoint(frames);
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
        /// The GetMixRate method returns the current mix rate.
        /// </summary>
        /// <returns>The current mix rate in frames.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.17</remarks>
        public uint GetMixRate()
        {
            this.InternalStingerParametersReference.GetMixRate(out uint frames);
            return frames;
        }

        /// <summary>
        /// The SetMixRate method sets the mix rate.
        /// </summary>
        /// <param name="frames">The desired mix rate in frames.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.9.18</remarks>
        public void SetMixRate(uint frames)
        {
            try
            {
                this.InternalStingerParametersReference.SetMixRate(frames);
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

        #region IBMDSwitcherTransitionStingerParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherTransitionStingerParameters events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherTransitionStingerParametersEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 3.2.10.1</remarks>
        void IBMDSwitcherTransitionStingerParametersCallback.Notify(_BMDSwitcherTransitionStingerParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeSourceChanged:
                    this.OnSourceChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypePreMultipliedChanged:
                    this.OnPreMultipliedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeClipChanged:
                    this.OnClipChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeGainChanged:
                    this.OnGainChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeInverseChanged:
                    this.OnInverseChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypePrerollChanged:
                    this.OnPrerollChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeClipDurationChanged:
                    this.OnClipDurationChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeTriggerPointChanged:
                    this.OnTriggerPointChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionStingerParametersEventType.bmdSwitcherTransitionStingerParametersEventTypeMixRateChanged:
                    this.OnMixRateChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
