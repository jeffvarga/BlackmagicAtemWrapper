//-----------------------------------------------------------------------------
// <copyright file="MultiView.cs">
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
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The MultiView class is used for accessing control functions of a MultiView output, such as setting up the layout format, or routing different inputs to windows.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 2.3.15</remarks>
    public class MultiView : IBMDSwitcherMultiViewCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherMultiView"/>.
        /// </summary>
        private readonly IBMDSwitcherMultiView InternalMultiViewReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiView"/> class.
        /// </summary>
        /// <param name="multiView">The native <seealso cref="IBMDSwitcherMultiView"/> from the BMDSwitcherAPI.</param>
        public MultiView(IBMDSwitcherMultiView multiView)
        {
            this.InternalMultiViewReference = multiView ?? throw new ArgumentNullException(nameof(multiView));
            this.InternalMultiViewReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MultiView"/> class.
        /// </summary>
        ~MultiView()
        {
            this.InternalMultiViewReference.RemoveCallback(this);
            Marshal.ReleaseComObject(this.InternalMultiViewReference);
        }

        /// <summary>
        /// Handles a MultiView event.
        /// </summary>
        /// <param name="sender">The object that received the event.</param>
        public delegate void MultiViewEventHandler(object sender);

        /// <summary>
        /// Handles a MultiView event with a specified window.
        /// </summary>
        /// <param name="sender">The object that received the event.</param>
        /// <param name="window">Specified the window index that changed.</param>
        public delegate void MultiViewWindowEventHandler(object sender, int window);

        /// <summary>
        /// The layout changed.
        /// </summary>
        public event MultiViewEventHandler OnLayoutChanged;

        /// <summary>
        /// Routing to a MultiView window has changed.
        /// </summary>
        public event MultiViewWindowEventHandler OnWindowChanged;

        /// <summary>
        /// The input of a MultiView window changed to/from an input that supports VU meters from/to one that does not.
        /// </summary>
        public event MultiViewEventHandler OnCurrentInputSupportsVuMeterChanged;

        /// <summary>
        /// The enabled state of one of the VU meters changed.
        /// </summary>
        public event MultiViewEventHandler OnVuMeterEnabledChanged;

        /// <summary>
        /// The opacity of one of the VU meters changed.
        /// </summary>
        public event MultiViewEventHandler OnVuMeterOpacityChanged;

        /// <summary>
        /// The input of a MultiView window changed to/from an input that supports safe area display from/to one that does not.
        /// </summary>
        public event MultiViewEventHandler OnCurrentInputSupportsSafeAreaChanged;

        /// <summary>
        /// The enabled state of the safe area overlay changed.
        /// </summary>
        public event MultiViewEventHandler OnSafeAreaEnabledChanged;

        /// <summary>
        /// The positioning of the program and preview windows changed from standard to swapped or from swapped to standard.
        /// </summary>
        public event MultiViewEventHandler OnProgramPreviewSwappedChanged;

        #region Properties
        /// <summary>
        /// Gets a value indicating whether the layout can be changed.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.1</remarks>
        public bool CanChangeLayout
        {
            get
            {
                this.InternalMultiViewReference.CanChangeLayout(out int canChangeLayout);
                return Convert.ToBoolean(canChangeLayout);
            }
        }

        /// <summary>
        /// Gets or sets the layout format.
        /// </summary>
        public _BMDSwitcherMultiViewLayout Layout
        {
            get { return this.GetLayout(); }
            set { this.SetLayout(value); }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the switcher supports quadrant layout.</para>
        /// <para>Some switchers are capable of configuring each quadrant of the MultiView as either one large window or four small windows. Switchers that are not capable of independent quadrant configuration are still capable of displaying the classic ten window configuration which is made up of two large windows either at the top, bottom, left or right side of the display and eight small windows in the remaining area of the display.</para>
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.4</remarks>
        public bool SupportsQuadrantLayout
        {
            get
            {
                this.InternalMultiViewReference.SupportsQuadrantLayout(out int supportsQuadrantLayout);
                return Convert.ToBoolean(supportsQuadrantLayout);
            }
        }

        /// <summary>
        /// Gets the total number of windows available to this MultiView.
        /// </summary>
        public ulong WindowCount
        {
            get { return this.GetWindowCount(); }
        }

        /// <summary>
        /// Gets a value indicating whether this MultiView has custom input-to-window routing capability. This feature allows custom selection of input sources on each window, whereas without this feature the configuration is static and the window input sources cannot be changed. If the MultiView has no such capability, any call to SetWindowInput will fail.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.9</remarks>
        public bool CanRouteInputs
        {
            get
            {
                this.InternalMultiViewReference.CanRouteInputs(out int canRoute);
                return Convert.ToBoolean(canRoute);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the switcher supports the display of VU meters on the MultiView.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.10</remarks>
        public bool SupportsVuMeters
        {
            get
            {
                this.InternalMultiViewReference.SupportsVuMeters(out int supportsVuMeters);
                return Convert.ToBoolean(supportsVuMeters);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the switcher supports changing the MultiView window VU meter opacity.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.14</remarks>
        public bool CanAdjustVuMeterOpacity
        {
            get
            {
                this.InternalMultiViewReference.CanAdjustVuMeterOpacity(out int canAdjustVuMeterOpacity);
                return Convert.ToBoolean(canAdjustVuMeterOpacity);
            }
        }

        /// <summary>
        /// Gets or sets a value representing the opacity of the VU meters from 0.0 (fully transparent) to 1.0 (fully opaque).
        /// </summary>
        public double VuMeterOpacity
        {
            get { return this.GetVuMeterOpacity(); }
            set { this.SetVuMeterOpacity(value); }
        }

        /// <summary>
        /// Gets a value indicating whether the switcher supports toggling the safe area overlay on and off.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.17</remarks>
        public bool CanToggleSafeAreaEnabled
        {
            get
            {
                this.InternalMultiViewReference.CanToggleSafeAreaEnabled(out int canToggleSafeAreaEnabled);
                return Convert.ToBoolean(canToggleSafeAreaEnabled);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the switcher supports swapping the positions of the program and preview windows on the MultiView. Standard positioning places the preview window to the left of or above the program window. Swapping places the preview window to the right of or below the program window.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.21</remarks>
        public bool SupportsProgramPreviewSwap
        {
            get
            {
                this.InternalMultiViewReference.SupportsProgramPreviewSwap(out int supportsProgramPreviewSwap);
                return Convert.ToBoolean(supportsProgramPreviewSwap);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the program and preview window positions are currently swapped.
        /// </summary>
        public bool IsProgramPreviewSwapped
        {
            get { return this.GetProgramPreviewSwapped(); }
            set { this.SetProgramPreviewSwapped(value); }
        }
        #endregion

        #region IBMDSwitcherMultiView
        /// <summary>
        /// The GetLayout method returns the current layout format.
        /// </summary>
        /// <returns>Current layout format as a BMDSwitcherMultiViewLayout.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.2</remarks>
        public _BMDSwitcherMultiViewLayout GetLayout()
        {
            this.InternalMultiViewReference.GetLayout(out _BMDSwitcherMultiViewLayout layout);
            return layout;
        }

        /// <summary>
        /// <para>The SetLayout method sets the layout format.</para>
        /// <para>If the switcher supports quadrant layout, then bmdSwitcherMultiViewLayoutTopLeftSmall, bmdSwitcherMultiViewLayoutTopRightSmall, bmdSwitcherMultiViewLayoutBottomLeftSmall, and bmdSwitcherMultiViewLayoutBottomRightSmall are bitmask fields that can be bitwise-ORed in any combination to describe which quadrants should show four small windows. If the bit for a quadrant is not set, the quadrant will display a large window</para>
        /// <para>If a switcher does not support quadrant layout, then only bmdSwitcherMultiViewLayoutProgramTop, bmdSwitcherMultiViewLayoutProgramBottom, bmdSwitcherMultiViewLayoutProgramLeft, and bmdSwitcherMultiViewLayoutProgramRight are valid.</para>
        /// </summary>
        /// <param name="layout">Desired layout format in BMDSwitcherMultiViewLayout.</param>
        /// <exception cref="ArgumentException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.3</remarks>
        public void SetLayout(_BMDSwitcherMultiViewLayout layout)
        {
            this.InternalMultiViewReference.SetLayout(layout);
            return;
        }

        /// <summary>
        /// The GetWindowInput method returns the current input source routed to the specified window.
        /// </summary>
        /// <param name="window">Zero-based window index</param>
        /// <returns>Input source as a BMDSwitcherInputId.</returns>
        /// <exception cref="ArgumentException">The <paramref name="window"/> parameter is invalid.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.5</remarks>
        public long GetWindowInput(uint window)
        {
            this.InternalMultiViewReference.GetWindowInput(window, out long input);
            return input;
        }

        /// <summary>
        /// The SetWindowInput method routes an input source to the specified window. Note that for switchers that do not support quadrant layout, the inputs for windows 0 and 1 are reserved for the Preview and Program outputs, and so cannot be set using this method.In this case calling this method with a window index of 0 or 1 will do nothing and will return S_FALSE.For switchers that do support quadrant layout, inputs for windows 0 and 1 may be assigned as normal.
        /// </summary>
        /// <param name="window">Zero-based window index.</param>
        /// <param name="input">BMDSwitcherInputId input source.</param>
        /// <exception cref="ArgumentException">The <paramref name="window"/> and/or <paramref name="input"/> parameter is invalid.</exception>
        /// <exception cref="FailedException">Failed to set window input. Possibly because the switcher does not support quadrant layout.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.6</remarks>
        public void SetWindowInput(uint window, long input)
        {
            try
            {
                this.InternalMultiViewReference.SetWindowInput(window, input);
            }
            catch (COMException e)
            {
                if (FailedException.IsFailedException(e.ErrorCode))
                {
                    throw new FailedException(e);
                }

                throw;
            }

            return;
        }

        /// <summary>
        /// The GetWindowCount method returns the total number of windows available to this MultiView.
        /// </summary>
        /// <returns>Total number of windows.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.7</remarks>
        public ulong GetWindowCount()
        {
            this.InternalMultiViewReference.GetWindowCount(out uint windowCount);
            return windowCount;
        }

        /// <summary>
        /// The GetInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask value for this MultiView.The input availability property of an IBMDSwitcherInput can be bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this input is available for viewing in a window.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.8</remarks>
        public _BMDSwitcherInputAvailability GetInputAvailabilityMask()
        {
            this.InternalMultiViewReference.GetInputAvailabilityMask(out _BMDSwitcherInputAvailability availabilityMask);
            return availabilityMask;
        }

        /// <summary>
        /// The CurrentInputSupportsVuMeter method is used to determine if a MultiView window is currently set to an input that supports the display of a VU meter.Some inputs, such as Color Bars and Color Generators do not support the display of a VU meter.
        /// </summary>
        /// <param name="window">Zero-based window index.</param>
        /// <returns>Boolean value indicating whether display of a VU meter is supported by the input that is currently selected on the specified window.</returns>
        /// <exception cref="ArgumentException">The window parameter is not a valid window index.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.11</remarks>
        public bool CurrentInputSupportsVuMeter(uint window)
        {
            this.InternalMultiViewReference.CurrentInputSupportsVuMeter(window, out int supportsVuMeter);
            return Convert.ToBoolean(supportsVuMeter);
        }

        /// <summary>
        /// The GetVuMeterEnabled method is used to determine if the the VU meter is currently visible on the specified MultiView window.
        /// </summary>
        /// <param name="window">Zero-based window index.</param>
        /// <returns>Boolean value indicating whether the VU meter is currently visible on the specified window.</returns>
        /// <exception cref="ArgumentException">The window parameter is not a valid window index.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.12</remarks>
        /// <bug>window parameter is uint rather than int.</bug>
        public bool GetVuMeterEnabled(uint window)
        {
            this.InternalMultiViewReference.GetVuMeterEnabled(window, out int enabled);
            return Convert.ToBoolean(enabled);
        }

        /// <summary>
        /// The SetVuMeterEnabled method is used to hide or show VU meters on the specified MultiView window.
        /// </summary>
        /// <param name="window">Zero-based window index.</param>
        /// <param name="enabled">Boolean value indicating whether VU meters should be made visible on the specified window.</param>
        /// <exception cref="ArgumentException">The window parameter is not a valid window index.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.13</remarks>
        /// <bug>window parameter is uint rather than int.</bug>
        public void SetVuMeterEnabled(uint window, bool enabled)
        {
            this.InternalMultiViewReference.SetVuMeterEnabled(window, Convert.ToInt32(enabled));
            return;
        }

        /// <summary>
        /// The GetVuMeterOpacity method returns the opacity of the VU meters displayed on the MultiView as a value between zero and one. A value of 0.0 is fully transparent, and a value of 1.0 is fully opaque.
        /// </summary>
        /// <returns>The opacity of the VU meters from 0.0 (fully transparent) to 1.0 (fully opaque).</returns>
        /// <exception cref="FailedException">Failure. This can happen if the switcher does not support VU meters.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.15</remarks>
        public double GetVuMeterOpacity()
        {
            try
            {
                this.InternalMultiViewReference.GetVuMeterOpacity(out double opacity);
                return opacity;
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
        /// The SetVuMeterOpacity method is used to set the opacity of the VU meters displayed on the MultiView.
        /// </summary>
        /// <param name="opacity">The opacity of the VU meters from 0.0 (fully transparent) to 1.0 (fully opaque).</param>
        /// <exception cref="FailedException">Failure. This can happen if the switcher does not support VU meters.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.16</remarks>
        public void SetVuMeterOpacity(double opacity)
        {
            try
            {
                this.InternalMultiViewReference.SetVuMeterOpacity(opacity);
            }
            catch (COMException e)
            {
                if (FailedException.IsFailedException(e.ErrorCode))
                {
                    throw new FailedException(e);
                }

                throw;
            }

            return;
        }

        /// <summary>
        /// The CurrentInputSupportsSafeArea method is used to determine if a MultiView window supports displaying the safe area overlay. Only large windows whose input is set to a preview output will support displaying the safe area overlay.
        /// </summary>
        /// <param name="window">Zero-based window index.</param>
        /// <returns>Boolean value indicating whether display of a the safe area is supported by the input that is currently selected on the specified window.</returns>
        /// <exception cref="ArgumentException">The window parameter is not a valid window index.</exception>
        /// <exception cref="FailedException">Failure. This can happen if the switcher does not support toggling the safe area overlay on and of</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.18</remarks>
        public bool CurrentInputSupportsSafeArea(uint window)
        {
            try
            {
                this.InternalMultiViewReference.CurrentInputSupportsSafeArea(window, out int supportsSafeArea);
                return Convert.ToBoolean(supportsSafeArea);
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
        /// The GetSafeAreaEnabled method is used to determine whether the safe area overlay is currently visible on the MultiView window
        /// </summary>
        /// <param name="window">Zero-based window index.</param>
        /// <returns>A Boolean value indicating whether the safe area overlay is currently visible on the MultiView window.</returns>
        /// <exception cref="ArgumentException">The window parameter is not a valid window index.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.19</remarks>
        public bool GetSafeAreaEnabled(uint window)
        {
            this.InternalMultiViewReference.GetSafeAreaEnabled(window, out int enabled);
            return Convert.ToBoolean(enabled);
        }

        /// <summary>
        /// The SetSafeAreaEnabled method is used to hide or show the safe area overlay on the MultiView window.
        /// </summary>
        /// <param name="window">Zero-based window index.</param>
        /// <param name="enabled">A Boolean value indicating whether the safe area overlay should be made visible on the MultiView window.</param>
        /// <exception cref="ArgumentException">The window parameter is not a valid window index.</exception>
        /// <exception cref="FailedException">Failure. This can happen if the switcher does not support toggling the safe area overlay on and off.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.20</remarks>
        public void SetSafeAreaEnabled(uint window, bool enabled)
        {
            try
            {
                this.InternalMultiViewReference.SetSafeAreaEnabled(window, Convert.ToInt32(enabled));
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
        /// The GetProgramPreviewSwapped method is used to determine if the MultiView program and preview window positions are currently swapped.
        /// </summary>
        /// <returns>A Boolean value indicating whether the program and preview window positions are currently swapped.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.22</remarks>
        public bool GetProgramPreviewSwapped()
        {
            this.InternalMultiViewReference.GetProgramPreviewSwapped(out int swapped);
            return Convert.ToBoolean(swapped);
        }

        /// <summary>
        /// The SetProgramPreviewSwapped method is used to specify whether the MultiView program and preview window positions should be swapped.
        /// </summary>
        /// <param name="swapped">A Boolean value indicating whether the program and preview window positions should be swapped.</param>
        /// <exception cref="FailedException">Failure. This can happen if the switcher does not support swapping the positions of the program and preview windows.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.15.23</remarks>
        public void SetProgramPreviewSwapped(bool swapped)
        {
            this.InternalMultiViewReference.SetProgramPreviewSwapped(Convert.ToInt32(swapped));
            return;
        }
        #endregion

        #region IBMDSwitcherMultiViewCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherMultiView events occur, events such as a property change.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherMultiViewEventType that describes the type of event that has occurred.</param>
        /// <param name="window">This parameter is only valid when eventType is bmdSwitcherMultiViewEventTypeWindowChanged, it specifies the window index that was changed</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.16.1</remarks>
        void IBMDSwitcherMultiViewCallback.Notify(_BMDSwitcherMultiViewEventType eventType, int window)
        {
            switch (eventType)
            {
                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeLayoutChanged:
                    this.OnLayoutChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeWindowChanged:
                    this.OnWindowChanged?.Invoke(this, window);
                    break;

                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeCurrentInputSupportsVuMeterChanged:
                    this.OnCurrentInputSupportsVuMeterChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeVuMeterEnabledChanged:
                    this.OnVuMeterEnabledChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeVuMeterOpacityChanged:
                    this.OnVuMeterOpacityChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeCurrentInputSupportsSafeAreaChanged:
                    this.OnCurrentInputSupportsSafeAreaChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeSafeAreaEnabledChanged:
                    this.OnSafeAreaEnabledChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMultiViewEventType.bmdSwitcherMultiViewEventTypeProgramPreviewSwappedChanged:
                    this.OnProgramPreviewSwappedChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
