//-----------------------------------------------------------------------------
// <copyright file="SerialPortCollection.cs">
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using BMDSwitcherAPI;

    /// <summary>
    /// The SerialPortCollection class is used to iterate over SerialPorts.
    /// </summary>
    /// <remarks>Wraps Blackmagic Switcher SDK - 2.3.17</remarks>
    public class SerialPortCollection : IEnumerable<SerialPort>
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherSerialPortIterator"/>.
        /// </summary>
        private readonly IBMDSwitcherSerialPortIterator InternalSerialPortIteratorReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialPortCollection"/> class.
        /// </summary>
        /// <param name="serialPortIterator">The native <seealso cref="IBMDSwitcherSerialPortIterator"/> from the BMDSwitcherAPI.</param>
        public SerialPortCollection(IBMDSwitcherSerialPortIterator serialPortIterator)
        {
            InternalSerialPortIteratorReference = serialPortIterator;
            return;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerialPortCollection"/> class from a <seealso cref="IBMDSwitcher"/>.
        /// </summary>
        /// <param name="switcher">The native <seealso cref="IBMDSwitcher"/> from the BMDSwitcherAPI.</param>
        public SerialPortCollection(IBMDSwitcher switcher)
        {
            if (null == switcher)
            {
                throw new ArgumentNullException(nameof(switcher));
            }

            switcher.CreateIterator(typeof(IBMDSwitcherSerialPortIterator).GUID, out IntPtr serialPortIteratorPtr);
            InternalSerialPortIteratorReference = Marshal.GetObjectForIUnknown(serialPortIteratorPtr) as IBMDSwitcherSerialPortIterator;

            return;
        }

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator that iterates through the collection.</returns>
        public IEnumerator<SerialPort> GetEnumerator()
        {
            while (true)
            {
                InternalSerialPortIteratorReference.Next(out IBMDSwitcherSerialPort serialPort);

                if (serialPort != null)
                {
                    yield return new SerialPort(serialPort);
                }
                else
                {
                    yield break;
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator that iterates through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
