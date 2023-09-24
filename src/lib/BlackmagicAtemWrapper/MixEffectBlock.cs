//-----------------------------------------------------------------------------
// <copyright file="MixEffectBlock.cs">
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

namespace BlackmagicAtemWrapper
{
    using System;
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The MixEffectBlock class represents a mix effect block of a switcher device.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 2.3.8</remarks>
    public class MixEffectBlock : IBMDSwitcherMixEffectBlockCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherMixEffectBlock"/>.
        /// </summary>
        private readonly IBMDSwitcherMixEffectBlock InternalMixEffectBlockReference;

        #region ctor/dtor
        /// <summary>
        /// Initializes a new instance of the <see cref="MixEffectBlock"/> class.
        /// </summary>
        /// <param name="mixEffectBlock">The native <seealso cref="IBMDSwitcherMixEffectBlock"/> from the BMDSwitcherAPI.</param>
        public MixEffectBlock(IBMDSwitcherMixEffectBlock mixEffectBlock)
        {
            this.InternalMixEffectBlockReference = mixEffectBlock;
            this.InternalMixEffectBlockReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MixEffectBlock"/> class.
        /// </summary>
        ~MixEffectBlock()
        {
            this.InternalMixEffectBlockReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalMixEffectBlockReference);
        }
        #endregion

        /// <summary>
        /// A delegate to handle events from <see cref="MixEffectBlock"/>.
        /// </summary>
        /// <param name="sender">The <see cref="MixEffectBlock"/> that received the event.</param>
        public delegate void MixEventBlockEventHandler(object sender);

        #region Events
        /// <summary>
        /// Called when the <see cref="ProgramInput"/> changes.
        /// </summary>
        public event MixEventBlockEventHandler OnProgramInputChanged;

        /// <summary>
        /// Called when the <see cref="PreviewInput"/> changes.
        /// </summary>
        public event MixEventBlockEventHandler OnPreviewInputChanged;

        /// <summary>
        /// Called when the <see cref="TransitionPosition"/> changes.
        /// </summary>
        public event MixEventBlockEventHandler OnTransitionPositionChanged;

        /// <summary>
        /// Called when <see cref="TransitionFramesRemaining"/> changes.
        /// </summary>
        public event MixEventBlockEventHandler OnTransitionFramesRemainingChanged;

        /// <summary>
        /// Called when the <see cref="IsInTransition"/> flag changes.
        /// </summary>
        public event MixEventBlockEventHandler OnInTransitionChanged;

        /// <summary>
        /// Called when the <see cref="FadeToBlackFramesRemaining"/> changes.
        /// </summary>
        public event MixEventBlockEventHandler OnFadeToBlackFramesRemainingChanged;

        /// <summary>
        /// Called when the <see cref="IsInFadeToBlack"/> flag changes.
        /// </summary>
        public event MixEventBlockEventHandler OnInFadeToBlackChanged;

        /// <summary>
        /// Called when the <see cref="IsPreviewLive"/> flag chanages;
        /// </summary>
        public event MixEventBlockEventHandler OnPreviewLiveChanged;

        /// <summary>
        /// Called when <see cref="PreviewTransitionMode"/> changes.
        /// </summary>
        public event MixEventBlockEventHandler OnPreviewTransitionChanged;

        /// <summary>
        /// Called when <see cref="InputAvailabilityMask"/> changes.
        /// </summary>
        public event MixEventBlockEventHandler OnInputAvailabilityMaskChanged;

        /// <summary>
        /// Called when <see cref="FadeToBlackRate"/> changes.
        /// </summary>
        public event MixEventBlockEventHandler OnFadeToBlackRateChanged;

        /// <summary>
        /// Called when the <see cref="IsFadeToBlackFullyBlack"/> flag changes.
        /// </summary>
        public event MixEventBlockEventHandler OnFadeToBlackFullyBlackChanged;

        /// <summary>
        /// Called when the <see cref="IsFadeToBlackInTransition"/> flag changes.
        /// </summary>
        public event MixEventBlockEventHandler OnFadeToBlackInTransitionChanged;
        #endregion

        #region QueryInterface methods
        /// <summary>
        /// Gets the transition parameters object.
        /// </summary>
        public Transitions.TransitionParameters TransitionParameters => new(this.InternalMixEffectBlockReference as IBMDSwitcherTransitionParameters);

        /// <summary>
        /// Gets the transition mix parameters object.
        /// </summary>
        public Transitions.MixParameters TransitionMixParameters => new(this.InternalMixEffectBlockReference as IBMDSwitcherTransitionMixParameters);

        /// <summary>
        /// Gets the transition dip parameters object.
        /// </summary>
        public Transitions.DipParameters TransitionDipParameters => new(this.InternalMixEffectBlockReference as IBMDSwitcherTransitionDipParameters);

        /// <summary>
        /// Gets the transition wipe parameters object.
        /// </summary>
        public Transitions.WipeParameters TransitionWipeParameters => new(this.InternalMixEffectBlockReference as IBMDSwitcherTransitionWipeParameters);

        /// <summary>
        /// Gets the transition DVE parameters object.
        /// </summary>
        public Transitions.DVEParameters TransitionDVEParameters => new(this.InternalMixEffectBlockReference as IBMDSwitcherTransitionDVEParameters);

        /// <summary>
        /// Gets the transition stinger parameters object.
        /// </summary>
        public Transitions.StingerParameters TransitionStingerParameters => new(this.InternalMixEffectBlockReference as IBMDSwitcherTransitionStingerParameters);

        /// <summary>
        /// Gets the collection of <see cref="Keyers.Key"/> objects for the <see cref="MixEffectBlock"/>.
        /// </summary>
        public Keyers.KeyCollection SwitcherKeys => new(this.InternalMixEffectBlockReference);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current program input.
        /// </summary>
        /// <seealso cref="OnProgramInputChanged"/>
        public long ProgramInput
        {
            get { return this.GetProgramInput(); }
            set { this.SetProgramInput(value); }
        }

        /// <summary>
        /// Gets or sets the current preview input.
        /// </summary>
        /// <seealso cref="OnPreviewInputChanged"/>
        public long PreviewInput
        {
            get { return this.GetPreviewInput(); }
            set { this.SetPreviewInput(value); }
        }

        /// <summary>
        /// Gets a value indicating whether the current preview-live flag is set.
        /// </summary>
        public bool IsPreviewLive
        {
            get { return this.GetPreviewLive(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current preview-transition flag is set.
        /// </summary>
        public bool PreviewTransitionMode
        {
            get { return this.GetPreviewTransition(); }
            set { this.SetPreviewTransition(value); }
        }

        /// <summary>
        /// Gets a value indicating whether the current in-transition flag is set.
        /// </summary>
        public bool IsInTransition
        {
            get { return this.GetInTransition(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current transition position value is set.
        /// </summary>
        public double TransitionPosition
        {
            get { return this.GetTransitionPosition(); }
            set { this.SetTransitionPosition(value); }
        }

        /// <summary>
        /// Gets the number of transition frames remaining.
        /// </summary>
        public uint TransitionFramesRemaining
        {
            get { return this.GetTransitionFramesRemaining(); }
        }

        /// <summary>
        /// Gets or sets the current fade to black rate value.
        /// </summary>
        public uint FadeToBlackRate
        {
            get { return this.GetFadeToBlackRate(); }
            set { this.SetFadeToBlackRate(value); }
        }

        /// <summary>
        /// Gets the current number of fade to black frames remaining.
        /// </summary>
        public uint FadeToBlackFramesRemaining
        {
            get { return this.GetFadeToBlackFramesRemaining(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current fade-to-black-fully-black flag is set.
        /// </summary>
        public bool IsFadeToBlackFullyBlack
        {
            get { return this.GetFadeToBlackFullyBlack(); }
            set { this.SetFadeToBlackFullyBlack(value); }
        }

        /// <summary>
        /// Gets a value indicating whether the current in-fade-to-black flag is set.
        /// </summary>
        public bool IsInFadeToBlack
        {
            get { return this.GetInFadeToBlack(); }
        }

        /// <summary>
        /// Gets a value indicating whether the current fade-to-black-in-transition flag is set.
        /// </summary>
        public bool IsFadeToBlackInTransition
        {
            get { return this.GetFadeToBlackInTransition(); }
        }

        /// <summary>
        /// Gets a value indicating the switcher input availability mask.
        /// </summary>
        public _BMDSwitcherInputAvailability InputAvailabilityMask
        {
            get { return this.GetInputAvailabilityMask(); }
        }
        #endregion

        #region IBMDSwitcherMixEffectBlock
        /// <summary>
        /// The GetProgramInput method returns the current program input to the mix effect block.
        /// </summary>
        /// <returns>The program input currently applied to the mix effect block.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.1</remarks>
        public long GetProgramInput()
        {
            this.InternalMixEffectBlockReference.GetProgramInput(out long value);
            return value;
        }

        /// <summary>
        /// The SetProgramInput method sets the program input to apply to the mix effect block.
        /// </summary>
        /// <param name="value">The program input to apply to the mix effect block.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.2</remarks>
        public void SetProgramInput(long value)
        {
            try
            {
                this.InternalMixEffectBlockReference.SetProgramInput(value);
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
        /// The GetPreviewInput method returns the current preview input to the mix effect block.
        /// </summary>
        /// <returns>The preview input currently applied to the mix effect block.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.3</remarks>
        public long GetPreviewInput()
        {
            this.InternalMixEffectBlockReference.GetPreviewInput(out long value);
            return value;
        }

        /// <summary>
        /// The SetPreviewInput method sets the preview input to apply to the mix effect block.
        /// </summary>
        /// <param name="value">The preview input to apply to the mix effect block.</param>
        /// <exception cref="ArgumentException">The <paramref name="value"/> is not a valid identifier.</exception>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.4</remarks>
        public void SetPreviewInput(long value)
        {
            try
            {
                this.InternalMixEffectBlockReference.SetPreviewInput(value);
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
        /// The GetPreviewLive method indicates whether the preview is live.
        /// </summary>
        /// <returns>The preview live flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.5</remarks>
        public bool GetPreviewLive()
        {
            this.InternalMixEffectBlockReference.GetPreviewLive(out int value);
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// The GetPreviewTransition method indicates whether the preview transition mode is currently enabled on the mix effect block.
        /// </summary>
        /// <returns>The current preview transition flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.6</remarks>
        public bool GetPreviewTransition()
        {
            this.InternalMixEffectBlockReference.GetPreviewTransition(out int value);
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// The SetPreviewTransition method is used to enable or disable the preview transition mode.
        /// </summary>
        /// <param name="value">The desired preview transition flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.7</remarks>
        public void SetPreviewTransition(bool value)
        {
            try
            {
                this.InternalMixEffectBlockReference.SetPreviewTransition(Convert.ToInt32(value));
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
        /// <para>The PerformAutoTransition method initiates an automatic transition for the mix effect block.</para>
        /// <para>When the transition begins and ends the bmdSwitcherMixEffectBlockEventTypeInTransitionChanged callback will be fired with the in transition flag set to true and then false on completion. Throughout the transition the bmdSwitcherMixEffectBlockEventTypeTransitionPositionChanged and bmdSwitcherMixEffectBlockEventTypeTransitionFramesRemainingChanged callbacks will be fired with property values corresponding to the progress through the transition.</para>
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.8</remarks>
        public void PerformAutoTransition()
        {
            try
            {
                this.InternalMixEffectBlockReference.PerformAutoTransition();
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
        /// The PerformCut method initiates a cut for the mix effect block.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.9</remarks>
        public void PerformCut()
        {
            try
            {
                this.InternalMixEffectBlockReference.PerformCut();
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
        /// The GetInTransition method indicates whether a transition is occurring.
        /// </summary>
        /// <returns>The current in transition flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.10</remarks>
        public bool GetInTransition()
        {
            this.InternalMixEffectBlockReference.GetInTransition(out int value);
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// The GetTransitionPosition method returns the current transition position value.
        /// </summary>
        /// <returns>The current transition position value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.11</remarks>
        public double GetTransitionPosition()
        {
            this.InternalMixEffectBlockReference.GetTransitionPosition(out double value);
            return value;
        }

        /// <summary>
        /// The SetTransitionPosition method sets the transition position value.
        /// </summary>
        /// <param name="value">The desired transition position value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.12</remarks>
        public void SetTransitionPosition(double value)
        {
            try
            {
                this.InternalMixEffectBlockReference.SetTransitionPosition(value);
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
        /// The GetTransitionFramesRemaining method returns the number of transition frames remaining for the transition.
        /// </summary>
        /// <returns>The number of transition frames remaining.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.13</remarks>
        public uint GetTransitionFramesRemaining()
        {
            this.InternalMixEffectBlockReference.GetTransitionFramesRemaining(out uint value);
            return value;
        }

        /// <summary>
        /// <para>The PerformFadeToBlack method initiates a fade to black for the mix effect block.</para>
        /// <para>When the fade to black begins and ends the bmdSwitcherMixEffectBlockEventTypeInFadeToBlackChanged callback will be fired with the in fade to black flag set to true and then false on completion. Throughout the transition the bmdSwitcherMixEffectBlockEventTypeFadeToBlackFramesRemainingChanged callback will be fired with values corresponding to the progress through the fade to black</para>
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.14</remarks>
        public void PerformFadeToBlack()
        {
            try
            {
                this.InternalMixEffectBlockReference.PerformFadeToBlack();
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
        /// The GetFadeToBlackRate method returns the current fade to black rate in frames.
        /// </summary>
        /// <returns>The current fade to black rate.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.15</remarks>
        public uint GetFadeToBlackRate()
        {
            this.InternalMixEffectBlockReference.GetFadeToBlackRate(out uint value);
            return value;
        }

        /// <summary>
        /// The SetFadeToBlackRate method returns the number of frames remaining for the fade to black.
        /// </summary>
        /// <param name="value">The desired fade to black rate.</param>
        /// <exception cref="ArgumentException">The value is not a valid number of frames.</exception>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.16</remarks>
        public void SetFadeToBlackRate(uint value)
        {
            try
            {
                this.InternalMixEffectBlockReference.SetFadeToBlackRate(value);
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
        /// The GetFadeToBlackFramesRemaining method returns the number of frames remaining for the fade to black.
        /// </summary>
        /// <returns>The number of fade to black frames remaining.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.17</remarks>
        public uint GetFadeToBlackFramesRemaining()
        {
            this.InternalMixEffectBlockReference.GetFadeToBlackFramesRemaining(out uint value);
            return value;
        }

        /// <summary>
        /// The GetFadeToBlackFullyBlack method indicates whether the current frame is completely black.
        /// </summary>
        /// <returns>The current fade to black fully black flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.18</remarks>
        public bool GetFadeToBlackFullyBlack()
        {
            this.InternalMixEffectBlockReference.GetFadeToBlackFullyBlack(out int value);
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// The SetFadeToBlackFullyBlack method sets the fade to black fully black flag.
        /// </summary>
        /// <param name="value">The desired fade to black fully black flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.19</remarks>
        public void SetFadeToBlackFullyBlack(bool value)
        {
            try
            {
                this.InternalMixEffectBlockReference.SetFadeToBlackFullyBlack(Convert.ToInt32(value));
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
        /// The GetInFadeToBlack method indicates whether fade to black is transitioning or the frame is completely black.
        /// </summary>
        /// <returns>The current in fade to black flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.20</remarks>
        public bool GetInFadeToBlack()
        {
            this.InternalMixEffectBlockReference.GetInFadeToBlack(out int value);
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// The GetFadeToBlackInTransition method indicates whether fade to black is transitioning.
        /// </summary>
        /// <returns>The current fade to black in transition flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.21</remarks>
        public bool GetFadeToBlackInTransition()
        {
            this.InternalMixEffectBlockReference.GetFadeToBlackInTransition(out int value);
            return Convert.ToBoolean(value);
        }

        /// <summary>
        /// The GetInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask value for this mix effect block.The input availability property of an IBMDSwitcherInput can be bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this input is available for use by this mix effect block.
        /// </summary>
        /// <returns>The availability of the input.</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.8.22</remarks>
        public _BMDSwitcherInputAvailability GetInputAvailabilityMask()
        {
            try
            {
                this.InternalMixEffectBlockReference.GetInputAvailabilityMask(out _BMDSwitcherInputAvailability value);
                return value;
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

        #region IBMDSwitcherMixEffectBlockCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherMixEffectBlock events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher. The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherMixEffectBlockEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.9.1</remarks>
        void IBMDSwitcherMixEffectBlockCallback.Notify(_BMDSwitcherMixEffectBlockEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeProgramInputChanged:
                    this.OnProgramInputChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypePreviewInputChanged:
                    this.OnPreviewInputChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeTransitionPositionChanged:
                    this.OnTransitionPositionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeTransitionFramesRemainingChanged:
                    this.OnTransitionFramesRemainingChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeInTransitionChanged:
                    this.OnInTransitionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeFadeToBlackFramesRemainingChanged:
                    this.OnFadeToBlackFramesRemainingChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeInFadeToBlackChanged:
                    this.OnInFadeToBlackChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypePreviewLiveChanged:
                    this.OnPreviewLiveChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypePreviewTransitionChanged:
                    this.OnPreviewTransitionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeInputAvailabilityMaskChanged:
                    this.OnInputAvailabilityMaskChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeFadeToBlackRateChanged:
                    this.OnFadeToBlackRateChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeFadeToBlackFullyBlackChanged:
                    this.OnFadeToBlackFullyBlackChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixEffectBlockEventType.bmdSwitcherMixEffectBlockEventTypeFadeToBlackInTransitionChanged:
                    this.OnFadeToBlackInTransitionChanged?.Invoke(this);
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false, "Unexpected _BMDSwitcherMixEffectBlockEventType", "_BMDSwitcherMixEffectBlockEventType = {0}", new object[] { eventType });
                    break;
            }

            return;
        }
        #endregion
    }
}