//-----------------------------------------------------------------------------
// <copyright file="DownstreamKey.cs">
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
    /// The DownstreamKey class is used for managing the settings of a downstream key.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 5.2.19</remarks>
    public class DownstreamKey : IBMDSwitcherDownstreamKeyCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherDownstreamKey"/>
        /// </summary>
        private readonly IBMDSwitcherDownstreamKey InternalDownstreamKeyReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownstreamKey" /> class.
        /// </summary>
        /// <param name="downstreamKey">The native <seealso cref="IBMDSwitcherDownstreamKey"/> from the BMDSwitcherAPI.</param>
        public DownstreamKey(IBMDSwitcherDownstreamKey downstreamKey)
        {
            this.InternalDownstreamKeyReference = downstreamKey;
            this.InternalDownstreamKeyReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DownstreamKey"/> class.
        /// </summary>
        ~DownstreamKey()
        {
            this.InternalDownstreamKeyReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalDownstreamKeyReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="DownstreamKey"/>.
        /// </summary>
        /// <param name="sender">The <see cref="DownstreamKey"/> that received the event.</param>
        public delegate void DownstreamKeyEventHandler(object sender);

        /// <summary>
        /// The <see cref="InputCut"/> source changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnInputCutChanged;

        /// <summary>
        /// The <see cref="InputFill"/> source changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnInputFillChanged;

        /// <summary>
        /// The <see cref="Tie"/> flag changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnTieChanged;

        /// <summary>
        /// The <see cref="Rate"/> value changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnRateChanged;

        /// <summary>
        /// The <see cref="OnAir"/> flag changed. 
        /// </summary>
        public event DownstreamKeyEventHandler OnOnAirChanged;

        /// <summary>
        /// The <see cref="IsTransitioning"/> flag changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnIsTransitioningChanged;

        /// <summary>
        /// The <see cref="IsAutoTransitioning"/> flag changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnIsAutoTransitioningChanged;

        /// <summary>
        /// The <see cref="IsTransitionTowardsOnAir"/> flag changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnIsTransitionTowardsOnAIrChanged;

        /// <summary>
        /// The <see cref="FramesRemaining"/> value changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnFramesRemainingChanged;

        /// <summary>
        /// The <see cref="PreMultiplied"/> flag changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnPreMultipliedChanged;

        /// <summary>
        /// The <see cref="Clip"/> value changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnClipChanged;

        /// <summary>
        /// The <see cref="Gain"/> value changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnGainChanged;

        /// <summary>
        /// The <see cref="IsInverse"/> flag changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnInverseChanged;

        /// <summary>
        /// The <see cref="IsMasked"/> flag changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnMaskedChanged;

        /// <summary>
        /// The <see cref="MaskTop"/> value changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnMaskTopChanged;

        /// <summary>
        /// The <see cref="MaskBottom"/> value changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnMaskBottomChanged;

        /// <summary>
        /// The <see cref="MaskLeft"/> value changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnMaskLeftChanged;

        /// <summary>
        /// The <see cref="MaskRight"/> value changed.
        /// </summary>
        public event DownstreamKeyEventHandler OnMaskRightChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current cut input source.
        /// </summary>
        public long InputCut
        {
            get { return this.GetInputCut(); }
            set { this.SetInputCut(value); }
        }
        
        /// <summary>
        /// Gets or sets the current fill input source.
        /// </summary>
        public long InputFill
        {
            get { return this.GetInputFill(); }
            set { this.SetInputFill(value); }
        }

        /// <summary>
        /// Gets the availability of fill inputs for this downstream key.
        /// </summary>
        public _BMDSwitcherInputAvailability FillInputAvailabilityMask
        {
            get { return this.GetFillInputAvailabilityMask(); }
        }

        /// <summary>
        /// Gets the availability of cut inputs for this downstream key.
        /// </summary>
        public _BMDSwitcherInputAvailability CutInputAvailabilityMask
        {
            get { return this.GetCutInputAvailabilityMask(); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the tie flag is enabled.
        /// </summary>
        public bool Tie
        {
            get { return this.GetTie(); }
            set { this.SetTie(value); }
        }

        /// <summary>
        /// Gets or sets the transition rate value.
        /// </summary>
        public uint Rate
        {
            get { return this.GetRate(); }
            set { this.SetRate(value); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the on-air flag is enabled.
        /// </summary>
        public bool OnAir
        {
            get { return this.GetOnAir(); }
            set { this.SetOnAir(OnAir); }
        }

        /// <summary>
        /// Gets a value indicating whether this downstream key is transitioning or not.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.15</remarks>
        public bool IsTransitioning
        {
            get
            {
                this.InternalDownstreamKeyReference.IsTransitioning(out int isTransitioning);
                return Convert.ToBoolean(isTransitioning);
            }
        }

        /// <summary>
        /// The IsAutoTransitioning method returns whether this downstream key is auto-transitioning or not.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.16</remarks>
        public bool IsAutoTransitioning
        {
            get
            {
                this.InternalDownstreamKeyReference.IsAutoTransitioning(out int isAutoTransitioning);
                return Convert.ToBoolean(isAutoTransitioning);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this downstream key is transitioning towards or away from on-air.
        /// </summary>
        public bool IsTransitionTowardsOnAir
        {
            get
            {
                this.InternalDownstreamKeyReference.IsTransitionTowardsOnAir(out int isTransitionTowardsOnAir);
                return Convert.ToBoolean(isTransitionTowardsOnAir);
            }
        }

        /// <summary>
        /// Gets the number of frames remaining in the transition.
        /// </summary>
        public uint FramesRemaining
        {
            get { return this.GetFramesRemaining(); }
        }

        /// <summary>
        /// Gets or sets a value indicating the state of the pre-multiplied flag.
        /// </summary>
        public bool PreMultiplied
        {
            get { return this.GetPreMultiplied(); }
            set { this.SetPreMultiplied(value); }
        }

        /// <summary>
        /// Gets or sets the clip value.
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
        /// Gets or sets a value indicating whether the inverse flag is set.
        /// </summary>
        public bool IsInverse
        {
            get { return this.GetInverse(); }
            set { this.SetInverse(value); }
        }

        /// <summary>
        /// Gets or set a value indicating whether masking is enabled or not.
        /// </summary>
        public bool IsMasked
        {
            get { return this.GetMasked(); }
            set { this.SetMasked(value); }
        }

        /// <summary>
        /// Gets or sets the current mask top value.
        /// </summary>
        public double MaskTop
        {
            get { return this.GetMaskTop(); }
            set { this.SetMaskTop(value); }
        }

        /// <summary>
        /// Gets or sets the current mask bottom value.
        /// </summary>
        public double MaskBottom
        {
            get { return this.GetMaskBottom(); }
            set { this.SetMaskBottom(value); }
        }

        /// <summary>
        /// Gets or sets the current mask left value.
        /// </summary>
        public double MaskLeft
        {
            get { return this.GetMaskLeft(); }
            set { this.SetMaskLeft(value); }
        }

        /// <summary>
        /// Gets or sets the current mask right value.
        /// </summary>
        public double MaskRight
        {
            get { return this.GetMaskRight(); }
            set { this.SetMaskRight(value); }
        }
        #endregion

        #region IBMDSwitcherDownstreamKey
        /// <summary>
        /// The GetInputCut method returns the selected cut input source.
        /// </summary>
        /// <returns>BMDSwitcherInputId of the selected cut input source.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.1</remarks>
        public long GetInputCut()
        {
            this.InternalDownstreamKeyReference.GetInputCut(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputCut method sets the cut input source.
        /// </summary>
        /// <param name="input">The desired cut input source’s BMDSwitcherInputId.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.2</remarks>
        public void SetInputCut(long input)
        { 
            try
            {
                this.InternalDownstreamKeyReference.SetInputCut(input);
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
        /// The GetInputFill method returns the selected fill input source.
        /// </summary>
        /// <returns>BMDSwitcherInputId of the selected fill input source.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.3</remarks>
        public long GetInputFill()
        {
            this.InternalDownstreamKeyReference.GetInputFill(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputFill method sets the fill input source.
        /// </summary>
        /// <param name="input">The desired fill input source’s BMDSwitcherInputId.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.4</remarks>
        public void SetInputFill(long input)
        { 
            try
            {
                this.InternalDownstreamKeyReference.SetInputFill(input);
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
        /// The GetFillInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask value for fill inputs available to this downstream key.The input availability property of an IBMDSwitcherInput can be bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this input is available for use as a fill input for this downstream key.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.5</remarks>
        public _BMDSwitcherInputAvailability GetFillInputAvailabilityMask()
        {
            this.InternalDownstreamKeyReference.GetFillInputAvailabilityMask(out _BMDSwitcherInputAvailability mask);
            return mask;
        }

        /// <summary>
        /// The GetCutInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask value for cut inputs available to this downstream key.The input availability property of an IBMDSwitcherInput can be bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this input is available for use as a cut input for this downstream key.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.6</remarks>
        public _BMDSwitcherInputAvailability GetCutInputAvailabilityMask()
        {
            this.InternalDownstreamKeyReference.GetCutInputAvailabilityMask(out _BMDSwitcherInputAvailability mask);
            return mask;
        }

        /// <summary>
        /// The GetTie method gets the current tie flag.
        /// </summary>
        /// <returns>Boolean tie flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.7</remarks>
        public bool GetTie()
        {
            this.InternalDownstreamKeyReference.GetTie(out int tie);
            return Convert.ToBoolean(tie);
        }

        /// <summary>
        /// The SetTie method sets the tie flag.
        /// </summary>
        /// <param name="tie">The desired tie flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.8</remarks>
        public void SetTie(bool tie)
        { 
            try
            {
                this.InternalDownstreamKeyReference.SetTie(Convert.ToInt32(tie));
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
        /// The GetRate method gets the current rate value.
        /// </summary>
        /// <returns>The current rate value in frames.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.9</remarks>
        public uint GetRate()
        {
            this.InternalDownstreamKeyReference.GetRate(out uint frames);
            return frames;
        }

        /// <summary>
        /// The SetRate method sets the rate value.
        /// </summary>
        /// <param name="rate">The desired rate value in frames.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.10</remarks>
        public void SetRate(uint rate)
        { 
            try
            {
                this.InternalDownstreamKeyReference.SetRate(rate);
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
        /// The GetOnAir method returns the on-air flag.
        /// </summary>
        /// <returns>Boolean on-air flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.11</remarks>
        public bool GetOnAir()
        {
            this.InternalDownstreamKeyReference.GetOnAir(out int onAir);
            return Convert.ToBoolean(onAir);
        }

        /// <summary>
        /// The SetOnAir method sets the on-air flag.
        /// </summary>
        /// <param name="onAir">The desired on-air flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.12</remarks>
        public void SetOnAir(bool onAir)
        { 
            try
            {
                this.InternalDownstreamKeyReference.SetOnAir(Convert.ToInt32(onAir));
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
        /// Use the PerformAutoTransition method to start an auto-transition.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.13</remarks>
        public void PerformAutoTransition()
        { 
            try
            {
                this.InternalDownstreamKeyReference.PerformAutoTransition();
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
        /// The PerformAutoTransitionInDirection method performs an auto-transition in the specified direction, either towards on-air or away from on-air.
        /// </summary>
        /// <param name="towardsOnAir">The desired direction.</param>
        /// <exception cref="FailedException">Failure.</exception>
        public void PerformAutoTransitionInDirection(bool towardsOnAir)
        { 
            try
            {
                this.InternalDownstreamKeyReference.PerformAutoTransitionInDirection(Convert.ToInt32(towardsOnAir));
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
        /// The GetFramesRemaining method gets the number of frames remaining in the transition.
        /// </summary>
        /// <returns>Number of frames remaining in the transition.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.18</remarks>
        public uint GetFramesRemaining()
        {
            this.InternalDownstreamKeyReference.GetFramesRemaining(out uint framesRemaining);
            return framesRemaining;
        }

        /// <summary>
        /// The GetPreMultiplied method returns the current pre-multiplied flag.
        /// </summary>
        /// <returns>The current pre-multiplied flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.19</remarks>
        public bool GetPreMultiplied()
        {
            this.InternalDownstreamKeyReference.GetPreMultiplied(out int preMultiplied);
            return Convert.ToBoolean(preMultiplied);
        }

        /// <summary>
        /// The SetPreMultiplied method sets the pre-multiplied flag. Note that clip, gain and inverse controls are not used when pre-multiplied flag is set to true.
        /// </summary>
        /// <param name="preMultiplied">The desired pre-multiplied flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.20</remarks>
        public void SetPreMultiplied(bool preMultiplied)
        { 
            try
            {
                this.InternalDownstreamKeyReference.SetPreMultiplied(Convert.ToInt32(preMultiplied));
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.21</remarks>
        public double GetClip()
        {
            this.InternalDownstreamKeyReference.GetClip(out double clip);
            return clip;
        }

        /// <summary>
        /// The SetClip method sets the clip value.
        /// </summary>
        /// <param name="clip">The desired clip value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.22</remarks>
        public void SetClip(double clip)
        { 
            try
            {
                this.InternalDownstreamKeyReference.SetClip(clip);
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
        /// The GetGain method returns the current gain value.
        /// </summary>
        /// <returns>The current gain value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.23</remarks>
        public double GetGain()
        {
            this.InternalDownstreamKeyReference.GetGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetGain method sets the gain value.
        /// </summary>
        /// <param name="gain">The desired gain value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.24</remarks>
        public void SetGain(double gain)
        { 
            try
            {
                this.InternalDownstreamKeyReference.SetGain(gain);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.25</remarks>
        public bool GetInverse()
        {
            this.InternalDownstreamKeyReference.GetInverse(out int inverse);
            return Convert.ToBoolean(inverse);
        }

        /// <summary>
        /// The SetInverse method sets the inverse flag.
        /// </summary>
        /// <param name="inverse">The desired inverse flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.26</remarks>
        public void SetInverse(bool inverse)
        { 
            try
            {
                this.InternalDownstreamKeyReference.SetInverse(Convert.ToInt32(inverse));
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
        /// The GetMasked method returns whether masking is enabled or not.
        /// </summary>
        /// <returns>Boolean flag of whether masking is enabled.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.27</remarks>
        public bool GetMasked()
        {
            this.InternalDownstreamKeyReference.GetMasked(out int maskEnabled);
            return Convert.ToBoolean(maskEnabled);
        }

        /// <summary>
        /// Use SetMasked method to enable or disable masking.
        /// </summary>
        /// <param name="maskEnabled">The desired masked value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.28</remarks>
        public void SetMasked(bool maskEnabled)
        {
            try
            {
                this.InternalDownstreamKeyReference.SetMasked(Convert.ToInt32(maskEnabled));
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
        /// The GetMaskTop method returns the current mask top value.
        /// </summary>
        /// <returns>The current mask top value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.29/remarks>
        public double GetMaskTop()
        {
            this.InternalDownstreamKeyReference.GetMaskTop(out double maskTop);
            return maskTop;
        }

        /// <summary>
        /// The SetMaskTop method sets the mask top value.
        /// </summary>
        /// <param name="maskTop">The desired mask top value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.30</remarks>
        public void SetMaskTop(double maskTop)
        {
            try
            {
                this.InternalDownstreamKeyReference.SetMaskTop(maskTop);
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
        /// The GetMaskBottom method returns the current mask bottom value.
        /// </summary>
        /// <returns>The current mask bottom value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.31</remarks>
        public double GetMaskBottom()
        {
            this.InternalDownstreamKeyReference.GetMaskBottom(out double maskBottom);
            return maskBottom;
        }

        /// <summary>
        /// The SetMaskBottom method sets the mask bottom value.
        /// </summary>
        /// <param name="maskBottom">The desired mask bottom value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.32</remarks>
        public void SetMaskBottom(double maskBottom)
        {
            try
            {
                this.InternalDownstreamKeyReference.SetMaskBottom(maskBottom);
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
        /// The GetMaskLeft method returns the current mask left value
        /// </summary>
        /// <returns>The current mask left value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.33</remarks>
        public double GetMaskLeft()
        {
            this.InternalDownstreamKeyReference.GetMaskLeft(out double maskLeft);
            return maskLeft;
        }

        /// <summary>
        /// The SetMaskLeft method sets the mask left value.
        /// </summary>
        /// <param name="maskLeft">The desired mask left value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.34</remarks>
        public void SetMaskLeft(double maskLeft)
        {
            try
            {
                this.InternalDownstreamKeyReference.SetMaskLeft(maskLeft);
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
        /// The GetMaskRight method returns the current mask right value.
        /// </summary>
        /// <returns>The current mask right value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.35</remarks>
        public double GetMaskRight()
        {
            this.InternalDownstreamKeyReference.GetMaskRight(out double maskRight);
            return maskRight;
        }

        /// <summary>
        /// The SetMaskRight method sets the mask right value.
        /// </summary>
        /// <param name="maskRight">The desired mask right value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.36</remarks>
        public void SetMaskRight(double maskRight)
        {
            try
            {
                this.InternalDownstreamKeyReference.SetMaskRight(maskRight);
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
        /// The ResetMask method resets the mask settings to default values.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.19.37</remarks>
        public void ResetMask()
        {
            try
            {
                this.InternalDownstreamKeyReference.ResetMask();
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

        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherDownstreamKey events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherDownstreamKeyEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.20.1</remarks>
        void IBMDSwitcherDownstreamKeyCallback.Notify(_BMDSwitcherDownstreamKeyEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeInputCutChanged:
                    this.OnInputCutChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeInputFillChanged:
                    this.OnInputFillChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeTieChanged:
                    this.OnTieChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeRateChanged:
                    this.OnRateChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeOnAirChanged:
                    this.OnOnAirChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeIsTransitioningChanged:
                    this.OnIsTransitioningChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeIsAutoTransitioningChanged:
                    this.OnIsAutoTransitioningChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeIsTransitionTowardsOnAirChanged:
                    this.OnIsTransitionTowardsOnAIrChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeFramesRemainingChanged:
                    this.OnFramesRemainingChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypePreMultipliedChanged:
                    this.OnPreMultipliedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeClipChanged:
                    this.OnClipChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeGainChanged:
                    this.OnGainChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeInverseChanged:
                    this.OnInverseChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeMaskedChanged:
                    this.OnMaskedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeMaskTopChanged:
                    this.OnMaskTopChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeMaskBottomChanged:
                    this.OnMaskBottomChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeMaskLeftChanged:
                    this.OnMaskLeftChanged?.Invoke(this);
                    break;

                case _BMDSwitcherDownstreamKeyEventType.bmdSwitcherDownstreamKeyEventTypeMaskRightChanged:
                    this.OnMaskRightChanged?.Invoke(this);
                    break;
            }

            return;
        }
    }
}
