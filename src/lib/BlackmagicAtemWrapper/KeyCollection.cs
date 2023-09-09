//-----------------------------------------------------------------------------
// <copyright file="KeyCollection.cs">
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
    /// The MultiViewCollection class is used to iterate over MixEffectBlocks.
    /// </summary>
    /// <remarks>Wraps Blackmagic Switcher SDK - 5.2.1</remarks>
    public class KeyCollection : IEnumerable<Key>
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherKeyIterator"/>.
        /// </summary>
        private readonly IBMDSwitcherKeyIterator InternalSwitcherKeyIteratorReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyCollection"/> class.
        /// </summary>
        /// <param name="switcherKeyIterator">The native <seealso cref="IBMDSwitcherKeyIterator"/> from the BMDSwitcherAPI.</param>
        public KeyCollection(IBMDSwitcherKeyIterator switcherKeyIterator)
        {
            this.InternalSwitcherKeyIteratorReference = switcherKeyIterator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyCollection"/> class from a <seealso cref="IBMDSwitcherMixEffectBlock"/>.
        /// </summary>
        /// <param name="mixEffectBlock">The native <seealso cref="IBMDSwitcherMixEffectBlock"/> from the BMDSwitcherAPI.</param>
        /// <exception cref="ArgumentNullException"><paramref name="mixEffectBlock"/> was null.</exception>
        public KeyCollection(IBMDSwitcherMixEffectBlock mixEffectBlock)
        {
            if (null == mixEffectBlock)
            {
                throw new ArgumentNullException(nameof(mixEffectBlock));
            }

            mixEffectBlock.CreateIterator(typeof(IBMDSwitcherKeyIterator).GUID, out IntPtr mebIteratorPtr);
            this.InternalSwitcherKeyIteratorReference = Marshal.GetObjectForIUnknown(mebIteratorPtr) as IBMDSwitcherKeyIterator;

            return;
        }

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator that iterates through the collection.</returns>
        public IEnumerator<Key> GetEnumerator()
        {
            while (true)
            {
                this.InternalSwitcherKeyIteratorReference.Next(out IBMDSwitcherKey input);

                if (input != null)
                {
                    yield return new Key(input);
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
