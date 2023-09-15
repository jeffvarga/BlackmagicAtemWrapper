//-----------------------------------------------------------------------------
// <copyright file="SuperSourceBox.cs">
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

namespace BlackmagicAtemWrapper.SuperSource
{
    using System;
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The SuperSourceBox class is used for manipulating supersource box settings.
    /// </summary>
    /// <remarks>Wraps Blackmagic Switcher SDK - 6.2.4</remarks>
    public class SuperSourceBox : IBMDSwitcherSuperSourceBoxCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherSuperSourceBox"/>.
        /// </summary>
        internal IBMDSwitcherSuperSourceBox InternalSuperSourceBoxReference;

        /// <summary>
        /// Initializes an instance of the <see cref="SuperSourceBox"/> class.
        /// </summary>
        /// <param name="superSourceBox">The native <seealso cref="IBMDSwitcherSuperSourceBox"/> from the BMDSwitcherAPI</param>
        /// <exception cref="ArgumentNullException"><paramref name="superSourceBox"/> was null.</exception>
        public SuperSourceBox(IBMDSwitcherSuperSourceBox superSourceBox)
        {
            this.InternalSuperSourceBoxReference = superSourceBox ?? throw new ArgumentNullException(nameof(superSourceBox));
            this.InternalSuperSourceBoxReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SuperSourceBox"/> class.
        /// </summary>
        ~SuperSourceBox()
        {
            this.InternalSuperSourceBoxReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalSuperSourceBoxReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="SuperSourceBox"/>.
        /// </summary>
        /// <param name="sender">The <see cref="SuperSourceBox"/> that received the event.</param>
        public delegate void SuperSourceBoxEventHandler(object sender);

        /// <summary>
        /// The <see cref="InputSource"/> changed.
        /// </summary>
        public event SuperSourceBoxEventHandler OnInputSourceChanged;

        /// <summary>
        /// The <see cref="PositionX"/> changed.
        /// </summary>
        public event SuperSourceBoxEventHandler OnPositionXChanged;

        /// <summary>
        /// The <see cref="PositionY"/> changed.
        /// </summary>
        public event SuperSourceBoxEventHandler OnPositionYChanged;

        /// <summary>
        /// The <see cref="Size"/> changed.
        /// </summary>
        public event SuperSourceBoxEventHandler OnSizeChanged;

        /// <summary>
        /// The <see cref="IsCropped"/> flag changed.
        /// </summary>
        public event SuperSourceBoxEventHandler OnCroppedChanged;

        /// <summary>
        /// The <see cref="CropTop"/> value changed.
        /// </summary>
        public event SuperSourceBoxEventHandler OnCropTopChanged;

        /// <summary>
        /// The <see cref="CropBottom"/> value changed.
        /// </summary>
        public event SuperSourceBoxEventHandler OnCropBottomChanged;

        /// <summary>
        /// The <see cref="CropLeft"/> value changed.
        /// </summary>
        public event SuperSourceBoxEventHandler OnCropLeftChanged;

        /// <summary>
        /// The <see cref="CropRight"/> value changed.
        /// </summary>
        public event SuperSourceBoxEventHandler OnCropRightChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the SuperSourceBox is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return this.GetEnabled(); }
            set { this.SetEnabled(value); }
        }

        /// <summary>
        /// Gets or sets the input source.
        /// </summary>
        public long InputSource
        {
            get { return this.GetInputSource(); }
            set { this.SetInputSource(value); }
        }

        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        public double PositionX
        {
            get { return this.GetPositionX(); }
            set { this.SetPositionX(value); }
        }

        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        public double PositionY
        {
            get { return this.GetPositionY(); }
            set { this.SetPositionY(value); }
        }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public double Size
        {
            get { return this.GetSize(); }
            set { this.SetSize(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="SuperSourceBox"/> is cropped.
        /// </summary>
        public bool IsCropped
        {
            get { return this.GetCropped(); }
            set { this.SetCropped(value); }
        }

        /// <summary>
        /// Gets or sets the top crop value.
        /// </summary>
        public double CropTop
        {
            get { return this.GetCropTop(); }
            set { this.SetCropTop(value); }
        }

        /// <summary>
        /// Gets or sets the bottom crop value.
        /// </summary>
        public double CropBottom
        {
            get { return this.GetCropBottom(); }
            set { this.SetCropBottom(value); }
        }

        /// <summary>
        /// Gets or sets the left crop value.
        /// </summary>
        public double CropLeft
        {
            get { return this.GetCropLeft(); }
            set { this.SetCropLeft(value); }
        }

        /// <summary>
        /// Gets or sets the right crop value.
        /// </summary>
        public double CropRight
        {
            get { return this.GetCropRight(); }
            set { this.SetCropRight(value); }
        }

        /// <summary>
        /// Gets the input availability mask.
        /// </summary>
        public _BMDSwitcherInputAvailability InputAvailability
        {
            get { return this.GetInputAvailability(); }
        }
        #endregion

        #region IBMDSwitcherSuperSourceBox
        /// <summary>
        /// The GetEnabled method returns the current enabled flag. Enabled supersource boxes are included in the corresponding supersource input.
        /// </summary>
        /// <returns>The current enabled flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.1</remarks>
        public bool GetEnabled()
        {
            this.InternalSuperSourceBoxReference.GetEnabled(out int enabled);
            return Convert.ToBoolean(enabled);
        }

        /// <summary>
        /// The SetEnabled method sets the enabled flag. Enabled supersource boxes are included in the corresponding supersource input.
        /// </summary>
        /// <param name="enabled">The desired enabled flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.2</remarks>
        public void SetEnabled(bool enabled)
        {
            try
            {
                this.InternalSuperSourceBoxReference.SetEnabled(Convert.ToInt32(enabled));
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
        /// The GetInputSource method returns the current input source.
        /// </summary>
        /// <returns>The current input source’s BMDSwitcherInputId.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.3</remarks>
        public long GetInputSource()
        {
            this.InternalSuperSourceBoxReference.GetInputSource(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputSource method sets the input source.
        /// </summary>
        /// <param name="input">The desired input source’s BMDSwitcherInputId.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.4</remarks>
        public void SetInputSource(long input)
        {
            try
            {
                this.InternalSuperSourceBoxReference.SetInputSource(input);
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
        /// The GetPositionX method returns the current x position.
        /// </summary>
        /// <returns>The current x position.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.5</remarks>
        public double GetPositionX()
        {
            this.InternalSuperSourceBoxReference.GetPositionX(out double positionX);
            return positionX;
        }

        /// <summary>
        /// The SetPositionX method sets the x position.
        /// </summary>
        /// <param name="positionX">The desired x position.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.6</remarks>
        public void SetPositionX(double positionX)
        {
            try
            {
                this.InternalSuperSourceBoxReference.SetPositionX(positionX);
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
        /// The GetPositionY method returns the current y position.
        /// </summary>
        /// <returns>The current y position.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.7</remarks>
        public double GetPositionY()
        {
            this.InternalSuperSourceBoxReference.GetPositionY(out double positionY);
            return positionY;
        }

        /// <summary>
        /// The SetPositionY method sets the y position.
        /// </summary>
        /// <param name="positionY">The desired y position.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.8</remarks>
        public void SetPositionY(double positionY)
        {
            try
            {
                this.InternalSuperSourceBoxReference.SetPositionY(positionY);
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
        /// The GetSize method returns the current size.
        /// </summary>
        /// <returns>The current size.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.9</remarks>
        public double GetSize()
        {
            this.InternalSuperSourceBoxReference.GetSize(out double size);
            return size;
        }

        /// <summary>
        /// The SetSize method sets the size.
        /// </summary>
        /// <param name="size">The desired size.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.10</remarks>
        public void SetSize(double size)
        {
            try
            {
                this.InternalSuperSourceBoxReference.SetSize(size);
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
        /// The GetCropped method returns the current cropped flag
        /// </summary>
        /// <returns>The current cropped flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.11</remarks>
        public bool GetCropped()
        {
            this.InternalSuperSourceBoxReference.GetCropped(out int cropped);
            return Convert.ToBoolean(cropped);
        }

        /// <summary>
        /// The SetCropped method sets the cropped flag.
        /// </summary>
        /// <param name="cropped">The desired cropped flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.12</remarks>
        public void SetCropped(bool cropped)
        {
            try
            {
                this.InternalSuperSourceBoxReference.SetCropped(Convert.ToInt32(cropped));
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
        /// The GetCropTop method returns the current top crop value.
        /// </summary>
        /// <returns>The current top crop value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.13</remarks>
        public double GetCropTop()
        {
            this.InternalSuperSourceBoxReference.GetCropTop(out double top);
            return top;
        }

        /// <summary>
        /// The SetCropTop method sets the top crop value.
        /// </summary>
        /// <param name="top">The desired top crop value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.14</remarks>
        public void SetCropTop(double top)
        {
            try
            {
                this.InternalSuperSourceBoxReference.SetCropTop(top);
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
        /// The GetCropBottom method returns the current bottom crop value.
        /// </summary>
        /// <returns>The current bottom crop value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.15</remarks>
        public double GetCropBottom()
        {
            this.InternalSuperSourceBoxReference.GetCropBottom(out double bottom);
            return bottom;
        }

        /// <summary>
        /// The SetCropBottom method sets the bottom crop value.
        /// </summary>
        /// <param name="bottom">The desired bottom crop value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.16</remarks>
        public void SetCropBottom(double bottom)
        {
            try
            {
                this.InternalSuperSourceBoxReference.SetCropBottom(bottom);
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
        /// The GetCropLeft method returns the current left crop value.
        /// </summary>
        /// <returns>The current left crop value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.17</remarks>
        public double GetCropLeft()
        {
            this.InternalSuperSourceBoxReference.GetCropLeft(out double left);
            return left;
        }

        /// <summary>
        /// The SetCropLeft method sets the left crop value.
        /// </summary>
        /// <param name="left">The desired left crop value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.18</remarks>
        public void SetCropLeft(double left)
        {
            try
            {
                this.InternalSuperSourceBoxReference.SetCropLeft(left);
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
        /// The GetCropRight method returns the current right crop value.
        /// </summary>
        /// <returns>The current right crop value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.19</remarks>
        public double GetCropRight()
        {
            this.InternalSuperSourceBoxReference.GetCropRight(out double right);
            return right;
        }

        /// <summary>
        /// The SetCropRight method sets the right crop value.
        /// </summary>
        /// <param name="right">The desired right crop value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.20</remarks>
        public void SetCropRight(double right)
        {
            try
            {
                this.InternalSuperSourceBoxReference.SetCropRight(right);
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
        /// The ResetCrop method resets the crop to default values.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.21</remarks>
        public void ResetCrop()
        {
            try
            {
                this.InternalSuperSourceBoxReference.ResetCrop();
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
        /// The GetInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask value for this supersource box.The input availability property of an IBMDSwitcherInput can be bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this input is available for use as a source for this supersource box.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.4.22</remarks>
        public _BMDSwitcherInputAvailability GetInputAvailability()
        {
            this.InternalSuperSourceBoxReference.GetInputAvailabilityMask(out _BMDSwitcherInputAvailability mask);
            return mask;
        }
        #endregion

        #region IBMDSwitcherSuperSourceBoxCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherSuperSourceBox events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherSuperSourceBoxEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 6.2.5.1</remarks>
        void IBMDSwitcherSuperSourceBoxCallback.Notify(_BMDSwitcherSuperSourceBoxEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherSuperSourceBoxEventType.bmdSwitcherSuperSourceBoxEventTypeInputSourceChanged:
                    this.OnInputSourceChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBoxEventType.bmdSwitcherSuperSourceBoxEventTypePositionXChanged:
                    this.OnPositionXChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBoxEventType.bmdSwitcherSuperSourceBoxEventTypePositionYChanged:
                    this.OnPositionYChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBoxEventType.bmdSwitcherSuperSourceBoxEventTypeSizeChanged:
                    this.OnSizeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBoxEventType.bmdSwitcherSuperSourceBoxEventTypeCroppedChanged:
                    this.OnCroppedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBoxEventType.bmdSwitcherSuperSourceBoxEventTypeCropTopChanged:
                    this.OnCropTopChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBoxEventType.bmdSwitcherSuperSourceBoxEventTypeCropBottomChanged:
                    this.OnCropBottomChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBoxEventType.bmdSwitcherSuperSourceBoxEventTypeCropLeftChanged:
                    this.OnCropLeftChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBoxEventType.bmdSwitcherSuperSourceBoxEventTypeCropRightChanged:
                    this.OnCropRightChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
