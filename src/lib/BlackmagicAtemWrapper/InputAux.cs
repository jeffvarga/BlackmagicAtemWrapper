//-----------------------------------------------------------------------------
// <copyright file="InputAux.cs">
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
    /// The InputAux class is used for managing an auxiliary output port.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 2.3.12</remarks>
    class InputAux : IBMDSwitcherInputAuxCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherInputAux"/>.
        /// </summary>
        private readonly IBMDSwitcherInputAux InternalInputAuxReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="Switcher"/> class.
        /// </summary>
        /// <param name="input">The native <seealso cref="IBMDSwitcherInputAux"/> from the BMDSwitcherAPI.</param>
        public InputAux(IBMDSwitcherInputAux input)
        {
            this.InternalInputAuxReference = input;
            this.InternalInputAuxReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="InputAux"/> class.
        /// </summary>
        ~InputAux()
        {
            this.InternalInputAuxReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalInputAuxReference);
        }

        public delegate void InputAuxEventHandler(object sender);

        #region Events
        /// <summary>
        /// Called when the input source changes.
        /// </summary>
        public event InputAuxEventHandler OnInputSourceChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the input source for this auxiliary port.
        /// </summary>
        public long InputSource
        {
            get { return this.GetInputSource(); }
            set { this.SetInputSource(value); }
        }
        #endregion

        #region IBMDSwitcherInputAux
        /// <summary>
        /// The GetInputSource method returns the currently selected input source.
        /// </summary>
        /// <returns>The BMDSwitcherInputId of the selected input source</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.12.1</remarks>
        public long GetInputSource()
        {
            this.InternalInputAuxReference.GetInputSource(out long inputId);
            return inputId;
        }

        /// <summary>
        /// The SetInputSource method selects an input source for this auxiliary port.
        /// </summary>
        /// <param name="inputId">The BMDSwitcherInputId of the desired input source.</param>
        /// <exception cref="ArgumentException">Invalid <paramref name="inputId"/> parameter.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.12.2</remarks>
        public void SetInputSource(long inputId)
        {
            this.InternalInputAuxReference.SetInputSource(inputId);
            return;
        }

        /// <summary>
        /// The GetInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask value for this auxiliary port. The input availability property of an IBMDSwitcherInput can be bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this input is available for use by this auxiliary port.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.12.3</remarks>
        public _BMDSwitcherInputAvailability GetInputAvailabilityMask()
        {
            this.InternalInputAuxReference.GetInputAvailabilityMask(out _BMDSwitcherInputAvailability availabilityMask);
            return availabilityMask;
        }
        #endregion

        #region IBMDSwitcherInputAuxCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherInputAux events occur, events such as a property change.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherInputAuxEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.13.1</remarks>
        void IBMDSwitcherInputAuxCallback.Notify(_BMDSwitcherInputAuxEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherInputAuxEventType.bmdSwitcherInputAuxEventTypeInputSourceChanged:
                    this.OnInputSourceChanged?.Invoke(this);
                    break;
            }
        }
        #endregion
    }
}