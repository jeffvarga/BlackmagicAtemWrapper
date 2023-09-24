//-----------------------------------------------------------------------------
// <copyright file="SerialPort.cs">
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

namespace BlackmagicAtemWrapper.device
{
    using System;
    using System.Runtime.InteropServices;
    using BMDSwitcherAPI;

    /// <summary>
    /// The SerialPort object is used for managing a serial port on the switcher.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 2.3.18</remarks>
    public class SerialPort : IBMDSwitcherSerialPortCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherSerialPort"/>.
        /// </summary>
        private readonly IBMDSwitcherSerialPort InternalSerialPortReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialPort"/> class.
        /// </summary>
        /// <param name="serialPort">The native <seealso cref="IBMDSwitcherSerialPort"/> from the BMDSwitcherAPI.</param>
        public SerialPort(IBMDSwitcherSerialPort serialPort)
        {
            this.InternalSerialPortReference = serialPort;
            this.InternalSerialPortReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SerialPort"/> class.
        /// </summary>
        ~SerialPort()
        {
            this.InternalSerialPortReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalSerialPortReference);
        }

        /// <summary>
        /// A delegate to handle events from <see cref="SerialPort"/>.
        /// </summary>
        /// <param name="sender">The <see cref="SerialPort"/> that received the event.</param>
        public delegate void SerialPortEventHandler(object sender);

        /// <summary>
        /// The function of the serial port has changed.
        /// </summary>
        public event SerialPortEventHandler OnFunctionChanged;

        #region Properties
        /// <summary>
        /// Gets or sets the function of the serial port using a <see cref="_BMDSwitcherSerialPortFunction"/>.
        /// </summary>
        public _BMDSwitcherSerialPortFunction Function
        {
            get { return GetFunction(); }
            set { SetFunction(value); }
        }
        #endregion

        #region IBMDSwitcherSerialPort
        /// <summary>
        /// The SetFunction method sets the function of the serial port
        /// </summary>
        /// <param name="function">The function to which the serial port should be set.</param>
        /// <exception cref="ArgumentException">The function parameter is not a valid serial port function.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.18.1</remarks>
        public void SetFunction(_BMDSwitcherSerialPortFunction function)
        {
            InternalSerialPortReference.SetFunction(function);
            return;
        }

        /// <summary>
        /// The GetFunction method returns the current function of the serial port.
        /// </summary>
        /// <returns>A BMDSwitcherSerialPortFunction describing which function the serial port is currently set to.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.18.2</remarks>
        public _BMDSwitcherSerialPortFunction GetFunction()
        {
            InternalSerialPortReference.GetFunction(out _BMDSwitcherSerialPortFunction function);
            return function;
        }

        /// <summary>
        /// The DoesSupportFunction method is used to determine if a given serial port function is supported by the switcher.
        /// </summary>
        /// <param name="function">The serial port function being queried.</param>
        /// <returns>Boolean value describing whether the specified function is supported by the switcher.</returns>
        /// <exception cref="ArgumentException">The function parameter is not a valid BMDSwitcherSerialPortFunction.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.18.3</remarks>
        public bool DoesSupportFunction(_BMDSwitcherSerialPortFunction function)
        {
            InternalSerialPortReference.DoesSupportFunction(function, out int supported);
            return Convert.ToBoolean(supported);
        }
        #endregion

        #region IBMDSwitcherSerialPortCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherSerialPort events occur, such as a change in the serial port function.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.</para>
        /// <para>Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherSerialPortEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.19.1</remarks>
        void IBMDSwitcherSerialPortCallback.Notify(_BMDSwitcherSerialPortEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherSerialPortEventType.bmdSwitcherSerialPortEventTypeFunctionChanged:
                    OnFunctionChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
