//-----------------------------------------------------------------------------
// <copyright file="KeyDVEParameters.cs">
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
    /// The DVEParameters object is used for manipulating settings specific to the DVE-type key. Note that properties that affect a fly key also affects a DVE key; they are access through the IBMDSwitcherKeyFlyParameters object interface. Also note that the mask properties in this interface only affect keys with their type set to DVE.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 5.2.12</remarks>
    public class DVEParameters : IBMDSwitcherKeyDVEParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherKeyDVEParameters"/>.
        /// </summary>
        private readonly IBMDSwitcherKeyDVEParameters InternalKeyDVEParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="DVEParameters"/> class.
        /// </summary>
        /// <param name="switcherKeyDVEParameters">The native <seealso cref="IBMDSwitcherKeyDVEParameters"/> from the BMDSwitcherAPI.</param>
        public DVEParameters(IBMDSwitcherKeyDVEParameters switcherKeyDVEParameters)
        {
            this.InternalKeyDVEParametersReference = switcherKeyDVEParameters;
            this.InternalKeyDVEParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DVEParameters"/> class.
        /// </summary>
        ~DVEParameters()
        {
            this.InternalKeyDVEParametersReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalKeyDVEParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="DVEParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="DVEParameters"/> that received the event.</param>
        public delegate void KeyDVEParametersEventHandler(object sender);

        /// <summary>
        /// The <see cref="HasShadow"/> flag changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnShadowChanged;

        /// <summary>
        /// The <see cref="LightSourceDirection"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnLightSourceDirectionChanged;

        /// <summary>
        /// The <see cref="LightSourceAltitude"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnLightSourceAltitudeChanged;

        /// <summary>
        /// The <see cref="IsBorderEnabled"/> flag changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderEnabledChanged;

        /// <summary>
        /// The <see cref="BorderBevel"/> option changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderBevelChanged;

        /// <summary>
        /// The <see cref="BorderInnerWidth"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderInnerWidthChanged;

        /// <summary>
        /// The <see cref="BorderOuterWidth"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderOuterWidthChanged;

        /// <summary>
        /// The <see cref="OnBorderInnerSoftnessChanged"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderInnerSoftnessChanged;

        /// <summary>
        /// The <see cref="OnBorderOuterSoftnessChanged"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderOuterSoftnessChanged;

        /// <summary>
        /// The <see cref="BorderBevelSoftness"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderBevelSoftnessChanged;

        /// <summary>
        /// The <see cref="BorderBevelPosition"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderBevelPositionChanged;

        /// <summary>
        /// The <see cref="BorderOpacity"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderOpacityChanged;

        /// <summary>
        /// The <see cref="BorderHue"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderHueChanged;

        /// <summary>
        /// The <see cref="BorderSaturation"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderSaturationChanged;

        /// <summary>
        /// The <see cref="BorderLuma"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnBorderLumaChanged;

        /// <summary>
        /// The <see cref="IsMasked"/> flag changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnMaskedChanged;

        /// <summary>
        /// The <see cref="MaskTop"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnMaskTopChanged;

        /// <summary>
        /// The <see cref="MaskBottom"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnMaskBottomChanged;

        /// <summary>
        /// The <see cref="MaskLeft"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnMaskLeftChanged;

        /// <summary>
        /// The <see cref="MaskRight"/> value changed.
        /// </summary>
        public event KeyDVEParametersEventHandler OnMaskRightChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating if the DVE has shadow enabled.
        /// </summary>
        public bool HasShadow
        {
            get { return this.GetShadow(); }
        }

        /// <summary>
        /// Gets or sets the current light source direction value.
        /// </summary>
        public double LightSourceDirection
        {
            get { return this.GetLightSourceDirection(); }
            set { this.SetLightSourceDirection(value); }
        }

        /// <summary>
        /// Gets or sets the current light source altitude value.
        /// </summary>
        public double LightSourceAltitude
        {
            get { return this.GetLightSourceAltitude(); }
            set { this.SetLightSourceAltitude(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating if the border is enabled.
        /// </summary>
        public bool IsBorderEnabled
        {
            get { return this.GetBorderEnabled(); }
            set { this.SetBorderEnabled(value); }
        }

        /// <summary>
        /// Gets or sets the current border bevel option.
        /// </summary>
        public _BMDSwitcherBorderBevelOption BorderBevel
        {
            get { return this.GetBorderBevel(); }
            set { this.SetBorderBevel(value); }
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
        /// Gets or set a value indicating whether the current DVE is masked.
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

        #region IBMDSwitcherKeyDVEParameters
        /// <summary>
        /// The GetShadow method gets the current shadow flag.
        /// </summary>
        /// <returns>The current shadow flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.1</remarks>
        public bool GetShadow()
        {
            this.InternalKeyDVEParametersReference.GetShadow(out int shadow);
            return Convert.ToBoolean(shadow);
        }

        /// <summary>
        /// The SetShadow method sets the shadow flag.
        /// </summary>
        /// <param name="shadow">The desired shadow flag.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.2</remarks>
        /// <exception cref="FailedException">Failure.</exception>
        public void SetShadow(bool shadow)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetShadow(Convert.ToInt32(shadow));
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
        /// The GetLightSourceDirection method gets the current light source direction value.
        /// </summary>
        /// <returns>The current light source direction in degrees.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.3</remarks>
        public double GetLightSourceDirection()
        {
            this.InternalKeyDVEParametersReference.GetLightSourceDirection(out double degrees);
            return degrees;
        }

        /// <summary>
        /// The SetLightSourceDirection method sets the light source direction value.
        /// </summary>
        /// <param name="degrees">The desired light source direction value in degrees.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.4</remarks>
        /// <exception cref="FailedException">Failure.</exception>
        public void SetLightSourceDirection(double degrees)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetLightSourceDirection(degrees);
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
        /// The GetLightSourceAltitude method gets the current light source altitude value.
        /// </summary>
        /// <returns>The current light source altitude value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.5</remarks>
        public double GetLightSourceAltitude()
        {
            this.InternalKeyDVEParametersReference.GetLightSourceAltitude(out double altitude);
            return altitude;
        }

        /// <summary>
        /// The SetLightSourceAltitude method sets the light source altitude value.
        /// </summary>
        /// <param name="altitude">The desired light source altitude value.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.6</remarks>
        /// <exception cref="FailedException">Failure.</exception>
        public void SetLightSourceAltitude(double altitude)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetLightSourceAltitude(altitude);
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
        /// The GetBorderEnabled method gets the current border enabled flag.
        /// </summary>
        /// <returns>The current border enabled flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.7</remarks>
        public bool GetBorderEnabled()
        {
            this.InternalKeyDVEParametersReference.GetBorderEnabled(out int enabled);
            return Convert.ToBoolean(enabled);
        }

        /// <summary>
        /// The SetBorderEnabled method sets the border enabled flag.
        /// </summary>
        /// <param name="enabled">The desired border enabled flag.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.8</remarks>
        /// <exception cref="FailedException">Failure.</exception>
        public void SetBorderEnabled(bool enabled)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetBorderEnabled(Convert.ToInt32(enabled));
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
        /// The GetBorderBevel method gets the current border bevel option.
        /// </summary>
        /// <returns>The current bevel option of BMDSwitcherBorderBevelOption.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.9</remarks>
        /// <exception cref="Exception">Unexpected error occurred.</exception>
        public _BMDSwitcherBorderBevelOption GetBorderBevel()
        {
            this.InternalKeyDVEParametersReference.GetBorderBevel(out _BMDSwitcherBorderBevelOption bevelOption);
            return bevelOption;
        }

        /// <summary>
        /// The SetBorderBevel method sets the border bevel option.
        /// </summary>
        /// <param name="bevelOption">The desired bevel option of BMDSwitcherBorderBevelOption.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.10</remarks>
        /// <exception cref="ArgumentException">The bevelOption parameter is invalid.</exception>
        /// <exception cref="FailedException">Failure.</exception>
        public void SetBorderBevel(_BMDSwitcherBorderBevelOption bevelOption)
        {
            this.InternalKeyDVEParametersReference.SetBorderBevel(bevelOption);
            return;
        }

        /// <summary>
        /// The GetBorderWidthIn method gets the current border inner width value.
        /// </summary>
        /// <returns>The current border inner width value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.11</remarks>
        public double GetBorderWidthIn()
        {
            this.InternalKeyDVEParametersReference.GetBorderWidthIn(out double widthIn);
            return widthIn;
        }

        /// <summary>
        /// The SetBorderWidthIn method sets the border inner width value.
        /// </summary>
        /// <param name="widthIn">The desired border inner width value.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.12</remarks>
        /// <exception cref="FailedException">Failure.</exception>
        public void SetBorderWidthIn(double widthIn)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetBorderWidthIn(widthIn);
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
        /// The GetBorderWidthOut method gets the current border outer width value.
        /// </summary>
        /// <returns>The current border outer width value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.13</remarks>
        public double GetBorderWidthOut()
        {
            this.InternalKeyDVEParametersReference.GetBorderWidthOut(out double widthOut);
            return widthOut;
        }

        /// <summary>
        /// The SetBorderWidthOut method sets the border outer width value.
        /// </summary>
        /// <param name="widthOut">The desired border outer width value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.14</remarks>
        public void SetBorderWidthOut(double widthOut)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetBorderWidthOut(widthOut);
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
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.15</remarks>
        public double GetBorderSoftnessIn()
        {
            try
            {
                this.InternalKeyDVEParametersReference.GetBorderSoftnessIn(out double widthSoftnessIn);
                return widthSoftnessIn;
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
        /// The SetBorderSoftnessIn method sets the border inner softness value.
        /// </summary>
        /// <param name="softnessIn">The desired border inner softness value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.16</remarks>
        public void SetBorderSoftnessIn(double softnessIn)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetBorderSoftnessIn(softnessIn);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.17</remarks>
        public double GetBorderSoftnessOut()
        {
            this.InternalKeyDVEParametersReference.GetBorderSoftnessOut(out double softOut);
            return softOut;
        }

        /// <summary>
        /// The SetBorderSoftnessOut method sets the border outer softness value.
        /// </summary>
        /// <param name="softOut">The desired border outer softness value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.18</remarks>
        public void SetBorderSoftnessOut(double softOut)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetBorderSoftnessOut(softOut);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.19</remarks>
        public double GetBorderBevelSoftness()
        {
            this.InternalKeyDVEParametersReference.GetBorderBevelSoftness(out double bevelSoft);
            return bevelSoft;
        }

        /// <summary>
        /// The SetBorderBevelSoftness method sets the border bevel softness value.
        /// </summary>
        /// <param name="bevelSoft">The desired border bevel softness value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.20</remarks>
        public void SetBorderBevelSoftness(double bevelSoft)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetBorderBevelSoftness(bevelSoft);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.21</remarks>
        public double GetBorderBevelPosition()
        {
            this.InternalKeyDVEParametersReference.GetBorderBevelPosition(out double bevelPosition);
            return bevelPosition;
        }

        /// <summary>
        /// The SetBorderBevelPosition method sets the border bevel position value.
        /// </summary>
        /// <param name="bevelPosition">The desired border bevel position value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.22</remarks>
        public void SetBorderBevelPosition(double bevelPosition)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetBorderBevelPosition(bevelPosition);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.23</remarks>
        public double GetBorderOpacity()
        {
            this.InternalKeyDVEParametersReference.GetBorderOpacity(out double opacity);
            return opacity;
        }

        /// <summary>
        /// The SetBorderOpacity method sets the border opacity value.
        /// </summary>
        /// <param name="opacity">The desired border opacity value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.24</remarks>
        public void SetBorderOpacity(double opacity)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetBorderOpacity(opacity);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.25</remarks>
        public double GetBorderHue()
        {
            this.InternalKeyDVEParametersReference.GetBorderHue(out double hue);
            return hue;
        }

        /// <summary>
        /// The SetBorderHue method sets the border hue value.
        /// </summary>
        /// <param name="hue">The desired border hue value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.26</remarks>
        public void SetBorderHue(double hue)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetBorderHue(hue);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.27</remarks>
        public double GetBorderSaturation()
        {
            this.InternalKeyDVEParametersReference.GetBorderSaturation(out double saturation);
            return saturation;
        }

        /// <summary>
        /// The SetBorderSaturation method sets the border saturation value.
        /// </summary>
        /// <param name="saturation">The desired border saturation value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.28</remarks>
        public void SetBorderSaturation(double saturation)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetBorderSaturation(saturation);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.29</remarks>
        public double GetBorderLuma()
        {
            this.InternalKeyDVEParametersReference.GetBorderLuma(out double luma);
            return luma;
        }

        /// <summary>
        /// The SetBorderLuma method sets the border luminance value.
        /// </summary>
        /// <param name="luma">The desired border luminance value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.30</remarks>
        public void SetBorderLuma(double luma)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetBorderLuma(luma);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.31</remarks>
        public bool GetMasked()
        {
            this.InternalKeyDVEParametersReference.GetMasked(out int maskEnabled);
            return Convert.ToBoolean(maskEnabled);
        }

        /// <summary>
        /// Use SetMasked method to enable or disable masking.
        /// </summary>
        /// <param name="maskEnabled">The desired masked value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.32</remarks>
        public void SetMasked(bool maskEnabled)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetMasked(Convert.ToInt32(maskEnabled));
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.33</remarks>
        public double GetMaskTop()
        {
            this.InternalKeyDVEParametersReference.GetMaskTop(out double maskTop);
            return maskTop;
        }

        /// <summary>
        /// The SetMaskTop method sets the mask top value.
        /// </summary>
        /// <param name="maskTop">The desired mask top value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.34</remarks>
        public void SetMaskTop(double maskTop)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetMaskTop(maskTop);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.35</remarks>
        public double GetMaskBottom()
        {
            this.InternalKeyDVEParametersReference.GetMaskBottom(out double maskBottom);
            return maskBottom;
        }

        /// <summary>
        /// The SetMaskBottom method sets the mask bottom value.
        /// </summary>
        /// <param name="maskBottom">The desired mask bottom value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.36</remarks>
        public void SetMaskBottom(double maskBottom)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetMaskBottom(maskBottom);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.37</remarks>
        public double GetMaskLeft()
        {
            this.InternalKeyDVEParametersReference.GetMaskLeft(out double maskLeft);
            return maskLeft;
        }

        /// <summary>
        /// The SetMaskLeft method sets the mask left value.
        /// </summary>
        /// <param name="maskLeft">The desired mask left value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.38</remarks>
        public void SetMaskLeft(double maskLeft)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetMaskLeft(maskLeft);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.39</remarks>
        public double GetMaskRight()
        {
            this.InternalKeyDVEParametersReference.GetMaskRight(out double maskRight);
            return maskRight;
        }

        /// <summary>
        /// The SetMaskRight method sets the mask right value.
        /// </summary>
        /// <param name="maskRight">The desired mask right value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.40</remarks>
        public void SetMaskRight(double maskRight)
        {
            try
            {
                this.InternalKeyDVEParametersReference.SetMaskRight(maskRight);
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
        /// <remarks>Blackmagic Switcher SDK - 5.2.12.41</remarks>
        public void ResetMask()
        {
            try
            {
                this.InternalKeyDVEParametersReference.ResetMask();
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

        #region IBMDSwitcherKeyDVEParametersCallback
        void IBMDSwitcherKeyDVEParametersCallback.Notify(_BMDSwitcherKeyDVEParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeShadowChanged:
                    this.OnShadowChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeLightSourceDirectionChanged:
                    this.OnLightSourceDirectionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeLightSourceAltitudeChanged:
                    this.OnLightSourceAltitudeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderEnabledChanged:
                    this.OnBorderEnabledChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderBevelChanged:
                    this.OnBorderBevelChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderWidthInChanged:
                    this.OnBorderInnerWidthChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderWidthOutChanged:
                    this.OnBorderOuterWidthChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderSoftnessInChanged:
                    this.OnBorderInnerSoftnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderSoftnessOutChanged:
                    this.OnBorderOuterSoftnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderBevelSoftnessChanged:
                    this.OnBorderBevelSoftnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderBevelPositionChanged:
                    this.OnBorderBevelPositionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderOpacityChanged:
                    this.OnBorderOpacityChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderHueChanged:
                    this.OnBorderHueChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderSaturationChanged:
                    this.OnBorderSaturationChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeBorderLumaChanged:
                    this.OnBorderLumaChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeMaskedChanged:
                    this.OnMaskedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeMaskTopChanged:
                    this.OnMaskTopChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeMaskBottomChanged:
                    this.OnMaskBottomChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeMaskLeftChanged:
                    this.OnMaskLeftChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyDVEParametersEventType.bmdSwitcherKeyDVEParametersEventTypeMaskRightChanged:
                    this.OnMaskRightChanged?.Invoke(this);
                    break;
            }
            return;
        }
        #endregion
    }
}