//-----------------------------------------------------------------------------
// <copyright file="PatternParameters.cs">
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
    /// The PatternParameters class is used for manipulating settings specific to the pattern type key.
    /// </summary>
    public class PatternParameters : IBMDSwitcherKeyPatternParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherKeyPatternParameters"/>
        /// </summary>
        private readonly IBMDSwitcherKeyPatternParameters InternalPatternParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatternParameters" /> class.
        /// </summary>
        /// <param name="patternParameters">The native <seealso cref="IBMDSwitcherKeyPatternParameters"/> from the BMDSwitcherAPI.</param>
        public PatternParameters(IBMDSwitcherKeyPatternParameters patternParameters)
        {
            this.InternalPatternParametersReference = patternParameters ?? throw new ArgumentNullException(nameof(patternParameters));
            this.InternalPatternParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="PatternParameters"/> class.
        /// </summary>
        ~PatternParameters()
        {
            this.InternalPatternParametersReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalPatternParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="PatternParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="PatternParameters"/> that received the event.</param>
        public delegate void PatternParametersEventHandler(object sender);

        /// <summary>
        /// The <see cref="Pattern"/> changed. 
        /// </summary>
        public event PatternParametersEventHandler OnPatternChanged;

        /// <summary>
        /// The <see cref="Size"/> value changed.
        /// </summary>
        public event PatternParametersEventHandler OnSizeChanged;

        /// <summary>
        /// The <see cref="Symmetry"/> value changed. 
        /// </summary>
        public event PatternParametersEventHandler OnSymmetryChanged;

        /// <summary>
        /// The <see cref="Softness"/> value changed.
        /// </summary>
        public event PatternParametersEventHandler OnSoftnessChanged;

        /// <summary>
        /// The <see cref="HorizontalOffset"/> changed.
        /// </summary>
        public event PatternParametersEventHandler OnHorizontalOffsetChanged;

        /// <summary>
        /// The <see cref="VerticalOffset"/> changed.
        /// </summary>
        public event PatternParametersEventHandler OnVerticalOffsetChanged;

        /// <summary>
        /// The <see cref="IsInverse"/> flag changed.
        /// </summary>
        public event PatternParametersEventHandler OnInverseChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current pattern style.
        /// </summary>
        public _BMDSwitcherPatternStyle Pattern
        {
            get { return this.GetPattern(); }
            set { this.SetPattern(value); }
        }

        /// <summary>
        /// Gets or sets the current size value.
        /// </summary>
        public double Size
        {
            get { return this.GetSize(); }
            set { this.SetSize(value); }
        }

        /// <summary>
        /// Gets or sets the current symmetry value.
        /// </summary>
        public double Symmetry
        {
            get { return this.GetSymmetry(); }
            set { this.SetSymmetry(value); }
        }

        /// <summary>
        /// Gets or sets the current softness value.
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
        /// Gets or sets a value indicating whether the inverse flag is set.
        /// </summary>
        public bool IsInverse
        {
            get { return this.GetInverse(); }
            set { this.SetInverse(value); }
        }
        #endregion

        #region IBMDSwitcherKeyPatternParameters
        /// <summary>
        /// The GetPattern method gets the current pattern style.
        /// </summary>
        /// <returns>The current pattern style of BMDSwitcherPatternStyle.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.1</remarks>
        public _BMDSwitcherPatternStyle GetPattern()
        {
            this.InternalPatternParametersReference.GetPattern(out _BMDSwitcherPatternStyle pattern);
            return pattern;
        }

        /// <summary>
        /// The SetPattern method sets the pattern style.
        /// </summary>
        /// <param name="pattern">The desired BMDSwitcherPatternStyle pattern style.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.2</remarks>
        public void SetPattern(_BMDSwitcherPatternStyle pattern)
        {
            try
            {
                this.InternalPatternParametersReference.SetPattern(pattern);
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
        /// The GetSize method gets the current size value.
        /// </summary>
        /// <returns>The current size value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.3</remarks>
        public double GetSize()
        {
            this.InternalPatternParametersReference.GetSize(out double size);
            return size;
        }

        /// <summary>
        /// The GetSymmetry method gets the current symmetry value.
        /// </summary>
        /// <returns>The current symmetry value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.5</remarks>
        public double GetSymmetry()
        {
            this.InternalPatternParametersReference.GetSymmetry(out double symmetry);
            return symmetry;
        }

        /// <summary>
        /// The SetSymmetry method sets the symmetry value.
        /// </summary>
        /// <param name="symmetry">The desired symmetry value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.6</remarks>
        public void SetSymmetry(double symmetry)
        { 
            try
            {
                this.InternalPatternParametersReference.SetSymmetry(symmetry);
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
        /// The GetSymmetry method gets the current symmetry value.
        /// </summary>
        /// <param name="size">The current symmetry value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.5</remarks>
        public void SetSize(double size)
        { 
            try
            {
                this.InternalPatternParametersReference.SetSize(size);
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
        /// The GetSoftness method gets the current softness value.
        /// </summary>
        /// <returns>The current softness value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.7</remarks>
        public double GetSoftness()
        {
            this.InternalPatternParametersReference.GetSoftness(out double softness);
            return softness;
        }

        /// <summary>
        /// The SetSoftness method sets the softness value.
        /// </summary>
        /// <param name="softness">The desired softness value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.8</remarks>
        public void SetSoftness(double softness)
        { 
            try
            {
                this.InternalPatternParametersReference.SetSoftness(softness);
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
        /// The GetHorizontalOffset method gets the current horizontal offset value.
        /// </summary>
        /// <returns>The current horizontal offset value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.9</remarks>
        public double GetHorizontalOffset()
        {
            this.InternalPatternParametersReference.GetHorizontalOffset(out double hOffset);
            return hOffset;
        }

        /// <summary>
        /// The SetHorizontalOffset method sets the horizontal offset value.
        /// </summary>
        /// <param name="hOffset">The desired horizontal offset value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.10</remarks>
        public void SetHorizontalOffset(double hOffset)
        { 
            try
            {
                this.InternalPatternParametersReference.SetHorizontalOffset(hOffset);
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
        /// The GetVerticalOffset method gets the current vertical offset value.
        /// </summary>
        /// <returns>The current vertical offset value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.11</remarks>
        public double GetVerticalOffset()
        {
            this.InternalPatternParametersReference.GetVerticalOffset(out double vOffset);
            return vOffset;
        }

        /// <summary>
        /// The SetVerticalOffset method sets the vertical offset value.
        /// </summary>
        /// <param name="vOffset">The desired vertical offset value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.12</remarks>
        public void SetVerticalOffset(double vOffset)
        { 
            try
            {
                this.InternalPatternParametersReference.SetVerticalOffset(vOffset);
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
        /// The GetInverse method gets the current inverse flag.
        /// </summary>
        /// <returns>The current inverse flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.13</remarks>
        public bool GetInverse()
        {
            this.InternalPatternParametersReference.GetInverse(out int inverse);
            return Convert.ToBoolean(inverse);
        }

        /// <summary>
        /// The SetInverse method sets the inverse flag.
        /// </summary>
        /// <param name="inverse">The desired inverse flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.10.14</remarks>
        public void SetInverse(bool inverse)
        { 
            try
            {
                this.InternalPatternParametersReference.SetInverse(Convert.ToInt32(inverse));
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

        #region IBMDSwitcherKeyPatternParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherKeyPatternParameters events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherKeyPatternParametersEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.11.1</remarks>
        void IBMDSwitcherKeyPatternParametersCallback.Notify(_BMDSwitcherKeyPatternParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypePatternChanged:
                    this.OnPatternChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeSizeChanged:
                    this.OnSizeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeSymmetryChanged:
                    this.OnSymmetryChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeSoftnessChanged:
                    this.OnSoftnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeHorizontalOffsetChanged:
                    this.OnHorizontalOffsetChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeVerticalOffsetChanged:
                    this.OnVerticalOffsetChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyPatternParametersEventType.bmdSwitcherKeyPatternParametersEventTypeInverseChanged:
                    this.OnInverseChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
