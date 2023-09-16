//-----------------------------------------------------------------------------
// <copyright file="AdvancedChromaParameters.cs">
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
    /// <para>The AdvancedChromaParameters class is used for manipulating settings specific to the advanced chroma type key.</para>
    /// <para>Advanced chroma key is an improved version of chroma key and is not available on all models of switchers.</para>
    /// <para>Use <see cref="Key.DoesSupportAdvancedChroma"/> to determine if a switcher supports the IBMDSwitcherKeyAdvancedChromaParameters interface.</para>
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 5.2.8</remarks>
    public class AdvancedChromaParameters : IBMDSwitcherKeyAdvancedChromaParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherKeyAdvancedChromaParameters"/>
        /// </summary>
        private readonly IBMDSwitcherKeyAdvancedChromaParameters InternalAdvancedChromaParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedChromaParameters" /> class.
        /// </summary>
        /// <param name="advChromaParameters">The native <seealso cref="IBMDSwitcherKeyAdvancedChromaParameters"/> from the BMDSwitcherAPI.</param>
        public AdvancedChromaParameters(IBMDSwitcherKeyAdvancedChromaParameters advChromaParameters)
        {
            this.InternalAdvancedChromaParametersReference = advChromaParameters ?? throw new ArgumentNullException(nameof(advChromaParameters));
            this.InternalAdvancedChromaParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="AdvancedChromaParameters"/> class.
        /// </summary>
        ~AdvancedChromaParameters()
        {
            this.InternalAdvancedChromaParametersReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalAdvancedChromaParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="AdvancedChromaParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="AdvancedChromaParameters"/> that received the event.</param>
        public delegate void AdvancedChromaParametersEventHandler(object sender);

        /// <summary>
        /// The key adjustment foreground level value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnForegroundLevelChanged;

        /// <summary>
        /// The key adjustment background level value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnBackgroundLevelChanged;

        /// <summary>
        /// The key adjustment key edge value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnKeyEdgeChanged;

        /// <summary>
        /// The chroma correction spill suppress value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnSpillSuppressChanged;

        /// <summary>
        /// The chroma correction flare suppress value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnFlareSuppressChanged;

        /// <summary>
        /// The color adjustment brightness value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnBrightnessChanged;

        /// <summary>
        /// The color adjustment contrast value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnContrastChanged;

        /// <summary>
        /// The color adjustment saturation value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnSaturationChanged;

        /// <summary>
        /// The color adjustment red value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnRedChanged;

        /// <summary>
        /// The color adjustment green value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnGreenChanged;

        /// <summary>
        /// The color adjustment blue value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnBlueChanged;

        /// <summary>
        /// The sampling mode enabled flag changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnSamplingModeEnabled;

        /// <summary>
        /// The preview enabled flag changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnPreviewEnabledChanged;

        /// <summary>
        /// The cursor X position value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnCursorXPositionChanged;

        /// <summary>
        /// The cursor Y position value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnCursorYPositionChanged;

        /// <summary>
        /// The cursor size value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnCursorSizeChanged;

        /// <summary>
        /// The sampled color value changed.
        /// </summary>
        public event AdvancedChromaParametersEventHandler OnSampledColorChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current key adjustment foreground level value.
        /// </summary>
        public double ForegroundLevel
        {
            get { return this.GetForegroundLevel(); }
            set { this.SetForegroundLevel(value); }
        }

        /// <summary>
        /// Gets or sets the current key adjustment background level value.
        /// </summary>
        public double BackgroundLevel
        {
            get { return this.GetBackgroundLevel(); }
            set { this.SetBackgroundLevel(value); }
        }

        /// <summary>
        /// Gets or sets the current key adjustment key edge value.
        /// </summary>
        public double KeyEdge
        {
            get { return this.GetKeyEdge(); }
            set { this.SetKeyEdge(value); }
        }

        /// <summary>
        /// Gets or sets the current chroma correction spill suppress value.
        /// </summary>
        public double SpillSuppress
        {
            get { return this.GetSpillSuppress(); }
            set { this.SetSpillSuppress(value); }
        }

        /// <summary>
        /// Gets or sets the current chroma correction flare suppress value.
        /// </summary>
        public double FlareSuppress
        {
            get { return this.GetFlareSuppress(); }
            set { this.SetFlareSuppress(value); }
        }

        /// <summary>
        /// Gets or sets the current color adjustment brightness value.
        /// </summary>
        public double Brightness
        {
            get { return this.GetBrightness(); }
            set { this.SetBrightness(value); }
        }

        /// <summary>
        /// Gets or sets the current color adjustment contrast value.
        /// </summary>
        public double Contrast
        {
            get { return this.GetContrast(); }
            set { this.SetContrast(value); }
        }

        /// <summary>
        /// Gets or sets the current color adjustment saturation value.
        /// </summary>
        public double Saturation
        {
            get { return this.GetSaturation(); }
            set { this.SetSaturation(value); }
        }

        /// <summary>
        /// Gets or sets the current color adjustment red value.
        /// </summary>
        public double Red
        {
            get { return this.GetRed(); }
            set { this.SetRed(value); }
        }

        /// <summary>
        /// Gets or sets the current color adjustment green value.
        /// </summary>
        public double Green
        {
            get { return this.GetGreen(); }
            set { this.SetGreen(value); }
        }

        /// <summary>
        /// Gets or sets the current color adjustment blue value.
        /// </summary>
        public double Blue
        {
            get { return this.GetBlue(); }
            set { this.SetBlue(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether sampling mode is enabled.
        /// </summary>
        public bool IsSamplingModeEnabled
        {
            get { return this.GetSamplingModeEnabled(); }
            set { this.SetSamplingModeEnabled(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether preview is enabled.
        /// </summary>
        public bool IsPreviewEnabled
        {
            get { return this.GetPreviewEnabled(); }
            set { this.SetPreviewEnabled(value); }
        }

        /// <summary>
        /// Gets or sets the current cursor x position.
        /// </summary>
        public double CursorXPosition
        {
            get { return this.GetCursorXPosition(); }
            set { this.SetCursorXPosition(value); }
        }

        /// <summary>
        /// Gets or sets the current cursor y position.
        /// </summary>
        public double CursorYPosition
        {
            get { return this.GetCursorYPosition(); }
            set { this.SetCursorYPosition(value); }
        }

        /// <summary>
        /// Gets or sets the current cursor size.
        /// </summary>
        public double CursorSize
        {
            get { return this.GetCursorSize(); }
            set { this.SetCursorSize(value); }
        }

        /// <summary>
        /// Gets or sets the current sampled color.
        /// </summary>
        public YCbCrColor SampledColor
        {
            get { return this.GetSampledColor(); }
            set { this.SetSampledColor(value); }
        }
        #endregion

        #region IBMDSwitcherKeyAdvancedChromaParameters
        /// <summary>
        /// The GetForegroundLevel method gets the current key adjustment foreground level value.
        /// </summary>
        /// <returns>The current key adjustment foreground level value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.1</remarks>
        public double GetForegroundLevel()
        {
            this.InternalAdvancedChromaParametersReference.GetForegroundLevel(out double level);
            return level;
        }

        /// <summary>
        /// The SetForegroundLevel method sets the key adjustment foreground level value.
        /// </summary>
        /// <param name="level">The desired key adjustment foreground level value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.2</remarks>
        public void SetForegroundLevel(double level)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetForegroundLevel(level);
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
        /// The GetBackgroundLevel method gets the current key adjustment background level value.
        /// </summary>
        /// <returns>The current key adjustment background level value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.3</remarks>
        public double GetBackgroundLevel()
        {
            this.InternalAdvancedChromaParametersReference.GetBackgroundLevel(out double level);
            return level;
        }

        /// <summary>
        /// The SetBackgroundLevel method sets the key adjustment background level value.
        /// </summary>
        /// <param name="level">The desired key adjustment background level value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.4</remarks>
        /// <bug>Documentation lists SetForegroundLevel as title</bug>
        public void SetBackgroundLevel(double level)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetBackgroundLevel(level);
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
        /// The GetKeyEdge method gets the current key adjustment key edge value.
        /// </summary>
        /// <returns>The current key adjustment key edge value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.5</remarks>
        public double GetKeyEdge()
        {
            this.InternalAdvancedChromaParametersReference.GetKeyEdge(out double keyEdge);
            return keyEdge;
        }

        /// <summary>
        /// The SetKeyEdge method sets the key adjustment key edge value.
        /// </summary>
        /// <param name="keyEdge">The desired key adjustment key edge value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.6</remarks>
        public void SetKeyEdge(double keyEdge)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetKeyEdge(keyEdge);
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
        /// The GetSpillSuppress method gets the current chroma correction spill suppress value.
        /// </summary>
        /// <returns>The current chroma correction spill suppress value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.7</remarks>
        public double GetSpillSuppress()
        {
            this.InternalAdvancedChromaParametersReference.GetSpillSuppress(out double spillSuppress);
            return spillSuppress;
        }

        /// <summary>
        /// The SetSpillSuppress method sets the chroma correction spill suppress value.
        /// </summary>
        /// <param name="spillSuppress">The desired chroma correction spill suppress value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.8</remarks>
        public void SetSpillSuppress(double spillSuppress)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetSpillSuppress(spillSuppress);
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
        /// The GetFlareSuppress method gets the current chroma correction flare suppress value.
        /// </summary>
        /// <returns>The current chroma correction flare suppress value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.9</remarks>
        public double GetFlareSuppress()
        {
            this.InternalAdvancedChromaParametersReference.GetFlareSuppress(out double flareSuppress);
            return flareSuppress;
        }

        /// <summary>
        /// The SetFlareSuppress method sets the chroma correction flare suppress value.
        /// </summary>
        /// <param name="flareSuppress">The desired chroma correction flare suppress value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.10</remarks>
        public void SetFlareSuppress(double flareSuppress)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetFlareSuppress(flareSuppress);
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
        /// The GetBrightness method gets the current color adjustment brightness value.
        /// </summary>
        /// <returns>The current color adjustment brightness value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.11</remarks>
        public double GetBrightness()
        {
            this.InternalAdvancedChromaParametersReference.GetBrightness(out double brightness);
            return brightness;
        }

        /// <summary>
        /// The SetBrightness method sets the color adjustment brightness value.
        /// </summary>
        /// <param name="brightness">The desired color adjustment brightness value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.12</remarks>
        public void SetBrightness(double brightness)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetBrightness(brightness);
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
        /// The GetContrast method gets the current color adjustment contrast value.
        /// </summary>
        /// <returns>The current color adjustment contrast value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.13</remarks>
        public double GetContrast()
        {
            this.InternalAdvancedChromaParametersReference.GetContrast(out double contrast);
            return contrast;
        }

        /// <summary>
        /// The SetContrast method sets the color adjustment contrast value.
        /// </summary>
        /// <param name="contrast">The desired color adjustment contrast value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.14</remarks>
        public void SetContrast(double contrast)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetContrast(contrast);
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
        /// The GetSaturation method gets the current color adjustment saturation value.
        /// </summary>
        /// <returns>The current color adjustment saturation value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.15</remarks>
        public double GetSaturation()
        {
            this.InternalAdvancedChromaParametersReference.GetSaturation(out double saturation);
            return saturation;
        }

        /// <summary>
        /// The SetSaturation method sets the color adjustment saturation value.
        /// </summary>
        /// <param name="saturation">The desired color adjustment saturation value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.16</remarks>
        public void SetSaturation(double saturation)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetSaturation(saturation);
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
        /// The GetRed method gets the current color adjustment red value.
        /// </summary>
        /// <returns>The current color adjustment red value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.17</remarks>
        public double GetRed()
        {
            this.InternalAdvancedChromaParametersReference.GetRed(out double red);
            return red;
        }

        /// <summary>
        /// The SetRed method sets the color adjustment red value.
        /// </summary>
        /// <param name="red">The desired color adjustment red value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.18</remarks>
        public void SetRed(double red)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetRed(red);
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
        /// The GetGreen method gets the current color adjustment green value.
        /// </summary>
        /// <returns>The current color adjustment green value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.19</remarks>
        public double GetGreen()
        {
            this.InternalAdvancedChromaParametersReference.GetGreen(out double green);
            return green;
        }

        /// <summary>
        /// The SetGreen method sets the color adjustment green value.
        /// </summary>
        /// <param name="green">The desired color adjustment green value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.20</remarks>
        public void SetGreen(double green)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetGreen(green);
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
        /// The GetBlue method gets the current color adjustment blue value.
        /// </summary>
        /// <returns>The current color adjustment blue value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.21</remarks>
        public double GetBlue()
        {
            this.InternalAdvancedChromaParametersReference.GetBlue(out double blue);
            return blue;
        }

        /// <summary>
        /// The SetBlue method sets the color adjustment blue value.
        /// </summary>
        /// <param name="blue">The desired color adjustment blue value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.22</remarks>
        public void SetBlue(double blue)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetBlue(blue);
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
        /// The GetSamplingModeEnabled method gets the current sampling mode enabled flag.
        /// </summary>
        /// <returns>The current sampling mode enabled flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.23</remarks>
        /// <bug>Title contains errant space in method name.</bug>
        public bool GetSamplingModeEnabled()
        {
            this.InternalAdvancedChromaParametersReference.GetSamplingModeEnabled(out int enabled);
            return Convert.ToBoolean(enabled);
        }

        /// <summary>
        /// The SetSamplingModeEnabled method sets the sampling mode enabled flag.
        /// </summary>
        /// <param name="enabled">The desired sampling mode enabled flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.24</remarks>
        public void SetSamplingModeEnabled(bool enabled)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetSamplingModeEnabled(Convert.ToInt32(enabled));
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
        /// The GetPreviewEnabled method gets the current preview enabled flag.
        /// </summary>
        /// <returns>The enabled parameter is invalid.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.25</remarks>
        public bool GetPreviewEnabled()
        {
            this.InternalAdvancedChromaParametersReference.GetPreviewEnabled(out int enabled);
            return Convert.ToBoolean(enabled);
        }

        /// <summary>
        /// The SetPreviewEnabled method sets the preview enabled flag.
        /// </summary>
        /// <param name="enabled">The desired preview enabled flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.26</remarks>
        public void SetPreviewEnabled(bool enabled)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetPreviewEnabled(Convert.ToInt32(enabled));
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
        /// The GetCursorXPosition method gets the current cursor x position value.
        /// </summary>
        /// <returns>The current cursor x position value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.27</remarks>
        public double GetCursorXPosition()
        {
            this.InternalAdvancedChromaParametersReference.GetCursorXPosition(out double position);
            return position;
        }

        /// <summary>
        /// The SetCursorXPosition method sets the cursor x position value.
        /// </summary>
        /// <param name="position">The desired cursor x position value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.28</remarks>
        public void SetCursorXPosition(double position)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetCursorXPosition(position);
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
        /// The GetCursorYPosition method gets the current cursor y position value.
        /// </summary>
        /// <returns>The current cursor y position value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.29</remarks>
        public double GetCursorYPosition()
        {
            this.InternalAdvancedChromaParametersReference.GetCursorYPosition(out double position);
            return position;
        }

        /// <summary>
        /// The SetCursorYPosition method sets the cursor y position value.
        /// </summary>
        /// <param name="position">The desired cursor y position value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.30</remarks>
        public void SetCursorYPosition(double position)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetCursorYPosition(position);
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
        /// The GetCursorSize method gets the current cursor size value.
        /// </summary>
        /// <returns>The current cursor size value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.31</remarks>
        public double GetCursorSize()
        {
            this.InternalAdvancedChromaParametersReference.GetCursorSize(out double size);
            return size;
        }

        /// <summary>
        /// The SetCursorSize method sets the cursor size value.
        /// </summary>
        /// <param name="size">The desired cursor size value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.32</remarks>
        public void SetCursorSize(double size)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetCursorSize(size);
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
        /// The GetSampledColor method gets the current sampled color value. The sampled color is in YCbCr format.
        /// </summary>
        /// <returns>The current sampled color.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.33</remarks>
        public YCbCrColor GetSampledColor()
        {
            this.InternalAdvancedChromaParametersReference.GetSampledColor(out double y, out double cb, out double cr);
            return new YCbCrColor(y, cb, cr);
        }

        /// <summary>
        /// The SetSampledColor method sets the sampled color value. The sampled color is in YCbCr format.
        /// </summary>
        /// <param name="color">The desired sampled color value.</param>
        public void SetSampledColor(YCbCrColor color)
        {
            this.SetSampledColor(color.Y, color.Cb, color.Cr);
            return;
        }

        /// <summary>
        /// The SetSampledColor method sets the sampled color value. The sampled color is in YCbCr format.
        /// </summary>
        /// <param name="y">The desired sampled color y value.</param>
        /// <param name="cb">The desired sampled color cb value.</param>
        /// <param name="cr">The desired sampled color cr value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.34</remarks>
        public void SetSampledColor(double y, double cb, double cr)
        {
            try
            {
                this.InternalAdvancedChromaParametersReference.SetSampledColor(y, cb, cr);
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
        /// The ResetKeyAdjustments method resets the key adjustment properties to default values. This includes foreground level, background level, and key edge values.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.35</remarks>
        public void ResetKeyAdjustments()
        {
            this.InternalAdvancedChromaParametersReference.ResetKeyAdjustments();
            return;
        }

        /// <summary>
        /// The ResetChromaCorrection method resets the chroma correction properties to default values. This includes spill suppress, and flare suppress values.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.36</remarks>
        public void ResetChromaCorrection()
        {
            this.InternalAdvancedChromaParametersReference.ResetChromaCorrection();
            return;
        }

        /// <summary>
        /// The ResetColorAdjustments method resets the color adjustment properties to default values. This includes brightness, contrast, saturation, red, green, and blue values.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 5.2.8.37</remarks>
        public void ResetColorAdjustments()
        {
            this.InternalAdvancedChromaParametersReference.ResetColorAdjustments();
            return;
        }
        #endregion

        #region IBMDSwitcherKeyAdvancedChromaParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherKeyAdvancedChromaParameters events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherKeyAdvancedChromaParametersEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.9.1</remarks>
        void IBMDSwitcherKeyAdvancedChromaParametersCallback.Notify(_BMDSwitcherKeyAdvancedChromaParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeForegroundLevelChanged:
                    this.OnForegroundLevelChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeBackgroundLevelChanged:
                    this.OnBackgroundLevelChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeKeyEdgeChanged:
                    this.OnKeyEdgeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeSpillSuppressChanged:
                    this.OnSpillSuppressChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeFlareSuppressChanged:
                    this.OnFlareSuppressChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeBrightnessChanged:
                    this.OnBrightnessChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeContrastChanged:
                    this.OnContrastChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeSaturationChanged:
                    this.OnSaturationChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeRedChanged:
                    this.OnRedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeGreenChanged:
                    this.OnGreenChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeBlueChanged:
                    this.OnBlueChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeSamplingModeEnabledChanged:
                    this.OnSamplingModeEnabled?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypePreviewEnabledChanged:
                    this.OnPreviewEnabledChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeCursorXPositionChanged:
                    this.OnCursorXPositionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeCursorYPositionChanged:
                    this.OnCursorYPositionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeCursorSizeChanged:
                    this.OnCursorSizeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyAdvancedChromaParametersEventType.bmdSwitcherKeyAdvancedChromaParametersEventTypeSampledColorChanged:
                    this.OnSampledColorChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
