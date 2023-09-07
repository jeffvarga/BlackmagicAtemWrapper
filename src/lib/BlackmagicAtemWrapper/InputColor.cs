//-----------------------------------------------------------------------------
// <copyright file="InputColor.cs">
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
    using BMDSwitcherAPI;

    /// <summary>
    /// The InputColor class is used for managing a color generator input port.
    /// </summary>
    public class InputColor : IBMDSwitcherInputColorCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherInputColor"/>.
        /// </summary>
        private readonly IBMDSwitcherInputColor inputColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputColor"/> class.
        /// </summary>
        /// <param name="inputColor">The native <seealso cref="IBMDSwitcherInputColor"/> from the BMDSwitcherAPI.</param>
        public InputColor(IBMDSwitcherInputColor inputColor)
        {
            this.inputColor = inputColor;
            this.inputColor.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="InputColor"/> class.
        /// </summary>
        ~InputColor()
        {
            this.inputColor.RemoveCallback(this);
            Marshal.ReleaseComObject(this.inputColor);
        }

        /// <summary>
        /// A delegate to handle events from <see cref="InputColor"/>.
        /// </summary>
        /// <param name="sender">The <see cref="InputColor"/> that received the event.</param>
        public delegate void InputColorEventHandler(object sender);

        /// <summary>
        /// Called when <see cref="Hue"/> changes.
        /// </summary>
        public event InputColorEventHandler OnHueChanged;

        /// <summary>
        /// Called when <see cref="Saturation"/> changes.
        /// </summary>
        public event InputColorEventHandler OnSaturationChanged;

        /// <summary>
        /// Called when <see cref="Luma"/> changes.
        /// </summary>
        public event InputColorEventHandler OnLumaChanged;

        #region Properties
        /// <summary>
        /// Gets or sets the current hue value.
        /// </summary>
        public double Hue
        {
            get { return this.GetHue(); }
            set { this.SetHue(value); }
        }

        /// <summary>
        /// Gets or sets the current saturation value.
        /// </summary>
        public double Saturation
        {
            get { return this.GetSaturation(); }
            set { this.SetSaturation(value); }
        }

        /// <summary>
        /// Gets or sets the current luminance value.
        /// </summary>
        public double Luma
        {
            get { return this.GetLuma(); }
            set { this.SetLuma(value); }
        }
        #endregion

        #region IBMDSwitcherInputColor
        /// <summary>
        /// The GetHue method gets the current hue value.
        /// </summary>
        /// <returns>The current hue value</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.10.1</remarks>
        /// <bug>2.3.10.1 parameter documentation copy and paste error.</bug>
        public double GetHue()
        {
            this.inputColor.GetHue(out double hue);
            return hue;
        }

        /// <summary>
        /// The SetHue method sets the hue value.
        /// </summary>
        /// <param name="hue">The desired hue value.</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.10.2</remarks>
        public void SetHue(double hue)
        {
            this.inputColor.SetHue(hue);
            return;
        }

        /// <summary>
        /// The GetSaturation method gets the current saturation value.
        /// </summary>
        /// <returns>The current saturation value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.10.3</remarks>
        /// <bug>2.3.10.3 parameter documentation copy and paste error.</bug>
        public double GetSaturation()
        {
            this.inputColor.GetSaturation(out double sat);
            return sat;
        }

        /// <summary>
        /// The SetSaturation method sets the saturation value.
        /// </summary>
        /// <param name="sat">The desired saturation value.</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.10.4</remarks>
        /// <bug>2.3.10.4 parameter documentation copy and paste error.</bug>
        public void SetSaturation(double sat)
        {
            this.inputColor.SetSaturation(sat);
            return;
        }

        /// <summary>
        /// The GetLuma method gets the current luminance value.
        /// </summary>
        /// <returns>The current luminance value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.10.5</remarks>
        public double GetLuma()
        {
            this.inputColor.GetLuma(out double luma);
            return luma;
        }

        /// <summary>
        /// The SetLuma method sets the luminance value.
        /// </summary>
        /// <param name="luma">The desired luminance value.</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.10.6</remarks>
        public void SetLuma(double luma)
        {
            this.inputColor.SetLuma(luma);
            return;
        }
        #endregion

        #region IBMDSwitcherInputColorCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherInputColor events occur, events such as a property change.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherInputColorEventType that describes the type of event that has occurred.</param>
        void IBMDSwitcherInputColorCallback.Notify(_BMDSwitcherInputColorEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherInputColorEventType.bmdSwitcherInputColorEventTypeHueChanged:
                    this.OnHueChanged?.Invoke(null);
                    break;

                case _BMDSwitcherInputColorEventType.bmdSwitcherInputColorEventTypeSaturationChanged:
                    this.OnSaturationChanged?.Invoke(null);
                    break;

                case _BMDSwitcherInputColorEventType.bmdSwitcherInputColorEventTypeLumaChanged:
                    this.OnLumaChanged?.Invoke(null);
                    break;

                default:
                    System.Diagnostics.Debug.Assert(false, "Unexpected _BMDSwitcherInputColorEventType", "_BMDSwitcherInputColorEventType = {0}", new object[] { eventType });
                    break;
            }
        }
        #endregion
    }
}
