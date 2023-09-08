//-----------------------------------------------------------------------------
// <copyright file="MixMinusOutputCollection.cs">
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
    /// The MixMinusOutputCollection class is used to iterate over MixMinusOutputs.
    /// </summary>
    /// <remarks>Wraps Blackmagic Switcher SDK - 2.3.20</remarks>
    public class MixMinusOutputCollection : IEnumerable<MixMinusOutput>
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherMixMinusOutputIterator"/>.
        /// </summary>
        private readonly IBMDSwitcherMixMinusOutputIterator InternalMixMinusOutputIteratorReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="MixMinusOutputCollection"/> class.
        /// </summary>
        /// <param name="mixMinusOutputIterator">The native <seealso cref="IBMDSwitcherMixMinusOutputIterator"/> from the BMDSwitcherAPI.</param>
        public MixMinusOutputCollection(IBMDSwitcherMixMinusOutputIterator mixMinusOutputIterator)
        {
            this.InternalMixMinusOutputIteratorReference = mixMinusOutputIterator;
            return;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MixMinusOutputCollection"/> class from a <seealso cref="IBMDSwitcher"/>.
        /// </summary>
        /// <param name="switcher">The native <seealso cref="IBMDSwitcher"/> from the BMDSwitcherAPI.</param>
        public MixMinusOutputCollection(IBMDSwitcher switcher)
        {
            if (null == switcher)
            {
                throw new ArgumentNullException(nameof(switcher));
            }

            switcher.CreateIterator(typeof(IBMDSwitcherMixMinusOutputIterator).GUID, out IntPtr mixMinusOutputIteratorPtr);
            this.InternalMixMinusOutputIteratorReference = Marshal.GetObjectForIUnknown(mixMinusOutputIteratorPtr) as IBMDSwitcherMixMinusOutputIterator;

            return;
        }

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator that iterates through the collection.</returns>
        public IEnumerator<MixMinusOutput> GetEnumerator()
        {
            while (true)
            {
                this.InternalMixMinusOutputIteratorReference.Next(out IBMDSwitcherMixMinusOutput mixMinusOutput);

                if (mixMinusOutput != null)
                {
                    yield return new MixMinusOutput(mixMinusOutput);
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
    }
}
