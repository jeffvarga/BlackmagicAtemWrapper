//-----------------------------------------------------------------------------
// <copyright file="FlyKeyFrameParameters.cs">
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
    /// The FlyKeyFrameParameters class provides access to individual key frame parameters.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 5.2.16</remarks>
    public class FlyKeyFrameParameters : IBMDSwitcherKeyFlyKeyFrameParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherKeyFlyKeyFrameParameters"/>
        /// </summary>
        private readonly IBMDSwitcherKeyFlyKeyFrameParameters InternalFlyKeyFrameParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlyKeyFrameParameters" /> class.
        /// </summary>
        /// <param name="flyParameters">The native <seealso cref="IBMDSwitcherKeyFlyKeyFrameParameters"/> from the BMDSwitcherAPI.</param>
        public FlyKeyFrameParameters(IBMDSwitcherKeyFlyKeyFrameParameters flyParameters)
        {
            this.InternalFlyKeyFrameParametersReference = flyParameters;
            this.InternalFlyKeyFrameParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FlyKeyFrameParameters"/> class.
        /// </summary>
        ~FlyKeyFrameParameters()
        {
            this.InternalFlyKeyFrameParametersReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalFlyKeyFrameParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="FlyKeyFrameParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="FlyKeyFrameParameters"/> that received the event.</param>
        public delegate void FlyKeyFrameParameterssEventHandler(object sender);

        /// <summary>
        /// The <see cref="BorderBevelPosition"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnBorderBevelPositionChanged;

        /// <summary>
        /// The <see cref="BorderBevelSoftness"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnBorderBevelSoftnessChanged;

        /// <summary>
        /// The <see cref="BorderHue"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnBorderHueChanged;

        /// <summary>
        /// The <see cref="BorderSaturation"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnBorderSaturationChanged;

        /// <summary>
        /// The <see cref="BorderLuma"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnBorderLumaChanged;

        /// <summary>
        /// The <see cref="BorderLightSourceDirection"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnBorderLightSourceDirectionChanged;

        /// <summary>
        /// The <see cref="BorderLightSourceAltitude"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnBorderLightSourceAltitudeChanged;

        /// <summary>
        /// The <see cref="OnBorderInnerSoftnessChanged"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnBorderInnerSoftnessChanged;

        /// <summary>
        /// The <see cref="OnBorderOuterSoftnessChanged"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnBorderOuterSoftnessChanged;

        /// <summary>
        /// The <see cref="BorderInnerWidth"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnBorderInnerWidthChanged;

        /// <summary>
        /// The <see cref="BorderOuterWidth"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnBorderOuterWidthChanged;

        /// <summary>
        /// The <see cref="PositionX"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnPositionXChanged;

        /// <summary>
        /// The <see cref="PositionY"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnPositionYChanged;

        /// <summary>
        /// The <see cref="Rotation"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnRotationChanged;

        /// <summary>
        /// The <see cref="SizeX"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnSizeXChanged;

        /// <summary>
        /// The <see cref="SizeY"/> value changed.
        /// </summary>
        public event FlyKeyFrameParameterssEventHandler OnSizeYChanged;
        #endregion

        #region Properties
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
        /// Gets a value indicating whether the Key Frame Key can scale up
        /// </summary>
        public bool CanScaleUp
        {
            get { return this.GetCanScaleUp(); }
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

        /// <summary>
        /// Gets a value indicating whether the Key Frame Key can rotate.
        /// </summary>
        public bool CanRotate
        {
            get { return this.GetCanRotate(); }
        }

        /// <summary>
        /// Gets or sets the current border inner width value.
        /// </summary>
        public double BorderInnerWidth
        {
            get { return this.GetBorderWidthIn(); }
            set { this.SetBorderWidthIn(value); }
        }

        /// <summary>
        /// Gets or sets the current border outer width value.
        /// </summary>
        public double BorderOuterWidth
        {
            get { return this.GetBorderWidthOut(); }
            set { this.SetBorderWidthOut(value); }
        }

        /// <summary>
        /// Gets or sets the current border inner softness value.
        /// </summary>
        public double BorderInnerSoftness
        {
            get { return this.GetBorderSoftnessIn(); }
            set { this.SetBorderSoftnessIn(value); }
        }

        /// <summary>
        /// Gets or sets the current border outer softness value.
        /// </summary>
        public double BorderOuterSoftness
        {
            get { return this.GetBorderSoftnessOut(); }
            set { this.SetBorderSoftnessOut(value); }
        }

        /// <summary>
        /// Gets or sets the current border bevel softness value.
        /// </summary>
        public double BorderBevelSoftness
        {
            get { return this.GetBorderBevelSoftness(); }
            set { this.SetBorderBevelSoftness(value); }
        }

        /// <summary>
        /// Gets or sets the current border bevel position value.
        /// </summary>
        public double BorderBevelPosition
        {
            get { return this.GetBorderBevelPosition(); }
            set { this.SetBorderBevelPosition(value); }
        }

        /// <summary>
        /// Gets or sets the current border opacity value.
        /// </summary>
        public double BorderOpacity
        {
            get { return this.GetBorderOpacity(); }
            set { this.SetBorderOpacity(value); }
        }

        /// <summary>
        /// Gets or sets the current border hue value.
        /// </summary>
        public double BorderHue
        {
            get { return this.GetBorderHue(); }
            set { this.SetBorderHue(value); }
        }

        /// <summary>
        /// Get the current border saturation value.
        /// </summary>
        public double BorderSaturation
        {
            get { return this.GetBorderSaturation(); }
            set { this.SetBorderSaturation(value); }
        }

        /// <summary>
        /// Get the current border luminance value.
        /// </summary>
        public double BorderLuma
        {
            get { return this.GetBorderLuma(); }
            set { this.SetBorderLuma(value); }
        }

        /// <summary>
        /// Gets or sets the current border light source altitude value.
        /// </summary>
        public double BorderLightSourceAltitude
        {
            get { return this.GetBorderLightSourceAltitude(); }
            set { this.SetBorderLightSourceAltitude(value); }
        }

        /// <summary>
        /// Gets or sets the current border light source direction value.
        /// </summary>
        public double BorderLightSourceDirection
        {
            get { return this.GetBorderLightSourceDirection(); }
            set { this.SetBorderLightSourceDirection(value); }
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

        #region IBMDSwitcherKeyFlyKeyFrameParameters
        /// <summary>
        /// The GetSizeX method gets the size x value.
        /// </summary>
        /// <returns>The current size x value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.1</remarks>
        public double GetSizeX()
        {
            this.InternalFlyKeyFrameParametersReference.GetSizeX(out double multiplierX);
            return multiplierX;
        }

        /// <summary>
        /// <para>The SetSizeX method sets the size x value.</para>
        /// <para>Note: On some switchers the maximum size x value is 1.0. The <see cref="GetCanScaleUp"/> method can be used to determine whether the switcher supports Fly Key size x values greater than 1.0.</para>
        /// </summary>
        /// <param name="multiplierX">The desired size x value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.2</remarks>
        public void SetSizeX(double multiplierX)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetSizeX(multiplierX);
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
        /// The GetSizeY method gets the size y value.
        /// </summary>
        /// <returns>The current size y value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.3</remarks>
        public double GetSizeY()
        {
            this.InternalFlyKeyFrameParametersReference.GetSizeY(out double multiplierY);
            return multiplierY;
        }

        /// <summary>
        /// <para>The SetSizeY method sets the size y value</para>
        /// <para>Note: On some switchers the maximum size y value is 1.0. The <see cref="GetCanScaleUp"/> method can be used to determine whether the switcher supports Fly Key size y values greater than 1.0.</para>
        /// </summary>
        /// <param name="multiplierY">The desired size y value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.4</remarks>
        public void SetSizeY(double multiplierY)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetSizeY(multiplierY);
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
        /// The GetCanScaleUp method is used to check whether the switcher supports Fly Key Key Frame size x and size y values greater than 1.0.
        /// </summary>
        /// <returns>A Boolean value indicating whether the switcher supports Fly Key Key Frame size x and size y values greater than 1.0.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.5</remarks>
        public bool GetCanScaleUp()
        {
            this.InternalFlyKeyFrameParametersReference.GetCanScaleUp(out int canScaleUp);
            return Convert.ToBoolean(canScaleUp);
        }

        /// <summary>
        /// The GetPositionX method gets the position x value.
        /// </summary>
        /// <returns>The current offset x value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.6</remarks>
        public double GetPositionX()
        {
            this.InternalFlyKeyFrameParametersReference.GetPositionX(out double offsetX);
            return offsetX;
        }

        /// <summary>
        /// The SetPositionX method sets the position x value.
        /// </summary>
        /// <param name="offsetX">The desired position x value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.7</remarks>
        public void SetPositionX(double offsetX)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetPositionX(offsetX);
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
        /// The GetPositionY method gets the position y value.
        /// </summary>
        /// <returns>The current offset y value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.8</remarks>
        public double GetPositionY()
        {
            this.InternalFlyKeyFrameParametersReference.GetPositionY(out double offsetY);
            return offsetY;
        }

        /// <summary>
        /// The SetPositionY method sets the position y value.
        /// </summary>
        /// <param name="offsetY">The desired position y value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.9</remarks>
        public void SetPositionY(double offsetY)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetPositionY(offsetY);
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
        /// The GetRotation method gets the rotation value.
        /// </summary>
        /// <returns>The current rotation value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.10</remarks>
        public double GetRotation()
        {
            this.InternalFlyKeyFrameParametersReference.GetRotation(out double degrees);
            return degrees;
        }

        /// <summary>
        /// The SetRotation method sets the rotation value.
        /// </summary>
        /// <param name="degrees">The desired rotation value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.11</remarks>
        public void SetRotation(double degrees)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetRotation(degrees);
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
        /// The GetCanRotate method determines whether the Fly Key Key Frame supports rotation via the SetRotation method.
        /// </summary>
        /// <returns>The rotation support of the current Fly Key Key frame.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.12</remarks>
        public bool GetCanRotate()
        {
            this.InternalFlyKeyFrameParametersReference.GetCanRotate(out int canRotate);
            return Convert.ToBoolean(canRotate);
        }

        /// <summary>
        /// The GetBorderWidthOut method gets the current border outer width value.
        /// </summary>
        /// <returns>The current border outer width value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.13</remarks>
        public double GetBorderWidthOut()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderWidthOut(out double widthOut);
            return widthOut;
        }

        /// <summary>
        /// The SetBorderWidthOut method sets the border outer width value.
        /// </summary>
        /// <param name="widthOut">The desired border outer width value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.14</remarks>
        public void SetBorderWidthOut(double widthOut)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderWidthOut(widthOut);
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
        /// The GetBorderWidthIn method gets the current border inner width value.
        /// </summary>
        /// <returns>The current border inner width value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.15</remarks>
        public double GetBorderWidthIn()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderWidthIn(out double widthIn);
            return widthIn;
        }

        /// <summary>
        /// The SetBorderWidthIn method sets the border inner width value.
        /// </summary>
        /// <param name="widthIn">The desired border inner width value.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.16</remarks>
        /// <exception cref="FailedException">Failure.</exception>
        public void SetBorderWidthIn(double widthIn)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderWidthIn(widthIn);
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
        /// The GetBorderSoftnessOut method gets the current border outer softness value.
        /// </summary>
        /// <returns>The current border outer softness value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.17</remarks>
        public double GetBorderSoftnessOut()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderSoftnessOut(out double softOut);
            return softOut;
        }

        /// <summary>
        /// The SetBorderSoftnessOut method sets the border outer softness value.
        /// </summary>
        /// <param name="softOut">The desired border outer softness value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.18</remarks>
        public void SetBorderSoftnessOut(double softOut)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderSoftnessOut(softOut);
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
        /// The GetBorderSoftnessIn method gets the current border inner softness value.
        /// </summary>
        /// <returns>The current border inner softness value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.19</remarks>
        public double GetBorderSoftnessIn()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderSoftnessIn(out double widthSoftnessIn);
            return widthSoftnessIn;
        }

        /// <summary>
        /// The SetBorderSoftnessIn method sets the border inner softness value.
        /// </summary>
        /// <param name="softnessIn">The desired border inner softness value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.20</remarks>
        public void SetBorderSoftnessIn(double softnessIn)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderSoftnessIn(softnessIn);
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
        /// The GetBorderBevelSoftness method gets the current border bevel softness value.
        /// </summary>
        /// <returns>The current border bevel softness value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.21</remarks>
        public double GetBorderBevelSoftness()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderBevelSoftness(out double bevelSoft);
            return bevelSoft;
        }

        /// <summary>
        /// The SetBorderBevelSoftness method sets the border bevel softness value.
        /// </summary>
        /// <param name="bevelSoft">The desired border bevel softness value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.22</remarks>
        public void SetBorderBevelSoftness(double bevelSoft)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderBevelSoftness(bevelSoft);
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
        /// The GetBorderBevelPosition method gets the current border bevel position value.
        /// </summary>
        /// <returns>The current border bevel position value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.23</remarks>
        public double GetBorderBevelPosition()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderBevelPosition(out double bevelPosition);
            return bevelPosition;
        }

        /// <summary>
        /// The SetBorderBevelPosition method sets the border bevel position value.
        /// </summary>
        /// <param name="bevelPosition">The desired border bevel position value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.24</remarks>
        public void SetBorderBevelPosition(double bevelPosition)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderBevelPosition(bevelPosition);
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
        /// The GetBorderOpacity method gets the current border opacity value.
        /// </summary>
        /// <returns>The current border opacity value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.25</remarks>
        public double GetBorderOpacity()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderOpacity(out double opacity);
            return opacity;
        }

        /// <summary>
        /// The SetBorderOpacity method sets the border opacity value.
        /// </summary>
        /// <param name="opacity">The desired border opacity value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.26</remarks>
        public void SetBorderOpacity(double opacity)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderOpacity(opacity);
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
        /// The GetBorderHue method gets the current border hue value.
        /// </summary>
        /// <returns>The current border hue value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.27</remarks>
        public double GetBorderHue()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderHue(out double hue);
            return hue;
        }

        /// <summary>
        /// The SetBorderHue method sets the border hue value.
        /// </summary>
        /// <param name="hue">The desired border hue value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.28</remarks>
        public void SetBorderHue(double hue)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderHue(hue);
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
        /// The GetBorderSaturation method gets the current border saturation value.
        /// </summary>
        /// <returns>The current border saturation value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.29</remarks>
        public double GetBorderSaturation()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderSaturation(out double saturation);
            return saturation;
        }

        /// <summary>
        /// The SetBorderSaturation method sets the border saturation value.
        /// </summary>
        /// <param name="saturation">The desired border saturation value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.30</remarks>
        public void SetBorderSaturation(double saturation)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderSaturation(saturation);
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
        /// The GetBorderLuma method gets the current border luminance value.
        /// </summary>
        /// <returns>The current border luminance value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.31</remarks>
        public double GetBorderLuma()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderLuma(out double luma);
            return luma;
        }

        /// <summary>
        /// The SetBorderLuma method sets the border luminance value.
        /// </summary>
        /// <param name="luma">The desired border luminance value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.32</remarks>
        public void SetBorderLuma(double luma)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderLuma(luma);
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
        /// The GetBorderLightSourceDirection method gets the current light source direction value.
        /// </summary>
        /// <returns>The current light source direction in degrees.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.33</remarks>
        public double GetBorderLightSourceDirection()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderLightSourceDirection(out double degrees);
            return degrees;
        }

        /// <summary>
        /// The SetBorderLightSourceDirection method sets the light source direction value.
        /// </summary>
        /// <param name="degrees">The desired light source direction value in degrees.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.34</remarks>
        /// <exception cref="FailedException">Failure.</exception>
        public void SetBorderLightSourceDirection(double degrees)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderLightSourceDirection(degrees);
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
        /// The GetBorderLightSourceAltitude method gets the current light source altitude value.
        /// </summary>
        /// <returns>The current light source altitude value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.35</remarks>
        public double GetBorderLightSourceAltitude()
        {
            this.InternalFlyKeyFrameParametersReference.GetBorderLightSourceAltitude(out double altitude);
            return altitude;
        }

        /// <summary>
        /// The SetBorderLightSourceAltitude method sets the light source altitude value.
        /// </summary>
        /// <param name="altitude">The desired light source altitude value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.36</remarks>
        /// <bug>Docs spelling error SetBorderLighSourceAltitude</bug>
        public void SetBorderLightSourceAltitude(double altitude)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetBorderLightSourceAltitude(altitude);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.37</remarks>
        public double GetMaskTop()
        {
            this.InternalFlyKeyFrameParametersReference.GetMaskTop(out double maskTop);
            return maskTop;
        }

        /// <summary>
        /// The SetMaskTop method sets the mask top value.
        /// </summary>
        /// <param name="maskTop">The desired mask top value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.38</remarks>
        public void SetMaskTop(double maskTop)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetMaskTop(maskTop);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.39</remarks>
        public double GetMaskBottom()
        {
            this.InternalFlyKeyFrameParametersReference.GetMaskBottom(out double maskBottom);
            return maskBottom;
        }

        /// <summary>
        /// The SetMaskBottom method sets the mask bottom value.
        /// </summary>
        /// <param name="maskBottom">The desired mask bottom value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.40</remarks>
        public void SetMaskBottom(double maskBottom)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetMaskBottom(maskBottom);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.41</remarks>
        public double GetMaskLeft()
        {
            this.InternalFlyKeyFrameParametersReference.GetMaskLeft(out double maskLeft);
            return maskLeft;
        }

        /// <summary>
        /// The SetMaskLeft method sets the mask left value.
        /// </summary>
        /// <param name="maskLeft">The desired mask left value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.42</remarks>
        public void SetMaskLeft(double maskLeft)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetMaskLeft(maskLeft);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.43</remarks>
        public double GetMaskRight()
        {
            this.InternalFlyKeyFrameParametersReference.GetMaskRight(out double maskRight);
            return maskRight;
        }

        /// <summary>
        /// The SetMaskRight method sets the mask right value.
        /// </summary>
        /// <param name="maskRight">The desired mask right value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.16.44</remarks>
        public void SetMaskRight(double maskRight)
        {
            try
            {
                this.InternalFlyKeyFrameParametersReference.SetMaskRight(maskRight);
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

        #region IBMDSwitcherKeyFlyKeyFrameParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherKeyFlyKeyFrameParameters events occur, events such as a property change.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.Callbacks should b processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherKeyFlyKeyFrameParametersEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.17.1</remarks>
        /// <bug>BMDSwitcherKeyFlyKeyFrameParametersEventType is missing from docs.</bug>
        /// <bug>No events for mask top/bottom/left/right</bug>
        void IBMDSwitcherKeyFlyKeyFrameParametersCallback.Notify(_BMDSwitcherKeyFlyKeyFrameParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderBevelPositionChanged:
                    this.OnBorderBevelPositionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderBevelSoftnessChanged:
                    this.OnBorderBevelSoftnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderHueChanged:
                    this.OnBorderHueChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderLightSourceAltitudeChanged:
                    this.OnBorderLightSourceAltitudeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderLightSourceDirectionChanged:
                    this.OnBorderLightSourceDirectionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderLumaChanged:
                    this.OnBorderLumaChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderSaturationChanged:
                    this.OnBorderSaturationChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderSoftnessInChanged:
                    this.OnBorderInnerSoftnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderSoftnessOutChanged:
                    this.OnBorderOuterSoftnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderWidthInChanged:
                    this.OnBorderInnerWidthChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeBorderWidthOutChanged:
                    this.OnBorderOuterWidthChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypePositionXChanged:
                    this.OnPositionXChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypePositionYChanged:
                    this.OnPositionYChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeRotationChanged:
                    this.OnRotationChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeSizeXChanged:
                    this.OnSizeXChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyFlyKeyFrameParametersEventType.bmdSwitcherKeyFlyKeyFrameParametersEventTypeSizeYChanged:
                    this.OnSizeYChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
