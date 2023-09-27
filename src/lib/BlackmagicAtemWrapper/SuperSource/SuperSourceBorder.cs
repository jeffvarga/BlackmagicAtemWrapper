//-----------------------------------------------------------------------------
// <copyright file="SuperSourceBorder.cs">
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
    /// The SuperSourceBorder class is used for manipulating supersource border settings.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 6.2.6</remarks>
    public class SuperSourceBorder : IBMDSwitcherSuperSourceBorderCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherSuperSourceBorder"/>.
        /// </summary>
        internal IBMDSwitcherSuperSourceBorder InternalSuperSourceBorderReference;

        /// <summary>
        /// Initializes an instance of the <see cref="SuperSourceBorder"/> class.
        /// </summary>
        /// <param name="superSourceBorder">The native <seealso cref="IBMDSwitcherSuperSourceBorder"/> from the BMDSwitcherAPI</param>
        /// <exception cref="ArgumentNullException"><paramref name="superSourceBorder"/> was null.</exception>
        public SuperSourceBorder(IBMDSwitcherSuperSourceBorder superSourceBorder)
        {
            this.InternalSuperSourceBorderReference = superSourceBorder ?? throw new ArgumentNullException(nameof(superSourceBorder));
            this.InternalSuperSourceBorderReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SuperSourceBorder"/> class.
        /// </summary>
        ~SuperSourceBorder()
        {
            this.InternalSuperSourceBorderReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalSuperSourceBorderReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="SuperSourceBorder"/>.
        /// </summary>
        /// <param name="sender">The <see cref="SuperSourceBorder"/> that received the event.</param>
        public delegate void SuperSourceBorderEventHandler(object sender);

        /// <summary>
        /// The <see cref="IsEnabled"/> flag changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnEnabledChanged;

        /// <summary>
        /// The <see cref="Bevel"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnBevelChanged;

        /// <summary>
        /// The <see cref="OuterWidth"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnOuterWidthChanged;

        /// <summary>
        /// The <see cref="InnerWidth"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnInnerWidthChanged;

        /// <summary>
        /// The <see cref="OuterSoftness"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnOuterSoftnessChanged;

        /// <summary>
        /// The <see cref="InnerSoftness"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnInnerSoftnessChanged;

        /// <summary>
        /// The <see cref="BevelSoftness"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnBevelSoftnessChanged;

        /// <summary>
        /// The <see cref="BevelPosition"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnBevelPositionChanged;

        /// <summary>
        /// The <see cref="Hue"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnHueChanged;

        /// <summary>
        /// The <see cref="Saturation"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnSaturationChanged;

        /// <summary>
        /// The <see cref="Luma"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnLumaChanged;

        /// <summary>
        /// The <see cref="LightSourceDirection"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnLightSourceDirectionChanged;

        /// <summary>
        /// The <see cref="LightSourceAltitude"/> value changed.
        /// </summary>
        public event SuperSourceBorderEventHandler OnLightSourceAltitudeChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the border is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return this.GetBorderEnabled(); }
            set { this.SetBorderEnabled(value); }
        }

        /// <summary>
        /// Gets or sets the current border bevel
        /// </summary>
        public _BMDSwitcherBorderBevelOption Bevel
        {
            get { return this.GetBorderBevel(); }
            set { this.SetBorderBevel(value); }
        }

        /// <summary>
        /// Gets or sets the current border outer width.
        /// </summary>
        public double OuterWidth
        {
            get { return this.GetBorderWidthOut(); }
            set { this.SetBorderWidthOut(value); }
        }

        /// <summary>
        /// Gets or sets the current border inner width.
        /// </summary>
        public double InnerWidth
        {
            get { return this.GetBorderWidthIn(); }
            set { this.SetBorderWidthIn(value); }
        }

        /// <summary>
        /// Gets or sets the border outer softness.
        /// </summary>
        public double OuterSoftness
        {
            get { return this.GetBorderSoftnessOut(); }
            set { this.SetBorderSoftnessOut(value); }
        }

        /// <summary>
        /// Gets or sets the border inner softness.
        /// </summary>
        public double InnerSoftness
        {
            get { return this.GetBorderSoftnessIn(); }
            set { this.SetBorderSoftnessIn(value); }
        }

        /// <summary>
        /// Gets or sets the current border bevel softness.
        /// </summary>
        public double BevelSoftness
        {
            get { return this.GetBorderBevelSoftness(); }
            set { this.SetBorderBevelSoftness(value); }
        }

        /// <summary>
        /// Gets or sets the current border bevel position.
        /// </summary>
        public double BevelPosition
        {
            get { return this.GetBorderBevelPosition(); }
            set { this.SetBorderBevelPosition(value); }
        }

        /// <summary>
        /// Gets or sets the current border hue.
        /// </summary>
        public double Hue
        {
            get { return this.GetBorderHue(); }
            set { this.SetBorderHue(value); }
        }

        /// <summary>
        /// Gets or sets the current border saturation.
        /// </summary>
        public double Saturation
        {
            get { return this.GetBorderSaturation(); }
            set { this.SetBorderSaturation(value); }
        }

        /// <summary>
        /// Gets or sets the current border luma.
        /// </summary>
        public double Luma
        {
            get { return this.GetBorderLuma(); }
            set { this.SetBorderLuma(value); }
        }

        /// <summary>
        /// Gets or sets the current border light source direction.
        /// </summary>
        public double LightSourceDirection
        {
            get { return this.GetBorderLightSourceDirection(); }
            set { this.SetBorderLightSourceDirection(value); }
        }

        /// <summary>
        /// Gets or sets the current border light source altitude.
        /// </summary>
        public double LightSourceAltitude
        {
            get { return this.GetBorderLightSourceAltitude(); }
            set { this.SetBorderLightSourceAltitude(value); }
        }
        #endregion

        #region IBMDSwitcherSuperSourceBorder
        /// <summary>
        /// The GetBorderEnabled method returns the current border enabled flag.
        /// </summary>
        /// <returns>The current border enabled flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.1</remarks>
        public bool GetBorderEnabled()
        {
            this.InternalSuperSourceBorderReference.GetBorderEnabled(out int enabled);
            return Convert.ToBoolean(enabled);
        }

        /// <summary>
        /// The SetBorderEnabled method sets the border enabled flag.
        /// </summary>
        /// <param name="enabled">The desired border enabled flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.2</remarks>
        public void SetBorderEnabled(bool enabled)
        {
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderEnabled(Convert.ToInt32(enabled));
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
        /// The GetBorderBevel method returns the current border bevel option.
        /// </summary>
        /// <returns>The current border bevel option.</returns>
        /// <exception cref="COMException">E_UNEXPECTED.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.3</remarks>
        public _BMDSwitcherBorderBevelOption GetBorderBevel()
        {
            this.InternalSuperSourceBorderReference.GetBorderBevel(out _BMDSwitcherBorderBevelOption bevelOption);
            return bevelOption;
        }

        /// <summary>
        /// The SetBorderBevel method sets the border bevel option.
        /// </summary>
        /// <param name="bevelOption">The desired border bevel option.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <exception cref="ArgumentException">The <paramref name="bevelOption"/> parameter is invalid.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.4</remarks>
        public void SetBorderBevel(_BMDSwitcherBorderBevelOption bevelOption)
        {
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderBevel(bevelOption);
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
        /// The GetBorderWidthOut method returns the current border outer width.
        /// </summary>
        /// <returns>The current border outer width.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.5</remarks>
        public double GetBorderWidthOut()
        {
            this.InternalSuperSourceBorderReference.GetBorderWidthOut(out double widthOut);
            return widthOut;
        }

        /// <summary>
        /// The SetBorderWidthOut method sets the border outer width.
        /// </summary>
        /// <param name="widthOut">The desired border outer width.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.7</remarks>
        public void SetBorderWidthOut(double widthOut)
        {
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderWidthOut(widthOut);
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
        /// The GetBorderWidthIn method returns the current border inner width.
        /// </summary>
        /// <returns>The current border inner width.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.8</remarks>
        public double GetBorderWidthIn()
        {
            this.InternalSuperSourceBorderReference.GetBorderWidthIn(out double widthIn);
            return widthIn;
        }

        /// <summary>
        /// The SetBorderWidthIn method sets the border inner width.
        /// </summary>
        /// <param name="widthIn">The desired border inner width.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.9</remarks>
        public void SetBorderWidthIn(double widthIn)
        {
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderWidthIn(widthIn);
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
        /// The GetBorderSoftnessOut method returns the current border outer softness.
        /// </summary>
        /// <returns>The current border outer softness.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.10</remarks>
        public double GetBorderSoftnessOut()
        {
            this.InternalSuperSourceBorderReference.GetBorderSoftnessOut(out double softnessOut);
            return softnessOut;
        }

        /// <summary>
        /// The SetBorderSoftnessOut method sets the border outer softness.
        /// </summary>
        /// <param name="softnessOut">The desired border outer softness.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.11</remarks>
        public void SetBorderSoftnessOut(double softnessOut)
        { 
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderSoftnessOut(softnessOut);
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
        /// The GetBorderSoftnessIn method returns the current border inner softness.
        /// </summary>
        /// <returns>The current border inner softness.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.12</remarks>
        public double GetBorderSoftnessIn()
        {
            this.InternalSuperSourceBorderReference.GetBorderSoftnessIn(out double softnessIn);
            return softnessIn;
        }

        /// <summary>
        /// The SetBorderSoftnessIn method sets the border inner softness.
        /// </summary>
        /// <param name="softnessIn">The desired border inner softness.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.13</remarks>
        public void SetBorderSoftnessIn(double softnessIn)
        { 
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderSoftnessIn(softnessIn);
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
        /// The GetBorderBevelSoftness method returns the current border bevel softness.
        /// </summary>
        /// <returns>The current border bevel softness.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.14</remarks>
        public double GetBorderBevelSoftness()
        {
            this.InternalSuperSourceBorderReference.GetBorderBevelSoftness(out double bevelSoftness);
            return bevelSoftness;
        }

        /// <summary>
        /// The SetBorderBevelSoftness method sets the border bevel softness.
        /// </summary>
        /// <param name="bevelSoftness">The desired border bevel softness.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.15</remarks>
        public void SetBorderBevelSoftness(double bevelSoftness)
        { 
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderBevelSoftness(bevelSoftness);
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
        /// The GetBorderBevelPosition method returns the current border bevel position.
        /// </summary>
        /// <returns>The current border bevel position.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.16</remarks>
        public double GetBorderBevelPosition()
        {
            this.InternalSuperSourceBorderReference.GetBorderBevelPosition(out double bevelPosition);
            return bevelPosition;
        }

        /// <summary>
        /// The SetBorderBevelPosition method sets the border bevel position.
        /// </summary>
        /// <param name="bevelPosition">The desired border bevel position.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.17</remarks>
        public void SetBorderBevelPosition(double bevelPosition)
        { 
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderBevelPosition(bevelPosition);
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
        /// The GetBorderHue method returns the current border hue.
        /// </summary>
        /// <returns>The current border hue.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.18</remarks>
        public double GetBorderHue()
        {
            this.InternalSuperSourceBorderReference.GetBorderHue(out double hue);
            return hue;
        }

        /// <summary>
        /// The SetBorderHue method sets the border hue.
        /// </summary>
        /// <param name="bue">The desired border hue.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.19</remarks>
        public void SetBorderHue(double bue)
        { 
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderHue(bue);
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
        /// The GetBorderSaturation method returns the current border saturation.
        /// </summary>
        /// <returns>The current border saturation.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.20</remarks>
        public double GetBorderSaturation()
        {
            this.InternalSuperSourceBorderReference.GetBorderSaturation(out double sat);
            return sat;
        }

        /// <summary>
        /// The SetBorderSaturation method sets the border saturation.
        /// </summary>
        /// <param name="sat">The desired border saturation.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.21</remarks>
        public void SetBorderSaturation(double sat)
        { 
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderSaturation(sat);
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
        /// The GetBorderLuma method returns the current border luminescence.
        /// </summary>
        /// <returns>The current border luminescence.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.22</remarks>
        public double GetBorderLuma()
        {
            this.InternalSuperSourceBorderReference.GetBorderLuma(out double luma);
            return luma;
        }

        /// <summary>
        /// The SetBorderLuma method sets the border luminescence.
        /// </summary>
        /// <param name="luma">The desired border luminescence.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.23</remarks>
        public void SetBorderLuma(double luma)
        { 
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderLuma(luma);
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
        /// The GetBorderLightSourceDirection method returns the current border light source direction.
        /// </summary>
        /// <returns>The current border light source direction in degrees.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.24</remarks>
        public double GetBorderLightSourceDirection()
        {
            this.InternalSuperSourceBorderReference.GetBorderLightSourceDirection(out double degrees);
            return degrees;
        }

        /// <summary>
        /// The SetBorderLightSourceDirection method sets the border light source direction.
        /// </summary>
        /// <param name="degrees">The desired border light source direction in degrees.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.25</remarks>
        public void SetBorderLightSourceDirection(double degrees)
        { 
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderLightSourceDirection(degrees);
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
        /// The GetBorderLightSourceAltitude method returns the current border light source altitude.
        /// </summary>
        /// <returns>The current border light source altitude.</returns>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.26</remarks>
        public double GetBorderLightSourceAltitude()
        {
            this.InternalSuperSourceBorderReference.GetBorderLightSourceAltitude(out double altitude);
            return altitude;
        }

        /// <summary>
        /// The SetBorderLightSourceAltitude method sets the border light source altitude.
        /// </summary>
        /// <param name="altitude">The desired border light source altitude.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 6.2.6.27</remarks>
        public void SetBorderLightSourceAltitude(double altitude)
        { 
            try
            {
                this.InternalSuperSourceBorderReference.SetBorderLightSourceAltitude(altitude);
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

        #region IBMDSwitcherSuperSourceBorderCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherSuperSourceBorder events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherSuperSourceBorderEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 6.2.7.1</remarks>
        void IBMDSwitcherSuperSourceBorderCallback.Notify(_BMDSwitcherSuperSourceBorderEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeEnabledChanged:
                    this.OnEnabledChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeBevelChanged:
                    this.OnBevelChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeWidthOutChanged:
                    this.OnOuterWidthChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeWidthInChanged:
                    this.OnInnerWidthChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeSoftnessOutChanged:
                    this.OnOuterSoftnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeSoftnessInChanged:
                    this.OnInnerSoftnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeBevelSoftnessChanged:
                    this.OnBevelSoftnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeBevelPositionChanged:
                    this.OnBevelPositionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeHueChanged:
                    this.OnHueChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeSaturationChanged:
                    this.OnSaturationChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeLumaChanged:
                    this.OnLumaChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeLightSourceDirectionChanged:
                    this.OnLightSourceDirectionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherSuperSourceBorderEventType.bmdSwitcherSuperSourceBorderEventTypeLightSourceAltitudeChanged:
                    this.OnLightSourceAltitudeChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
