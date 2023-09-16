//-----------------------------------------------------------------------------
// <copyright file="FlyParameters.cs">
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
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// <para>The FlyParameters class is used for manipulating fly settings of a key.</para>
    /// <para>A luminance, chroma or pattern key can be made a "fly" key, filtering its current state through the DVE hardware.Turning off the fly setting will remove the filter and return the key to its original state. Note that most properties in this interface also take effect when the key type is set to DVE.</para>
    /// </summary>
    public class FlyParameters : IBMDSwitcherKeyFlyParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherKeyFlyParameters"/>
        /// </summary>
        private readonly IBMDSwitcherKeyFlyParameters InternalFlyParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlyParameters" /> class.
        /// </summary>
        /// <param name="flyParameters">The native <seealso cref="IBMDSwitcherKeyFlyParameters"/> from the BMDSwitcherAPI.</param>
        public FlyParameters(IBMDSwitcherKeyFlyParameters flyParameters)
        {
            this.InternalFlyParametersReference = flyParameters;
            this.InternalFlyParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FlyParameters"/> class.
        /// </summary>
        ~FlyParameters()
        {
            this.InternalFlyParametersReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalFlyParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="FlyParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="FlyParameters"/> that received the event.</param>
        public delegate void FlyParametersEventHandler(object sender);

        /// <summary>
        /// A delegate to handle events from <see cref="FlyParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="FlyParameters"/> that received the event.</param>
        /// <param name="keyFrame">The changed key frame.</param>
        public delegate void FlyParametersKeyFrameStoredEventHandler(object sender, _BMDSwitcherFlyKeyFrame keyFrame);

        /// <summary>
        /// The <see cref="FlyEnabled"/> flag changed.
        /// </summary>
        public event FlyParametersEventHandler OnFlyChanged;

        /// <summary>
        /// the <see cref="GetCanFly"/> flag changed.
        /// </summary>
        public event FlyParametersEventHandler OnCanFlyChanged;

        /// <summary>
        /// The <see cref="Rate"/> value changed.
        /// </summary>
        public event FlyParametersEventHandler OnRateChanged;

        /// <summary>
        /// The <see cref="SizeX"/> value changed.
        /// </summary>
        public event FlyParametersEventHandler OnSizeXChanged;

        /// <summary>
        /// The <see cref="SizeY"/> value changed.
        /// </summary>
        public event FlyParametersEventHandler OnSizeYChanged;

        /// <summary>
        /// The <see cref="PositionX"/> value changed.
        /// </summary>
        public event FlyParametersEventHandler OnPositionXChanged;

        /// <summary>
        /// The <see cref="PositionY"/> value changed.
        /// </summary>
        public event FlyParametersEventHandler OnPositionYChanged;

        /// <summary>
        /// The <see cref="Rotation"/> value changed.
        /// </summary>
        public event FlyParametersEventHandler OnRotationChanged;

        /// <summary>
        /// The <see cref="IsKeyFrameStored"/> flag changed.
        /// </summary>
        public event FlyParametersKeyFrameStoredEventHandler OnIsKeyFrameStoredChanged;

        /// <summary>
        /// The <see cref="IsAtKeyFrames"/> status changed.
        /// </summary>
        public event FlyParametersEventHandler OnIsAtKeyFramesChanged;

        /// <summary>
        /// The <see cref="IsRunning"/> status changed.
        /// </summary>
        public event FlyParametersEventHandler OnIsRunningChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether fly is enabled or not.
        /// </summary>
        public bool FlyEnabled
        {
            get { return this.GetFly(); }
            set { this.SetFly(value); }
        }

        /// <summary>
        /// Gets a value indicating whether this key can enable fly or not.
        /// </summary>
        public bool CanFly
        {
            get { return this.GetCanFly(); }
        }

        /// <summary>
        /// Gets or sets the current fly rate.
        /// </summary>
        public uint Rate
        {
            get { return this.GetRate(); }
            set { this.SetRate(value); }
        }

        /// <summary>
        /// Gets or sets the current size X value.
        /// </summary>
        public double SizeX
        {
            get { return this.GetSizeX(); }
            set { this.SetSizeX(value); }
        }

        /// <summary>
        /// Gets or sets the current size Y value.
        /// </summary>
        public double SizeY
        {
            get { return this.GetSizeY(); }
            set { this.SetSizeY(value); }
        }

        /// <summary>
        /// Gets or sets the current position X value.
        /// </summary>
        public double PositionX
        {
            get { return this.GetPositionX(); }
            set { this.SetPositionX(value); }
        }

        /// <summary>
        /// Gets or sets the current position Y value.
        /// </summary>
        public double PositionY
        {
            get { return this.GetPositionY(); }
            set { this.SetPositionY(value); }
        }

        /// <summary>
        /// Gets or sets the current rotation value.
        /// </summary>
        public double Rotation
        {
            get { return this.GetRotation(); }
            set { this.SetRotation(value); }
        }
        #endregion

        #region IBMDSwitcherKeyFlyParameters
        /// <summary>
        /// The GetFly method returns whether fly is enabled or not.
        /// </summary>
        /// <returns>Boolean status of whether fly is enabled.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.1</remarks>
        public bool GetFly()
        {
            this.InternalFlyParametersReference.GetFly(out int isFlyKey);
            return Convert.ToBoolean(isFlyKey);
        }

        /// <summary>
        /// Use the SetFly method to enable or disable fly.
        /// </summary>
        /// <param name="isFlyKey">The desired fly enable flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.2</remarks>
        public void SetFly(bool isFlyKey)
        {
            try
            {
                this.InternalFlyParametersReference.SetFly(Convert.ToInt32(isFlyKey));
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
        /// The GetCanFly method returns whether this key can enable fly or not. The DVE hardware is a shared resource; if another component is currently using the resource, it may not be available for this key.
        /// </summary>
        /// <returns>Boolean status of the can-fly flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.3</remarks>
        public bool GetCanFly()
        {
            this.InternalFlyParametersReference.GetCanFly(out int canFly);
            return Convert.ToBoolean(canFly);
        }

        /// <summary>
        /// The GetRate method gets the current fly rate value.
        /// </summary>
        /// <returns>The current rate value in frames.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.4</remarks>
        public uint GetRate()
        {
            this.InternalFlyParametersReference.GetRate(out uint frames);
            return frames;
        }

        /// <summary>
        /// The SetRate method sets the fly rate value.
        /// </summary>
        /// <param name="frames">The desired rate value in frames.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.5</remarks>
        public void SetRate(uint frames)
        { 
            try
            {
                this.InternalFlyParametersReference.SetRate(frames);
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
        /// The GetSizeX method gets the current size x value. The flying size is a multiple of the original key size.
        /// </summary>
        /// <returns>The current size x value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.6</remarks>
        public double GetSizeX()
        {
            this.InternalFlyParametersReference.GetSizeX(out double multiplierX);
            return multiplierX;
        }

        /// <summary>
        /// <para>The SetSizeX method sets the size x value. The flying size is a multiple of the original key size.</para>
        /// <para>Note: On some switchers the maximum size x value is 1.0. The <see cref="GetCanScaleUp"/> method can be used to determine whether the switcher supports Fly Key size x values greater than 1.0.</para>
        /// </summary>
        /// <param name="multiplierX">The desired size x value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.7</remarks>
        public void SetSizeX(double multiplierX)
        {
            try
            {
                this.InternalFlyParametersReference.SetSizeX(multiplierX);
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
        /// The GetSizeY method gets the current size y value. The flying size is a multiple of the original key size.
        /// </summary>
        /// <returns>The current size y value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.8</remarks>
        public double GetSizeY()
        {
            this.InternalFlyParametersReference.GetSizeY(out double multiplierY);
            return multiplierY;
        }

        /// <summary>
        /// <para>The SetSizeY method sets the size y value. The flying size is a multiple of the original key size.</para>
        /// <para>Note: On some switchers the maximum size y value is 1.0. The <see cref="GetCanScaleUp"/> method can be used to determine whether the switcher supports Fly Key size y values greater than 1.0.</para>
        /// </summary>
        /// <param name="multiplierY">The desired size y value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.9</remarks>
        public void SetSizeY(double multiplierY)
        {
            try
            {
                this.InternalFlyParametersReference.SetSizeY(multiplierY);
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
        /// The GetCanScaleUp method is used to check whether the switcher supports Fly Key size x and size y values greater than 1.0.
        /// </summary>
        /// <returns>A Boolean value indicating whether the switcher supports Fly Key size x and size y values greater than 1.0.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.10</remarks>
        public bool GetCanScaleUp()
        {
            this.InternalFlyParametersReference.GetCanScaleUp(out int canScaleUp);
            return Convert.ToBoolean(canScaleUp);
        }

        /// <summary>
        /// The GetPositionX method gets the current position x value. This is an offset from the original key position.
        /// </summary>
        /// <returns>The current offset x value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.11</remarks>
        public double GetPositionX()
        {
            this.InternalFlyParametersReference.GetPositionX(out double offsetX);
            return offsetX;
        }

        /// <summary>
        /// The SetPositionX method sets the position x value. This is an offset from the original key position.
        /// </summary>
        /// <param name="offsetX">The desired position x value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.12</remarks>
        public void SetPositionX(double offsetX)
        { 
            try
            {
                this.InternalFlyParametersReference.SetPositionX(offsetX);
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
        /// The GetPositionY method gets the current position y value. This is an offset from the original key position.
        /// </summary>
        /// <returns>The current offset y value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.13</remarks>
        public double GetPositionY()
        {
            this.InternalFlyParametersReference.GetPositionY(out double offsetY);
            return offsetY;
        }

        /// <summary>
        /// The SetPositionY method sets the position y value. This is an offset from the original key position.
        /// </summary>
        /// <param name="offsetY">The desired position y value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.14</remarks>
        public void SetPositionY(double offsetY)
        {
            try
            {
                this.InternalFlyParametersReference.SetPositionY(offsetY);
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
        /// The GetRotation method gets the current rotation value.
        /// </summary>
        /// <returns>The current rotation value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.15</remarks>
        public double GetRotation()
        {
            this.InternalFlyParametersReference.GetRotation(out double degrees);
            return degrees;
        }

        /// <summary>
        /// The SetRotation method sets the rotation value.
        /// </summary>
        /// <param name="degrees">The desired rotation value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.16</remarks>
        public void SetRotation(double degrees)
        { 
            try
            {
                this.InternalFlyParametersReference.SetRotation(degrees);
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
        /// The GetCanRotate method determines whether the current Fly Key supports rotation via the SetRotation method.
        /// </summary>
        /// <returns>The rotation support of the current Fly Key.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.17</remarks>
        public bool GetCanRotate()
        {
            this.InternalFlyParametersReference.GetCanRotate(out int canRotate);
            return Convert.ToBoolean(canRotate);
        }

        /// <summary>
        /// The ResetRotation method resets the rotation value to its default.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.18</remarks>
        public void ResetRotation()
        {
            try
            {
                this.InternalFlyParametersReference.ResetRotation();
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
        /// The ResetDVE method resets the DVE parameters to their default values, i.e. size, position and rotation.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.19</remarks>
        public void ResetDVE()
        {
            try
            {
                this.InternalFlyParametersReference.ResetDVE();
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
        /// The IsKeyFrameStored method returns whether the specified key frame has been stored or not. It is intended for use with user-defined key frames to determine if they have been stored.
        /// </summary>
        /// <param name="keyFrame">Specify a single key frame of BMDSwitcherFlyKeyFrame to query the status on.</param>
        /// <returns>The current status flag of whether the specified key frame has been stored.</returns>
        /// <exception cref="ArgumentException">The <paramref name="keyFrame"/> parameter is invalid.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.21</remarks>
        public bool IsKeyFrameStored(_BMDSwitcherFlyKeyFrame keyFrame)
        {
            this.InternalFlyParametersReference.IsKeyFrameStored(keyFrame, out int stored);
            return Convert.ToBoolean(stored);
        }

        /// <summary>
        /// The StoreAsKeyFrame method stores the current frame into the specified key frame(s). Multiple user-defined key frames can be specified.
        /// </summary>
        /// <param name="keyFrames">Specify where to store the current frame, must be user-defined key frame(s).</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <exception cref="ArgumentException">The <paramref name="keyFrames"/> parameter is invalid.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.22</remarks>
        public void StoreAsKeyFrame(_BMDSwitcherFlyKeyFrame keyFrames)
        { 
            try
            {
                this.InternalFlyParametersReference.StoreAsKeyFrame(keyFrames);
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
        /// The RunToKeyFrame method commences a run from current frame to the specified key frame.
        /// </summary>
        /// <param name="destination">The destination key frame.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <exception cref="ArgumentException">The <paramref name="destination"/> parameter is invalid.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.23</remarks>
        public void RunToKeyFrame(_BMDSwitcherFlyKeyFrame destination)
        {
            try
            {
                this.InternalFlyParametersReference.RunToKeyFrame(destination);
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
        /// <param>The IsAtKeyFrames method returns a bit set of key frames that match the current frame.</param>
        /// <param>Zero is returned if the current frame does not match any built-in or user-defined frames.</param>
        /// </summary>
        /// <returns>All key frames that match the current frame.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.24</remarks>
        public _BMDSwitcherFlyKeyFrame IsAtKeyFrames()
        {
            this.InternalFlyParametersReference.IsAtKeyFrames(out _BMDSwitcherFlyKeyFrame keyFrames);
            return keyFrames;
        }

        /// <summary>
        /// The GetKeyFrameParameters method returns an object interface for accessing individual parameters in a key frame.
        /// </summary>
        /// <param name="keyFrame">The desired key frame.</param>
        /// <returns>IBMDSwitcherKeyFlyKeyFrameParameters object interface.</returns>
        /// <exception cref="ArgumentException">The <paramref name="keyFrame"/> parameter is invalid.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.25</remarks>
        public IBMDSwitcherKeyFlyKeyFrameParameters GetKeyFrameParameters(_BMDSwitcherFlyKeyFrame keyFrame)
        {
            this.InternalFlyParametersReference.GetKeyFrameParameters(keyFrame, out IBMDSwitcherKeyFlyKeyFrameParameters keyFrameParameters);
            return keyFrameParameters;
        }

        /// <summary>
        /// The IsRunning method returns the current run status.
        /// </summary>
        /// <param name="destination">If the key is running, this is the destination of the run.</param>
        /// <returns>Boolean status of whether the key is running.</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.14.26</remarks>
        public bool IsRunning(out _BMDSwitcherFlyKeyFrame destination)
        {
            try
            {
                this.InternalFlyParametersReference.IsRunning(out int isRunning, out destination);
                return Convert.ToBoolean(isRunning);
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

        #region IBMDSwitcherKeyFlyParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherKeyFlyParameters events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherKeyFlyParametersEventType that describes the type of event that has occurred.</param>
        /// <param name="keyFrame">This parameter is only valid when eventType is bmdSwitcherKeyFlyParametersEventTypeIsKeyFrameStoredChanged, it specifies the changed key frame.</param>
        void IBMDSwitcherKeyFlyParametersCallback.Notify(_BMDSwitcherKeyFlyParametersEventType eventType, _BMDSwitcherFlyKeyFrame keyFrame)
        {
            switch (eventType)
            {
                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeFlyChanged:
                    this.OnFlyChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeCanFlyChanged:
                    this.OnCanFlyChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeRateChanged:
                    this.OnRateChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeSizeXChanged:
                    this.OnSizeXChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeSizeYChanged:
                    this.OnSizeYChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypePositionXChanged:
                    this.OnPositionXChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypePositionYChanged:
                    this.OnPositionYChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeRotationChanged:
                    this.OnRotationChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeIsKeyFrameStoredChanged:
                    this.OnIsKeyFrameStoredChanged?.Invoke(this, keyFrame);
                    break;

                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeIsAtKeyFramesChanged:
                    this.OnIsAtKeyFramesChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyParametersEventType.bmdSwitcherKeyFlyParametersEventTypeIsRunningChanged:
                    this.OnIsRunningChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}