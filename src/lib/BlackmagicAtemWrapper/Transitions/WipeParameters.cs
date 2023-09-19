//-----------------------------------------------------------------------------
// <copyright file="WipeParameters.cs">
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
    /// The WipeParameters class is used for manipulating transition settings specific to wipe parameters.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 3.2.6</remarks>
    public class WipeParameters : IBMDSwitcherTransitionWipeParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherMixEffectBlock"/>.
        /// </summary>
        private readonly IBMDSwitcherTransitionWipeParameters InternalWipeParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="WipeParameters"/> class.
        /// </summary>
        /// <param name="wipeParameters">The native <seealso cref="IBMDSwitcherTransitionWipeParameters"/> from the BMDSwitcherAPI.</param>
        public WipeParameters(IBMDSwitcherTransitionWipeParameters wipeParameters)
        {
            this.InternalWipeParametersReference = wipeParameters ?? throw new ArgumentNullException(nameof(wipeParameters));
            this.InternalWipeParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="WipeParameters"/> class.
        /// </summary>
        ~WipeParameters()
        {
            this.InternalWipeParametersReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalWipeParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="WipeParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="WipeParameters"/> that received the event.</param>
        public delegate void WipeParametersEventHandler(object sender);

        /// <summary>
        /// The <see cref="Rate"/> changed.
        /// </summary>
        public event WipeParametersEventHandler OnRateChanged;

        /// <summary>
        /// The <see cref="Pattern"/> value changed.
        /// </summary>
        public event WipeParametersEventHandler OnPatternChanged;

        /// <summary>
        /// The <see cref="BorderSize"/> value changed.
        /// </summary>
        public event WipeParametersEventHandler OnBorderSizeChanged;

        /// <summary>
        /// The <see cref="BorderInput"/> value changed.
        /// </summary>
        public event WipeParametersEventHandler OnBorderInputChanged;

        /// <summary>
        /// The <see cref="Symmetry"/> value changed.
        /// </summary>
        public event WipeParametersEventHandler OnSymmetryChanged;

        /// <summary>
        /// The <see cref="Softness"/> value changed.
        /// </summary>
        public event WipeParametersEventHandler OnSoftnessChanged;

        /// <summary>
        /// The <see cref="HorizontalOffset"/> value changed.
        /// </summary>
        public event WipeParametersEventHandler OnHorizontalOffsetChanged;

        /// <summary>
        /// The <see cref="VerticalOffset"/> value changed.
        /// </summary>
        public event WipeParametersEventHandler OnVerticalOffsetChanged;

        /// <summary>
        /// The <see cref="IsReverse"/> flag changed.
        /// </summary>
        public event WipeParametersEventHandler OnReverseChanged;

        /// <summary>
        /// The <see cref="IsFlipFlopped"/> flag changed.
        /// </summary>
        public event WipeParametersEventHandler OnFlipFlopChanged;
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
        /// Gets or sets the current pattern style.
        /// </summary>
        public _BMDSwitcherPatternStyle Pattern
        {
            get { return this.GetPattern(); }
            set { this.SetPattern(value); }
        }

        /// <summary>
        /// Gets or sets the current border size.
        /// </summary>
        public double BorderSize
        {
            get { return this.GetBorderSize(); }
            set { this.SetBorderSize(value); }
        }

        /// <summary>
        /// Gets or sets the current border input.
        /// </summary>
        public long BorderInput
        {
            get { return this.GetInputBorder(); }
            set { this.SetInputBorder(value); }
        }

        /// <summary>
        /// Gets or sets the current symmetry
        /// </summary>
        public double Symmetry
        {
            get { return this.GetSymmetry(); }
            set { this.SetSymmetry(value); }
        }

        /// <summary>
        /// Gets or sets the current softness.
        /// </summary>
        public double Softness
        {
            get { return this.GetSoftness(); }
            set { this.SetSoftness(value); }
        }

        /// <summary>
        /// Gets or sets the current horizontal offset.
        /// </summary>
        public double HorizontalOffset
        {
            get { return this.GetHorizontalOffset(); }
            set { this.SetHorizontalOffset(value); }
        }

        /// <summary>
        /// Gets or sets the current vertical offset.
        /// </summary>
        public double VerticalOffset
        {
            get { return this.GetVerticalOffset(); }
            set { this.SetVerticalOffset(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the reverse flag is set.
        /// </summary>
        public bool IsReverse
        {
            get { return this.GetReverse(); }
            set { this.SetReverse(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the flip flop flag is set.
        /// </summary>
        public bool IsFlipFlopped
        {
            get { return this.GetFlipFlop(); }
            set { this.SetFlipFlop(value); }
        }
        #endregion

        #region IBMDSwitcherTransitionWipeParameters
        /// <summary>
        /// The GetRate method returns the current rate in frames.
        /// </summary>
        /// <returns>The current rate.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.1</remarks>
        public uint GetRate()
        {
            this.InternalWipeParametersReference.GetRate(out uint frameRate);
            return frameRate;
        }

        /// <summary>
        /// The SetRate method sets the rate in frames.
        /// </summary>
        /// <param name="frameRate">The desired rate in frames.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.2</remarks>
        public void SetRate(uint frameRate)
        {
            try
            {
                this.InternalWipeParametersReference.SetRate(frameRate);
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
        /// The GetPattern method returns the current pattern style.
        /// </summary>
        /// <returns>The current pattern.</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.3</remarks>
        public _BMDSwitcherPatternStyle GetPattern()
        { 
            try
            {
                this.InternalWipeParametersReference.GetPattern(out _BMDSwitcherPatternStyle patternStyle);
                return patternStyle;
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
        /// Te SetPattern method sets the pattern style.
        /// </summary>
        /// <param name="pattern">The desired pattern.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.4</remarks>
        /// <bug>Function description is for SetRate</bug>
        public void SetPattern(_BMDSwitcherPatternStyle pattern)
        { 
            try
            {
                this.InternalWipeParametersReference.SetPattern(pattern);
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
        /// The GetBorderSize method returns the current border size.
        /// </summary>
        /// <returns>The current border size.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.5</remarks>
        public double GetBorderSize()
        {
            this.InternalWipeParametersReference.GetBorderSize(out double size);
            return size;
        }

        /// <summary>
        /// The SetBorderSize method sets the border size.
        /// </summary>
        /// <param name="size">The desired border size.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.6</remarks>
        public void SetBorderSize(double size)
        { 
            try
            {
                this.InternalWipeParametersReference.SetBorderSize(size);
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
        /// The GetInputBorder method returns the current border input.
        /// </summary>
        /// <returns>The current border input.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.7</remarks>
        public long GetInputBorder()
        {
            this.InternalWipeParametersReference.GetInputBorder(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputBorder method sets the border input.
        /// </summary>
        /// <param name="input">The desired border input.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.8</remarks>
        /// <bug>Input parameters has "92.323" as the description for 'input'</bug>
        public void SetInputBorder(long input)
        { 
            try
            {
                this.InternalWipeParametersReference.SetInputBorder(input);
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
        /// The GetSymmetry method returns the current symmetry.
        /// </summary>
        /// <returns>The current symmetry.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.9</remarks>
        public double GetSymmetry()
        {
            this.InternalWipeParametersReference.GetSymmetry(out double symmetry);
            return symmetry;
        }

        /// <summary>
        /// The SetSymmetry method sets the symmetry.
        /// </summary>
        /// <param name="symmetry">The desired symmetry.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.10</remarks>
        public void SetSymmetry(double symmetry)
        { 
            try
            {
                this.InternalWipeParametersReference.SetSymmetry(symmetry);
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
        /// The GetSoftness method returns the current softness.
        /// </summary>
        /// <returns>The current softness.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.11</remarks>
        public double GetSoftness()
        {
            this.InternalWipeParametersReference.GetSoftness(out double soft);
            return soft;
        }

        /// <summary>
        /// The SetSoftness method sets the softness.
        /// </summary>
        /// <param name="softness">The desired softness.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.12</remarks>
        public void SetSoftness(double softness)
        { 
            try
            {
                this.InternalWipeParametersReference.SetSoftness(softness);
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
        /// The GetHorizontalOffset method returns the current horizontal offset.
        /// </summary>
        /// <returns>The current horizontal offset.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.13</remarks>
        public double GetHorizontalOffset()
        {
            this.InternalWipeParametersReference.GetHorizontalOffset(out double hOffset);
            return hOffset;
        }

        /// <summary>
        /// The SetHorizontalOffset method sets the horizontal offset.
        /// </summary>
        /// <param name="hOffset">The desired horizontal offset.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.14</remarks>
        public void SetHorizontalOffset(double hOffset)
        { 
            try
            {
                this.InternalWipeParametersReference.SetHorizontalOffset(hOffset);
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
        /// The GetVerticalOffset method returns the current vertical offset.
        /// </summary>
        /// <returns>The current vertical offset.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.15</remarks>
        public double GetVerticalOffset()
        {
            this.InternalWipeParametersReference.GetVerticalOffset(out double vOffset);
            return vOffset;
        }

        /// <summary>
        /// The SetVerticalOffset method sets the vertical offset.
        /// </summary>
        /// <param name="vOffset">The desired vertical offset.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.16</remarks>
        public void SetVerticalOffset(double vOffset)
        { 
            try
            {
                this.InternalWipeParametersReference.SetVerticalOffset(vOffset);
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
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.17</remarks>
        public bool GetReverse()
        {
            this.InternalWipeParametersReference.GetReverse(out int reverse);
            return Convert.ToBoolean(reverse);
        }

        /// <summary>
        /// The SetReverse method sets the reverse flag.
        /// </summary>
        /// <param name="reverse">The desired reverse flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.18</remarks>
        public void SetReverse(bool reverse)
        { 
            try
            {
                this.InternalWipeParametersReference.SetReverse(Convert.ToInt32(reverse));
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
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.19</remarks>
        public bool GetFlipFlop()
        {
            this.InternalWipeParametersReference.GetFlipFlop(out int flipflop);
            return Convert.ToBoolean(flipflop);
        }

        /// <summary>
        /// The SetFlipFlop method sets the flip flop flag.
        /// </summary>
        /// <param name="flipflop">The desired flip flop flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.6.20</remarks>
        public void SetFlipFlop(bool flipflop)
        { 
            try
            {
                this.InternalWipeParametersReference.SetFlipFlop(Convert.ToInt32(flipflop));
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

        #region IBMDSwitcherTransitionWipeParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherTransitionWipeParameters events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherTransitionWipeParametersEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 3.2.7.1</remarks>
        void IBMDSwitcherTransitionWipeParametersCallback.Notify(_BMDSwitcherTransitionWipeParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeRateChanged:
                    this.OnRateChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypePatternChanged:
                    this.OnPatternChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeBorderSizeChanged:
                    this.OnBorderSizeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeInputBorderChanged:
                    this.OnBorderInputChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeSymmetryChanged:
                    this.OnSymmetryChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeSoftnessChanged:
                    this.OnSoftnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeHorizontalOffsetChanged:
                    this.OnHorizontalOffsetChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeVerticalOffsetChanged:
                    this.OnVerticalOffsetChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeReverseChanged:
                    this.OnReverseChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionWipeParametersEventType.bmdSwitcherTransitionWipeParametersEventTypeFlipFlopChanged:
                    this.OnFlipFlopChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
