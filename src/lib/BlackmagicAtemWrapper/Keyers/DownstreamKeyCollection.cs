//-----------------------------------------------------------------------------
// <copyright file="DownstreamKeyCollection.cs">
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using BMDSwitcherAPI;

    /// <summary>
    /// The DownstreamKey class is used to iterate over DownstreamKeys.
    /// </summary>
    /// <remarks>Wraps Blackmagic Switcher SDK - 5.2.18</remarks>
    public class DownstreamKeyCollection : IEnumerable<DownstreamKey>
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherKeyIterator"/>.
        /// </summary>
        private readonly IBMDSwitcherDownstreamKeyIterator InternalDownstreamKeyIteratorReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownstreamKeyCollection"/> class.
        /// </summary>
        /// <param name="switcherKeyIterator">The native <seealso cref="IBMDSwitcherDownstreamKeyIterator"/> from the BMDSwitcherAPI.</param>
        public DownstreamKeyCollection(IBMDSwitcherDownstreamKeyIterator switcherKeyIterator)
        {
            this.InternalDownstreamKeyIteratorReference = switcherKeyIterator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownstreamKeyCollection"/> class from a <seealso cref="IBMDSwitcherMixEffectBlock"/>.
        /// </summary>
        /// <param name="switcher">The native <seealso cref="IBMDSwitcher"/> from the BMDSwitcherAPI.</param>
        /// <exception cref="ArgumentNullException"><paramref name="switcher"/> was null.</exception>
        public DownstreamKeyCollection(IBMDSwitcher switcher)
        {
            if (null == switcher)
            {
                throw new ArgumentNullException(nameof(switcher));
            }

            switcher.CreateIterator(typeof(IBMDSwitcherDownstreamKeyIterator).GUID, out IntPtr mebIteratorPtr);
            this.InternalDownstreamKeyIteratorReference = Marshal.GetObjectForIUnknown(mebIteratorPtr) as IBMDSwitcherDownstreamKeyIterator;

            return;
        }

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator that iterates through the collection.</returns>
        public IEnumerator<DownstreamKey> GetEnumerator()
        {
            while (true)
            {
                this.InternalDownstreamKeyIteratorReference.Next(out IBMDSwitcherDownstreamKey input);

                if (input != null)
                {
                    yield return new DownstreamKey(input);
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
