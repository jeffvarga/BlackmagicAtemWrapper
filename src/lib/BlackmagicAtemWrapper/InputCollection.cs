//-----------------------------------------------------------------------------
// <copyright file="InputCollection.cs">
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using BMDSwitcherAPI;

    /// <summary>
    /// The InputCollection class is used to iterate over Inputs.
    /// </summary>
    /// <remarks>Wraps Blackmagic Switcher SDK - 2.1.1</remarks>
    public class InputCollection : IEnumerable<Input>
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherInputIterator"/>.
        /// </summary>
        private readonly IBMDSwitcherInputIterator InternalInputIterator;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputCollection"/> class.
        /// </summary>
        /// <param name="inputIterator">The native <seealso cref="IBMDSwitcherInputIterator"/> from the BMDSwitcherAPI.</param>
        public InputCollection(IBMDSwitcherInputIterator inputIterator)
        {
            this.InternalInputIterator = inputIterator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputCollection"/> class from a <seealso cref="IBMDSwitcher"/>.
        /// </summary>
        /// <param name="switcher">The native <seealso cref="IBMDSwitcher"/> from the BMDSwitcherAPI.</param>
        public InputCollection(IBMDSwitcher switcher)
        {
            if (null == switcher)
            {
                throw new ArgumentNullException(nameof(switcher));
            }

            switcher.CreateIterator(typeof(IBMDSwitcherInputIterator).GUID, out IntPtr inputIteratorPtr);
            this.InternalInputIterator = Marshal.GetObjectForIUnknown(inputIteratorPtr) as IBMDSwitcherInputIterator;

            return;
        }

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator that iterates through the collection.</returns>
        public IEnumerator<Input> GetEnumerator()
        {
            while (true)
            {
                this.InternalInputIterator.Next(out IBMDSwitcherInput input);

                if (input != null)
                {
                    yield return new Input(input);
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
            return this.GetEnumerator();
        }
        #endregion

        /// <summary>
        /// The GetById method returns the IBMDSwitcherInput object interface corresponding to the specified Id.
        /// </summary>
        /// <param name="inputId">BMDSwitcherInputId of input.</param>
        /// <returns>A <see cref="Input"/> for the specified Id. </returns>
        /// <exception cref="ArgumentException">The <paramref name="inputId"/> parameter is invalid.</exception>
        public Input GetById(long inputId)
        {
            this.InternalInputIterator.GetById(inputId, out IBMDSwitcherInput input);
            return new Input(input);
        }
    }
}
